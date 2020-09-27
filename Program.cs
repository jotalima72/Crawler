using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using crawlerLib.DAO;
using crawlerLib.models;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;

namespace crawler
{
    class Program
    {
        static void Main()
        {
            CrawDB db = null;
            Task taskA = Task.Run(() => db = new CrawDB());
            try
            {
                taskA.Wait();
                Console.WriteLine("Banco de boa");
            }
            catch (Exception e)
            {
                Console.WriteLine("Banco deu erro");
                return;
            }
            //Noticia noticia = new Noticia
            //{
            //    Titulo = "teste",
            //    Descricao = "descricao",
            //    Link = "https://saude.gov.br"
            //};

            //db.noticias.add(noticia);
            //db.savechanges();
            string juncao;


            var httpClient = new HttpClient();

            var HtmlDoc = new HtmlDocument();


            for (int i = 0; i < 15; i++)
            {
                
                juncao = "https://www.gov.br/pt-br/noticias/ultimas-noticias?b_start:int=" + i * 10;
                string html = "";
                Task taskB = Task.Run(async () =>
                html = await httpClient.GetStringAsync(juncao));
                try
                {
                    taskB.Wait();
                    Console.WriteLine("html pego e pronto");
                }
                catch (Exception e)
                {
                    Console.WriteLine("erro na pegação do html");
                }
                HtmlDoc.LoadHtml(html);
                Console.WriteLine("carregando html no htmldoc");

                var divs = HtmlDoc.DocumentNode.Descendants("article").
               Where(node =>
               node.GetAttributeValue("class", "").Equals("tileItem visualIEFloatFix tile-collective-nitf-content")).ToList();

                foreach (var div in divs)
                {
                    var titulo = div?.Descendants("h2").First().Descendants("a").First().InnerText;
                    var descricao = div?.Descendants("p").FirstOrDefault().InnerText.Trim();
                    var link = div?.Descendants("h2").First().Descendants("a").First().GetAttributeValue("href", string.Empty);

                    Noticia noticia = new Noticia
                    {
                        Titulo = titulo,
                        Descricao = descricao,
                        Link = link
                    };
                    try
                    {
                        db.Noticias.Add(noticia);
                        db.SaveChanges();
                        Console.WriteLine(noticia.ToString());
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("noticia existente ou erro de inserção no banco");
                    }
                }

            }
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }


    }
}
