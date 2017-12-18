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
        private Operacija operacija;

        public enum Operacija
        {
            Dodavanje,
            Izmena
        }

        public AkcijaDodavanje(Akcija akcija,Operacija operacija)
        {
            InitializeComponent();
            this.akcija = akcija;
            this.operacija = operacija;
            tbPopust.DataContext = akcija;
            dpPocetak.DataContext = akcija;
            dpZavrsetak.DataContext = akcija;
            cbNamestaj.ItemsSource = Projekat.Instance.Namestaj;
            cbNamestaj.DataContext = akcija;

        }
        public void Sacuvaj_Izmene()
        {
            ObservableCollection<Akcija> postojeceAkcije = Projekat.Instance.Akcija;

            switch (operacija)
            {
                case Operacija.Dodavanje:
                    akcija.Id = postojeceAkcije.Count + 1;
                    akcija.IdNamestaj = akcija.Namestaj.Id;
                    postojeceAkcije.Add(akcija);
                    break;
                case Operacija.Izmena:
                    break;
                default:
                    break;
            }
            GenericsSerializer.Serialize("akcija.xml", postojeceAkcije);
        }

        private void Sacuvaj_Akciju(object sender, RoutedEventArgs e)
        {
            Sacuvaj_Izmene();
            var aw = new AkcijaWindow();
            this.Close();
            aw.ShowDialog();
        }

        private void Izlaz(object sender, RoutedEventArgs e)
        {

        }
    }
}
