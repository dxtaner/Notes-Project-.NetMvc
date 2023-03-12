using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using noteproject.Models;

public class HomeController : Controller
{
    private readonly NoteContext _context;

    public HomeController(NoteContext context)
    {   
        // Veritabanı bağlantısı için gerekli olan context alınır
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var notes = await _context.Notes.ToListAsync();
        // Notlar listesindeki notları al
        return View(notes);
    }

    public IActionResult Create()
    {
        // Tüm notlar listesi veritabanından al ve SelectList nesnesi oluştur
        ViewBag.ParentNotes = new SelectList(_context.Notes, "Id", "Title");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Note note)
    {
        if (ModelState.IsValid)
        {
            // Notu mevcut veritabanına ekle
            _context.Add(note);
            
            // Eklenen Notlar tablosuna ekle    
            _context.AddedNotes.Add(note);
            await _context.SaveChangesAsync();

            // Yönlendirme yap
            return RedirectToAction(nameof(Index));
        }
        // Gelen veriler başarılı ise tekrar oluşturma sayfasını göster
        ViewBag.ParentNotes = new SelectList(_context.Notes, "Id", "Title", note.ParentId);
        return View(note);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        // Silinecek notu notlar listesinde buluyorum
        Note note = await _context.Notes.FindAsync(id);
        if (note == null)
        {
            return NotFound();
        }

        List<Note> notes = _context.Notes.ToList();
        // Silinecek notun index'ini notlar listesinde buluyorum
        int index = notes.IndexOf(note);
        // MoveChildNotes metodu ile silinen notun child notlarını üst notun altına taşı
        MoveChildNotes(notes, note, index);
        
        // Silme işlemini yapıp kaydediyorum 
        _context.Notes.Remove(note);
        await _context.SaveChangesAsync();

        // Silinen Notlar tablosuna ekle
        _context.DeletedNotes.Add(note);

        // Yönlendirme
        return RedirectToAction("Index");
    }

    public static void MoveChildNotes(List<Note> notes, Note deletedNote, int index)
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
