using Platforme.model;
using Platforme.util;
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
    /// Interaction logic for TipNamestajaWindow.xaml
    /// </summary>
    public partial class TipNamestajaWindow : Window
    {
        private ICollectionView view;

        public TipNamestajaWindow()
        {
            InitializeComponent();
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.TipNamestaja);
            view.Filter = NamestajFilter;
            dgTipNamestaja.ItemsSource = view;
            dgTipNamestaja.IsSynchronizedWithCurrentItem = true;
            dgTipNamestaja.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
        private bool NamestajFilter(object obj)
        {
            return !((TipNamestaja)obj).Obrisan;
        }

        private void Dodaj(object sender, RoutedEventArgs e)
        {
            TipNamestaja tipNamestaja = new TipNamestaja();
            TipNamestajaIzmene tni = new TipNamestajaIzmene(tipNamestaja, TipNamestajaIzmene.Operacija.Dodavanje);
            this.Close();
            tni.ShowDialog();
        }
    
        private void Izmeni(object sender, RoutedEventArgs e)
        {
            var selectedTipNamestaja = (TipNamestaja)dgTipNamestaja.SelectedItem;
            var tni = new TipNamestajaIzmene(selectedTipNamestaja, TipNamestajaIzmene.Operacija.Izmena);
            this.Close();
            tni.ShowDialog();
        }
        private void Ukloni(object sender, RoutedEventArgs e)
        {
            ObservableCollection<TipNamestaja> lista = Projekat.Instance.TipNamestaja;
            ObservableCollection<Namestaj> listaNamestaja = Projekat.Instance.Namestaj;
            TipNamestaja selectedTipNamestaja = (TipNamestaja)dgTipNamestaja.SelectedItem;
            foreach(TipNamestaja tn in lista)
            {
                if (tn.Id == selectedTipNamestaja.Id)
                {
                    tn.Obrisan=true;
                    foreach (Namestaj n in listaNamestaja)
                    {
                        if (n.IdTip == tn.Id)
                        {
                            n.Obrisan=true;
                        }
                    }
                }
            }
            view.Refresh();
            GenericsSerializer.Serialize("tipNamestaja.xml", lista);
            GenericsSerializer.Serialize("namestaj.xml", listaNamestaja);

        }
       
        private void Izlaz(object sender, RoutedEventArgs e)
        {
            Window1 w1 = new Window1();
            this.Close();
            w1.ShowDialog();
        }
        private void dgTipNamestaja_AutoGeneratingColumn(object sender,
            DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Obrisan")
            {
                e.Cancel = true;
            }
        }
    }
}
