using Platforme.UI;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Platforme
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            cbTipKorisnika.Items.Add("Administrator");
            cbTipKorisnika.Items.Add("Prodavac");
            cbTipKorisnika.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Potvrda(object sender, RoutedEventArgs e)
        {
            if (cbTipKorisnika.Text == "Administrator")
            {
                var meniAdministratora = new MeniAdministratora();
                this.Close();
                meniAdministratora.ShowDialog();
            }
            if (cbTipKorisnika.Text == "Prodavac")
            {
                var meniProdavac = new MeniProdavac();
                this.Close();
                meniProdavac.ShowDialog();
            }
            if (cbTipKorisnika.Text == "")
            {
                if(MessageBox.Show("Ponovo se ulogujte?","Niste se ulogovali", MessageBoxButton.YesNo) == MessageBoxResult.Yes) { }
                else { this.Close(); }
            }
        }
    }
}
