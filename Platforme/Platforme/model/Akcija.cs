using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Platforme.model
{
    public class Akcija : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int id;
        private int idNamestaj;
        private DateTime datumPocetka;
        private DateTime datumZavrsetka;
        private Namestaj namestaj;
        private int popust;
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

        public int Popust
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
        public Object Clone()
        {
            Akcija kopija = new Akcija();
            kopija.Id = Id;
            kopija.DatumPocetka = DatumPocetka;
            kopija.DatumZavrsetka = DatumZavrsetka;
            kopija.Popust = Popust;
            kopija.Namestaj = Namestaj;
            kopija.IdNamestaj = IdNamestaj;
            kopija.Obrisan = Obrisan;

            return kopija;
        }

            public Akcija()
        {
            DatumPocetka = DateTime.Today;
            DatumZavrsetka = DateTime.Today;
        }

        public Akcija(int Id, int IdNamestaj, DateTime DatumPocetka, DateTime DatumZavrsetka, Namestaj Namestaj, int Popust, bool Obrisan)
        {
            this.Id = Id;
            this.IdNamestaj = IdNamestaj;
            this.DatumPocetka = DatumPocetka;
            this.DatumZavrsetka = DatumZavrsetka;
            this.Namestaj = Namestaj;
            this.Popust = Popust;
            this.Obrisan = Obrisan;
        }
        public static void UcitajAkcije()
        {
            using (SqlConnection connection = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                connection.Open();

                DataSet ds = new DataSet();

                SqlCommand namestajCommand = connection.CreateCommand();
                namestajCommand.CommandText = @"SELECT * FROM Akcija ";
                SqlDataAdapter daNamestaj = new SqlDataAdapter();
                daNamestaj.SelectCommand = namestajCommand;
                daNamestaj.Fill(ds, "Akcija");

                foreach (DataRow row in ds.Tables["Akcija"].Rows)
                {
                    Akcija n = new Akcija();
                    n.Id = (int)row["Id"];
                    n.DatumPocetka = (DateTime)row["DatumPocetka"];
                    n.DatumZavrsetka = (DateTime)row["DatumZavrsetka"];
                    n.Popust = (int)row["Popust"];
                    n.IdNamestaj = (int)row["IdNamestaj"];
                    n.Namestaj = Namestaj.GetById(n.IdNamestaj);
                    n.Obrisan = (bool)row["Obrisan"];

                    Projekat.Instance.Akcija.Add(n);
                }
            }
        }

        public static void UcitajAktuelneAkcije()
        {
            using (SqlConnection connection = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                connection.Open();
                string date = Convert.ToString(DateTime.Today);
                DataSet ds = new DataSet();

                SqlCommand namestajCommand = connection.CreateCommand();
                namestajCommand.CommandText = @"SELECT * FROM Akcija Where "+date+" BETWEEN DatumPocetka AND DatumZavrsetka ";
                SqlDataAdapter daNamestaj = new SqlDataAdapter();
                daNamestaj.SelectCommand = namestajCommand;
                daNamestaj.Fill(ds, "Akcija");

                foreach (DataRow row in ds.Tables["Akcija"].Rows)
                {
                    Akcija n = new Akcija();
                    n.Id = (int)row["Id"];
                    n.DatumPocetka = (DateTime)row["DatumPocetka"];
                    n.DatumZavrsetka = (DateTime)row["DatumZavrsetka"];
                    n.Popust = (int)row["Popust"];
                    n.IdNamestaj = (int)row["IdNamestaj"];
                    n.Namestaj = Namestaj.GetById(n.IdNamestaj);
                    n.Obrisan = (bool)row["Obrisan"];

                    Projekat.Instance.Akcija.Add(n);
                }
            }
        }

        public static void DodajAkciju(Akcija akcija)
        {
            using (SqlConnection conn = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"INSERT INTO Akcija (DatumPocetka, DatumZavrsetka,IdNamestaj,Popust,Obrisan) 
                                                     VALUES (@DatumPocetka, @DatumZavrsetka,@IdNamestaj,@Popust,@Obrisan)";

                command.Parameters.Add(new SqlParameter("@DatumPocetka", akcija.DatumPocetka));
                command.Parameters.Add(new SqlParameter("@DatumZavrsetka", akcija.DatumZavrsetka));
                command.Parameters.Add(new SqlParameter("@IdNamestaj", akcija.IdNamestaj));
                command.Parameters.Add(new SqlParameter("@Popust", akcija.Popust));
                command.Parameters.Add(new SqlParameter("@Obrisan", akcija.Obrisan));

                command.ExecuteNonQuery();
            }
        }

        public static void IzmeniAkciju(Akcija akcija)
        {
            using (SqlConnection conn = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"UPDATE Akcija SET DatumPocetka=@DatumPocetka, DatumZavrsetka=@DatumZavrsetka,
                                            IdNamestaj=@IdNamestaj,Popust=@Popust,Obrisan=@Obrisan WHERE Id=@Id ";

                command.Parameters.Add(new SqlParameter("@DatumPocetka", akcija.DatumPocetka));
                command.Parameters.Add(new SqlParameter("@DatumZavrsetka", akcija.DatumZavrsetka));
                command.Parameters.Add(new SqlParameter("@IdNamestaj", akcija.IdNamestaj));
                command.Parameters.Add(new SqlParameter("@Popust", akcija.Popust));
                command.Parameters.Add(new SqlParameter("@Obrisan", akcija.Obrisan));
                command.Parameters.Add(new SqlParameter("@Id", akcija.Id));

                command.ExecuteNonQuery();
            }
        }
        public static void ObrisiAkciju(Akcija akcija)
        {
            {
                using (SqlConnection conn = new SqlConnection(Projekat.CONNECTION_STRING))
                {
                    if (akcija.Id != 0)//ako Akcija postoji u bazi
                    {
                        conn.Open();

                        SqlCommand command = conn.CreateCommand();
                        command.CommandText = @"DELETE FROM Akcija WHERE Id=@Id";

                        command.Parameters.Add(new SqlParameter("@Id", akcija.Id));

                        command.ExecuteNonQuery();
                    }
                }
            }
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
