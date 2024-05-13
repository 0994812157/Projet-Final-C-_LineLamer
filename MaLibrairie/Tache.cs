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
        private static int _compteurIdTache = 0;
        private int _idTache;
        private string _titre;
        private Membre _membre;

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

        public Membre Membre
        {
            get { return _membre; }
            set { _membre = value; OnPropertyChanged(); }
        }

        public Tache()
        {
            _idTache = ++_compteurIdTache;
            _titre = "Nouvelle Tâche";
            _membre = new Membre(); 
        }

        public Tache(string titre, Membre membre)
        {
            _titre = titre;
            _membre = membre;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
