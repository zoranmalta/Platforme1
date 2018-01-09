using Platforme.model;
using Platforme.util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for NamestajWindow.xaml
    /// </summary>
    public partial class NamestajWindow : Window
    {
        private Namestaj namestaj;
    
        public NamestajWindow(Namestaj namestaj)
        {
            InitializeComponent();
            InicijalizujVrednosti(namestaj);
        }

        public void InicijalizujVrednosti(Namestaj namestaj)
        {
            this.namestaj = namestaj;
           
            tbNaziv.DataContext = namestaj;
            tbCena.DataContext = namestaj;
            tbSifra.DataContext = namestaj;
            tbKolicina.DataContext = namestaj;

            cbTipNamestaja.ItemsSource = Projekat.Instance.TipNamestaja;
            cbTipNamestaja.DataContext = namestaj;
        }
       
        private void Sacuvaj_Namestaj(object sender, RoutedEventArgs e) 
        {
            this.DialogResult = true;
            if (namestaj.TipNamestaja == null)
            {
                MessageBox.Show("Morate izabrati tip namestaja");
                return;
            }
            if (namestaj.Naziv==null || namestaj.Sifra==null)
            {
                MessageBox.Show("Niste uneli sve podatke");
                return;
            }
            try
            {
                if (((string)tbCena.Text).Length > 0)
                    namestaj.Cena = Double.Parse((String)tbCena.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cena mora biti broj veci od nule");
                return;
            }
            if (namestaj.Cena <= 0)
            {
                MessageBox.Show("Cena mora biti veca od 0");
                return;
            }
            try
            {
                if (((string)tbKolicina.Text).Length > 0)
                    namestaj.Kolicina = Int32.Parse((String)tbKolicina.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kolicina mora biti ceo broj veci od nule");
                return;
            }
            if (namestaj.Kolicina <= 0)
            {
                MessageBox.Show("Kolicina mora biti veca od 0");
                return;
            }
            if (namestaj.Id != 0) //ako postoji id, namestaj je vec u bazi, sto znaci da se radi izmena namestaja
            {
                namestaj.IdTip = namestaj.TipNamestaja.Id;
                Namestaj.IzmeniNamestaj(namestaj);
            }
            else
            {
                namestaj.IdTip = namestaj.TipNamestaja.Id;
                Namestaj.DodajNamestaj(namestaj);
                Projekat.Instance.Namestaj.Clear();
                Namestaj.UcitajNamestaj();
            }

            this.Close();
          
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
