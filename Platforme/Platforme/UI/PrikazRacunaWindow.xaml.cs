using Platforme.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for PrikazRacunaWindow.xaml
    /// </summary>
    public partial class PrikazRacunaWindow : Window
    {
        private Salon salon;
  

        public PrikazRacunaWindow(Racun racun)
        {
            InitializeComponent();
            salon = new Salon();
            tbSalon.Text = salon.ToString();
            tbIdRacuna.DataContext = racun;
            tbDatum.DataContext = racun;
            tbKupac.DataContext = racun;
            vrednostPdv.Text = racun.TotalPricePDV().ToString();
            vrednostBezPdv.Text = racun.TotalPriceBezPDV().ToString();
            tbUkupanRacun.Text = racun.TotalPrice().ToString();

            dgStavkeProdaje.ItemsSource = racun.listaStavkiNamestaja;
            dgStavkeProdaje.IsSynchronizedWithCurrentItem = true;
            dgStavkeProdaje.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            dgStavkeUsluga.ItemsSource = racun.listaStavkiUsluga;
            dgStavkeUsluga.IsSynchronizedWithCurrentItem = true;
            dgStavkeUsluga.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
       
        private void dgPrikazRacuna_AutoGeneratingColumn(object sender,
            DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id_Racun" || (string)e.Column.Header == "Id" || (string)e.Column.Header == "Id_Namestaj")
            {
                e.Cancel = true;
            }
        }
        private void dgPrikazUsluga_AutoGeneratingColumn(object sender,
           DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan" || (string)e.Column.Header == "Id_Racun" || (string)e.Column.Header == "Id" || (string)e.Column.Header == "Id_Usluga")
            {
                e.Cancel = true;
            }
        }


        public void Izlaz(object sender, RoutedEventArgs e) {
            this.Close();
        }

        public void Stampaj(object sender, RoutedEventArgs e) { }

       
    }
}
