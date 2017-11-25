using Platforme.util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platforme.model
{
    public class Projekat
    {
        public static Projekat Instance { get; private set; } = new Projekat();

        public ObservableCollection<Namestaj> Namestaj;
        public ObservableCollection<TipNamestaja> TipNamestaja;

        private Projekat()
        {
            TipNamestaja = new ObservableCollection<model.TipNamestaja>(GenericsSerializer.DeSerialize<TipNamestaja>("TipNamestaja.xml"));
            Namestaj = new ObservableCollection<model.Namestaj>(GenericsSerializer.DeSerialize<Namestaj>("namestaj.xml"));
        }

    }
}
