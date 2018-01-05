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
        public ObservableCollection<Kupac> Kupac { get; set; }
        public ObservableCollection<StavkaProdajeNamestaja> StavkaProdajeNamestaja { get; set; }
        public ObservableCollection<StavkaProdajeUsluge> StavkaProdajeUsluge { get; set; }
        public ObservableCollection<Racun> Racun { get; set; }
        public ObservableCollection<Akcija> Akcija { get; set; }
        public ObservableCollection<Usluga> Usluga { get; set; }
        public ObservableCollection<Zaposleni> Zaposleni { get; set; }

        private Projekat()
        {
            TipNamestaja = new ObservableCollection<model.TipNamestaja>();
            Namestaj = new ObservableCollection<model.Namestaj>();
            Kupac = new ObservableCollection<model.Kupac>();
            StavkaProdajeNamestaja = new ObservableCollection<model.StavkaProdajeNamestaja>();
            Racun = new ObservableCollection<model.Racun>();
            Akcija = new ObservableCollection<model.Akcija>();
            Usluga = new ObservableCollection<model.Usluga>();
            StavkaProdajeUsluge = new ObservableCollection<model.StavkaProdajeUsluge>();
            Zaposleni = new ObservableCollection<model.Zaposleni>();
        }

    }
}
