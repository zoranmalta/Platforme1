using Platforme.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platforme.model
{
    public class Projekat
    {
        public static Projekat Instance { get; private set; } = new Projekat();

        private List<Namestaj> namestaj;
        public List<Namestaj> Namestaj
        {
            get
            {
                this.namestaj = GenericsSerializer.DeSerialize<Namestaj>("namestaj.xml");

                return this.namestaj;
            }
            set
            {
                this.namestaj = value;
                GenericsSerializer.Serialize<Namestaj>("namestaj.xml", this.namestaj);
            }
        }

        public List<TipNamestaja> tipoviNamestaja;

        public List<TipNamestaja> TipoviNamestaja
        {
            get
            {
                this.tipoviNamestaja = GenericsSerializer.DeSerialize<TipNamestaja>("tipNamestaja.xml");
                return this.tipoviNamestaja;
            }
            set
            {
                this.tipoviNamestaja = value;
                GenericsSerializer.Serialize<TipNamestaja>("tipNamestaja.xml", this.tipoviNamestaja);
            }
        }


        //public Projekat()
        //{
        //    Instance = new Projekat();
        //}
        //pzivamo sa Projekat.Instance.Namestaj=novi namestaj tj value
    }
}
