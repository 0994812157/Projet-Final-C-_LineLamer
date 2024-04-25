using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MaLibrairie
{
    public enum StatutTache
    {
        Afaire = 1,
        Encours = 2,
        Termine = 3,
        Enpause = 4,
        Annule = 5
    }

    public class Tache : INotifyPropertyChanged
    {
        private static int _compteurIdTache = 0;
        private int _idTache;
        private string _titre;
        private DateTime _dateEcheance;
        private int _priorite;
        private Membre _membre;
        private StatutTache _statut;

        public int IdTache
        {
            get { return _idTache; }
            set { _idTache = value; OnPropertyChanged(); }
        }

        public string Titre
        {
            get { return _titre; }
            set { _titre = value; OnPropertyChanged(); }
        }

        public DateTime DateEcheance
        {
            get { return _dateEcheance; }
            set { _dateEcheance = value; OnPropertyChanged(); }
        }

        public int Priorite
        {
            get { return _priorite; }
            set { _priorite = value; OnPropertyChanged(); }
        }

        public Membre Membre
        {
            get { return _membre; }
            set { _membre = value; OnPropertyChanged(); }
        }

        public StatutTache Statut
        {
            get { return _statut; }
            set { _statut = value; OnPropertyChanged(); }
        }

        public Tache()
        {
            _idTache = _compteurIdTache++;
            _titre = "Nouvelle Tâche";
            _dateEcheance = DateTime.Now;
            _priorite = 1; // Supposons que 1 est la priorité la plus basse
            _membre = new Membre(); // Assurez-vous que Membre a un constructeur par défaut
            _statut = StatutTache.Afaire;
        }

        public Tache(int idTache, string titre, DateTime dateEcheance, int priorite, Membre membre, StatutTache statut)
        {
            _idTache = idTache;
            _titre = titre;
            _dateEcheance = dateEcheance;
            _priorite = priorite;
            _membre = membre;
            _statut = statut;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
