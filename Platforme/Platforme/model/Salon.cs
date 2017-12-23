using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platforme.model
{
    public class Salon
    {
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public string Telefon { get; set; }
        public int PIB { get; set; }

        public Salon()
        {
            this.Naziv = "Geo";
            this.Adresa = "Cika Stevina NS";
            this.Telefon = "021-22-333";
            this.PIB = 1234567;
        }

        public Salon(string Naziv,string Adresa,string Telefon,int PIB)
        {
            this.Naziv = Naziv;
            this.Adresa = Adresa;
            this.Telefon = Telefon;
            this.PIB = PIB;
        }
        public override string ToString()
        {
            return $"{Naziv} , {Adresa} ,Tel: {Telefon} ,PIB: {PIB}";
        }
    }
}
