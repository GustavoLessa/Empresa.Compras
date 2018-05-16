using Empresa.Compras.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Empresa.Compras.Web.Controllers
{
    public class PropostasController : Controller
    {
        HttpClient client = new HttpClient();

        public PropostasController()
        {
            client.BaseAddress = new Uri("http://localhost:5677");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "admin"); // TODO Ajustar autenticação
        }

        // GET: Propostas
        public ActionResult Index()
        {
            List<Proposta> propostas = new List<Proposta>();

            HttpResponseMessage response = client.GetAsync("/api/propostas").Result;

            if (response.IsSuccessStatusCode)
            {
                propostas = response.Content.ReadAsAsync<List<Proposta>>().Result;
            }
            return View(propostas);
        }        

        // GET: Propostas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Propostas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Proposta proposta)
        {
            try
            {
                HttpResponseMessage response = client.PostAsJsonAsync<Proposta>($"/api/propostas", proposta).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    TempData["mensagem"] = $"{proposta.Nome} foi salvo com sucesso";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "Erro ao criar proposta.";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Propostas/Edit/5
        public ActionResult Edit(int idProposta)
        {
            HttpResponseMessage response = client.GetAsync($"/api/propostas/{idProposta}").Result;
            Proposta proposta = response.Content.ReadAsAsync<Proposta>().Result;

            if (proposta != null)
                return View(proposta);
            else
                return HttpNotFound();
        }

        // POST: Propostas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int idProposta, Proposta proposta)
        {
            try
            {
                HttpResponseMessage response = client.PutAsJsonAsync<Proposta>($"/api/propostas/{idProposta}", proposta).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    TempData["mensagem"] = $"{proposta.Nome} foi salvo com sucesso";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "Erro ao alterar proposta.";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }        

        // POST: Propostas/Delete/5
        [HttpPost]
        public JsonResult Delete(int idProposta)
        {
            string mensagem = string.Empty;
            HttpResponseMessage response = client.GetAsync($"/api/propostas/{idProposta}").Result;
            Proposta proposta = response.Content.ReadAsAsync<Proposta>().Result;

            if (proposta != null)           
                mensagem = $"{proposta.Nome} foi excluido com sucesso";           

            return Json(mensagem, JsonRequestBehavior.AllowGet);
        }
    }
}
