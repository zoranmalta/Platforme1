﻿using Platforme.model;
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
        private ICollectionView view;
  

        public PrikazRacunaWindow(Racun racun)
        {
            InitializeComponent();
            salon = new Salon();
            tbSalon.Text = salon.ToString();
            tbIdRacuna.DataContext = racun;
            tbDatum.DataContext = racun;
            tbKupac.DataContext = racun;
            tbUkupanRacun.Text = racun.TotalPrice().ToString();

            //view = CollectionViewSource.GetDefaultView(Projekat.Instance.StavkaProdajeNamestaja);
            ObservableCollection<StavkaProdajeNamestaja> listaStavkiProdajeNamestaja= new ObservableCollection<StavkaProdajeNamestaja>();
            ObservableCollection<StavkaProdajeNamestaja> StavkaProdajeNamestaja = Projekat.Instance.StavkaProdajeNamestaja;
            foreach(StavkaProdajeNamestaja s in StavkaProdajeNamestaja)
            {
                if (s.Id_Racun == racun.Id)
                {
                    listaStavkiProdajeNamestaja.Add(s);
                }
            }
            dgStavkeProdaje.ItemsSource = listaStavkiProdajeNamestaja;
            dgStavkeProdaje.IsSynchronizedWithCurrentItem = true;

            dgStavkeProdaje.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

       

        public void Izlaz(object sender, RoutedEventArgs e) {
            this.Close();
        }

        public void Stampaj(object sender, RoutedEventArgs e) { }

       
    }
}
