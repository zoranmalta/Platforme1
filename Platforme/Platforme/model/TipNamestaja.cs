using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platforme.model
{
    public class TipNamestaja :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int id;
        private string naziv;
        private bool obrisan;

        public int Id
        {
            get { return id; }
            set { id = value;
                OnPropertyChanged("id");
            }
        }

        public string Naziv
        {
            get { return naziv; }
            set { naziv = value;
                OnPropertyChanged("naziv");
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
            TipNamestaja kopija = new TipNamestaja();
            kopija.Id = Id;
            kopija.Naziv = Naziv;
            kopija.Obrisan = Obrisan;
            return kopija;
        }

        public TipNamestaja() { }

        public TipNamestaja(int Id, string Naziv, bool Obrisan)
        {
            this.Id = Id;
            this.Naziv = Naziv;
            this.Obrisan = Obrisan;
        }
        public override string ToString()
        {
            return Naziv;
        }
        public static TipNamestaja GetById(int Id)
        {
            foreach (var tipNamestaja in Projekat.Instance.TipNamestaja)
            {
                if (tipNamestaja.Id == Id)
                {
                    return tipNamestaja;
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
        #region Database
        public static void UcitajTipNamestaja()
        {
            using (SqlConnection connection=new SqlConnection(Projekat.CONNECTION_STRING))
            {
                try
                {
                    connection.Open();

                    DataSet ds = new DataSet();

                    SqlCommand tipNamestajaCommand = connection.CreateCommand();
                    tipNamestajaCommand.CommandText = @"SELECT * FROM TipNamestaja ";
                    SqlDataAdapter daTipNamestaja = new SqlDataAdapter();
                    daTipNamestaja.SelectCommand = tipNamestajaCommand;
                    daTipNamestaja.Fill(ds, "TipNamestaja");

                    foreach (DataRow row in ds.Tables["TipNamestaja"].Rows)
                    {
                        TipNamestaja t = new TipNamestaja();
                        t.Id = (int)row["Id"];
                        t.Naziv = (string)row["Naziv"];
                        t.Obrisan = (bool)row["Obrisan"];

                        Projekat.Instance.TipNamestaja.Add(t);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Nije uspela sql naredba");
                    return;
                }
            }
        }
        #endregion
        public static void DodajTipNamestaja(TipNamestaja tipNamestaja)
        {
            using (SqlConnection conn = new SqlConnection(Projekat.CONNECTION_STRING))
            {
                try
                {
                    conn.Open();
                    SqlCommand command = conn.CreateCommand();
                    command.CommandText = $"INSERT INTO TipNamestaja (Naziv,Obrisan) VALUES (@Naziv,@Obrisan)";

                    command.Parameters.Add(new SqlParameter("@Naziv", tipNamestaja.Naziv));
                    command.Parameters.Add(new SqlParameter("@Obrisan", tipNamestaja.Obrisan));

                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    Console.WriteLine("Nije uspela sql naredba");
                    return;
                }
            }
        }
        public static void IzmeniTipNamestaja(TipNamestaja tipNamestaja)
        {
            using(SqlConnection conn=new SqlConnection(Projekat.CONNECTION_STRING))
            {
                try
                {
                    if (tipNamestaja.Id != 0)
                    {
                        conn.Open();
                        SqlCommand command = conn.CreateCommand();
                        command.CommandText = $"UPDATE TipNamestaja SET Naziv=@Naziv,Obrisan=@Obrisan WHERE Id=@Id";

                        command.Parameters.Add(new SqlParameter("@Naziv", tipNamestaja.Naziv));
                        command.Parameters.Add(new SqlParameter("@Obrisan", tipNamestaja.Obrisan));
                        command.Parameters.Add(new SqlParameter("@Id", tipNamestaja.Id));

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
        public static void ObrisiTipNamestaja(TipNamestaja tipNamestaja)
        {
            using(SqlConnection conn=new SqlConnection(Projekat.CONNECTION_STRING))
            {
                try
                {
                    if (tipNamestaja.Id != 0)
                    {
                        conn.Open();
                        SqlCommand command = conn.CreateCommand();
                        command.CommandText = $"DELETE FROM TipNamestaja WHERE Id=@Id";

                        command.Parameters.Add(new SqlParameter("@Id", tipNamestaja.Id));

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
    }
}
