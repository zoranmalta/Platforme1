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
        public ObservableCollection<Kupac> Kupac;
        public ObservableCollection<StavkaProdajeNamestaja> StavkaProdajeNamestaja;
        public ObservableCollection<Racun> Racun;

        private Projekat()
        {
            TipNamestaja = new ObservableCollection<model.TipNamestaja>(GenericsSerializer.DeSerialize<TipNamestaja>("TipNamestaja.xml"));
            Namestaj = new ObservableCollection<model.Namestaj>(GenericsSerializer.DeSerialize<Namestaj>("namestaj.xml"));
            Kupac = new ObservableCollection<model.Kupac>(GenericsSerializer.DeSerialize<Kupac>("kupac.xml"));
            StavkaProdajeNamestaja = new ObservableCollection<model.StavkaProdajeNamestaja>(GenericsSerializer.DeSerialize<StavkaProdajeNamestaja>("stavkaProdajeNamestaja.xml"));
            Racun = new ObservableCollection<model.Racun>(GenericsSerializer.DeSerialize<Racun>("racun.xml"));
        }

    }
}
