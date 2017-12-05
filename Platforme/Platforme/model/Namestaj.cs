using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Platforme.model
{
    public class Namestaj : INotifyPropertyChanged
    {
        private int id;
        private string sifra;
        private string naziv;
        private double cena;
        private int kolicina;
        private int idTip;
        private bool obrisan;
        private TipNamestaja tipNamestaja;

        public int Id
        {
            get { return id; }
            set { id = value;
                OnProtertyChanged("id");
            }
        }

        public string Sifra
        {
            get { return sifra; }
            set { sifra = value;
                OnProtertyChanged("sifra");
            }
        }


        public string Naziv
        {
            get { return naziv; }
            set { naziv = value;
                OnProtertyChanged("naziv");
            }
        }

        public double Cena
        {
            get { return cena; }
            set { cena = value;
                OnProtertyChanged("cena");
            }
        }


        public int Kolicina
        {
            get { return kolicina; }
            set { kolicina = value; }
        }


        [XmlIgnore]
        public TipNamestaja TipNamestaja
        {
            get
            {
                if (tipNamestaja == null)
                {
                    tipNamestaja = TipNamestaja.GetById(IdTip);
                }
                return tipNamestaja;
            }
            set
            {
                tipNamestaja = value;
                TipNamestaja.Id = tipNamestaja.Id;
                OnProtertyChanged("TipNamestaja");
            }
        }

        public int IdTip
        {
            get {return idTip; }
            set { idTip = value;

                OnProtertyChanged("idTip");
            }
        }

        public bool Obrisan
        {
            get { return obrisan; }
            set { obrisan = value;
                OnProtertyChanged("obrisan");
            }
        }

       

        public Namestaj() { }

        public Namestaj(int Id,string Sifra, string Naziv,  double Cena, int Kolicina, int IdTip, bool Obrisan)
        {
            this.Id = Id;
            this.IdTip = IdTip;
            this.Naziv = Naziv;
            this.Cena = Cena;
            this.Obrisan = Obrisan;
            this.Sifra = Sifra;
            this.Kolicina = Kolicina;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return $"{Id},{Naziv},{Cena},{TipNamestaja.GetById(IdTip).Naziv}";
        }

        public static Namestaj GetById(int Id)
        {
           foreach(var n in Projekat.Instance.Namestaj)
            {
                if (n.Id == Id)
                {
                    return n;
                }
            }
            return null;
        }
        public void OnProtertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
    }
}
