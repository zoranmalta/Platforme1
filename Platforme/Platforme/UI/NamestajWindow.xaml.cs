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
            //var w1 = new Window1();
            //w1.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
