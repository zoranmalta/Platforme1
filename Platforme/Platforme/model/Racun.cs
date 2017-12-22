using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Platforme.model
{
    public class Racun : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int id;
        private DateTime datum;
        private Kupac kupac;
        private int id_Kupac;
        private int id_Zaposleni;
        [XmlIgnore]
        public ObservableCollection<StavkaProdajeNamestaja> listaStavkiNamestaja { get; set; }

        public int Id
        {
            get { return id; }
            set { id = value;
                OnPropertyChanged("id");
            }
        }
        public DateTime Datum
        {
            get { return datum; }
            set { datum = value;
                OnPropertyChanged("datum");
            }
        }
        [XmlIgnore]
        public Kupac Kupac
        {
            get { if (kupac == null)
                {
                    kupac = Kupac.GetById(Id_Kupac);
                }
                return kupac;
            }
            set { kupac = value;
                    Kupac.Id = kupac.Id;
                OnPropertyChanged("Kupac");
            }
        }
        public int Id_Kupac
        {
            get { return id_Kupac; }
            set { id_Kupac = value;
                OnPropertyChanged("id_Kupac");
            }
        }
        public int Id_Zaposleni
        {
            get { return id_Zaposleni; }
            set { id_Zaposleni = value;
                OnPropertyChanged("id_Zaposleni");
            }
        }
        public Racun()
        {
            this.Datum = DateTime.Today;
            listaStavkiNamestaja = new ObservableCollection<StavkaProdajeNamestaja>();
        }
        public Racun(int id,DateTime datum,int Id_Kupac, Kupac kupac,int Id_Zaposleni)
        {
            this.id = id;
            this.Id_Kupac = Id_Kupac;
            this.Id_Zaposleni = Id_Zaposleni;
            this.datum = datum;
            this.kupac = kupac;
            listaStavkiNamestaja = new ObservableCollection<StavkaProdajeNamestaja>();
        }
        public static void UcitajRacune()
        {

        }
        public static void DodajRacun(Racun racun)
        {
            using (SqlConnection conn = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = $"INSERT INTO Racun (Id_Kupac,Id_Zaposleni,Datum) " +
                                                 $"VALUES(@Id_Kupac,@Id_Zaposleni,@Datum)";

                command.Parameters.Add(new SqlParameter("@Id_Kupac", racun.Id_Kupac));
                command.Parameters.Add(new SqlParameter("@Id_Zaposleni", racun.Id_Zaposleni));
                command.Parameters.Add(new SqlParameter("@Datum", racun.Datum));

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
                namestajCommand.CommandText = @"SELECT Id FROM Racun ";
                SqlDataAdapter daNamestaj = new SqlDataAdapter();
                daNamestaj.SelectCommand = namestajCommand;
                daNamestaj.Fill(ds, "Racun");
                int max = 0;
                foreach (DataRow row in ds.Tables["Racun"].Rows)
                {
                    Racun n = new Racun();
                    n.Id = (int)row["Id"];

                    if (n.Id > max)
                    {
                        max = n.Id;
                    }

                }
                return max;
            }
        }


        public double TotalPrice()
        {
            double totalPrice = 0;
            foreach(var s in listaStavkiNamestaja)
            {
                totalPrice += s.vrednostJedneStavke();
            }
            return totalPrice;
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
