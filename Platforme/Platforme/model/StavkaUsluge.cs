using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platforme.model
{
    public class StavkaUsluge: INotifyPropertyChanged
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value;
                OnProtertyChanged("id");
            }
        }
        private int id_Usluga;

        public int Id_Usluga
        {
            get { return id_Usluga; }
            set { id_Usluga = value;
                OnProtertyChanged("Id_Usluga");
            }
        }
        private int id_Racun;

        public int Id_Racun
        {
            get { return id_Racun; }
            set { id_Racun = value;
                OnProtertyChanged("Id_Racun");
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
