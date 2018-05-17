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
using System;

namespace Empresa.Compras.Api.Controllers
{
    public class PropostasController : ApiController
    {
        private ComprasContext db = new ComprasContext();
        private PropostaValidator validador = new PropostaValidator();

        // GET: api/Propostas
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.Expand  | AllowedQueryOptions.Skip | AllowedQueryOptions.Top | AllowedQueryOptions.InlineCount,
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

            //Verifica se alterou o status para aprovada
            //RN04.01 - Validade das propostas: as propostas expiram após 24h, não podendo mais ser aprovadas;
            if (GetStatus(id) != "Aprovada" && proposta.Status == "Aprovada")
            {                
                TimeSpan diferenca = DateTime.Now - proposta.DataProposta;
                int dias = (int)diferenca.TotalDays;

                if (dias > 1)//Todo Deixar prazo dinamico conforme configuração do administrador
                {
                    return BadRequest("Proposta expirada! Não é possível aprovar uma proposta após 24hs.");
                }            
            }

            //RN04.03 - Edição de propostas: as propostas não podem ser editadas caso já tenham passado pela aprovação do analista financeiro.
            if (AprovadaFinanceiro(id))
            {
                return BadRequest("Proposta já foi aprovada pelo financeiro e não pode se alterada.");                 
            }

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

        private string GetStatus(int id)
        {
            using (var db = new ComprasContext())
            {
                return db.Propostas.Find(id).Status;
            }
        }

        private bool AprovadaFinanceiro(int id)
        {
            using (var db = new ComprasContext())
                return db.Propostas.Find(id).AprovadoPeloAnalista;            
        }
    }
}