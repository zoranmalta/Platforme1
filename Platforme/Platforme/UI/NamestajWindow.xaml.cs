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
        private Operacija operacija;

        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        }
        public NamestajWindow(Namestaj namestaj,Operacija operacija)
        {
            InitializeComponent();
            InicijalizujVrednosti(namestaj, operacija);
        }

        public void InicijalizujVrednosti(Namestaj namestaj,Operacija operacija)
        {
            this.namestaj = namestaj;
            this.operacija = operacija;
            tbNaziv.DataContext = namestaj;
            tbCena.DataContext = namestaj;

            cbTipNamestaja.ItemsSource = Projekat.Instance.TipNamestaja;
            cbTipNamestaja.DataContext = namestaj;
        }
        public void SacuvajIzmene()
        {
            ObservableCollection<Namestaj> pospojeciNamestaj= Projekat.Instance.Namestaj;
          
            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    namestaj.Id = pospojeciNamestaj.Count + 1;
                    pospojeciNamestaj.Add(namestaj);
                    namestaj.IdTip = namestaj.TipNamestaja.Id;
                    break;
                case Operacija.IZMENA:
                    foreach(var n in pospojeciNamestaj)
                    {
                        if (n.Id == namestaj.Id)
                        {
                            n.IdTip = namestaj.TipNamestaja.Id;
                        }
                    }
                    break;
                default:
                    break;
            }
            GenericsSerializer.Serialize("namestaj.xml", pospojeciNamestaj);
        }
        private void Sacuvaj_Namestaj(object sender, RoutedEventArgs e) 
        {
            SacuvajIzmene();
            this.Close();
            var w1 = new Window1();
            w1.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
