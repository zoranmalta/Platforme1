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
        private int id_Kupac;
        private int id_Zaposleni;
        private Zaposleni zaposleni;
        private Kupac kupac;
        public ObservableCollection<StavkaProdajeNamestaja> listaStavkiNamestaja { get; set; }
        public ObservableCollection<StavkaProdajeUsluge> listaStavkiUsluga { get; set; }

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
        public Zaposleni Zaposleni
        {
            get
            {
                if (zaposleni == null)
                {
                    zaposleni = Zaposleni.GetById(Id_Zaposleni);
                }
                return zaposleni;
            }
            set
            {
                zaposleni = value;
                Zaposleni.Id = zaposleni.Id;
                OnPropertyChanged("Zaposleni");
            }
        }
        public Racun()
        {
            this.Datum = DateTime.Today.Date;
            listaStavkiNamestaja = new ObservableCollection<StavkaProdajeNamestaja>();
            listaStavkiUsluga = new ObservableCollection<StavkaProdajeUsluge>();
        }
        public Racun(int id,DateTime datum,int Id_Kupac, Kupac kupac,int Id_Zaposleni,Zaposleni zaposleni)
        {
            this.id = id;
            this.Id_Kupac = Id_Kupac;
            this.Id_Zaposleni = Id_Zaposleni;
            this.datum = datum;
            this.kupac = kupac;
            this.zaposleni = zaposleni;
            listaStavkiNamestaja = new ObservableCollection<StavkaProdajeNamestaja>();
            listaStavkiUsluga = new ObservableCollection<StavkaProdajeUsluge>();
        }
        public static void UcitajRacune()
        {
            using (SqlConnection connection = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                connection.Open();

                DataSet ds = new DataSet();

                SqlCommand namestajCommand = connection.CreateCommand();
                namestajCommand.CommandText = @"SELECT * FROM Racun ";
                SqlDataAdapter daNamestaj = new SqlDataAdapter();
                daNamestaj.SelectCommand = namestajCommand;
                daNamestaj.Fill(ds, "Racun");

                foreach (DataRow row in ds.Tables["Racun"].Rows)
                {
                    Racun n = new Racun();
                    n.Id = (int)row["Id"];
                    n.Datum = (DateTime)row["Datum"];
                    n.Id_Zaposleni = (int)(row["Id_Zaposleni"]);
                    n.Id_Kupac = (int)row["Id_Kupac"];
                    n.Kupac = Kupac.GetById(n.Id_Kupac);
                    n.Zaposleni = Zaposleni.GetById(n.Id_Zaposleni);

                    StavkaProdajeNamestaja.UcitajStavkeRacuna(n, n.Id);
                    StavkaProdajeUsluge.UcitajStavkeRacuna(n, n.Id);
                    //DataSet ds2 = new DataSet();

                    //SqlCommand command1 = connection.CreateCommand();
                    //command1.CommandText = $"SELECT * FROM StavkaUsluge s WHERE s.Id_Racun=@Id ";
                    //SqlDataAdapter daStavkaUsluge = new SqlDataAdapter();
                    //daStavkaUsluge.SelectCommand = command1;
                    //daStavkaUsluge.Fill(ds2, "StavkaUsluge");
                    //command1.Parameters.Add(new SqlParameter("@Id", n.Id));
                    //foreach (DataRow row2 in ds2.Tables["StavkaUsluge"].Rows)
                    //{
                    //    StavkaProdajeUsluge s = new StavkaProdajeUsluge();
                    //    s.Id = (int)row2["Id"];
                    //    s.Id_Usluga = (int)row2["Id_Usluga"];
                    //    s.Usluga = Usluga.GetById(s.Id_Usluga);
                    //    s.Id_Racun = (int)row2["Id_Racun"];


                    //    n.listaStavkiUsluga.Add(s);
                    //}
                    Projekat.Instance.Racun.Add(n);
                }
            }
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
                totalPrice += s.UkupnaVrednostStavke();
            }
            foreach(var u in listaStavkiUsluga)
            {
                totalPrice += u.vrednostJedneStavke();
            }
            return totalPrice;
        }
        public double TotalPriceBezPDV()
        {
            return TotalPrice()*0.83;
        }
        public double TotalPricePDV()
        {
            return TotalPrice() *0.17;
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
