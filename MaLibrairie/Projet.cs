using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;


namespace MaLibrairie
{
    [Serializable]
    public class Projet : INotifyPropertyChanged
    {
        private static int compteurIdProjet = 0;
        private int _idProjet;
        private string _nom;
        private DateTime _dateFin;
        private DateTime _dateDebut;
        private ObservableCollection<Tache> _taches;
        private string _statut;


        public int IdProjet
        {
            get { return _idProjet; }
            set { _idProjet = value; OnPropertyChanged(); }
        }

        public string Statut
        {
            get { return _statut; }
            set { _statut = value; OnPropertyChanged(); }
        }

        public DateTime DateDebut
        {
            get { return _dateDebut; }
            set 
            {
                if (value < DateTime.Now)
                {
                    _dateDebut = value;
                    OnPropertyChanged();
                }
                else
                {
                    throw new ArgumentException("La date de debut ne doit pas être avant la date du jour.");
                }
            }
        }

        public string Nom
        {
            get { return _nom; }
            set { _nom = value; OnPropertyChanged(); }
        }

        public DateTime DateFin
        {
            get { return _dateFin; }
            set
            {
                if (value.Date < _dateDebut.Date)
                {
                    throw new ArgumentException("La date de fin doit être après la date de début.");
                }
                else
                {
                    _dateFin = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Tache> Taches
        {
            get { return _taches; }
            set { _taches = value; OnPropertyChanged(); }
        }

        public Projet()
        {
            _idProjet = ++compteurIdProjet;
            _dateDebut = DateTime.Now;
            _taches = new ObservableCollection<Tache>();
        }

        public Projet(string nom, DateTime dateFin, DateTime datedebut, ObservableCollection<Tache> taaches)
        {
            _nom = nom;
            _dateDebut = datedebut;
            _dateFin = dateFin;
            _taches = taaches;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
