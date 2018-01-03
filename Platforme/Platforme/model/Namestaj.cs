using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
            set { kolicina = value;
                OnProtertyChanged("kolicina");
            }
        }
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
        public Object Clone()
        {
            Namestaj kopija = new Namestaj();
            kopija.Id = Id;
            kopija.IdTip = IdTip;
            kopija.Naziv = Naziv;
            kopija.TipNamestaja = TipNamestaja;
            kopija.Cena = Cena;
            kopija.Obrisan = Obrisan;
            kopija.Sifra = Sifra;
            kopija.Kolicina = Kolicina;

            return kopija;
        }

        public Namestaj() { }

        public Namestaj(int Id,string Sifra, string Naziv,TipNamestaja tipNamestaja,  double Cena, int Kolicina, int IdTip, bool Obrisan)
        {
            this.Id = Id;
            this.IdTip = IdTip;
            this.Naziv = Naziv;
            this.TipNamestaja = TipNamestaja;
            this.Cena = Cena;
            this.Obrisan = Obrisan;
            this.Sifra = Sifra;
            this.Kolicina = Kolicina;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return $"Naziv: {Naziv},Cena:{Cena} din,{TipNamestaja.GetById(IdTip).Naziv},kol: {Kolicina}";
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

        public static void UcitajNamestaj()
        {
            using (SqlConnection connection = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                connection.Open();

                DataSet ds = new DataSet();

                SqlCommand namestajCommand = connection.CreateCommand();
                namestajCommand.CommandText = @"SELECT * FROM Namestaj  ";
                SqlDataAdapter daNamestaj = new SqlDataAdapter();
                daNamestaj.SelectCommand = namestajCommand;
                daNamestaj.Fill(ds, "Namestaj");

                foreach (DataRow row in ds.Tables["Namestaj"].Rows)
                {
                    Namestaj n = new Namestaj();
                    n.Id = (int)row["Id"];
                    n.Sifra = (string)row["Sifra"];
                    n.Naziv = (string)row["Naziv"];
                    n.Cena = Convert.ToDouble(row["Cena"]);
                    n.Kolicina = (int)row["Kolicina"];
                    n.IdTip = (int)row["IdTip"];
                    n.TipNamestaja = TipNamestaja.GetById(n.IdTip);
                    n.Obrisan = (bool)row["Obrisan"];

                    Projekat.Instance.Namestaj.Add(n);
                }
            }
        }
        public static void DodajNamestaj(Namestaj namestaj)
        {
            using (SqlConnection conn = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"INSERT INTO Namestaj (Naziv, Sifra,Cena,Kolicina,Obrisan,IdTip) 
                                                     VALUES (@Naziv, @Sifra,@Cena,@Kolicina,@Obrisan,@IdTip)";

                command.Parameters.Add(new SqlParameter("@Naziv", namestaj.Naziv));
                command.Parameters.Add(new SqlParameter("@Sifra", namestaj.Sifra));
                command.Parameters.Add(new SqlParameter("@Cena", namestaj.Cena));
                command.Parameters.Add(new SqlParameter("@Kolicina", namestaj.Kolicina));
                command.Parameters.Add(new SqlParameter("@Obrisan", namestaj.Obrisan));
                command.Parameters.Add(new SqlParameter("@IdTip", namestaj.IdTip));

                command.ExecuteNonQuery();
            }
        }
        public static void IzmeniNamestaj(Namestaj namestaj)
        {
            using (SqlConnection conn = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                if (namestaj.Id != 0)//ako namestaj postoji u bazi
                {
                    conn.Open();

                    SqlCommand command = conn.CreateCommand();
                    command.CommandText = @"UPDATE Namestaj SET NAZIV=@Naziv, Sifra=@Sifra,Cena=@Cena,Kolicina=@Kolicina,
                                                                Obrisan=@Obrisan,IdTip=@IdTip WHERE Id=@Id";

                    command.Parameters.Add(new SqlParameter("@Naziv", namestaj.Naziv));
                    command.Parameters.Add(new SqlParameter("@Sifra", namestaj.Sifra));
                    command.Parameters.Add(new SqlParameter("@Cena", namestaj.Cena));
                    command.Parameters.Add(new SqlParameter("@Kolicina", namestaj.Kolicina));
                    command.Parameters.Add(new SqlParameter("@Obrisan", namestaj.Obrisan));
                    command.Parameters.Add(new SqlParameter("@IdTip", namestaj.IdTip));
                    command.Parameters.Add(new SqlParameter("@Id", namestaj.Id));

                    command.ExecuteNonQuery();
                }
            }
        }
        public static void ObrisiNamestaj(Namestaj namestaj)
        {
            using(SqlConnection conn=new SqlConnection(Projekat.CONNECTION_STRING))
            {
                if (namestaj.Id != 0)//ako namestaj postoji u bazi
                {
                    conn.Open();

                    SqlCommand command = conn.CreateCommand();
                    command.CommandText = @"DELETE FROM Namestaj WHERE Id=@Id";

                    command.Parameters.Add(new SqlParameter("@Id", namestaj.Id));

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
