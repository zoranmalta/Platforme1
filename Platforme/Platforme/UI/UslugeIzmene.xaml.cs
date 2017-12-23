using Platforme.model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for UslugeIzmene.xaml
    /// </summary>
    public partial class UslugeIzmene : Window
    {
        private Usluga usluga;

        public UslugeIzmene(Usluga usluga)
        {
            InitializeComponent();
            this.usluga = usluga;
            tbNaziv.DataContext = usluga;
            tbCena.DataContext = usluga;
        }
        private void Sacuvaj_Uslugu(object sender, RoutedEventArgs e)
        {

            this.DialogResult = true;
            if (usluga.Id != 0) //ako postoji id, namestaj je vec u bazi, sto znaci da se radi izmena Usluge
            {
                Usluga.IzmeniUslugu(usluga);
            }
            else
            {
                Usluga.DodajUslugu(usluga);
                Projekat.Instance.Usluga.Clear();
                Usluga.UcitajUsluge();
            }

            this.Close();
        }
        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
