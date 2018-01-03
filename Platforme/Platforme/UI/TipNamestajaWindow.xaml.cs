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
    /// Interaction logic for TipNamestajaWindow.xaml
    /// </summary>
    public partial class TipNamestajaWindow : Window
    {
        private ICollectionView view;

        public TipNamestajaWindow()
        {
            InitializeComponent();
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.TipNamestaja);
            view.Filter = NamestajFilter;
            dgTipNamestaja.ItemsSource = view;
            dgTipNamestaja.IsSynchronizedWithCurrentItem = true;
            dgTipNamestaja.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
        private bool NamestajFilter(object obj)
        {
            return !((TipNamestaja)obj).Obrisan;
        }

        private void Dodaj(object sender, RoutedEventArgs e)
        {
            TipNamestaja tipNamestaja = new TipNamestaja();
            TipNamestajaIzmene tni = new TipNamestajaIzmene(tipNamestaja);
            //this.Close();
            tni.ShowDialog();
        }
    
        private void Izmeni(object sender, RoutedEventArgs e)
        {
            TipNamestaja selektovaniTipNamestaja = view.CurrentItem as TipNamestaja; //preuzimanje selektovanog tipa

            if (selektovaniTipNamestaja != null)//ako je neki tip namestaja selektovan
            {
                //napravimo kopiju trenutnih vrednosti u objektu,  da bi ih mogli preuzeti ako korisnik ponisti napravljenje izmene
                TipNamestaja old = (TipNamestaja)selektovaniTipNamestaja.Clone();
                TipNamestajaIzmene nw = new TipNamestajaIzmene(selektovaniTipNamestaja);
                if (nw.ShowDialog() != true) //ako je kliknuo cancel, ponistavaju se izmene nad objektom
                {
                    //pronadjemo indeks selektovanog tipa namestaja
                    int index = Projekat.Instance.TipNamestaja.IndexOf(selektovaniTipNamestaja);
                    //vratimo vrednosti njegovih atributa na stare vrednosti, jer je izmena ponistena
                    Projekat.Instance.TipNamestaja[index] = old;
                }
            }
            //var selectedTipNamestaja = (TipNamestaja)dgTipNamestaja.SelectedItem;
            //var tni = new TipNamestajaIzmene(selectedTipNamestaja);
            ////this.Close();
            //tni.ShowDialog();
        }
        private void Ukloni(object sender, RoutedEventArgs e)
        {
            TipNamestaja selektovaniTip = view.CurrentItem as TipNamestaja;

            if (MessageBox.Show($"Da li sigurno zelite da obrisete tip namestaja: {selektovaniTip.Naziv}", "Potvrda",
                                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach(Namestaj n in Projekat.Instance.Namestaj)
                {
                    if (n.IdTip == selektovaniTip.Id)
                    {
                        Namestaj.ObrisiNamestaj(n);
                    }
                }
                Projekat.Instance.Namestaj.Clear();
                Namestaj.UcitajNamestaj();
                TipNamestaja.ObrisiTipNamestaja(selektovaniTip);
                Projekat.Instance.TipNamestaja.Clear();
                TipNamestaja.UcitajTipNamestaja();
                view.Refresh();
            }
        }
       
        private void Izlaz(object sender, RoutedEventArgs e)
        {
            //Window1 w1 = new Window1();
            this.Close();
            //w1.ShowDialog();
        }
        private void dgTipNamestaja_AutoGeneratingColumn(object sender,
            DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan")
            {
                e.Cancel = true;
            }
        }
    }
}
