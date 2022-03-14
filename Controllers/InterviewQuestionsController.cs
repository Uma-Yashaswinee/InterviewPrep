using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InterviewPrep.Data;
using InterviewPrep.Models;
using Microsoft.AspNetCore.Authorization;

namespace InterviewPrep.Controllers
{
    public class InterviewQuestionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InterviewQuestionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InterviewQuestions
        public async Task<IActionResult> Index()
        {
            return View(await _context.InterviewQuestions.ToListAsync());
        }

        // GET: InterviewQuestions/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // POST: InterviewQuestions/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.InterviewQuestions.Where(j => j.Question.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: InterviewQuestions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interviewQuestions = await _context.InterviewQuestions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (interviewQuestions == null)
            {
                return NotFound();
            }

            return View(interviewQuestions);
        }

        // GET: InterviewQuestions/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: InterviewQuestions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Question,Answers")] InterviewQuestions interviewQuestions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(interviewQuestions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(interviewQuestions);
        }

        // GET: InterviewQuestions/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interviewQuestions = await _context.InterviewQuestions.FindAsync(id);
            if (interviewQuestions == null)
            {
                return NotFound();
            }
            return View(interviewQuestions);
        }

        // POST: InterviewQuestions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Question,Answers")] InterviewQuestions interviewQuestions)
        {
            if (id != interviewQuestions.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(interviewQuestions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InterviewQuestionsExists(interviewQuestions.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(interviewQuestions);
        }

        // GET: InterviewQuestions/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interviewQuestions = await _context.InterviewQuestions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (interviewQuestions == null)
            {
                return NotFound();
            }

            return View(interviewQuestions);
        }

        // POST: InterviewQuestions/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var interviewQuestions = await _context.InterviewQuestions.FindAsync(id);
            _context.InterviewQuestions.Remove(interviewQuestions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InterviewQuestionsExists(int id)
        {
            return _context.InterviewQuestions.Any(e => e.Id == id);
        }
    }
}
