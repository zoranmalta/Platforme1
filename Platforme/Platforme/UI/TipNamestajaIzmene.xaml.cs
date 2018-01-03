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
    /// Interaction logic for TipNamestajaIzmene.xaml
    /// </summary>
    public partial class TipNamestajaIzmene : Window
    {
        private TipNamestaja tipNamestaja;
       
        public TipNamestajaIzmene(TipNamestaja tipNamestaja)
        {
            InitializeComponent();
            this.tipNamestaja = tipNamestaja;
            tbTipNamestaja.DataContext = tipNamestaja;
        }
        public void Potvrdi(object sender, RoutedEventArgs e)
        {

            this.DialogResult = true;
            if (tipNamestaja.Id != 0) //ako postoji id, tip namestaja je vec u bazi, sto znaci da se radi izmena tipa namestaja
            {
                TipNamestaja.IzmeniTipNamestaja(tipNamestaja);
                Projekat.Instance.TipNamestaja.Clear();
                TipNamestaja.UcitajTipNamestaja();
                Projekat.Instance.Namestaj.Clear();
                Namestaj.UcitajNamestaj();
            }
            else
            {
               
                TipNamestaja.DodajTipNamestaja(tipNamestaja);
                Projekat.Instance.TipNamestaja.Clear();
                TipNamestaja.UcitajTipNamestaja();
            }

            this.Close();
        }
        public void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
