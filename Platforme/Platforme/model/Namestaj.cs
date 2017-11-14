using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platforme.model
{
    public class Namestaj
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Sifra { get; set; }
        public double Cena { get; set; }
        public int Kolicina { get; set; }
        public int Id_Tip { get; set; }
        public bool Akcija { get; set; }
        public bool Obrisan { get; set; }
        public double PDV { get; set; }

        public Namestaj() { }

        public Namestaj(int Id, string Naziv, string Sifra, double Cena, int Kolicina, int Id_Tip, bool
                         Akcija, bool Obrisan, double PDV)
        {
            this.Id = Id;
            this.Naziv = Naziv;
            this.Sifra = Sifra;
            this.Cena = Cena;
            this.Kolicina = Kolicina;
            this.Id_Tip = Id_Tip;
            this.Akcija = Akcija;
            this.Obrisan = Obrisan;
            this.PDV = PDV;
        }
        public override string ToString()
        {
            return $"{Id},{Naziv},{Cena},{TipNamestaja.GetById(Id_Tip).Naziv}";
        }

        public static Namestaj GetNamestajById(int Id)
        {
           foreach(var n in Projekat.Instance.Namestaj)
            {
                if (n.Id == Id)
                {
                    return n;
                }
            }
            return null;
        }
    }
}
