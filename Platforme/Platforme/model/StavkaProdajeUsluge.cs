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
    public class StavkaProdajeUsluge : INotifyPropertyChanged
    {
        private int id;
        private int id_Racun;
        private Usluga usluga;
        private int id_Usluga;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnProtertyChanged("id");
            }
        }

        public int Id_Racun
        {
            get { return id_Racun; }
            set
            {
                id_Racun = value;
                OnProtertyChanged("id_Racun");
            }
        }

        public Usluga Usluga
        {
            get
            {
                if (usluga == null)
                {
                    usluga = Usluga.GetById(Id_Usluga);
                }
                return usluga;
            }
            set
            {
                usluga = value;
                OnProtertyChanged("usluga");
            }
        }

        public int Id_Usluga
        {
            get { return id_Usluga; }
            set
            {
                id_Usluga = value;
                OnProtertyChanged("id_Usluga");
            }
        }

        public StavkaProdajeUsluge() { }

        public StavkaProdajeUsluge(int Id, int Id_Racun, Usluga usluga, int Id_Usluga)
        {
            this.Id = Id;
            this.Id_Racun = Id_Racun;
            this.usluga = usluga;
            this.Id_Usluga = Id_Usluga;
        }

        public override string ToString()
        {
            return $"{usluga.Naziv}";
        }
        public static void UcitajStavkeRacuna(Racun racun,int Id_Racun)
        {
            using (SqlConnection conn = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                conn.Open();
                DataSet ds = new DataSet();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = $"SELECT * FROM StavkaUsluge s WHERE s.Id_Racun=@Id_Racun ";
                command.Parameters.Add(new SqlParameter("@Id_Racun", Id_Racun));
                SqlDataAdapter daStavkaNamestaja = new SqlDataAdapter();
                daStavkaNamestaja.SelectCommand = command;
                daStavkaNamestaja.Fill(ds, "StavkaUsluge");
                Projekat.Instance.StavkaProdajeUsluge.Clear();
                foreach (DataRow row in ds.Tables["StavkaUsluge"].Rows)
                {
                    StavkaProdajeUsluge s = new StavkaProdajeUsluge();
                    s.Id = (int)row["Id"];
                    s.Id_Usluga = (int)row["Id_Usluga"];
                    s.Usluga = Usluga.GetById(s.Id_Usluga);
                    s.Id_Racun = (int)row["Id_Racun"];

                    racun.listaStavkiUsluga.Add(s);
                }
            }
        }
        public static void DodajStavkuProdajeUsluga(StavkaProdajeUsluge stavkaProdajeUsluge)
        {
            using (SqlConnection conn = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = $"INSERT INTO StavkaUsluge (Id_Racun,Id_Usluga)" +
                                                                    $"VALUES(@Id_Racun,@Id_Usluga)";
                command.Parameters.Add(new SqlParameter("@Id_Racun", stavkaProdajeUsluge.Id_Racun));
                command.Parameters.Add(new SqlParameter("@Id_Usluga", stavkaProdajeUsluge.Id_Usluga));

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
            double vrednost =usluga.Cena;
            return vrednost;
        }

    }
}
