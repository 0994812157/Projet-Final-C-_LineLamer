using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MaLibrairie
{
    public class Membre : INotifyPropertyChanged
    {
        private static int id=0;
        private int _idMembre;
        private string _nom;
        private string _prenom;
        private Image _photo;
        private string _role;

        public int IdMembre
        {
            get { return _idMembre; }
            set { _idMembre = value; OnPropertyChanged(); }
        }

        public string Nom
        {
            get { return _nom; }
            set { _nom = value; OnPropertyChanged(); }
        }

        public string Prenom
        {
            get { return _prenom; }
            set { _prenom = value; OnPropertyChanged(); }
        }

        public Image Photo
        {
            get { return _photo; }
            set { _photo = value; OnPropertyChanged(); }
        }

        public string Role
        {
            get { return _role; }
            set { _role = value; OnPropertyChanged(); }
        }

        public Membre() : this("inconnu", "inconnu", new Image(), "admin")
        {
            _idMembre = ++id;
        }

        public override string ToString()
        {
            return IdMembre+" " + Nom + " " + Prenom + " " + Photo + " " + Role;
        }

        public Membre( string nom, string prenom, Image photo, string role)
        {
            _idMembre = ++id;
            _nom = nom;
            _prenom = prenom;
            _photo = photo;
            _role = role;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }


}
