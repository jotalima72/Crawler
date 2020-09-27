using System;
using System.Collections.Generic;
using System.Text;

namespace crawlerLib.models
{
    public class Noticia
    {
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Link { get; set; }

        public override string ToString()
        {
            return "Titulo - " + Titulo + "\nLink - " + Link +"\n\n";
        }
   
    }
}
