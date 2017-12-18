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
        //(localdb)\ProjectsV13(SQL Server 13.0.400-DESKTOP-PU4J8RE\Zoran)
        public const string CONNECTION_STRING = @"Integrated Security=true;
                                          Initial Catalog=Platforme;
                                          Data Source=(localdb)\ProjectsV13";
        public static Projekat Instance { get; private set; } = new Projekat();

        public ObservableCollection<Namestaj> Namestaj { get; set; }
        public ObservableCollection<TipNamestaja> TipNamestaja { get; set; }
        public ObservableCollection<Kupac> Kupac;
        public ObservableCollection<StavkaProdajeNamestaja> StavkaProdajeNamestaja;
        public ObservableCollection<Racun> Racun;
        public ObservableCollection<Akcija> Akcija;

        private Projekat()
        {
            TipNamestaja = new ObservableCollection<model.TipNamestaja>();
            Namestaj = new ObservableCollection<model.Namestaj>();
            Kupac = new ObservableCollection<model.Kupac>(GenericsSerializer.DeSerialize<Kupac>("kupac.xml"));
            StavkaProdajeNamestaja = new ObservableCollection<model.StavkaProdajeNamestaja>(GenericsSerializer.DeSerialize<StavkaProdajeNamestaja>("stavkaProdajeNamestaja.xml"));
            Racun = new ObservableCollection<model.Racun>(GenericsSerializer.DeSerialize<Racun>("racun.xml"));
            Akcija = new ObservableCollection<model.Akcija>(GenericsSerializer.DeSerialize<Akcija>("akcija.xml"));
        }

    }
}
