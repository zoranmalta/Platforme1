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
        public StavkaProdajeUsluge stavkaUsluga { get; set; }

        public ProdajaWindow()
        {
            InitializeComponent();
            stavka = new StavkaProdajeNamestaja();
            stavkaUsluga = new StavkaProdajeUsluge();
            tbImeKupca.DataContext = kupac;
            tbPrezimeKupca.DataContext = kupac;
            tbTelefonKupca.DataContext = kupac;
            cbOdaberite.ItemsSource = Projekat.Instance.Namestaj;
            cbOdaberite.DataContext = stavka;
            tbKolicina.DataContext = stavka;
            cbUsluge.ItemsSource = Projekat.Instance.Usluga;
            cbUsluge.DataContext = stavkaUsluga;

        }

        public void DodajNovuStavkuNamestaja(object sender, RoutedEventArgs e)
        {
            stavka.Namestaj = (Namestaj)cbOdaberite.SelectedItem;
            if (stavka.Namestaj == null)
            {
                return;
            }
            stavka.Id_Namestaj = stavka.Namestaj.Id;
            if (stavka.Kolicina < stavka.Namestaj.Kolicina+1)
            {
                namestaj = stavka.Namestaj;
                foreach(Namestaj n in Projekat.Instance.Namestaj)
                {
                    if (namestaj.Id == n.Id)
                    {
                        n.Kolicina = n.Kolicina - stavka.Kolicina;
                    }
                }
                racun.listaStavkiNamestaja.Add(stavka);
            }
            else if(stavka.Kolicina > stavka.Namestaj.Kolicina)
            {
                MessageBox.Show("nema dovoljno na lageru");
                return;
               
            }

            stavka = new StavkaProdajeNamestaja();
            tbKolicina.DataContext = stavka;
            cbOdaberite.Text = "";
            tbKolicina.Text = "";

        }
        public void DodajNovuStavkuUsluge(object sender, RoutedEventArgs e)
        {
            stavkaUsluga.Usluga = (Usluga)cbUsluge.SelectedItem;
            if (stavkaUsluga.Usluga == null)
            {
                return;
            }
            stavkaUsluga.Id_Usluga = stavkaUsluga.Usluga.Id;
            racun.listaStavkiUsluga.Add(stavkaUsluga);
            stavkaUsluga = new StavkaProdajeUsluge();
            cbUsluge.Text = "";
        }
        public void ZavrsiRacun(object sender, RoutedEventArgs e)
        {
            if (racun.listaStavkiNamestaja.Count != 0)
            {
                
                Kupac.DodajKupca(kupac);
                int idkupacmax = Kupac.UzmiMaxId();
                racun.Id_Kupac = idkupacmax;
                kupac.Id = idkupacmax;
                racun.Kupac = kupac;
                Racun.DodajRacun(racun);
                int max=Racun.UzmiMaxId();
                racun.Id = max;
                foreach(StavkaProdajeNamestaja s in racun.listaStavkiNamestaja)
                {
                    s.Id_Racun = max;
                    StavkaProdajeNamestaja.DodajStavkuProdajeNamestaja(s);
                    Namestaj.IzmeniNamestaj(s.Namestaj);
                }
                foreach (StavkaProdajeUsluge s in racun.listaStavkiUsluga)
                {
                    s.Id_Racun = max;
                    StavkaProdajeUsluge.DodajStavkuProdajeUsluga(s);
                }

                var prw = new PrikazRacunaWindow(racun);
                this.Close();
                prw.ShowDialog();
            }
        }
    }
}
