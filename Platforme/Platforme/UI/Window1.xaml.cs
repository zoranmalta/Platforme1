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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private ICollectionView view;

        public Window1()
        {
            InitializeComponent();
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Namestaj);
            view.Filter = NamestajFilter;
            dgNamestaj.ItemsSource = view;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            cbPretraga.Items.Add("Tip Namestaja");
            cbPretraga.Items.Add("Naziv");
            cbPretraga.Items.Add("Sifra");
            cbSortiranje.Items.Add("Naziv");
            cbSortiranje.Items.Add("Sifra");

           
        }
        private bool NamestajFilter(object obj)
        {
            return !((Namestaj)obj).Obrisan;
        }

       
        private void DodajNamestaj(object sender, RoutedEventArgs e)
        {
            var noviNamestaj = new Namestaj();
            var nw = new NamestajWindow(noviNamestaj);
            //this.Close();
            nw.ShowDialog();
        }
        private void IzmeniNamestaj(object sender, RoutedEventArgs e)
        {
            Namestaj selektovaniNamestaj = view.CurrentItem as Namestaj; //preuzimanje selektovanog fakulteta

            if (selektovaniNamestaj != null)//ako je neki namestaj selektovan
            {
                //napravimo kopiju trenutnih vrednosti u objektu,  da bi ih mogli preuzeti ako korisnik ponisti napravljenje izmene
                Namestaj old = (Namestaj)selektovaniNamestaj.Clone();
                NamestajWindow nw = new NamestajWindow(selektovaniNamestaj);
                if (nw.ShowDialog() != true) //ako je kliknuo cancel, ponistavaju se izmene nad objektom
                {
                    //pronadjemo indeks selektovanog namestaja
                    int index = Projekat.Instance.Namestaj.IndexOf(selektovaniNamestaj);
                    //vratimo vrednosti njegovih atributa na stare vrednosti, jer je izmena ponistena
                    Projekat.Instance.Namestaj[index] = old;
                }
            }
            //var selectedNamestaj = (Namestaj)dgNamestaj.SelectedItem;
            //this.Close();
            //nw.ShowDialog();
        }
        private void ObrisiNamestaj(object sender, RoutedEventArgs e)
        {
            Namestaj selektovaniNamestaj = view.CurrentItem as Namestaj;
            if (selektovaniNamestaj == null)
            {
                MessageBox.Show("Morate izabrati namestaj");
                return;
            }
           
            if(MessageBox.Show($"Da li sigurno zelite da obrisete namestaj: {selektovaniNamestaj.Naziv}","Potvrda",
                                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Projekat.Instance.Namestaj.Remove(selektovaniNamestaj);
                Namestaj.ObrisiNamestaj(selektovaniNamestaj);
            }
          
        }
        private void Pretraga_po_tipu(object sender, RoutedEventArgs e)
        {
            Namestaj selektovaniNamestaj = view.CurrentItem as Namestaj;
            if (selektovaniNamestaj != null)
            {
                ObservableCollection<Namestaj> lista = new ObservableCollection<Namestaj>();
                foreach (Namestaj n in Projekat.Instance.Namestaj)
                {
                    if (n.IdTip == selektovaniNamestaj.IdTip)
                    {
                        lista.Add(n);
                    }
                }
                PrikazSelektovanihNamestaja psn = new PrikazSelektovanihNamestaja(lista);
                psn.ShowDialog();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Tip_Namestaja(object sender, RoutedEventArgs e)
        {
            TipNamestajaWindow tnw = new TipNamestajaWindow();
            //this.Close();
            tnw.ShowDialog();
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
            string filterText = tbFilter.Text;
            string action = cbPretraga.Text;
            switch (action)
            {
                case "Tip Namestaja":

                    ObservableCollection<Namestaj> lista1 = new ObservableCollection<Namestaj>();
                    foreach (Namestaj n in view)
                    {
                        if (n.TipNamestaja.Naziv.ToUpper().Contains(tbFilter.Text.ToUpper()))
                        {
                            lista1.Add(n);
                        }
                    }
                    dgNamestaj.ItemsSource = lista1;
                    dgNamestaj.IsSynchronizedWithCurrentItem = true;
                    dgNamestaj.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
                    break;
                case "Naziv":

                    ObservableCollection<Namestaj> lista = new ObservableCollection<Namestaj>();
                    foreach (Namestaj n in view)
                    {
                        if (n.Naziv.ToUpper().StartsWith(tbFilter.Text.ToUpper()))
                        {
                            lista.Add(n);
                        }
                    }
                    dgNamestaj.ItemsSource = lista;
                    dgNamestaj.IsSynchronizedWithCurrentItem = true;
                    dgNamestaj.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
                    break;
                case "Sifra":
                    ObservableCollection<Namestaj> lista2 = new ObservableCollection<Namestaj>();
                    foreach (Namestaj n in view)
                    {
                        if (n.Sifra.ToUpper().StartsWith(tbFilter.Text.ToUpper()))
                        {
                            lista2.Add(n);
                        }
                    }
                    dgNamestaj.ItemsSource = lista2;
                    dgNamestaj.IsSynchronizedWithCurrentItem = true;
                    dgNamestaj.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
                    break;
            }
        }

        private void cbSortiranje_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            dgNamestaj.ItemsSource = view;
            //ako koristimo string vrednosti za items iz sendera
            string action = (sender as ComboBox).SelectedItem as string;
            //string action = cbSortiranje.Text;
            switch (action)
            {
                case "Naziv":
                    view.SortDescriptions.Clear();
                    view.SortDescriptions.Add(new SortDescription("Naziv", ListSortDirection.Ascending));
                    view.Refresh();
                    break;
                case "Sifra":
                    view.SortDescriptions.Clear();
                    view.SortDescriptions.Add(new SortDescription("Sifra", ListSortDirection.Ascending));
                    view.Refresh();
                    break;

                default:
                    break;
            }
        }

            //if (cbSortiranje.Text == "Naziv")
            //{
            //    view.SortDescriptions.Clear();
            //    view.SortDescriptions.Add(new SortDescription("Naziv", ListSortDirection.Descending));
            //}
            //if (cbSortiranje.Text == "Tip Namestaja")
            //{
            //    view.SortDescriptions.Clear();
            //    view.SortDescriptions.Add(new SortDescription("Tip Namestaja", ListSortDirection.Descending));
            //}
            //if (cbSortiranje.Text == "Sifra")
            //{
            //    view.SortDescriptions.Clear();
            //    view.SortDescriptions.Add(new SortDescription("Sifra", ListSortDirection.Descending));
            //}
            //else {

            //        view.SortDescriptions.Clear();
            //        view.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Descending));

            //}

            //private void dgNamestaj_Sorting(object sender, DataGridSortingEventArgs e)
            //{
            //    SortDescription sd;
            //    string action = cbSortiranje.SelectedItem as string;

            //    switch (action)
            //    {
            //        case "Naziv":
            //            sd = new SortDescription("Naziv", ListSortDirection.Ascending);
            //            dgNamestaj.Sorting()
            //            break;
            //        case "Sifra":
            //            sd = new SortDescription("Sifra", ListSortDirection.Ascending);
            //            break;

            //        default:
            //            break;
            //    }

            //}
        }
}
