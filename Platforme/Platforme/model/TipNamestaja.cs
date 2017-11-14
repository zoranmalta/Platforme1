using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platforme.model
{
    public class TipNamestaja
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public bool Obrisan { get; set; }

        public TipNamestaja() { }

        public TipNamestaja(int Id, string Naziv, bool Obrisan)
        {
            this.Id = Id;
            this.Naziv = Naziv;
            this.Obrisan = Obrisan;
        }
        public override string ToString()
        {
            return Naziv;
        }
        public static TipNamestaja GetById(int Id)
        {
            foreach (var tipNamestaja in Projekat.Instance.TipoviNamestaja)
            {
                if (tipNamestaja.Id == Id)
                {
                    return tipNamestaja;
                }
            }
            return null;
        }
    }
}
