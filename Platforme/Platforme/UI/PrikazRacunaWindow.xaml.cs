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
            tbUkupanRacun.Text = racun.TotalPrice().ToString();
        }
    }
}
