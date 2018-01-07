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
    /// Interaction logic for ZaposleniWindow.xaml
    /// </summary>
    public partial class ZaposleniWindow : Window
    {
        private ICollectionView view;
        public ZaposleniWindow()
        {
            InitializeComponent();
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Zaposleni);
            view.Filter = ZaposleniFilter;
            dgZaposleni.ItemsSource = view;
            dgZaposleni.IsSynchronizedWithCurrentItem = true;
            dgZaposleni.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            cbPretragaZaposlenih.Items.Add("Ime");
            cbPretragaZaposlenih.Items.Add("Prezime");
            cbPretragaZaposlenih.Items.Add("Korisnicko Ime");
            cbSortiranjeZaposlenih.Items.Add("Ime");
            cbSortiranjeZaposlenih.Items.Add("Prezime");
            cbSortiranjeZaposlenih.Items.Add("Korisnicko Ime");
        }
        private bool ZaposleniFilter(object obj)
        {
            return !((Zaposleni)obj).Obrisan;
        }
        private void Dodaj(object sender, RoutedEventArgs e)
        {
            Zaposleni zaposleni = new Zaposleni();
            ZaposleniIzmene zi = new ZaposleniIzmene(zaposleni);
            zi.ShowDialog();
        }
        private void Izmeni(object sender, RoutedEventArgs e)
        {
            Zaposleni selektovanizaposleni = view.CurrentItem as Zaposleni;
            if (selektovanizaposleni != null)//ako je neki zaposleni selektovan
            {
                //napravimo kopiju trenutnih vrednosti u objektu,  da bi ih mogli preuzeti ako korisnik ponisti napravljenje izmene
                Zaposleni old = (Zaposleni)selektovanizaposleni.Clone();
                ZaposleniIzmene nw = new ZaposleniIzmene(selektovanizaposleni);
                if (nw.ShowDialog() != true) //ako je kliknuo cancel, ponistavaju se izmene nad objektom
                {
                    //pronadjemo indeks selektovanog zaposlenog
                    int index = Projekat.Instance.Zaposleni.IndexOf(selektovanizaposleni);
                    //vratimo vrednosti njegovih atributa na stare vrednosti, jer je izmena ponistena
                    Projekat.Instance.Zaposleni[index] = old;
                }
            }
        }
        private void Obrisi(object sender, RoutedEventArgs e)
        {
            Zaposleni selektovaniZ = view.CurrentItem as Zaposleni;
            if (selektovaniZ == null)
            {
                MessageBox.Show("Morate izabrati zaposlenog");
                return;
            }

            if (MessageBox.Show($"Da li sigurno zelite da obrisete zaposlenog: {selektovaniZ.Ime}", "Potvrda",
                                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                selektovaniZ.Obrisan = true;
                Projekat.Instance.Zaposleni.Remove(selektovaniZ);
                Zaposleni.ObrisiZaposlenog(selektovaniZ);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void tbFilter_TextChanged(Object sender, TextChangedEventArgs e)
        {
            string filterText = tbfilterZaposlenih.Text;
            string action = cbPretragaZaposlenih.Text;
            switch (action)
            {
                case "Ime":

                    ObservableCollection<Zaposleni> lista1 = new ObservableCollection<Zaposleni>();
                    foreach (Zaposleni n in view)
                    {
                        if (n.Ime.ToUpper().StartsWith(tbfilterZaposlenih.Text.ToUpper()))
                        {
                            lista1.Add(n);
                        }
                    }
                    dgZaposleni.ItemsSource = lista1;
                    dgZaposleni.IsSynchronizedWithCurrentItem = true;
                    dgZaposleni.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
                    break;
                case "Prezime":

                    ObservableCollection<Zaposleni> lista = new ObservableCollection<Zaposleni>();
                    foreach (Zaposleni n in view)
                    {
                        if (n.Prezime.ToUpper().StartsWith(tbfilterZaposlenih.Text.ToUpper()))
                        {
                            lista.Add(n);
                        }
                    }
                    dgZaposleni.ItemsSource = lista;
                    dgZaposleni.IsSynchronizedWithCurrentItem = true;
                    dgZaposleni.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
                    break;
                case "Korisnicko Ime":
                    ObservableCollection<Zaposleni> lista2 = new ObservableCollection<Zaposleni>();
                    foreach (Zaposleni n in view)
                    {
                        if (n.KorisnickoIme.ToUpper().StartsWith(tbfilterZaposlenih.Text.ToUpper()))
                        {
                            lista2.Add(n);
                        }
                    }
                    dgZaposleni.ItemsSource = lista2;
                    dgZaposleni.IsSynchronizedWithCurrentItem = true;
                    dgZaposleni.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
                    break;
            }
        }
        private void cbSortiranje_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            dgZaposleni.ItemsSource = view;
            //ako koristimo string vrednosti za items iz sendera
            string action = (sender as ComboBox).SelectedItem as string;
            //string action = cbSortiranje.Text;
            switch (action)
            {
                case "Ime":
                    view.SortDescriptions.Clear();
                    view.SortDescriptions.Add(new SortDescription("Ime", ListSortDirection.Ascending));
                    view.Refresh();
                    break;
                case "Prezime":
                    view.SortDescriptions.Clear();
                    view.SortDescriptions.Add(new SortDescription("Prezime", ListSortDirection.Descending));
                    view.Refresh();
                    break;
                case "Korisnicko Ime":
                    view.SortDescriptions.Clear();
                    view.SortDescriptions.Add(new SortDescription("KorisnickoIme", ListSortDirection.Ascending));
                    view.Refresh();
                    break;
                default:
                    break;
            }
        }

        private void dgZaposleni_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id" )
            {
                e.Cancel = true;
            }
        }
    }
}
