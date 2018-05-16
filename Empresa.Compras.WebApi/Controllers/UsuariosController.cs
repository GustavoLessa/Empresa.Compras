using Empresa.Compras.WebApi.Filters;
using Empresa.Compras.WebApi.Models.Context;
using Empresa.Compras.WebApi.Models.Entities;
using Empresa.Compras.WebApi.Models.Validation;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using System.Web.Mvc;

namespace Empresa.Compras.WebApi.Controllers
{
    public class UsuariosController : ApiController
    {
        private ComprasContext db = new ComprasContext();
        private UsuarioValidator validador = new UsuarioValidator();

        // GET: api/Usuarios
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Select | AllowedQueryOptions.Skip | AllowedQueryOptions.Top,
                     MaxTop = 10,
                     PageSize = 10)]
        public IQueryable GetUsuarios()
        {
            return db.Usuarios;
        }

        // GET: api/Usuarios/5
        public IHttpActionResult GetUsuario(int id)
        {
            if (id <= 0)
                return BadRequest("O id informado na URL deve ser maior que zero.");

            Usuario usuarios = db.Usuarios.Find(id);

            if (usuarios == null)
                return NotFound();

            return Ok(usuarios);
        }

        // PUT: api/Usuarios/5
        [BasicAuhtentication]
        public IHttpActionResult PutUsuario(int id, Usuario usuario)
        {
            if (id <= 0)
                return BadRequest("O id informado na URL deve ser maior que zero.");

            if (id != usuario.IdUsuario)
                return BadRequest("O id informado na URL deve ser igual ao id informado no corpo da requisição.");

            if (db.Usuarios.Count(v => v.IdUsuario == id) == 0)
                return NotFound();

            validador.ValidateAndThrow(usuario);           

            db.Entry(usuario).State = EntityState.Modified;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Usuarios
        [BasicAuhtentication]
        public IHttpActionResult PostUsuario(Usuario usuario)
        {
            validador.ValidateAndThrow(usuario);            

            db.Usuarios.Add(usuario);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = usuario.IdUsuario }, usuario);
        }

        // DELETE: api/Usuarios/5
        [BasicAuhtentication]
        public IHttpActionResult DeleteUsuario(int id)
        {
            if (id <= 0)
                return BadRequest("O id informado na URL deve ser maior que zero.");

            Usuario usuario = db.Usuarios.Find(id);

            if (usuario == null)
                return NotFound();

            db.Usuarios.Remove(usuario);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}