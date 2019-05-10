using CrudRavenDB.Models;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CrudRavenDB.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using (var session = DataContext.Store.OpenAsyncSession())
            {
                var model = await session.Query<Cliente>().ToListAsync();
                return View(model);
            };
        }

        public async Task<IActionResult> Details(string Id)
        {
            using (var session = DataContext.Store.OpenAsyncSession())
            {
                var model = await session.LoadAsync<Cliente>(Id);
                return View(model);
            };
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            using (var session = DataContext.Store.OpenAsyncSession())
            {
                var model = await session.LoadAsync<Cliente>(Id);
                return View(model);
            };
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Cliente model)
        {
            using (var session = DataContext.Store.OpenAsyncSession())
            {
                var bug = await session.LoadAsync<Cliente>(model.Id);
                bug.Nome = model.Nome;
                bug.Descricao = model.Descricao;

                await session.SaveChangesAsync();
            };

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string Id)
        {
            using (var session = DataContext.Store.OpenAsyncSession())
            {
                var model = await session.LoadAsync<Cliente>(Id);
                return View(model);
            };
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Cliente model)
        {
            using (var session = DataContext.Store.OpenAsyncSession())
            {
                session.Delete(model.Id);
                await session.SaveChangesAsync();
            };

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Cliente model)
        {
            using (var session = DataContext.Store.OpenSession())
            {
                session.Store(model);
                session.SaveChanges();
            };

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}