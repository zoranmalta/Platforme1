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
        public override string ToString()
        {
            return $"{Naziv},{Cena} din";
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
        public Usluga() { }

        public Usluga(int Id,string Naziv,double Cena,bool Obrisan)
        {
            this.Id = Id;
            this.Naziv = Naziv;
            this.Cena = Cena;
            this.Obrisan = Obrisan;
        }

        public static Usluga GetById(int Id)
        {
            foreach (var n in Projekat.Instance.Usluga)
            {
                if (n.Id == Id)
                {
                    return n;
                }
            }
            return null;
        }
        public static void UcitajUsluge()
        {
            using (SqlConnection connection = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                try
                {
                    connection.Open();

                    DataSet ds = new DataSet();

                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = @"SELECT * FROM Usluga ";
                    SqlDataAdapter daUsluga = new SqlDataAdapter();
                    daUsluga.SelectCommand = command;
                    daUsluga.Fill(ds, "Usluga");

                    foreach (DataRow row in ds.Tables["Usluga"].Rows)
                    {
                        Usluga n = new Usluga();
                        n.Id = (int)row["Id"];
                        n.Naziv = (string)row["Naziv"];
                        n.Cena = Convert.ToDouble(row["Cena"]);
                        n.Obrisan = (bool)row["Obrisan"];

                        Projekat.Instance.Usluga.Add(n);
                    }
                }
                catch (Exception)
                {

                    Console.WriteLine("Nije uspela sql naredba");
                    return;
                }
            }
        }
        public static void DodajUslugu(Usluga usluga)
        {
            using (SqlConnection conn = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                try
                {
                    conn.Open();

                    SqlCommand command = conn.CreateCommand();
                    command.CommandText = @"INSERT INTO Usluga (Naziv,Cena,Obrisan) 
                                                     VALUES (@Naziv,@Cena,@Obrisan)";

                    command.Parameters.Add(new SqlParameter("@Naziv", usluga.Naziv));
                    command.Parameters.Add(new SqlParameter("@Cena", usluga.Cena));
                    command.Parameters.Add(new SqlParameter("@Obrisan", usluga.Obrisan));

                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    Console.WriteLine("Nije uspela sql naredba");
                    return;
                }
            }
        }
        public static void IzmeniUslugu(Usluga usluga)
        {
            using (SqlConnection conn = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                try
                {
                    if (usluga.Id != 0)//ako namestaj postoji u bazi
                    {
                        conn.Open();

                        SqlCommand command = conn.CreateCommand();
                        command.CommandText = @"UPDATE Usluga SET NAZIV=@Naziv,Cena=@Cena,
                                                                Obrisan=@Obrisan WHERE Id=@Id";

                        command.Parameters.Add(new SqlParameter("@Naziv", usluga.Naziv));
                        command.Parameters.Add(new SqlParameter("@Cena", usluga.Cena));
                        command.Parameters.Add(new SqlParameter("@Obrisan", usluga.Obrisan));
                        command.Parameters.Add(new SqlParameter("@Id", usluga.Id));

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {

                    Console.WriteLine("Nije uspela sql naredba");
                    return;
                }
            }
        }
        public static void ObrisiUslugu(Usluga usluga)
        {
            using (SqlConnection conn = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                try
                {
                    if (usluga.Id != 0)//ako namestaj postoji u bazi
                    {
                        conn.Open();

                        SqlCommand command = conn.CreateCommand();
                        command.CommandText = @"DELETE FROM Usluga WHERE Id=@Id";

                        command.Parameters.Add(new SqlParameter("@Id", usluga.Id));

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {

                    Console.WriteLine("Nije uspela sql naredba");
                    return;
                }
            }
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
