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
    /// Interaction logic for ZaposleniIzmene.xaml
    /// </summary>
    public partial class ZaposleniIzmene : Window
    {
        private Zaposleni zaposleni;
        public ZaposleniIzmene(Zaposleni zaposleni)
        {
            this.zaposleni = zaposleni;
            InitializeComponent();
            tbIme.DataContext = zaposleni;
            tbPrezime.DataContext = zaposleni;
            tbKorisnickoIme.DataContext = zaposleni;
            tbLozinka.DataContext = zaposleni;
            cbTip.DataContext = zaposleni;
            cbTip.Items.Add("Administrator");
            cbTip.Items.Add("Prodavac");
        }
        private void Sacuvaj(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            if (zaposleni.Tip == null)
            {
                MessageBox.Show("Morate izabrati tip zaposlenog");
                return;
            }
            if (zaposleni.Ime == null || zaposleni.Prezime == null)
            {
                MessageBox.Show("Niste uneli sve podatke");
                return;
            }
            if (zaposleni.KorisnickoIme == null)
            {
                MessageBox.Show("Morate uneti korisnicko ime");
                return;
            }
            if (zaposleni.Lozinka == null)
            {
                MessageBox.Show("Morate uneti lozinku");
                return;
            }
            foreach(Zaposleni z in Projekat.Instance.Zaposleni)
            {
                if (z.KorisnickoIme.Equals(zaposleni.KorisnickoIme))
                {
                    MessageBox.Show("Korisnicko ime mora biti jedinstveno");
                    return;
                }
            }
            if (zaposleni.Id != 0) //ako postoji id, zaposleni je vec u bazi, sto znaci da se radi izmena zaposlenog
            {

                Zaposleni.IzmeniZaposlenog(zaposleni);
            }
            else
            {
                Zaposleni.DodajZaposlenog(zaposleni);
                Projekat.Instance.Zaposleni.Clear();
                Zaposleni.UcitajZaposlene();
            }
            this.Close();
        }
        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
