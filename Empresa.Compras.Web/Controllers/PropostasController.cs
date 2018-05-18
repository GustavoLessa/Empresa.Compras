using Empresa.Compras.Entities;
using Empresa.Compras.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UriApi"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "admin"); // TODO Ajustar autenticação

            ViewBag.filtroCategoria = new SelectList(GetCategorias(), "Nome", "Nome");
            ViewBag.filtroFornecedor = new SelectList(GetFornecedores(), "Nome", "Nome");
        }

        // GET: Propostas
        public ActionResult Index(string filtroNomeProposta, string filtroFornecedor, string filtroStatus, string filtroCategoria, string filtroDataIni, string filtroDataFim)
        {
            DateTime? dtIni = null;
            DateTime? dtFim = null;

            if (!string.IsNullOrEmpty(filtroDataIni))           
                dtIni = DateTime.Parse(filtroDataIni);          

            if (!string.IsNullOrEmpty(filtroDataFim))            
                dtFim = DateTime.Parse(filtroDataFim);
           


            List<Proposta> propostas = new List<Proposta>();

            string filtro = string.Empty;
            filtro = "?$expand=Fornecedor,Categoria";
            filtro += string.IsNullOrEmpty(filtroFornecedor)  ? "" : $"&$filter=Fornecedor/Nome eq '{filtroFornecedor}'";
            filtro += string.IsNullOrEmpty(filtroCategoria) ? "" : $"&$filter=Categoria/Nome eq '{filtroCategoria}'";
            filtro += string.IsNullOrEmpty(filtroStatus) ? "" : $"&$filter=Status eq '{filtroStatus}'" ;
            filtro += string.IsNullOrEmpty(filtroNomeProposta) ? "" : $"&$filter=Nome eq '{filtroNomeProposta}'";

            filtro += dtIni != null && dtFim == null ? $"&$filter=DataProposta gt DateTime'{String.Format("{0:yyyy-MM-dd}", dtIni)}'" : "";
            filtro += dtFim != null && dtIni == null ? $"&$filter=DataProposta lt DateTime'{String.Format("{0:yyyy-MM-dd}", dtFim)}'" : "";
            filtro += dtIni != null && dtFim != null ? $"&$filter=DataProposta gt DateTime'{String.Format("{0:yyyy-MM-dd}", dtIni)}' and DataProposta lt DateTime'{String.Format("{0:yyyy-MM-dd}", dtFim)}'" : "";            
           

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
                proposta.DataProposta = DateTime.Now;
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
                    var x = response.Content.ReadAsAsync<ReturnMessage>();
                    ViewBag.IdCategoria = new SelectList(GetCategorias(), "IdCategoria", "Nome", proposta.IdCategoria);
                    ViewBag.IdFornecedor = new SelectList(GetFornecedores(), "IdFornecedor", "Nome", proposta.IdFornecedor);
                    TempData["error"] = x.Result.Message;
                    return View(proposta);
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

        public JsonResult GerarPdfProposta(int idProposta)
        {
            string mensagem = string.Empty;
            HttpResponseMessage response = client.GetAsync($"/api/propostas/{idProposta}").Result;
            Proposta prop = response.Content.ReadAsAsync<Proposta>().Result;

            if (prop !=null)
            {
                PdfGenerator pdf = new PdfGenerator(prop);
                mensagem = "Pdf da proposta criado com sucesso";
                TempData["mensagem"] = $"{prop.Nome}.pdf foi salvo com sucesso";
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
    public class ReturnMessage
    {
        public string Message { get; set; }
    }
}
