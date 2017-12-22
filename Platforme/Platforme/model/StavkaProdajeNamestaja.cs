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
    public class StavkaProdajeNamestaja : INotifyPropertyChanged
    {
        private int id;
        private int id_Racun;
        private Namestaj namestaj;
        private int kolicina;
        private int id_Namestaj;

        public int Id
        {
            get { return id; }
            set { id = value;
                OnProtertyChanged("id");
            }
        }

        public int Id_Racun
        {
            get { return id_Racun; }
            set { id_Racun = value;
                OnProtertyChanged("id_Racun");
            }
        }
        [XmlIgnore]
        public Namestaj Namestaj
        {
            get {
                if (namestaj == null)
                {
                    namestaj = Namestaj.GetById(Id_Namestaj);
                }
                return namestaj; }
            set { namestaj = value;
                OnProtertyChanged("namestaj");
            }
        }

        public int Id_Namestaj
        {
            get { return id_Namestaj; }
            set { id_Namestaj = value;
                OnProtertyChanged("id_Namestaj");
            }
        }

        public int Kolicina
        {
            get { return kolicina; }
            set { kolicina = value;
                OnProtertyChanged("kolicina");
            }
        }

        public StavkaProdajeNamestaja() { }

        public StavkaProdajeNamestaja(int Id, int Id_Racun, Namestaj namestaj, int Kolicina,int Id_Namestaj)
        {
            this.Id = Id;
            this.Id_Racun = Id_Racun;
            this.namestaj = namestaj;
            this.Kolicina = Kolicina;
            this.Id_Namestaj = Id_Namestaj;
        }
        public static void UcitajStavkeRacuna(int Id_Racun)
        {
            using(SqlConnection conn=new SqlConnection(Projekat.CONNECTION_STRING))
            {
                conn.Open();
                DataSet ds = new DataSet();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = $"SELECT * FROM StavkaProdajeNamestaja s WHERE s.Id_Racun=Id_Racun ";
                SqlDataAdapter daStavkaNamestaja = new SqlDataAdapter();
                daStavkaNamestaja.SelectCommand = command;
                daStavkaNamestaja.Fill(ds, "StavkaProdajeNamestaja");
                Projekat.Instance.StavkaProdajeNamestaja.Clear();
                foreach(DataRow row in ds.Tables["StavkaProdajeNamestaja"].Rows)
                {
                    StavkaProdajeNamestaja s = new StavkaProdajeNamestaja();
                    s.Id = (int)row["Id"];
                    s.Id_Namestaj = (int)row["Id_Namestaj"];
                    s.Id_Racun = (int)row["Id_Racun"];
                    s.Kolicina = (int)row["Kolicina"];
                    s.Namestaj = Namestaj.GetById(s.id_Namestaj);

                    Projekat.Instance.StavkaProdajeNamestaja.Add(s);
                }
            }
        }
        public static void DodajStavkuProdajeNamestaja(StavkaProdajeNamestaja stavkaProdajeNamestaja)
        {
            using(SqlConnection conn=new SqlConnection(Projekat.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = $"INSERT INTO StavkaProdajeNamestaja (Id_Racun,Id_Namestaj,Kolicina)" +
                                                                    $"VALUES(@Id_Racun,@Id_Namestaj,@Kolicina)";
                command.Parameters.Add(new SqlParameter("@Id_Racun", stavkaProdajeNamestaja.Id_Racun));
                command.Parameters.Add(new SqlParameter("@Id_Namestaj", stavkaProdajeNamestaja.Id_Namestaj));
                command.Parameters.Add(new SqlParameter("@Kolicina", stavkaProdajeNamestaja.Kolicina));

                command.ExecuteNonQuery();
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

        public double vrednostJedneStavke()
        {
            double vrednost = namestaj.Cena * Kolicina;
            return vrednost;
        }
    }
}
