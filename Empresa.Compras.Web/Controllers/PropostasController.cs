using Empresa.Compras.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
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

            ViewBag.filtroCategoria = new SelectList(GetCategorias(), "Nome", "Nome");
            ViewBag.filtroFornecedor = new SelectList(GetFornecedores(), "Nome", "Nome");
        }

        // GET: Propostas
        public ActionResult Index(string filtroNomeProposta, string filtroFornecedor, DateTime? filtroDataIni, DateTime? filtroDataFim, string filtroStatus, string filtroCategoria)
        {
            List<Proposta> propostas = new List<Proposta>();

            string filtro = string.Empty;
            filtro = "? $expand=Fornecedor,Categoria";
            filtro += string.IsNullOrEmpty(filtroFornecedor)  ? "" : $"&$filter=Fornecedor/Nome eq '{filtroFornecedor}'";
            filtro += string.IsNullOrEmpty(filtroCategoria) ? "" : $"&$filter=Categoria/Nome eq '{filtroCategoria}'";
            filtro += string.IsNullOrEmpty(filtroStatus) ? "" : $"&$filter=Status eq '{filtroStatus}'" ;
            filtro += string.IsNullOrEmpty(filtroNomeProposta) ? "" : $"&$filter=Nome eq '{filtroNomeProposta}'";

            filtro += filtroDataIni.ToString() != null || filtroDataFim.ToString() == null ? "" : $"&$filter=DataProposta gt DateTime '{filtroDataIni}'";
            filtro += filtroDataFim.ToString() != null || filtroDataIni.ToString() == null ? "" : $"&$filter=DataProposta lt DateTime '{filtroDataFim}'";
            filtro += filtroDataFim.ToString() != null || filtroDataIni.ToString() != null ? "" : $"&$filter=DataProposta gt DateTime '{filtroDataIni}' and DataProposta lt DateTime '{filtroDataFim}'";           

            HttpResponseMessage response = client.GetAsync($"/api/propostas" + filtro).Result;

            if (response.IsSuccessStatusCode)
            {
                propostas = response.Content.ReadAsAsync<List<Proposta>>().Result;
            }
            return View(propostas);
        }

        // GET: Propostas/Create
        public ActionResult Create()
        {
            ViewBag.IdCategoria = new SelectList(GetCategorias(), "IdCategoria", "Nome");
            ViewBag.IdFornecedor = new SelectList(GetFornecedores(), "IdFornecedor", "Nome");           

            return View();
        }

        private List<Categoria> GetCategorias()
        {
            List<Categoria> categorias = new List<Categoria>();
            HttpResponseMessage response = client.GetAsync("/api/categorias").Result;
            if (response.IsSuccessStatusCode)
            {
                categorias = response.Content.ReadAsAsync<List<Categoria>>().Result;
            }
            return categorias;
        }

        private List<Fornecedor> GetFornecedores()
        {
            List<Fornecedor> fornecedores = new List<Fornecedor>();
            HttpResponseMessage responseForn = client.GetAsync("/api/fornecedores").Result;
            if (responseForn.IsSuccessStatusCode)
            {
                fornecedores = responseForn.Content.ReadAsAsync<List<Fornecedor>>().Result;
            }
            return fornecedores;
        }

        // POST: Propostas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Proposta proposta)
        {
            try
            {
                proposta.Status = Status.PendenteAnalise.ToString();

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
            {
                ViewBag.IdCategoria = new SelectList(GetCategorias(), "IdCategoria", "Nome", proposta.IdCategoria);
                ViewBag.IdFornecedor = new SelectList(GetFornecedores(), "IdFornecedor", "Nome", proposta.IdFornecedor);
                return View(proposta);
            }
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
            HttpResponseMessage response = client.DeleteAsync($"/api/propostas/{idProposta}").Result;
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                mensagem = "Proposta excluída com sucesso";
            }
            return Json(mensagem, JsonRequestBehavior.AllowGet);
        }
    }

    enum Status
    {        
        Aprovada, 
        Reprovada,
        PendenteAnalise,
        PendenteDiretoria
    };
}
