using Platforme.model;
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
            OsveziPrikaz();
        }
        private void OsveziPrikaz()
        {
            lbNamestaj.Items.Clear();
            foreach(Namestaj n in Projekat.Instance.Namestaj)
            {
                if (n.Obrisan == false)
                {
                    lbNamestaj.Items.Add(n);
                }
            }
            lbNamestaj.SelectedIndex = 0;
        }
        private void DodajNamestaj(object sender, RoutedEventArgs e)
        {
            var nw = new NamestajWindow();
            nw.ShowDialog();
        }
        private void IzmeniNamestaj(object sender, RoutedEventArgs e)
        {

        }
        private void ObrisiNamestaj(object sender, RoutedEventArgs e)
        {

        }
        private void Pretraga_po_tipu(object sender, RoutedEventArgs e)
        {
            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
