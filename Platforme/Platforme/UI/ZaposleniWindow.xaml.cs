using Platforme.model;
using System;
using System.Collections.Generic;
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

        private void dgZaposleni_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id" )
            {
                e.Cancel = true;
            }
        }
    }
}
