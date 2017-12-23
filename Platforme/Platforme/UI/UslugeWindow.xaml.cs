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
    /// Interaction logic for UslugeWindow.xaml
    /// </summary>
    public partial class UslugeWindow : Window
    {
        private ICollectionView view;
        public UslugeWindow()
        {
            InitializeComponent();
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Usluga);
            view.Filter = UslugaFilter;
            dgUsluga.ItemsSource = view;
            dgUsluga.IsSynchronizedWithCurrentItem = true;

            dgUsluga.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

        }
        private bool UslugaFilter(object obj)
        {
            return !((Usluga)obj).Obrisan;
        }
        private void DodajUslugu(object sender, RoutedEventArgs e)
        {
            Usluga usluga = new Usluga();
            UslugeIzmene nw = new UslugeIzmene(usluga);
            //this.Close();
            nw.ShowDialog();
        }
        private void IzmeniUslugu(object sender, RoutedEventArgs e)
        {
            Usluga selektovanaUsluga = view.CurrentItem as Usluga; //preuzimanje selektovane usluge

            if (selektovanaUsluga != null)//ako je neki namestaj selektovan
            {
                //napravimo kopiju trenutnih vrednosti u objektu,  da bi ih mogli preuzeti ako korisnik ponisti napravljenje izmene
                Usluga old = (Usluga)selektovanaUsluga.Clone();
                UslugeIzmene nw = new UslugeIzmene(selektovanaUsluga);
                if (nw.ShowDialog() != true) //ako je kliknuo cancel, ponistavaju se izmene nad objektom
                {
                    //pronadjemo indeks selektovanog namestaja
                    int index = Projekat.Instance.Usluga.IndexOf(selektovanaUsluga);
                    //vratimo vrednosti njegovih atributa na stare vrednosti, jer je izmena ponistena
                    Projekat.Instance.Usluga[index] = old;
                }
            }
        }
        private void ObrisiUslugu(object sender, RoutedEventArgs e)
        {
            Usluga selektovanaUsluga = view.CurrentItem as Usluga;

            if (selektovanaUsluga != null)
            {
                if (MessageBox.Show($"Da li sigurno zelite da obrisete uslugu: {selektovanaUsluga.Naziv}", "Potvrda",
                                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Projekat.Instance.Usluga.Remove(selektovanaUsluga);
                    Usluga.ObrisiUslugu(selektovanaUsluga);
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void dgPrikazUsluga_AutoGeneratingColumn(object sender,
            DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id" )
            {
                e.Cancel = true;
            }
        }
    }
}
