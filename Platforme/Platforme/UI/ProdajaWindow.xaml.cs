using Platforme.model;
using Platforme.util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private ICollectionView view;
        private Kupac kupac = new Kupac();
        private Namestaj namestaj=new Namestaj();
        private TipNamestaja tipNamestaja=new TipNamestaja();
        private Racun racun = new Racun();
        public StavkaProdajeNamestaja stavka { get; set; }
        public StavkaProdajeUsluge stavkaUsluga { get; set; }
        public List<Object> lista { get; set; }

        public ProdajaWindow()
        {
            InitializeComponent();
            Projekat.Instance.Namestaj.Clear();
            Namestaj.UcitajNamestaj();

            lista = new List<Object>();
            stavka = new StavkaProdajeNamestaja();
            stavkaUsluga = new StavkaProdajeUsluge();
            tbImeKupca.DataContext = kupac;
            tbPrezimeKupca.DataContext = kupac;
            tbTelefonKupca.DataContext = kupac;
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Namestaj);
            view.Filter = NamestajFilter;
            dgNamestaj.ItemsSource = view;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            tbKolicina.DataContext = stavka;
            cbUsluge.ItemsSource = Projekat.Instance.Usluga;
            cbUsluge.DataContext = stavkaUsluga;
            lbStavke.ItemsSource = lista;
        }

        public void DodajNovuStavkuNamestaja(object sender, RoutedEventArgs e)
        {
            stavka.Namestaj = view.CurrentItem as Namestaj;
            if (stavka.Namestaj == null)
            {
                MessageBox.Show("Niste odabrali namestaj za prodaju");
                return;
            }
            stavka.Id_Namestaj = stavka.Namestaj.Id;
            foreach(Akcija a in Projekat.Instance.Akcija)
            {
                if (stavka.Namestaj.Id == a.IdNamestaj&& racun.Datum.CompareTo(a.DatumPocetka)>=0
                    && racun.Datum.CompareTo(a.DatumZavrsetka)<=0  )
                {
                    stavka.Popust = a.Popust;
                }
            }
            try
            {
                if (((string)tbKolicina.Text).Length > 0)
                    stavka.Kolicina = Int32.Parse((String)tbKolicina.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kolicina mora biti ceo broj veci od nule");
                return;
            }

            if (stavka.Kolicina <= 0)
            {
                MessageBox.Show("Kolicina mora biti ceo broj veci od nule");
                return;
            }
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
                lista.Add(stavka);
                lbStavke.Items.Refresh();
            }
            else if(stavka.Kolicina > stavka.Namestaj.Kolicina)
            {
                MessageBox.Show("nema dovoljno na lageru");
                tbKolicina.DataContext = stavka;
                tbKolicina.Text = "";
                return;
            }
            stavka = new StavkaProdajeNamestaja();
            tbKolicina.DataContext = stavka;
            tbKolicina.Text = "";

        }
        public void DodajNovuStavkuUsluge(object sender, RoutedEventArgs e)
        {
            stavkaUsluga.Usluga = (Usluga)cbUsluge.SelectedItem;
            if (stavkaUsluga.Usluga == null)
            {
                MessageBox.Show("Niste odabrali uslugu");
                return;
            }
            stavkaUsluga.Id_Usluga = stavkaUsluga.Usluga.Id;
            racun.listaStavkiUsluga.Add(stavkaUsluga);
            lista.Add(stavkaUsluga);
            lbStavke.Items.Refresh();
            stavkaUsluga = new StavkaProdajeUsluge();
            cbUsluge.Text = "";
        }
        public void ZavrsiRacun(object sender, RoutedEventArgs e)
        {
            if (kupac == null)
            {
                MessageBox.Show("Morate uneti sve podatke o kupcu");
                return;
            }
            if (kupac.Ime=="" || kupac.Prezime=="" || kupac.Telefon==""||
                kupac.Ime==null || kupac.Prezime==null || kupac.Telefon==null)
            {
                MessageBox.Show("Morate uneti sve podatke o kupcu");
                return;
            }
            if (racun.listaStavkiNamestaja.Count != 0||racun.listaStavkiUsluga.Count !=0)
            {
                Kupac.DodajKupca(kupac);
                int idkupacmax = Kupac.UzmiMaxId();
                racun.Id_Kupac = idkupacmax;
                kupac.Id = idkupacmax;
                racun.Kupac = kupac;
                racun.Id_Zaposleni = Projekat.Instance.UlogovaniKorisnik.Id;
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
        private bool NamestajFilter(object obj)
        {
            return !((Namestaj)obj).Obrisan;
        }

        private void Ukloni(object sender, RoutedEventArgs e)
        {
            var selektovanaStavka = lbStavke.SelectedItem;
            if (selektovanaStavka == null)
            {
                MessageBox.Show("Niste odabrali stavku");
                return;
            }
            if (selektovanaStavka.GetType() == typeof(StavkaProdajeNamestaja))
            {
                foreach(StavkaProdajeNamestaja s in racun.listaStavkiNamestaja)
                {
                    if (selektovanaStavka == s)
                    {
                        namestaj = s.Namestaj;
                        foreach (Namestaj n in Projekat.Instance.Namestaj)
                        {
                            if (namestaj.Id == n.Id)
                            {
                                n.Kolicina = n.Kolicina + s.Kolicina;
                            }
                        }
                        racun.listaStavkiNamestaja.Remove(s);
                        lista.Remove(s);
                        lbStavke.Items.Refresh();
                        
                        return;
                    }
                }
            }
            if(selektovanaStavka is StavkaProdajeUsluge)
            {
                foreach(StavkaProdajeUsluge u in racun.listaStavkiUsluga)
                {
                    if (selektovanaStavka == u)
                    {
                        racun.listaStavkiUsluga.Remove(u);
                        lista.Remove(u);
                        lbStavke.Items.Refresh();
                        return;
                    }
                }
            }
        }
        private void dgPrikazNamestaja_AutoGeneratingColumn(object sender,
           DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id" || (string)e.Column.Header == "IdTip")
            {
                e.Cancel = true;
            }
        }
        private void tbFilter_TextChanged(Object sender, TextChangedEventArgs e)
        {
            string filterText = tbfilter.Text;
            ObservableCollection<Namestaj> lista = new ObservableCollection<Namestaj>();
            foreach (Namestaj n in Projekat.Instance.Namestaj)
            {
                if (n.Naziv.ToUpper().StartsWith(tbfilter.Text.ToUpper()))
                {
                    lista.Add(n);
                }
            }
            view = CollectionViewSource.GetDefaultView(lista);
            dgNamestaj.ItemsSource = view;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
    }
}
