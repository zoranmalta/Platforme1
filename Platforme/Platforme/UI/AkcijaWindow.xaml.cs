using Platforme.model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AkcijaWindow.xaml
    /// </summary>
    public partial class AkcijaWindow : Window
    {
        private ICollectionView view;

        public AkcijaWindow()
        {
            InitializeComponent();
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Akcija);
            view.Filter = AkcijaFilter;
            dgAkcija.ItemsSource = view;
            dgAkcija.IsSynchronizedWithCurrentItem = true;
            dgAkcija.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
        private bool AkcijaFilter(object obj)
        {
            return !((Akcija)obj).Obrisan;
        }

        private void Dodaj(object sender, RoutedEventArgs e)
        {
            Akcija akcija = new Akcija();
            var ad = new AkcijaDodavanje(akcija, AkcijaDodavanje.Operacija.Dodavanje);
            this.Close();
            ad.ShowDialog();
        }

        private void Izmeni(object sender, RoutedEventArgs e)
        {

        }
    }
    
}
