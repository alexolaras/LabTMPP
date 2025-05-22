using Api.Models;
using Laborator4.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Laborator4.Services
{
    public class CategoryService
    {
        private readonly HttpClient _httpClient = new();

        private const string BaseUrl = "https://localhost:5001/api/Category";

        public async Task<List<Category>> GetAllCategoriesAsync()

        {
            return await _httpClient.GetFromJsonAsync<List<Category>>($"{BaseUrl}/categories") ?? new();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/categories/{id}");

            if (!response.IsSuccessStatusCode)
                return null;

            var list = await response.Content.ReadFromJsonAsync<List<Category>>();
            return list?.FirstOrDefault();
        }

        public async Task CreateCategoryAsync(string name)
        {
            var body = new { Title = name };
            await _httpClient.PostAsJsonAsync($"{BaseUrl}/categories", body);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _httpClient.DeleteAsync($"{BaseUrl}/categories/{id}");
        }

        public async Task UpdateCategoryTitleAsync(int id, string newName)
        {
            var body = new { Title = newName };
            await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", body);
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _httpClient.GetFromJsonAsync<List<Product>>($"{BaseUrl}/categories/{categoryId}/products") ?? new();
        }

        public async Task AddProductToCategoryAsync(int categoryId, Product product)
        {
            await _httpClient.PostAsJsonAsync($"{BaseUrl}/categories/{categoryId}/products", product);
        }
    }
}
