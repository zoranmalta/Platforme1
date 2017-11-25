using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platforme.model
{
    public class TipNamestaja :INotifyPropertyChanged
    {

        private int id;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Id
        {
            get { return id; }
            set { id = value;
                OnPropertyChanged("id");
            }
        }
        private string naziv;

        public string Naziv
        {
            get { return naziv; }
            set { naziv = value;
                OnPropertyChanged("naziv");
            }
        }
        private bool obrisan;

        public bool Obrisan
        {
            get { return obrisan; }
            set { obrisan = value;
                OnPropertyChanged("obrisan");
            }
        }

        public TipNamestaja() { }

        public TipNamestaja(int Id, string Naziv, bool Obrisan)
        {
            this.Id = Id;
            this.Naziv = Naziv;
            this.Obrisan = Obrisan;
        }
        public override string ToString()
        {
            return Naziv;
        }
        public static TipNamestaja GetById(int Id)
        {
            foreach (var tipNamestaja in Projekat.Instance.TipNamestaja)
            {
                if (tipNamestaja.Id == Id)
                {
                    return tipNamestaja;
                }
            }
            return null;
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
