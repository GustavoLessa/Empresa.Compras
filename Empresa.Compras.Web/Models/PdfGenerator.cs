using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Empresa.Compras.Entities;

namespace Empresa.Compras.Web.Models
{
    public class PdfGenerator
    {
        public PdfGenerator(Proposta proposta)
        {
            Document doc = new Document(PageSize.A4);
            doc.SetMargins(40, 40, 40, 80);//estibulando o espaçamento das margens que queremos
            doc.AddCreationDate();//adicionando as configuracoes

            //caminho onde sera criado o pdf + nome desejado
            //OBS: o nome sempre deve ser terminado com .pdf
            string caminho = @"C:\Propostas\";

            if (!Directory.Exists(caminho))
            {                
                Directory.CreateDirectory(caminho);
            }

            caminho += $"{proposta.Nome}.pdf";
            
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));

            doc.Open();

           
            string dados = "";
            
            Paragraph paragrafo = new Paragraph(dados, new Font(Font.NORMAL, 14));
          
            paragrafo.Alignment = Element.ALIGN_JUSTIFIED;

            
            paragrafo.Font = new Font(Font.NORMAL, 14, (int)System.Drawing.FontStyle.Bold);
            paragrafo.Add("Proposta: ");
            paragrafo.Font = new Font(Font.NORMAL, 14, (int)System.Drawing.FontStyle.Regular);
            paragrafo.Add(proposta.Nome);           
            paragrafo.Add("Data: " + proposta.DataProposta.ToShortDateString());
            paragrafo.Add("Valor: " + proposta.ValorProposta.ToString("c"));
            paragrafo.Add("Status: " + proposta.Status);
            paragrafo.Add("Fornecedor: " + proposta.Fornecedor.Nome);
            paragrafo.Add("Categoria: " + proposta.Categoria.Nome);
            paragrafo.Add("Aprovado Financeiro: " + (proposta.AprovadoPeloAnalista == true ? "Sim" : "Não"));
            paragrafo.Add("Aprovado pelo diretor: " + (proposta.AprovadoPeloDiretor == true ? "Sim" : "Não"));

            doc.Add(paragrafo);
            
            doc.Close();
        }
        
    }
}