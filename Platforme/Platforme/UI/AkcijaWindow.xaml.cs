using Platforme.model;
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
    /// Interaction logic for AkcijaWindow.xaml
    /// </summary>
    public partial class AkcijaWindow : Window
    {
        private ICollectionView view;

        public AkcijaWindow()
        {
            InitializeComponent();
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Akcija);
            view.Filter = AkcijaFilter;
            dgAkcija.ItemsSource = view;
            dgAkcija.IsSynchronizedWithCurrentItem = true;
            dgAkcija.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
        private bool AkcijaFilter(object obj)
        {
            return !((Akcija)obj).Obrisan;
        }

        private void Dodaj(object sender, RoutedEventArgs e)
        {
            Akcija akcija = new Akcija();
            var ad = new AkcijaDodavanje(akcija);
            ad.ShowDialog();
        }

        private void Izmeni(object sender, RoutedEventArgs e)
        {
            Akcija selektovanaAkcija = view.CurrentItem as Akcija; 

            if (selektovanaAkcija != null)
            {
                //napravimo kopiju trenutnih vrednosti u objektu,  da bi ih mogli preuzeti ako korisnik ponisti napravljenje izmene
                Akcija old = (Akcija)selektovanaAkcija.Clone();
                AkcijaDodavanje nw = new AkcijaDodavanje(selektovanaAkcija);
                if (nw.ShowDialog() != true) //ako je kliknuo cancel, ponistavaju se izmene nad objektom
                {
                    //pronadjemo indeks selektovanog akcija
                    int index = Projekat.Instance.Akcija.IndexOf(selektovanaAkcija);
                    //vratimo vrednosti njegovih atributa na stare vrednosti, jer je izmena ponistena
                    Projekat.Instance.Akcija[index] = old;
                }
            }
        }
        private void Obrisi(object sender, RoutedEventArgs e)
        {
            Akcija selektovanaAkcija = view.CurrentItem as Akcija;

            if (MessageBox.Show($"Da li sigurno zelite da obrisete izabranu akciju?", "Potvrda",
                                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Projekat.Instance.Akcija.Remove(selektovanaAkcija);
                Akcija.ObrisiAkciju(selektovanaAkcija);
            }

        }
        public void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PrikazAktuelnihAkcija(object sender, RoutedEventArgs e)
        {
            DateTime danas = DateTime.Today;
            ObservableCollection<Akcija> lista = new ObservableCollection<Akcija>();
            foreach(Akcija a in view)
            {
                if (danas <= a.DatumZavrsetka && danas >= a.DatumPocetka)
                {
                    lista.Add(a);
                }
            }
            dgAkcija.ItemsSource = lista;
            dgAkcija.IsSynchronizedWithCurrentItem = true;
            dgAkcija.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

        }
        private void PrikazSvihAkcija(object sender, RoutedEventArgs e)
        {
            dgAkcija.ItemsSource = view;
            dgAkcija.IsSynchronizedWithCurrentItem = true;
            dgAkcija.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void dgAkcija_AutoGeneratingColumn(object sender,
           DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id" || (string)e.Column.Header == "IdNamestaj")
            {
                e.Cancel = true;
            }
        }
    }
    
}
