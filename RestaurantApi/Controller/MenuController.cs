using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Data;
using RestaurantApi.Models;
using RestaurantApi.Models.DTOs;

namespace RestaurantApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        
        private readonly AppDbContext _context;

        public MenuController(AppDbContext context)
        {
            _context = context;
        }
        //HttpGet attribute'u, bu metodun HTTP GET isteklerine yanıt vereceğini belirtir. Bu metod, veritabanındaki tüm ürünleri asenkron olarak alır ve bir liste olarak döndürür.
        [HttpGet]
        [HttpGet]
        public async Task<List<ProductDto>> GetProducts()
        {
            return await _context.Products
                .Include(x => x.Category)
                .Where(x => x.IsAvailable == true)
                .Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Portion = x.Portion,
                    ImageUrl = x.ImageUrl,
                    IsAvailable = x.IsAvailable,
                    CategoryName = x.Category.Name
                })
                .ToListAsync();
        }
        // HttpPost attribute'u, bu metodun HTTP POST isteklerine yanıt vereceğini belirtir. Bu metod, istemciden gelen bir Product nesnesini alır, veritabanına ekler ve kaydeder. Ardından, eklenen ürünü geri döndürür.
        [HttpPost]
        public async Task<Product> AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
        [HttpPut("{id}")]
        public async Task<Product> UpdateProduct(int id, Product product)
        {
            var existing = await _context.Products.FindAsync(id);
            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Description = product.Description;
            existing.Portion = product.Portion;
            existing.IsAvailable = product.IsAvailable;
            await _context.SaveChangesAsync();
            return existing;
        }

        [HttpDelete("{id}")]
        public async Task<string> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return "Ürün silindi";
        }
        // Bu metod, "api/menu/bugun" URL'sine yapılan GET isteklerine yanıt verecektir.
        [HttpGet("Bugun")]
        public String BugununMenusu()
        {
            return "Bugünün menüsü: Mercimek Çorbası, Izgara Tavuk, Pilav, Salata, Tatlı";
        }
        // Bu metod, "api/menu/bugun/sicakyemek" URL'sine yapılan GET isteklerine yanıt verecektir.
        [HttpGet("Bugun/SicakYemek")]
        public String BugununSicakYemegi()
        {
            return "Bugünün sıcak yemeği: Izgara Tavuk";
        }
        [HttpGet("{gun}")]
        public String GunlukMenu(string gun)
        {
            if (gun.ToLower() == "pazartesi")
            {
                return "Pazartesi Menüsü: Mercimek Çorbası, Izgara Tavuk, Pilav, Salata, Tatlı";
            }
            else if (gun.ToLower() == "sali")
            {
                return "Salı Menüsü: Domates Çorbası, Kuru Fasulye, Pilav, Salata, Tatlı";
            }
            else if (gun.ToLower() == "carsamba")
            {
                return "Çarşamba Menüsü: Tarhana Çorbası, Salçalı Makarna, Salata, Tatlı";
            }
            else
            {
                return "Geçersiz gün. Lütfen 'Pazartesi', 'Salı' veya 'Çarşamba' olarak giriniz.";
            }
        }
    }
}
