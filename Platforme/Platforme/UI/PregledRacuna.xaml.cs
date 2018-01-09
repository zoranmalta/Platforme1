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
    /// Interaction logic for PregledRacuna.xaml
    /// </summary>
    public partial class PregledRacuna : Window
    {
        private ICollectionView view;
        public PregledRacuna()
        {
            InitializeComponent();
            Projekat.Instance.Racun.Clear();
            Racun.UcitajRacune();
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Racun);
            dgRacun.ItemsSource = view;
            dgRacun.IsSynchronizedWithCurrentItem = true;
            dgRacun.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            cbOdabir.Items.Add("Ime kupca");
            cbOdabir.Items.Add("Prezime kupca");
            cbOdabir.Items.Add("Broj racuna");
            cbOdabir.Items.Add("Datum");
            cbOdabir.Items.Add("Naziv namestaja");
            dpDatum.DataContext = DateTime.Today;
        }

        private void tbfilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filterText = tbfilter.Text;
            string action = cbOdabir.Text;
            switch (action)
            {
                case "Ime kupca":
                    ObservableCollection<Racun> lista1 = new ObservableCollection<Racun>();
                    foreach (Racun n in Projekat.Instance.Racun)
                    {
                        if (n.Kupac.Ime.ToUpper().Contains(tbfilter.Text.ToUpper()))
                        {
                            lista1.Add(n);
                        }
                    }
                    view = CollectionViewSource.GetDefaultView(lista1);
                    dgRacun.ItemsSource = view;
                    //dgRacun.IsSynchronizedWithCurrentItem = true;
                    //dgRacun.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
                    break;
                case "Prezime kupca":
                    ObservableCollection<Racun> lista2 = new ObservableCollection<Racun>();
                    foreach (Racun n in Projekat.Instance.Racun)
                    {
                        if (n.Kupac.Prezime.ToUpper().Contains(tbfilter.Text.ToUpper()))
                        {
                            lista2.Add(n);
                        }
                    }
                    view = CollectionViewSource.GetDefaultView(lista2);
                    dgRacun.ItemsSource = view;
                    break;
                case "Broj racuna":
                    ObservableCollection<Racun> lista3 = new ObservableCollection<Racun>();
                    foreach (Racun n in Projekat.Instance.Racun)
                    {
                        int brojRacuna = 0;
                        try
                        {
                            brojRacuna = Int32.Parse(tbfilter.Text);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Morate uneti ceo broj");
                            tbfilter.Text = "";
                            return;
                        }
                        if (n.Id==(brojRacuna))
                        {
                            lista3.Add(n);
                        }
                    }
                    view = CollectionViewSource.GetDefaultView(lista3);
                    dgRacun.ItemsSource = view;
                    break;
                case "Datum":
                    break;
                case "Naziv namestaja":
                    ObservableCollection<Racun> lista5 = new ObservableCollection<Racun>();
                    foreach (Racun n in Projekat.Instance.Racun)
                    {
                        foreach(StavkaProdajeNamestaja s in n.listaStavkiNamestaja)
                        {
                            if (s.Namestaj.Naziv.ToUpper().Equals(tbfilter))
                            {
                                lista5.Add(n);
                                break;
                            }
                        }
                    }
                    view = CollectionViewSource.GetDefaultView(lista5);
                    dgRacun.ItemsSource = view;
                    break;
            }
        }

        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PrikaziRacun(object sender, RoutedEventArgs e)
        {
            Racun racun = view.CurrentItem as Racun;
            if (racun == null)
            {
                MessageBox.Show("Niste odabrali nijedan racun za prikazati");
                return;
            }
            var pr = new PrikazRacunaWindow(racun);
            pr.ShowDialog();
        }

        private void dgRacun_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "listaStavkiNamestaja" || (string)e.Column.Header == "listaStavkiUsluga" ||
                (string)e.Column.Header == "Id_Kupac" || (string)e.Column.Header == "Id_Zaposleni")
            {
                e.Cancel = true;
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime d = (DateTime)dpDatum.SelectedDate;
            string textfilter = cbOdabir.Text;
            if (textfilter == "Datum")
            {
                ObservableCollection<Racun> lista2 = new ObservableCollection<Racun>();
                foreach (Racun n in Projekat.Instance.Racun)
                {
                    if (n.Datum.CompareTo(d)==0)
                    {
                        lista2.Add(n);
                    }
                }
                view = CollectionViewSource.GetDefaultView(lista2);
                dgRacun.ItemsSource = view;
            }
        }
    }
}
