using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platforme.model
{
    public class Akcija : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int id;
        private int idNamestaj;
        private DateTime datumZavrsetka;
        private Namestaj namestaj;
        private double popust;
        private bool obrisan;

        public int Id
        {
            get { return id; }
            set { id = value;
                OnPropertyChanged("id");
            }
        }

        public int IdNamestaj
        {
            get { return idNamestaj; }
            set { idNamestaj = value;
                OnPropertyChanged("idNamestaj");
            }
        }

        private DateTime datumPocetka;

        public DateTime DatumPocetka

        {
            get { return datumPocetka; }
            set { datumPocetka = value;
                OnPropertyChanged("datumPocetka");
            }
        }


        public DateTime DatumZavrsetka
        {
            get { return datumZavrsetka; }
            set { datumZavrsetka = value;
                OnPropertyChanged("datumZavrsetka");
            }
        }

        public Namestaj Namestaj
        {
            get
            {
                if (namestaj == null)
                {
                    namestaj = Namestaj.GetById(IdNamestaj);
                }
                return namestaj;
            }
            set
            {
                namestaj = value;
                Namestaj.Id = namestaj.Id;
                OnPropertyChanged("namestaj");
            }
        }

        public double Popust
        {
            get { return popust; }
            set { popust = value;
                OnPropertyChanged("popust");
            }
        }

        public bool Obrisan
        {
            get { return obrisan; }
            set { obrisan = value;
                OnPropertyChanged("obrisan");
            }
        }

        public Akcija()
        {
            DatumPocetka = DateTime.Today;
            DatumZavrsetka = DateTime.Today;
        }

        public Akcija(int Id, int IdNamestaj, DateTime DatumPocetka, DateTime DatumZavrsetka, Namestaj Namestaj, double Popust, bool Obrisan)
        {
            this.Id = Id;
            this.IdNamestaj = IdNamestaj;
            this.DatumPocetka = DatumPocetka;
            this.DatumZavrsetka = DatumZavrsetka;
            this.Namestaj = Namestaj;
            this.Popust = Popust;
            this.Obrisan = Obrisan;
        }

        public static Akcija GetById(int Id)
        {
            foreach (var a in Projekat.Instance.Akcija)
            {
                if (a.Id == Id)
                {
                    return a;
                }
            }
            return null;
        }

        public override string ToString()
        {
            return $"{Namestaj.GetById(IdNamestaj).Naziv},{Popust},{datumPocetka},{DatumZavrsetka}";
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
