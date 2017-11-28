using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platforme.model
{
    public class Kupac : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int id;
        private string ime;
        private string prezime;
        private string telefon;
        private bool obrisan;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("id");
            }
        }
        public string Ime
        {
            get { return ime; }
            set
            {
                ime = value;
                OnPropertyChanged("ime");
            }
        }
        public string Prezime
        {
            get { return prezime; }
            set
            {
                prezime = value;
                OnPropertyChanged("prezime");
            }
        }
        public string Telefon
        {
            get { return telefon; }
            set
            {
                telefon = value;
                OnPropertyChanged("telefon");
            }
        }
        public bool Obrisan
        {
            get { return obrisan; }
            set
            {
                obrisan = value;
                OnPropertyChanged("obrisan");
            }
        }
        public Kupac() { }
        public Kupac(int Id, string Ime, string Prezime, string Telefon, bool Obrisan)
        {
            this.Id = Id;
            this.Ime = Ime;
            this.Prezime = Prezime;
            this.Telefon = Telefon;
            this.Obrisan = Obrisan;
        }

        public override string ToString()
        {
            return $"{Ime},{Prezime},{Telefon}";
        }

        public static Kupac GetById(int Id)
        {
            foreach(Kupac k in Projekat.Instance.Kupac)
            {
                if (k.id == Id) {
                    return k;
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
