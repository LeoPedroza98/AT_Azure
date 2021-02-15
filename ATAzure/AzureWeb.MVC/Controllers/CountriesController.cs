using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Azure.Domain.Entities;
using AzureWeb.MVC.Data;
using AzureWeb.MVC.ApiClientHelperCountryState;
using System.Net.Http;
using Newtonsoft.Json;

namespace AzureWeb.MVC.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ApiClientCountryState _apiClient;

        public CountriesController(ApplicationDbContext context)
        {
            _context = context;
            _apiClient = new ApiClientCountryState();
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            List<Country> c = new List<Country>();
            HttpResponseMessage response = await _apiClient.Client.GetAsync("api/CountryAPI");
            if (response.IsSuccessStatusCode)
            {
                var results = response.Content.ReadAsStringAsync().Result;
                c = JsonConvert.DeserializeObject<List<Country>>(results);
            }
            return View(c);
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Country c;
            HttpResponseMessage response = await _apiClient.Client.GetAsync($"api/CountryAPI/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                c = JsonConvert.DeserializeObject<Country>(result);
                return View(c);
            }
            return NotFound();
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Country country)
        {
            if (ModelState.IsValid)
            {
                var post = _apiClient.Client.PostAsJsonAsync<Country>("api/CountryAPI", country);
                post.Wait();

                var result = post.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(country);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Country c;
            HttpResponseMessage response = await _apiClient.Client.GetAsync($"api/CountryAPI/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                c = JsonConvert.DeserializeObject<Country>(result);
                return View(c);
            }
            return NotFound();
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Country country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var post = _apiClient.Client.PutAsJsonAsync<Country>($"api/CountryAPI/{country.Id}", country);
                post.Wait();

                var result = post.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(country);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Country c;
            HttpResponseMessage response = await _apiClient.Client.GetAsync($"api/CountryAPI/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                c = JsonConvert.DeserializeObject<Country>(result);
                return View(c);
            }
            return NotFound();
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _apiClient.Client.DeleteAsync($"api/CountryAPI/{id}");
            return RedirectToAction("Index");
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }
    }
}
