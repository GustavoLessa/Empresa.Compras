using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Empresa.Compras.Api.Models.Context;
using Empresa.Compras.Entities;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using Empresa.Compras.Api.Models.Validation;
using Empresa.Compras.Api.Filters;
using FluentValidation;

namespace Empresa.Compras.Api.Controllers
{
    public class PropostasController : ApiController
    {
        private ComprasContext db = new ComprasContext();
        private PropostaValidator validador = new PropostaValidator();

        // GET: api/Propostas
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.Skip | AllowedQueryOptions.Top | AllowedQueryOptions.InlineCount,
                     MaxTop = 10,
                     PageSize = 10)]
        public IQueryable<Proposta> GetPropostas()
        {
            return db.Propostas;
        }

        // GET: api/Propostas/5
        [ResponseType(typeof(Proposta))]
        public IHttpActionResult GetProposta(int id)
        {
            if (id <= 0)
                return BadRequest("O id informado na URL deve ser maior que zero.");

            Proposta proposta = db.Propostas.Find(id);
            if (proposta == null)
            {
                return NotFound();
            }

            return Ok(proposta);
        }

        // PUT: api/Propostas/5
        [ResponseType(typeof(void))]
        [BasicAuhtentication]
        public IHttpActionResult PutProposta(int id, Proposta proposta)
        {
            if (id <= 0)
                return BadRequest("O id informado na URL deve ser maior que zero.");

            if (id != proposta.IdProposta)
                return BadRequest("O id informado na URL deve ser igual ao id informado no corpo da requisição.");

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if (id != proposta.IdProposta)
            //{
            //    return BadRequest();
            //}

            validador.ValidateAndThrow(proposta);

            db.Entry(proposta).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropostaExists(id))
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

        // POST: api/Propostas
        [ResponseType(typeof(Proposta))]
        [BasicAuhtentication]
        public IHttpActionResult PostProposta(Proposta proposta)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            validador.ValidateAndThrow(proposta);

            db.Propostas.Add(proposta);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = proposta.IdProposta }, proposta);
        }

        // DELETE: api/Propostas/5
        [ResponseType(typeof(Proposta))]
        [BasicAuhtentication]
        public IHttpActionResult DeleteProposta(int id)
        {
            if (id <= 0)
                return BadRequest("O id informado na URL deve ser maior que zero.");

            Proposta proposta = db.Propostas.Find(id);
            if (proposta == null)
            {
                return NotFound();
            }

            db.Propostas.Remove(proposta);
            db.SaveChanges();

            return Ok(proposta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PropostaExists(int id)
        {
            return db.Propostas.Count(e => e.IdProposta == id) > 0;
        }
    }
}