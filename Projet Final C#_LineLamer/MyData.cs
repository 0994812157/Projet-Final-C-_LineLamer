using System;
using MaLibrairie;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Shapes;

namespace Projet_Final_CSharp_LineLamer
{
    public class MyData : INotifyPropertyChanged
    {
        private ObservableCollection<Membre> _listEmployee;// = new ObservableCollection<Membre>(); // Initialisez la collection ici
        private ObservableCollection<Tache> _listTache;
        private ObservableCollection<Projet> _listProjet;
        //private ObservableCollection<String> _statutProjet;
        
        public ObservableCollection<Membre> ListEmployee
        {
            get { return _listEmployee; }
            set
            {
                if (_listEmployee != value)
                {
                    _listEmployee = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /*
        public String StatutProjet
        {
            get { return _statutProjet; }
            set
            {
                if (_statutProjet != value)
                {
                    _statutProjet = value;
                    NotifyPropertyChanged();
                }
            }
        }*/
        public ObservableCollection<Tache> ListTache
        {
            get { return _listTache; }
            set
            {
                if (_listTache != value)
                {
                    _listTache = value;
                    NotifyPropertyChanged();
                }
            }
        }
        
        public ObservableCollection<Projet> ListProjet
        {
            get { return _listProjet; }
            set
            {
                if (_listProjet != value)
                {
                    _listProjet = value;
                    NotifyPropertyChanged();
                }
            }
        }

        
        private Tache _currentTache;
        public Tache CurrentTache
        {
            get { return this._currentTache; }
            set
            {
                if (_currentTache != value)
                {
                    _currentTache = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private Membre _currentEmployee;
        public Membre CurrentEmployee
        {
            get { return _currentEmployee; }
            set
            {
                if (_currentEmployee != value)
                {
                    _currentEmployee = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Projet _currentProjet;
        public Projet CurrentProjet
        {
            get { return _currentProjet; }
            set
            {
                if (_currentProjet != value)
                {
                    _currentProjet = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public MyData()
        {

            // Initialisez ListEmployee ici
            ListEmployee = new ObservableCollection<Membre>();
            /*
            ListEmployee.Add(new Membre("Caprasse", "François", new MaLibrairie.Image("mon image1"), "manager"));
            ListEmployee.Add(new Membre("Wagner", "Jean-Marc", new MaLibrairie.Image("mon image2"), "employe"));
            ListEmployee.Add(new Membre("Lamer", "Line", new MaLibrairie.Image("mon image3"), "manager"));
            ListEmployee.Add(new Membre("Pierrot", "Albert", new MaLibrairie.Image("mon image4"), "employe"));
            CurrentEmployee = ListEmployee.ElementAt(0);
            */

            ListProjet = new ObservableCollection<Projet>();
            /*
            ListProjet.Add(new Projet
            {
                Nom = "coucou",
                DateFin = DateTime.Now,
                Taches = new ObservableCollection<Tache>
                {
                    new Tache("moi", new Membre { Nom = "Lamer" }),
                    new Tache("toi", new Membre { Nom = "capr" })
                }
            });
            CurrentProjet = ListProjet.ElementAt(0);*/

            ListTache = new ObservableCollection<Tache>();
            /*ListTache.Add(new Tache("moi", new Membre { Nom = "Lamer" }));
            ListTache.Add(new Tache("toi", new Membre { Nom = "Caprasse" }));
            ListTache.Add(new Tache("nous", new Membre { Nom = "Toto" }));
            ListTache.Add(new Tache("vous", new Membre { Nom = "Pierrot" }));
            CurrentTache = ListTache.ElementAt(0);*/

        }


        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }


   
}
