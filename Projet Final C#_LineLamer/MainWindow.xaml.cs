using Microsoft.Win32;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;
using MaLibrairie;
using System.Collections.ObjectModel;
using MaLibrairie;
using System.Xml.Serialization;

namespace Projet_Final_CSharp_LineLamer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public enum StatutProjet
        {
            Afaire,   // à faire
            Encours,  // en cours
            Termine,  // terminé
            Enpause,  // en pause
            Annule    // annulé
        }

        //MyData ViewModel = null;
        private DateTime dateDebut {  get; set; }
        private DateTime dateLimite { get; set; }
        private string nomProjet { get; set; }
        private string statutProjet { get; set; }
        public ObservableCollection<Projet> ListProjet { get; set; }

        private bool isProjectNameLocked = false; // État de verrouillage du nom du projet
        private bool isDateLocked = false;
        private bool isFolderLocked = false;

        public MyData Data { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            ListProjet = new ObservableCollection<Projet>();

            //this.Data = new MyData();
            /*ViewModel = new MyData();
            DataContext = ViewModel;*/
            //----------------------------------------//
            RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\HEPL\\TestRegistry")!;
            if (registryKey2 != null)
            {
                this.Left = (int)registryKey2.GetValue("XPosition", 0);
                this.Top = (int)registryKey2.GetValue("YPosition", 0);
            }

            if (File.Exists("d:\\testjson.json"))
            {
                JsonSerializerOptions options = new()
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };

                Data = JsonSerializer.Deserialize<MyData>(File.ReadAllText("d:\\testjson.json"), options)!;
            }
            else
            {
                Data = new MyData();
            }

            DataContext = Data;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("Voullez-vous sauvegarder les modification ?", "Sauvegarder ?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                //e.Cancel = true;
                RegistryKey registryKey1 = Registry.CurrentUser.CreateSubKey("SOFTWARE\\HEPL\\TestRegistry");
                if (registryKey1 != null)
                {
                    registryKey1.SetValue("XPosition", (int)this.Left);
                    registryKey1.SetValue("YPosition", (int)this.Top);
                }
            }
            else
            {
                RegistryKey registryKey1 = Registry.CurrentUser.CreateSubKey("SOFTWARE\\HEPL\\TestRegistry");

                if (registryKey1 != null)
                {
                    registryKey1.SetValue("XPosition", (int)this.Left);
                    registryKey1.SetValue("YPosition", (int)this.Top);
                    jSaveToJson();
                }
            }
        }

        private void jSaveToJson()
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
            File.WriteAllText("d:\\testjson.json", JsonSerializer.Serialize(Data, options));
        }
        /*----------------------------------------------------------------*/
        private void MenuItem_Ajouter_Click(object sender, RoutedEventArgs e)
        {
            /*
            nomProjet = txtParticipant.Text;
            dateLimite = dpDateLimite.SelectedDate ?? DateTime.Now;

            if (nomProjet != null)
            {
                Projet projet = new Projet
                {
                    //DateFin = dateLimite,
                    Nom = nomProjet,
                    //Membres = new ObservableCollection<Membre>(),
                    Taches = new ObservableCollection<Tache>(Data.ListTache),
                    DateFin = dateLimite
                };

                nomDuProject.Text = nomProjet;
                Data.ListProjet.Add(projet);

            }
            else
                MessageBox.Show("Selectionnez un membre pour cette Tâche.");

            // Masquer le formulaire après avoir ajouté les données
            //participantForm.Visibility = Visibility.Collapsed;
            txtParticipant.Clear();
            nomDuProject.Text = "Création d'un Projet";
            Data.ListTache.Clear();*/
            if (isProjectNameLocked)  // Ensure the project details are validated before adding
            {
                try
                {
                    string statutValue =  (cmbStatut.SelectedItem as ComboBoxItem).Tag.ToString();

                    Projet projet = new Projet
                    {
                        Nom = nomProjet,
                        DateFin = dpDateLimite.SelectedDate ?? DateTime.Now,
                        Taches = new ObservableCollection<Tache>(Data.ListTache),
                        Statut = statutValue
                    };

                    Data.ListProjet.Add(projet);
                    ResetProjectCreationForm(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Une erreur a été provoqué1 : " + ex.Message); 
                }
            }
            else
            {
                MessageBox.Show("Validez le projet avant de l'ajouter.");
            }


        }
        private void ResetProjectCreationForm()
        {
            txtParticipant.Clear();
            txtParticipant.IsReadOnly = false;
            dpDateLimite.IsEnabled = true;
            dpDateLimite.SelectedDate = null;

            cmbStatut.IsEnabled = true;
            cmbStatut.SelectedIndex = -1;

            isProjectNameLocked = false;
            isDateLocked = false;
            nomDuProject.Text = "Création d'un Projet";
            Data.ListTache.Clear();
        }

        private void MenuItem_Supprimer_Click(object sender, RoutedEventArgs e)
        {
            if (Data.CurrentProjet != null)
            {
                var confirmation = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer le projet '{Data.CurrentProjet.Nom}' et toutes ses tâches associées ?", "Confirmation de suppression", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (confirmation == MessageBoxResult.Yes)
                {
                    if (Data.CurrentProjet.Taches != null && Data.CurrentProjet.Taches.Count > 0)
                    {
                        Data.CurrentProjet.Taches.Clear();  
                    }

                    // Suppression du projet de la liste
                    Data.ListProjet.Remove(Data.CurrentProjet);
                    Data.CurrentProjet = null;  

                    MessageBox.Show("Le projet et ses tâches associées ont été supprimés.");
                }
            }
            else
            {
                MessageBox.Show("Aucun projet n'est sélectionné pour la suppression.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuItem_ListEmployee_Click(object sender, RoutedEventArgs e)
        {
            ListEmployee listEmployee = new ListEmployee();
            //listEmployee.Owner = this;
            listEmployee.ShowDialog();
        }

        private void AjouterParticipant_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTache.Text) || cmbMembres.SelectedItem == null)
            {
                MessageBox.Show("Veuillez remplir la tâche et sélectionner un membre.");
                return;
            }

            var nouvelleTache = new Tache
            {
                Titre = txtTache.Text,
                Membre = cmbMembres.SelectedItem as Membre
            };

            Data.ListTache.Add(nouvelleTache);

            txtTache.Clear();
            cmbMembres.SelectedIndex = -1;
            //participantForm.Visibility = Visibility.Visible;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(txtParticipant.Text) && !isProjectNameLocked)
            {
                nomProjet = txtParticipant.Text;
                dateLimite = dpDateLimite.SelectedDate ?? DateTime.Now;  
                statutProjet = cmbStatut.SelectedItem as String; 
                dateDebut = dpDateDeDebut.SelectedDate ?? DateTime.Now;
                /*if (statutProjet != null)
                {
                    statutProjet.
                }*/

                nomDuProject.Text = "Nom du Projet : " + nomProjet;
                txtParticipant.IsReadOnly = true;  // Verrouille le champ du nom
                dpDateLimite.IsEnabled = false;    // Verrouille le DatePicker
                dpDateDeDebut.IsEnabled = false;

                cmbStatut.IsEnabled = false;
                isFolderLocked = true;

                isProjectNameLocked = true;        // Marquez que le nom du projet est verrouillé
                isDateLocked = true;               // Marquez que la date est verrouillée
            }
            else
            {
                MessageBox.Show("Veuillez entrer un nom pour le projet.");
            }

            //participantForm.Visibility = Visibility.Collapsed;
        }


        private void MenuItem_Import_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Files (*.xml)|*.xml";
            if (saveFileDialog.ShowDialog() == true)
            {
                ExporterProjets(saveFileDialog.FileName, Data.ListProjet);
            }
        }

        private void MenuItem_Exporter_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Files (*.xml)|*.xml";
            if (openFileDialog.ShowDialog() == true)
            {
                Data.ListProjet = ImporterProjets(openFileDialog.FileName);
            }
        }

        public void ExporterProjets(string cheminFichier, ObservableCollection<Projet> projets)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Projet>));
            try
            {
                using (StreamWriter writer = new StreamWriter(cheminFichier))
                {
                    serializer.Serialize(writer, projets);
                }
                MessageBox.Show("Exportation réussie.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'exportation : {ex.Message}");
            }
        }

        public ObservableCollection<Projet> ImporterProjets(string cheminFichier)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Projet>));
            try
            {
                using (StreamReader reader = new StreamReader(cheminFichier))
                {
                    return (ObservableCollection<Projet>)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'importation : {ex.Message}");
                return null;
            }
        }


    }
}
