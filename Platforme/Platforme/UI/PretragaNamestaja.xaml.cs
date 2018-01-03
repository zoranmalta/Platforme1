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
    /// Interaction logic for PretragaNamestaja.xaml
    /// </summary>
    public partial class PretragaNamestaja : Window
    {
        private ICollectionView view;

        public PretragaNamestaja()
        {
            InitializeComponent();
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Namestaj);
            view.Filter = PretragaFilter;
            dgPretragaNamestaja.ItemsSource = view;
            dgPretragaNamestaja.IsSynchronizedWithCurrentItem = true;

            dgPretragaNamestaja.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private bool PretragaFilter(object obj)
        {
            return !((Namestaj)obj).Obrisan;
        }
        private void PretragaNmestajaPoImenu( object sender, RoutedEventArgs e)
        {
            ObservableCollection<Namestaj> lista = new ObservableCollection<Namestaj>();
            foreach(Namestaj n in view)
            {
                if (n.Naziv.ToUpper().StartsWith(tbPretraga.Text.ToUpper()))
                {
                    lista.Add(n);
                }
            }
            dgPretragaNamestaja.ItemsSource = lista;
            dgPretragaNamestaja.IsSynchronizedWithCurrentItem = true;
            dgPretragaNamestaja.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

        }
        private void dgPrikazNamestaja_AutoGeneratingColumn(object sender,
           DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id" || (string)e.Column.Header == "IdTip")
            {
                e.Cancel = true;
            }
        }
    }
}
