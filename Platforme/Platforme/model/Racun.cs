using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Platforme.model
{
    public class Racun : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int id;
        private DateTime datum;
        private Kupac kupac;
        private int id_Kupac;
        [XmlIgnore]
        public ObservableCollection<StavkaProdajeNamestaja> listaStavkiNamestaja { get; set; }

        public int Id
        {
            get { return id; }
            set { id = value;
                OnPropertyChanged("id");
            }
        }

        public DateTime Datum
        {
            get { return datum; }
            set { datum = value;
                OnPropertyChanged("datum");
            }
        }
        [XmlIgnore]
        public Kupac Kupac
        {
            get { if (kupac == null)
                {
                    kupac = Kupac.GetById(Id_Kupac);
                }
                return kupac;
            }
            set { kupac = value;
                    Kupac.Id = kupac.Id;
                OnPropertyChanged("Kupac");
            }
        }

        public int Id_Kupac
        {
            get { return id_Kupac; }
            set { id_Kupac = value;
                OnPropertyChanged("id_Kupac");
            }
        }


        public Racun()
        {
            listaStavkiNamestaja = new ObservableCollection<StavkaProdajeNamestaja>();
        }

        public Racun(int id,DateTime datum,Kupac kupac)
        {
            this.id = id;
            this.datum = datum;
            this.kupac = kupac;
            listaStavkiNamestaja = new ObservableCollection<StavkaProdajeNamestaja>();
        }

        public double TotalPrice()
        {
            double totalPrice = 0;
            foreach(var s in listaStavkiNamestaja)
            {
                totalPrice += s.vrednostJedneStavke();
            }
            return totalPrice;
        }


        public void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
    }
}
