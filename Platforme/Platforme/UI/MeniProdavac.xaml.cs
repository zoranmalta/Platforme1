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
    /// Interaction logic for MeniProdavac.xaml
    /// </summary>
    public partial class MeniProdavac : Window
    {
        public MeniProdavac()
        {
            InitializeComponent();
        }

        public void Prodaja(object Sender,RoutedEventArgs e)
        {
            var pw = new ProdajaWindow();
            pw.ShowDialog();
        }
        public void PrikazRacuna(object Sender, RoutedEventArgs e)
        {
            var pr = new PregledRacuna();
            pr.ShowDialog();
        }
        public void Izlaz(object Sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
