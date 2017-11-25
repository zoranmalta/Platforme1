using Platforme.model;
using Platforme.util;
using System;
using System.Collections.Generic;
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
        public Window1()
        {
            InitializeComponent();
            dgNamestaj.ItemsSource = Projekat.Instance.Namestaj;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
           
        }
       
        private void DodajNamestaj(object sender, RoutedEventArgs e)
        {
            var noviNamestaj = new Namestaj();
            var nw = new NamestajWindow(noviNamestaj,NamestajWindow.Operacija.DODAVANJE);
            nw.ShowDialog();
        }
        private void IzmeniNamestaj(object sender, RoutedEventArgs e)
        {
            var selectedNamestaj = (Namestaj)dgNamestaj.SelectedItem;
            var nw = new NamestajWindow(selectedNamestaj, NamestajWindow.Operacija.IZMENA);
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
                    }
                }
            }
            GenericsSerializer.Serialize("namestaj.xml",Projekat.Instance.Namestaj);
        }
        private void Pretraga_po_tipu(object sender, RoutedEventArgs e)
        {
            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
