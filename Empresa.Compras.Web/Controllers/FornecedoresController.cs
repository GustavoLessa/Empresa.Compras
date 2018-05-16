using Empresa.Compras.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;

namespace Empresa.Compras.Web.Controllers
{
    public class FornecedoresController : Controller
    {
        HttpClient client = new HttpClient();

        public FornecedoresController()
        {
            client.BaseAddress = new Uri("http://localhost:5677");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "admin"); // TODO Ajustar autenticação
        }

        // GET: Fornecedores
        public ActionResult Index()
        {
            List<Fornecedor> fornecedores = new List<Fornecedor>();

            HttpResponseMessage response = client.GetAsync("/api/fornecedores").Result;

            if (response.IsSuccessStatusCode)
            {
                fornecedores = response.Content.ReadAsAsync<List<Fornecedor>>().Result;
            }
            return View(fornecedores);
        }       

        // GET: Fornecedores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fornecedores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Fornecedor fornecedor)
        {
            try
            {
                HttpResponseMessage response = client.PostAsJsonAsync<Fornecedor>($"/api/fornecedores", fornecedor).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    TempData["mensagem"] = $"{fornecedor.Nome} foi salvo com sucesso";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "Erro ao criar fornecedor.";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Fornecedores/Edit/5
        public ActionResult Edit(int idFornecedor)
        {
            HttpResponseMessage response = client.GetAsync($"/api/fornecedores/{idFornecedor}").Result;
            Fornecedor fornecedor = response.Content.ReadAsAsync<Fornecedor>().Result;

            if (fornecedor != null)
                return View(fornecedor);
            else
                return HttpNotFound();
        }

        // POST: Fornecedores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int idForncedor, Fornecedor fornecedor)
        {
            try
            {
                HttpResponseMessage response = client.PutAsJsonAsync<Fornecedor>($"/api/fornecedores/{idForncedor}", fornecedor).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    TempData["mensagem"] = $"{fornecedor.Nome} foi salvo com sucesso";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "Erro ao alterar fornecdor.";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // POST: Fornecedores/Delete/5
        [HttpPost]
        public JsonResult Delete(int idFornecedor)
        {
            string mensagem = string.Empty;

            HttpResponseMessage response = client.GetAsync($"/api/fornecedores/{idFornecedor}").Result;

            Fornecedor fornecedor = response.Content.ReadAsAsync<Fornecedor>().Result;

            if (fornecedor != null)
            {
                mensagem = $"{fornecedor.Nome} foi excluido com sucesso";
            }

            return Json(mensagem, JsonRequestBehavior.AllowGet);
        }
    }
}
