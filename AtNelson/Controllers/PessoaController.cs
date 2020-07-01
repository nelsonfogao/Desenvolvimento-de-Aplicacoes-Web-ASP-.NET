using AtNelson.Models;
using AtNelson.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AtNelson.Controllers
{
    public class PessoaController : Controller
    {
        private PessoaRepository PessoaRepository { get; set; }

        public PessoaController(PessoaRepository pessoaRepository)
        {
            this.PessoaRepository = pessoaRepository;
        }

        // GET: PessoaController
        public ActionResult Index()
        {
            var model = this.PessoaRepository.GetAll();

            return View(model);
        }

        // GET: PessoaController/Details/5
        public ActionResult Details(int id)
        {
            var model = this.PessoaRepository.GetPessoaById(id);
            return View(model);
        }

        // GET: PessoaController/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Search([FromQuery] string query)
        {
            var model = this.PessoaRepository.Search(query);

            return View("Index", model);
        }

        // POST: PessoaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pessoa pessoa)
        {
            try
            {
                this.PessoaRepository.Save(pessoa);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = this.PessoaRepository.GetPessoaById(id);
            return View(model);
        }

        // POST: PessoaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Pessoa model)
        {
            try
            {
                model.Id = id;
                this.PessoaRepository.Update(model);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PessoaController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = this.PessoaRepository.GetPessoaById(id);
            return View(model);
        }

        // POST: PessoaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Pessoa model)
        {
            try
            {
                model.Id = id;
                this.PessoaRepository.Delete(model);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
