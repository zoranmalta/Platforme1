using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platforme.model
{
    public class Usluga : INotifyPropertyChanged
    {
        private int id;
        private string naziv;
        private double cena;
        private bool obrisan;

        public int Id
        {
            get { return id; }
            set { id = value;
                OnProtertyChanged("Id");
            }
        }


        public string Naziv
        {
            get { return naziv; }
            set { naziv = value;
                OnProtertyChanged("Naziv");
            }
        }

        public double Cena
        {
            get { return cena; }
            set { cena = value;
                OnProtertyChanged("Cena");
            }
        }

        public bool Obrisan
        {
            get { return obrisan; }
            set { obrisan = value;
                OnProtertyChanged("Obrisan");
            }
        }
   

        public object Clone()
        {
            Usluga kopija = new Usluga();
            kopija.Id = Id;
            kopija.Naziv = Naziv;
            kopija.Cena = Cena;
            kopija.Obrisan = Obrisan;
            
            return kopija;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnProtertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
    }
    
}
