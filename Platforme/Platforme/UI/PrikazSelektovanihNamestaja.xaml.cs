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
    /// Interaction logic for PrikazSelektovanihNamestaja.xaml
    /// </summary>
    public partial class PrikazSelektovanihNamestaja : Window
    {
        private ICollectionView view;

        public PrikazSelektovanihNamestaja(ObservableCollection<Namestaj> lista)
        {
            InitializeComponent();
            view = CollectionViewSource.GetDefaultView(lista);
            view.Filter = NamestajFilter;
            dgSelektovaniNamestaj.ItemsSource = view;
            dgSelektovaniNamestaj.IsSynchronizedWithCurrentItem = true;

            dgSelektovaniNamestaj.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
        private bool NamestajFilter(object obj)
        {
            return !((Namestaj)obj).Obrisan;
        }
        private void dgSelektovaniNamestaj_AutoGeneratingColumn(object sender,
            DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id"|| (string)e.Column.Header == "IdTip")
            {
                e.Cancel = true;
            }
        }
    }
}
