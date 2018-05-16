using Empresa.Compras.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;

namespace Empresa.Compras.Web.Controllers
{
    public class CategoriasController : Controller
    {
        HttpClient client = new HttpClient();

        public CategoriasController()
        {
            client.BaseAddress = new Uri("http://localhost:5677");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "admin"); // TODO Ajustar autenticação
        }
        // GET: Categorias
        public ActionResult Index()
        {
            List<Categoria> categorias = new List<Categoria>();

            HttpResponseMessage response = client.GetAsync("/api/categorias").Result;

            if (response.IsSuccessStatusCode)
            {
                categorias = response.Content.ReadAsAsync<List<Categoria>>().Result;
            }
            return View(categorias);
        }       

        // GET: Categorias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categoria categoria)
        {
            
            try
            {
                HttpResponseMessage response = client.PostAsJsonAsync<Categoria>($"/api/categorias", categoria).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    TempData["mensagem"] = $"{categoria.Nome} foi salvo com sucesso";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "Erro ao criar a categoria.";
                    return View();
                }
                
            }
            catch
            {
                return View();
            }
        }

        // GET: Categorias/Edit/5
        public ActionResult Edit(int idCategoria)
        {
            HttpResponseMessage response = client.GetAsync($"/api/categorias/{idCategoria}").Result;
            Categoria categoria = response.Content.ReadAsAsync<Categoria>().Result;

            if (categoria != null)
                return View(categoria);
            else
                return HttpNotFound();            
        }

        // POST: Categorias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int IdCategoria, Categoria categoria)
        {
            try
            {
                HttpResponseMessage response = client.PutAsJsonAsync<Categoria>($"/api/categorias/{IdCategoria}", categoria).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    TempData["mensagem"] = $"{categoria.Nome} foi salvo com sucesso";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "Erro ao alterar a categoria.";
                    return View();
                }

            }
            catch
            {
                return View();
            }
        }       

        // POST: Categorias/Delete/5
        [HttpPost]
        public JsonResult Delete(int idCategoria)
        {
            string mensagem = string.Empty;

            HttpResponseMessage response = client.GetAsync($"/api/categorias/{idCategoria}").Result;

            Categoria categoria = response.Content.ReadAsAsync<Categoria>().Result;          

            if (categoria != null)
            {
                mensagem = $"{categoria.Nome} foi excluida com sucesso";
            }

            return Json(mensagem, JsonRequestBehavior.AllowGet);
        }
    }
}
