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
                connection.Open();

                DataSet ds = new DataSet();

                SqlCommand tipNamestajaCommand = connection.CreateCommand();
                tipNamestajaCommand.CommandText = @"SELECT * FROM TipNamestaja";
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
        }
        #endregion
    }
}
