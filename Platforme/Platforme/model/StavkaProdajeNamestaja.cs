using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Platforme.model
{
    public class StavkaProdajeNamestaja : INotifyPropertyChanged
    {
        private int id_Racun;
        private Namestaj namestaj;
        private int kolicina;
        private int id_Namestaj;

        public int Id_Racun
        {
            get { return id_Racun; }
            set { id_Racun = value;
                OnProtertyChanged("id_Racun");
            }
        }
        public Namestaj Namestaj
        {
            get {
                if (namestaj == null)
                {
                    namestaj = Namestaj.GetById(Id_Namestaj);
                }
                return namestaj; }
            set { namestaj = value;
                OnProtertyChanged("namestaj");
            }
        }

        public int Id_Namestaj
        {
            get { return id_Namestaj; }
            set { id_Namestaj = value;
                OnProtertyChanged("id_Namestaj");
            }
        }


        public int Kolicina
        {
            get { return kolicina; }
            set { kolicina = value;
                OnProtertyChanged("kolicina");
            }
        }

        public StavkaProdajeNamestaja() { }

        public StavkaProdajeNamestaja(int id_Racun, Namestaj Komad, int Kolicina)
        {
            this.Id_Racun = Id_Racun;
            this.namestaj = Komad;
            this.Kolicina = Kolicina;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnProtertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }

        public double vrednostJedneStavke()
        {
            double vrednost = namestaj.Cena * Kolicina;
            return vrednost;
        }
    }
}
