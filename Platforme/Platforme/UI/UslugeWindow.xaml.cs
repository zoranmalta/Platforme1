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
            dgUsluge.ItemsSource = view;
            dgUsluge.IsSynchronizedWithCurrentItem = true;

            dgUsluge.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
        private bool UslugaFilter(object obj)
        {
            return !((Namestaj)obj).Obrisan;
        }
        private void DodajUslugu(object sender, RoutedEventArgs e)
        {

        }
        private void IzmeniUslugu(object sender, RoutedEventArgs e)
        {

        }
        private void ObrisiUslugu(object sender, RoutedEventArgs e)
        {

        }
        private void Izlaz(object sender, RoutedEventArgs e)
        {

        }
    }
}
