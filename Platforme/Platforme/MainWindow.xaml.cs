using Platforme.model;
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
            TipNamestaja.UcitajTipNamestaja();
            Namestaj.UcitajNamestaj();
            Usluga.UcitajUsluge();
            Akcija.UcitajAkcije();
            Zaposleni.UcitajZaposlene();
            Kupac.UcitajKupce();
            
            Racun.UcitajRacune();

           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Potvrda(object sender, RoutedEventArgs e)
        {
            if (tbKorisnickoIme.Text.Equals("") || tbPassword.Password.ToString().Equals(""))
            {
                MessageBox.Show("Niste uneli sve podatke");
                return;
            }
            foreach (Zaposleni z in Projekat.Instance.Zaposleni)
            {
                if(z.KorisnickoIme.ToUpper().Equals(tbKorisnickoIme.Text.ToUpper())
                    && z.Lozinka.Equals(tbPassword.Password.ToString())&& z.Tip.Equals("Administrator"))
                {
                    tbKorisnickoIme.Text = "";
                    tbPassword.Password = "";
                    Projekat.Instance.UlogovaniKorisnik = z;
                    var meniAdministratora = new MeniAdministratora();
                    meniAdministratora.ShowDialog();
                    return;
                }
                if (z.KorisnickoIme.ToUpper().Equals(tbKorisnickoIme.Text.ToUpper())
                    && z.Lozinka.Equals(tbPassword.Password.ToString()) && z.Tip.Equals("Prodavac"))
                {
                    tbKorisnickoIme.Text = "";
                    tbPassword.Password = "";
                    Projekat.Instance.UlogovaniKorisnik = z;
                    var meniProdavac = new MeniProdavac();
                    meniProdavac.ShowDialog();
                    return;
                }
            }
            MessageBox.Show("Ne postoji takav zaposleni");
            tbKorisnickoIme.Text = "";
            tbPassword.Password = "";
        }

        private void tbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
