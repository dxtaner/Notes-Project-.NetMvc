using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using noteproject.Models;
using System;

namespace noteproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : Controller
    {
        private readonly NoteContext _context;

        public NotesController(NoteContext context)
        {
            _context = context;
        }

        // GET api/notes
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var notes = await _context.Notes.ToListAsync();
                return Ok(notes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving notes: " + ex.Message);
            }
        }

        // POST api/notes
        [HttpPost]
        public async Task<IActionResult> Create(Note note)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(note);
                    await _context.SaveChangesAsync();
                    return Ok(note);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the note: " + ex.Message);
            }
        }

        // DELETE api/notes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var note = await _context.Notes.FindAsync(id);
                if (note == null)
                {
                    return NotFound();
                }

                List<Note> notes = _context.Notes.ToList();
                // Silinecek notun index'ini notlar listesinde buluyorum
                int index = notes.IndexOf(note);

                // MoveChildNotes metodu ile silinen notun child notlarını üst notun altına taşı
                MoveChildNotes(notes, note, index);
                
                _context.Notes.Remove(note);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the note: " + ex.Message);
            }
        }

        private void MoveChildNotes(List<Note> notes, Note deletedNote, int index)
        {
            // Silinen notun altındaki notları bul
            List<Note> childNotes = notes.Where(n => n.ParentId == deletedNote.Id).ToList();
            // Alt notların parentId değerini üst notun parentId değerine eşitle
            foreach (Note childNote in childNotes)
            {
                childNote.ParentId = deletedNote.ParentId;
                index++;
            }

            // Silinen notu listeden kaldır 
            notes.Remove(deletedNote);
        }

    }
}
