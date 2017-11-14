using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platforme.model
{
    public class Akcija
    {
        public int Id_Namestaj { get; set; }
        public DateTime DatumPocetka { get; set; }
        public DateTime DatumZavrsetka { get; set; }
        public Namestaj KomadNamestaja { get; set; }
        public double Popust { get; set; }
        public bool Obrisan { get; set; }

        public Akcija() { }

        public Akcija(int Id_Namestaj, DateTime DatumPocetka, DateTime DatumZavrsetka, Namestaj KomadNamestaja, double Popust, bool Obrisan)
        {
            this.Id_Namestaj = Id_Namestaj;
            this.DatumPocetka = DatumPocetka;
            this.DatumZavrsetka = DatumZavrsetka;
            this.KomadNamestaja = KomadNamestaja;
            this.Popust = Popust;
            this.Obrisan = Obrisan;
        }
    }
}
