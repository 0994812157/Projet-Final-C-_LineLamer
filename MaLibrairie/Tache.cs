using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MaLibrairie
{
    public class Tache : INotifyPropertyChanged
    {
        private int _idTache;
        private string _titre;
        private DateTime _dateEcheance;
        private int _priorite;
        private Membre _membre;
        private Projet _projet;

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
            set { _membre = value; OnPropertyChanged(); } // Corrigé pour inclure la notification de changement
        }
        public Projet Projet
        {
            get { return _projet; }
            set { _projet = value; OnPropertyChanged(); } // Corrigé pour inclure la notification de changement
        }

        // Constructeur par défaut qui initialise une tâche avec des valeurs par défaut
        // Notez que la création de nouvelles instances de Membre et Projet sans paramètres peut nécessiter des constructeurs par défaut dans ces classes.
        public Tache() : this(0, "Nouvelle Tâche", DateTime.Now, 1, new Membre(), new Projet())
        {

        }

        public Tache(int idTache, string titre, DateTime dateEcheance, int priorite, Membre membre, Projet projet)
        {
            _idTache = idTache;
            _titre = titre;
            _dateEcheance = dateEcheance;
            _priorite = priorite;
            _membre = membre;
            _projet = projet;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
