using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platforme.model
{
    public class Zaposleni:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value;
                OnPropertyChanged("id");
            }
        }
        private string ime;

        public string Ime
        {
            get { return ime; }
            set { ime = value;
                OnPropertyChanged("ime");
            }
        }
        private string prezime;

        public string Prezime
        {
            get { return prezime; }
            set { prezime = value;
                OnPropertyChanged("prezime");
            }
        }
        private string korisnickoIme;

        public string KorisnickoIme
        {
            get { return korisnickoIme; }
            set { korisnickoIme = value;
                OnPropertyChanged("korisnickoIme");
            }
        }
        private string lozinka;

        public string Lozinka
        {
            get { return lozinka; }
            set { lozinka = value;
                OnPropertyChanged("lozinka");
            }
        }
        private string tip;

        public string Tip
        {
            get { return tip; }
            set { tip = value;
                OnPropertyChanged("tip");
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

        public Zaposleni()
        {

        }
        public Zaposleni(int Id,string Ime,string Prezime,string KorisnickoIme,string Lozinka,string Tip,bool Obrisan)
        {
            this.Id = Id;
            this.Ime = Ime;
            this.Prezime = Prezime;
            this.KorisnickoIme = KorisnickoIme;
            this.Lozinka = Lozinka;
            this.Tip = Tip;
            this.Obrisan = Obrisan;
        }
        public Object Clone()
        {
            Zaposleni kopija = new Zaposleni();
            kopija.Id = Id;
            kopija.Ime = Ime;
            kopija.Prezime = Prezime;
            kopija.Tip = Tip;
            kopija.KorisnickoIme = KorisnickoIme;
            kopija.Lozinka = Lozinka;
            kopija.Obrisan = Obrisan;
            
            return kopija;
        }
        public static Zaposleni GetById(int Id)
        {
            foreach (var a in Projekat.Instance.Zaposleni)
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
            return $"Ime: {Ime},Prezime: {Prezime},Uloga: {Tip}";
        }
        public static void UcitajZaposlene()
        {
            using (SqlConnection connection = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                connection.Open();

                DataSet ds = new DataSet();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"SELECT * FROM Zaposleni ";
                SqlDataAdapter daZaposleni = new SqlDataAdapter();
                daZaposleni.SelectCommand = command;
                daZaposleni.Fill(ds, "Zaposleni");

                foreach (DataRow row in ds.Tables["Zaposleni"].Rows)
                {
                    Zaposleni n = new Zaposleni();
                    n.Id = (int)row["Id"];
                    n.Ime = (string)row["Ime"];
                    n.Prezime = (string)row["Prezime"];
                    n.KorisnickoIme = (string)row["KorisnickoIme"];
                    n.Lozinka = (string)row["Lozinka"];
                    n.Tip = (string)row["Tip"];
                    n.Obrisan = (bool)row["Obrisan"];

                    Projekat.Instance.Zaposleni.Add(n);
                }
            }
        }
        public static void DodajZaposlenog(Zaposleni zaposleni)
        {
            using (SqlConnection conn = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"INSERT INTO Zaposleni (Ime, Prezime,Tip,KorisnickoIme,Lozinka,Obrisan) 
                                                     VALUES (@Ime, @Prezime,@Tip,@KorisnickoIme,@Lozinka,@Obrisan)";

                command.Parameters.Add(new SqlParameter("@Ime", zaposleni.Ime));
                command.Parameters.Add(new SqlParameter("@Prezime", zaposleni.Prezime));
                command.Parameters.Add(new SqlParameter("@Tip", zaposleni.Tip));
                command.Parameters.Add(new SqlParameter("@KorisnickoIme", zaposleni.KorisnickoIme));
                command.Parameters.Add(new SqlParameter("@Lozinka", zaposleni.Lozinka));
                command.Parameters.Add(new SqlParameter("@Obrisan", zaposleni.Obrisan));

                command.ExecuteNonQuery();
            }
        }
        public static void IzmeniZaposlenog(Zaposleni zaposleni)
        {
            using (SqlConnection conn = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"UPDATE Zaposleni SET Ime=@Ime, Prezime=@Prezime,Tip=@Tip,
                                        KorisnickoIme=@KorisnickoIme,Lozinka=@Lozinka,Obrisan=@Obrisan WHERE Id=@Id ";

                command.Parameters.Add(new SqlParameter("@Ime", zaposleni.Ime));
                command.Parameters.Add(new SqlParameter("@Prezime", zaposleni.Prezime));
                command.Parameters.Add(new SqlParameter("@Tip", zaposleni.Tip));
                command.Parameters.Add(new SqlParameter("@KorisnickoIme", zaposleni.KorisnickoIme));
                command.Parameters.Add(new SqlParameter("@Lozinka", zaposleni.Lozinka));
                command.Parameters.Add(new SqlParameter("@Obrisan", zaposleni.Obrisan));
                command.Parameters.Add(new SqlParameter("@Id", zaposleni.Id));

                command.ExecuteNonQuery();
            }
        }
        public static void ObrisiZaposlenog(Zaposleni zaposleni)
        {
            using (SqlConnection conn = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"UPDATE Zaposleni SET Obrisan=@Obrisan WHERE Id=@Id ";

                command.Parameters.Add(new SqlParameter("@Obrisan", zaposleni.Obrisan));
                command.Parameters.Add(new SqlParameter("@Id", zaposleni.Id));

                command.ExecuteNonQuery();
            }
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
