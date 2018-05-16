using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Empresa.Compras.Api.Models.Context;
using Empresa.Compras.Entities;
using Empresa.Compras.Api.Models.Validation;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using Empresa.Compras.Api.Filters;
using FluentValidation;
using Empresa.Compras.Entities;

namespace Empresa.Compras.Api.Controllers
{
    public class FornecedoresController : ApiController
    {
        private ComprasContext db = new ComprasContext();
        private ForncedorValidator validador = new ForncedorValidator();

        // GET: api/Fornecedores
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.Skip | AllowedQueryOptions.Top | AllowedQueryOptions.InlineCount,
                     MaxTop = 10,
                     PageSize = 10)]
        public IQueryable<Fornecedor> GetFornecedores()
        {
            return db.Fornecedores;
        }

        // GET: api/Fornecedores/5
        [ResponseType(typeof(Fornecedor))]
        public IHttpActionResult GetFornecedor(int id)
        {
            if (id <= 0)
                return BadRequest("O id informado na URL deve ser maior que zero.");

            Fornecedor fornecedor = db.Fornecedores.Find(id);
            if (fornecedor == null)
            {
                return NotFound();
            }

            return Ok(fornecedor);
        }

        // PUT: api/Fornecedores/5
        [ResponseType(typeof(void))]
        [BasicAuhtentication]
        public IHttpActionResult PutFornecedor(int id, Fornecedor fornecedor)
        {
            if (id <= 0)
                return BadRequest("O id informado na URL deve ser maior que zero.");

            if (id != fornecedor.IdFornecedor)
                return BadRequest("O id informado na URL deve ser igual ao id informado no corpo da requisição.");

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if (id != fornecedor.IdFornecedor)
            //{
            //    return BadRequest();
            //}

            validador.ValidateAndThrow(fornecedor);

            db.Entry(fornecedor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FornecedorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Fornecedores
        [ResponseType(typeof(Fornecedor))]
        [BasicAuhtentication]
        public IHttpActionResult PostFornecedor(Fornecedor fornecedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            validador.ValidateAndThrow(fornecedor);

            db.Fornecedores.Add(fornecedor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = fornecedor.IdFornecedor }, fornecedor);
        }

        // DELETE: api/Fornecedores/5
        [ResponseType(typeof(Fornecedor))]
        [BasicAuhtentication]
        public IHttpActionResult DeleteFornecedor(int id)
        {
            if (id <= 0)
                return BadRequest("O id informado na URL deve ser maior que zero.");

            Fornecedor fornecedor = db.Fornecedores.Find(id);
            if (fornecedor == null)
            {
                return NotFound();
            }

            if (fornecedor.Propostas.Count() > 0)//TODO ver a necessidade de verificar apenas a  propostas ativas
                return Content(HttpStatusCode.Forbidden, "Este fornecedor não pode ser excluído, pois ele possui propostas relacionados a ele.");

            db.Fornecedores.Remove(fornecedor);
            db.SaveChanges();

            return Ok(fornecedor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FornecedorExists(int id)
        {
            return db.Fornecedores.Count(e => e.IdFornecedor == id) > 0;
        }
    }
}