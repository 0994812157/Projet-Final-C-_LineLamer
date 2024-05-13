using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Reflection.Metadata;
using System.Windows.Controls;
using MaLibrairie;
//using static Projet_Final_CSharp_LineLamer.MainWindow;

namespace Projet_Final_CSharp_LineLamer
{

    public class StatutColor : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2 || !(values[0] is DateTime) || !(values[1] is DateTime))
                return Brushes.Black;  // Retourne une couleur par défaut si les valeurs ne sont pas valides

            DateTime dateDebut = (DateTime)values[0];
            DateTime dateFin = (DateTime)values[1];
            DateTime aujourdHui = DateTime.Now;
            DateTime debutMois = new DateTime(aujourdHui.Year, aujourdHui.Month, 1);
            DateTime finMois = debutMois.AddMonths(1).AddDays(-1);

            if (dateFin.Date == aujourdHui.Date)
            {
                return new SolidColorBrush(Color.FromRgb(255, 179, 179)); // Rouge clair pour "Terminé"
            }
            else if (dateDebut > aujourdHui)
            {
                return new SolidColorBrush(Color.FromRgb(173, 216, 230)); // Bleu clair pour "À faire"
            }
            else if (debutMois <= dateFin && dateFin <= finMois)
            {
                return new SolidColorBrush(Color.FromRgb(255, 165, 0)); // Orange clair pour "En cours"
            }
            else if (dateFin < aujourdHui)
            {
                return Brushes.Red; // Rouge pour "Annulé"
            }

            return Brushes.Orange; // Orange pour "En pause" ou autres cas non spécifiés
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
/*
a faire si la date de debut est apres la date du jour: bleu claire
terminé si la date de fin est = a la date du jour : rouge claire
en cours si le mois de la date de fin de est le meme que le mois en en cours de la meme année : orange clair
annule si la date de fin est apres la date du jour
*/
/*

Afaire = 1, //bleu
Encours = 2, //gris // tout le mois
Termine = 3, //vert // date de fin 
Enpause = 4, //orange // ou il nya pas de taches
Annule = 5 //rouge // bouton supprimé change le statut en Annulé

*/