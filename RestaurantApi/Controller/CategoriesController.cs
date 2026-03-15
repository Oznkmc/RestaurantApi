using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Data;
using RestaurantApi.Models;


namespace RestaurantApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<List<Category>> GetCategoryById(int id)
        {
            var existing = await _context.Categories.FindAsync(id);
            return existing == null ? new List<Category>() : new List<Category> { existing };
        }
         [HttpPost]
         public async Task<Category> PostCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }
        [HttpPut("{id}")]
        public async Task<Category> CategoryUpdate(int id, Category category )
        {
            var existing = await _context.Categories.FindAsync(id);
            existing.Name = category.Name;
            existing.IsActive = category.IsActive;
            return category;
        }
        [HttpDelete("{id}")]
        public async Task<string> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return "Kategori silindi";
        }
    }
}
