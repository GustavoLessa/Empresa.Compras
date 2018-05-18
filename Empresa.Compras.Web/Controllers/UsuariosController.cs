using Empresa.Compras.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;

namespace Empresa.Compras.Web.Controllers
{
    public class UsuariosController : Controller
    {
        HttpClient client = new HttpClient();

        public UsuariosController()
        {
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UriApi"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "admin"); // TODO Ajustar autenticação
        }

        // GET: Usuarios
        public ActionResult Index()
        {
            List<Usuario> usuarios = new List<Usuario>();

            HttpResponseMessage response = client.GetAsync("/api/usuarios").Result;

            if (response.IsSuccessStatusCode)
            {
                usuarios = response.Content.ReadAsAsync<List<Usuario>>().Result;
            }
            return View(usuarios);
        }        

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario)
        {
            try
            {
                HttpResponseMessage response = client.PostAsJsonAsync<Usuario>($"/api/usuarios", usuario).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    TempData["mensagem"] = $"{usuario.Nome} foi salvo com sucesso";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "Erro ao criar usuário.";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int idUsuario)
        {
            HttpResponseMessage response = client.GetAsync($"/api/usuarios/{idUsuario}").Result;
            Usuario usuario = response.Content.ReadAsAsync<Usuario>().Result;

            if (usuario != null)
                return View(usuario);
            else
                return HttpNotFound();
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int idUsuario, Usuario usuario)
        {
            try
            {
                HttpResponseMessage response = client.PutAsJsonAsync<Usuario>($"/api/usuarios/{idUsuario}", usuario).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    TempData["mensagem"] = $"{usuario.Nome} foi salvo com sucesso";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "Erro ao alterar usuario.";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // POST: Usuarios/Delete/5
        [HttpPost]
        public JsonResult Delete(int idUsuario)
        {
            string mensagem = string.Empty;
            HttpResponseMessage response = client.DeleteAsync($"/api/usuarios/{idUsuario}").Result;
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                mensagem = "Usuário excluído com sucesso";
            }
            return Json(mensagem, JsonRequestBehavior.AllowGet);          
        }
    }
}
