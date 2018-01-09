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
    public class Kupac : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int id;
        private string ime;
        private string prezime;
        private string telefon;

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
        
        public Kupac() { }
        public Kupac(int Id, string Ime, string Prezime, string Telefon)
        {
            this.Id = Id;
            this.Ime = Ime;
            this.Prezime = Prezime;
            this.Telefon = Telefon;
        }

        public static void UcitajKupce()
        {
            using (SqlConnection conn = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                conn.Open();
                DataSet ds = new DataSet();

                SqlCommand kupacCommand = conn.CreateCommand();
                kupacCommand.CommandText = @"SELECT * FROM Kupac ";
                SqlDataAdapter daKupac = new SqlDataAdapter();
                daKupac.SelectCommand = kupacCommand;
                daKupac.Fill(ds, "Kupac");

                foreach (DataRow row in ds.Tables["Kupac"].Rows)
                {
                    Kupac n = new Kupac();
                    n.Id = (int)row["Id"];
                    n.Ime = (string)row["Ime"];
                    n.Prezime = (string)row["Prezime"];
                    n.Telefon = (string)row["Telefon"];

                    Projekat.Instance.Kupac.Add(n);
                }

            }
        }
        public static void DodajKupca(Kupac kupac)
        {
            using(SqlConnection conn=new SqlConnection(Projekat.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = $"INSERT INTO Kupac (Ime,Prezime,Telefon) " +
                                                 $"VALUES(@Ime,@Prezime,@Telefon)";

                command.Parameters.Add(new SqlParameter("@Ime", kupac.Ime));
                command.Parameters.Add(new SqlParameter("@Prezime", kupac.Prezime));
                command.Parameters.Add(new SqlParameter("@Telefon", kupac.Telefon));

                command.ExecuteNonQuery();

            }
        }
        public static int UzmiMaxId()
        {
            using (SqlConnection connection = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                connection.Open();

                DataSet ds = new DataSet();

                SqlCommand namestajCommand = connection.CreateCommand();
                namestajCommand.CommandText = @"SELECT Id FROM Kupac ";
                SqlDataAdapter daNamestaj = new SqlDataAdapter();
                daNamestaj.SelectCommand = namestajCommand;
                daNamestaj.Fill(ds, "Kupac");
                int max = 0;
                foreach (DataRow row in ds.Tables["Kupac"].Rows)
                {
                    Kupac n = new Kupac();
                    n.Id = (int)row["Id"];

                    if (n.Id > max)
                    {
                        max = n.Id;
                    }
                }
                return max;
            }
        }

        public override string ToString()
        {
            return $"Ime: {Ime},Prezime:{Prezime},Tel:{Telefon}";
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
