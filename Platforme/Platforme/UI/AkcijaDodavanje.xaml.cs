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
    /// Interaction logic for AkcijaDodavanje.xaml
    /// </summary>
    public partial class AkcijaDodavanje : Window
    {
        private Akcija akcija;
       

        public AkcijaDodavanje(Akcija akcija)
        {
            InitializeComponent();
            this.akcija = akcija;
            tbPopust.DataContext = akcija;
            dpPocetak.DataContext = akcija;
            dpZavrsetak.DataContext = akcija;
            cbNamestaj.ItemsSource = Projekat.Instance.Namestaj;
            cbNamestaj.DataContext = akcija;

        }

        private void Sacuvaj_Akciju(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            if (akcija.DatumPocetka.CompareTo(akcija.DatumZavrsetka) > 0)
            {
                MessageBox.Show("Datum zavrsetka treba da je posle datuma pocetka akcije");
                return;
            }
            if (akcija.Id != 0) //ako postoji id, Akcija je vec u bazi, sto znaci da se radi izmena akcije
            {
                akcija.IdNamestaj = akcija.Namestaj.Id;
                Akcija.IzmeniAkciju(akcija);
            }
            else
            {
                if (akcija.Namestaj == null)
                {
                    MessageBox.Show("Niste izabrali namestaj za akciju");
                    return;
                }
                akcija.IdNamestaj = akcija.Namestaj.Id;
                Akcija.DodajAkciju(akcija);
                Projekat.Instance.Akcija.Clear();
                Akcija.UcitajAkcije();
            }

            this.Close();
        }

        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
