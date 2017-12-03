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
           
        }
        private bool NamestajFilter(object obj)
        {
            return !((Namestaj)obj).Obrisan;
        }

       
        private void DodajNamestaj(object sender, RoutedEventArgs e)
        {
            var noviNamestaj = new Namestaj();
            var nw = new NamestajWindow(noviNamestaj,NamestajWindow.Operacija.DODAVANJE);
            this.Close();
            nw.ShowDialog();
        }
        private void IzmeniNamestaj(object sender, RoutedEventArgs e)
        {
            var selectedNamestaj = (Namestaj)dgNamestaj.SelectedItem;
            var nw = new NamestajWindow(selectedNamestaj, NamestajWindow.Operacija.IZMENA);
            this.Close();
            nw.ShowDialog();
        }
        private void ObrisiNamestaj(object sender, RoutedEventArgs e)
        {
            var selectedNamestaj = (Namestaj)dgNamestaj.SelectedItem;
            if(MessageBox.Show($"Da li sigurno zelite da obrisete namestaj: {selectedNamestaj.Naziv}","Potvrda",
                                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach(var n in Projekat.Instance.Namestaj)
                {
                    if (n.Id == selectedNamestaj.Id)
                    {
                        n.Obrisan = true;
                        view.Refresh();
                        break;
                    }
                }
            }
            GenericsSerializer.Serialize("namestaj.xml",Projekat.Instance.Namestaj);
        }
        private void Pretraga_po_tipu(object sender, RoutedEventArgs e)
        {
            Namestaj selektovaniNamestaj = view.CurrentItem as Namestaj;
            if (selektovaniNamestaj != null)
            {
                ObservableCollection<Namestaj> lista = new ObservableCollection<Namestaj>();
                foreach(Namestaj n in Projekat.Instance.Namestaj)
                {
                    if (n.IdTip == selektovaniNamestaj.IdTip)
                    {
                        lista.Add(n);
                    }
                }
            PrikazSelektovanihNamestaja psn = new PrikazSelektovanihNamestaja( lista);
                psn.ShowDialog();
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
