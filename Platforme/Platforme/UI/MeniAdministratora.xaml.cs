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
    /// Interaction logic for MeniAdministratora.xaml
    /// </summary>
    public partial class MeniAdministratora : Window
    {
        public MeniAdministratora()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Namestaj(object sender, RoutedEventArgs e)
        {
            var w1 = new Window1();
            w1.ShowDialog();
        }
        private void Korisnici(object sender, RoutedEventArgs e)
        {

        }
        private void Akcije(object sender, RoutedEventArgs e)
        {
            var ak = new AkcijaWindow();
            ak.ShowDialog();
        }
        private void Nazad(object sender, RoutedEventArgs e)
        {
            var mw = new MainWindow();
            this.Close();
            mw.ShowDialog();
        }
    }
}
