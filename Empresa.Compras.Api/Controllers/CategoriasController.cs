using Empresa.Compras.Api.Models.Context;
using Empresa.Compras.Api.Models.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using FluentValidation;
using System.Data;
using System.Data.Entity;
using System.Web.Http.OData;
using System.Net;
using System.Web.Http.OData.Extensions;
using System.Web.Http.OData.Query;
using Empresa.Compras.Api.Filters;
using Empresa.Compras.Entities;

namespace Empresa.Compras.Api.Controllers
{
    public class CategoriasController : ApiController
    {
        private ComprasContext db = new ComprasContext();
        private CategoriaValidator validador = new CategoriaValidator();

        // GET: api/Categorias
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.Skip | AllowedQueryOptions.Top | AllowedQueryOptions.InlineCount,
                     MaxTop = 10,
                     PageSize = 10)]
        public IQueryable<Categoria> GetCategorias()
        {
            return db.Categorias;
        }

        // GET: api/Categorias/5
        public IHttpActionResult GetCategoria(int id)
        {
            if (id <= 0)
                return BadRequest("O id informado na URL deve ser maior que zero.");

            Categoria categoria = db.Categorias.Find(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(categoria);
        }

        [Route("api/categorias/{id}/propostas")]
        public IHttpActionResult GetPropostasCategoria(int id)
        {
            if (id <= 0)
                return BadRequest("O id informado na URL deve ser maior que zero.");

            Categoria categoria = db.Categorias.Find(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(categoria.Propostas);
        }

        // PUT: api/Categorias/5
        [BasicAuhtentication]
        public IHttpActionResult PutCategoria(int id, Categoria categoria)
        {
            if (id <= 0)
                return BadRequest("O id informado na URL deve ser maior que zero.");

            if (id != categoria.IdCategoria)
                return BadRequest("O id informado na URL deve ser igual ao id informado no corpo da requisição.");

            if (db.Categorias.Count(e => e.IdCategoria == id) == 0)
                return NotFound();

            validador.ValidateAndThrow(categoria);

            db.Entry(categoria).State = EntityState.Modified;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Categorias
        [BasicAuhtentication]
        public IHttpActionResult PostCategorias(Categoria categoria)
        {
            validador.ValidateAndThrow(categoria);

            db.Categorias.Add(categoria);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = categoria.IdCategoria }, categoria);
        }

        // DELETE: api/Categorias/5
        [BasicAuhtentication]
        public IHttpActionResult DeleteCategoria(int id)
        {
            if (id <= 0)
                return BadRequest("O id informado na URL deve ser maior que zero.");

            Categoria categoria = db.Categorias.Find(id);

            if (categoria == null)
                return NotFound();

            if (categoria.Propostas.Count() > 0)//TODO verificar necessidade de filtrar por status
                return Content(HttpStatusCode.Forbidden, "Essa categoria não pode ser excluída, pois há propostas relacionadas a ela.");

            db.Categorias.Remove(categoria);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}