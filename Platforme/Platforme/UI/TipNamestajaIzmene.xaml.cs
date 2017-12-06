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
        private Operacija operacija;

        public enum Operacija
        {
            Dodavanje,
            Izmena
        }

        public TipNamestajaIzmene(TipNamestaja tipNamestaja,Operacija operacija)
        {
            InitializeComponent();
            this.tipNamestaja = tipNamestaja;
            this.operacija = operacija;
            tbTipNamestaja.DataContext = tipNamestaja;
        }
        public void Potvrdi(object sender, RoutedEventArgs e)
        {
            ObservableCollection<TipNamestaja> lista = Projekat.Instance.TipNamestaja;

            switch (operacija)
            {
                case Operacija.Dodavanje:
                    tipNamestaja.Id = lista.Count + 1;
                    lista.Add(tipNamestaja);
                    break;
                case Operacija.Izmena:

                    break;
                default:
                    break;
            }
            GenericsSerializer.Serialize("tipNamestaja.xml", lista);
            var tnw = new TipNamestajaWindow();
            this.Close();
            tnw.ShowDialog();
        }
        public void Izlaz(object sender, RoutedEventArgs e)
        {
            TipNamestajaWindow tnw = new TipNamestajaWindow();
            this.Close();
            tnw.ShowDialog();
        }
    }
}
