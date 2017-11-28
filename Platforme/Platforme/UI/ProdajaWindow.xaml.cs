using Platforme.model;
using Platforme.util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Platforme.UI
{
    /// <summary>
    /// Interaction logic for ProdajaWindow.xaml
    /// </summary>
    public partial class ProdajaWindow : Window
    {
        private Kupac kupac = new Kupac();
        private Namestaj namestaj=new Namestaj();
        private TipNamestaja tipNamestaja=new TipNamestaja();
        private Racun racun = new Racun();
        public StavkaProdajeNamestaja stavka { get; set; }

        public ProdajaWindow()
        {
            InitializeComponent();
            stavka = new StavkaProdajeNamestaja();
            tbImeKupca.DataContext = kupac;
            tbPrezimeKupca.DataContext = kupac;
            tbTelefonKupca.DataContext = kupac;
            cbOdaberite.ItemsSource = Projekat.Instance.Namestaj;
            cbOdaberite.DataContext = stavka;
            cbVrstaProdaje.Items.Add("Namestaj");
            cbVrstaProdaje.Items.Add("Usluge");
            cbVrstaProdaje.SelectedIndex = 0;

            if (cbVrstaProdaje.Text == "Namestaj")
            {
                cbOdaberite.ItemsSource = Projekat.Instance.Namestaj;
                cbOdaberite.DataContext = namestaj;
            }
            else if (cbVrstaProdaje.Text == "Usluge")
            {
                
            }
            tbKolicina.DataContext = stavka;

        }
        ObservableCollection<StavkaProdajeNamestaja> postojeceStavke = Projekat.Instance.StavkaProdajeNamestaja;

        public void DodajNovuStavku(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Racun> postojeciRacuni = Projekat.Instance.Racun;
            stavka.Id_Racun = postojeciRacuni.Count + 1;
            stavka.Namestaj = (Namestaj)cbOdaberite.SelectedItem;
            stavka.Id_Namestaj = stavka.Namestaj.Id;
            racun.listaStavkiNamestaja.Add(stavka);
            stavka = new StavkaProdajeNamestaja();
            tbKolicina.DataContext = stavka;
            cbOdaberite.Text = "";
            tbKolicina.Text = "";

        }
        public void ZavrsiRacun(object sender, RoutedEventArgs e)
        {
            if (racun.listaStavkiNamestaja.Count != 0)
            {
                ObservableCollection<Racun> postojeciRacuni = Projekat.Instance.Racun;
                racun.Id = postojeciRacuni.Count + 1;
                racun.Datum = new DateTime();
                ObservableCollection<Kupac> postojeciKupci = Projekat.Instance.Kupac;
                kupac.Id = postojeciKupci.Count + 1;
                postojeciKupci.Add(kupac);
                GenericsSerializer.Serialize("kupac.xml", postojeciKupci);
                racun.Kupac = kupac;
                racun.Id_Kupac = kupac.Id;
                foreach (StavkaProdajeNamestaja s in racun.listaStavkiNamestaja)
                {
                    postojeceStavke.Add(s);
                }
                GenericsSerializer.Serialize<StavkaProdajeNamestaja>("stavkaProdajeNamestaja.xml", postojeceStavke);
                postojeciRacuni.Add(racun);
                GenericsSerializer.Serialize<Racun>("racun.xml", postojeciRacuni);
                var prw = new PrikazRacunaWindow(racun);
                prw.ShowDialog();
            }
        }
    }
}
