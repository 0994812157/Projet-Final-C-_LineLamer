using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;




using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MaLibrairie
{
    public class Projet : INotifyPropertyChanged
    {
        private static int compteurIdProjet = 0;
        private int _idProjet;
        private string _nom;
        private string _description;
        private DateTime _dateDebut;
        private DateTime _dateFin;
        private ObservableCollection<Tache> _taches;


        public int IdProjet
        {
            get { return _idProjet; }
            set { _idProjet = value; OnPropertyChanged(); }
        }

        public string Nom
        {
            get { return _nom; }
            set { _nom = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }

        public DateTime DateDebut
        {
            get { return _dateDebut; }
            set { _dateDebut = value; OnPropertyChanged(); }
        }

        public DateTime DateFin
        {
            get { return _dateFin; }
            set { _dateFin = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Tache> Taches
        {
            get { return _taches; }
            set { _taches = value; OnPropertyChanged(); }
        }


        public Projet()
        {
            _idProjet = compteurIdProjet++;
            _nom = "Nouveau Projet";
            _description = "Description par défaut";
            _dateDebut = DateTime.Now;
            _dateFin = DateTime.Now.AddMonths(1);
            _taches = new ObservableCollection<Tache>();
        }

        public Projet(int idProjet, string nom, string description, DateTime dateDebut, DateTime dateFin)
        {
            _idProjet = idProjet;
            _nom = nom;
            _description = description;
            _dateDebut = dateDebut;
            _dateFin = dateFin;
            _taches = new ObservableCollection<Tache>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
