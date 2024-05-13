using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MaLibrairie;
using Microsoft.Win32;
using System.IO; // Assurez-vous d'ajouter cette directive pour utiliser OpenFileDialog

namespace Projet_Final_CSharp_LineLamer
{
    /// <summary>
    /// Interaction logic for ListEmployee.xaml
    /// </summary>
    public partial class ListEmployee : Window
    {
        private int _idMembre { get; set; }
        private string _nomMembre { get; set; }
        private string _prenomMembre { get; set; }
        private string _fonctionMembre { get; set; }
        private MaLibrairie.Image _imageMembre { get; set; }


        public MyData Data { get; set; }

        public ListEmployee()
        {
            InitializeComponent();
            //Data = new MyData(); // Créez une instance de votre classe MyData
            //DataContext = Data; // Définissez votre instance MyData comme DataContext de la fenêtre

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


        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            // Créer une instance de OpenFileDialog pour choisir une image
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (openFileDialog.ShowDialog() == true)
            {
                // Supposons que vous avez une propriété ou un champ pour stocker le chemin de l'image
                _imageMembre = new MaLibrairie.Image(openFileDialog.FileName);
                // Ici, vous devriez mettre à jour l'UI ou le modèle de données avec ce chemin
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            if (radioButton != null && radioButton.IsChecked == true)
            {
                //_fonctionMembre = RadioButtonÉquipedeDéveloppement.ToString();

                if (radioButton == RadioButtonStakeholders)
                {
                    _fonctionMembre = "Stake holders";
                }
                else if (radioButton == RadioButtonProductOwner)
                {
                    _fonctionMembre = "Product Owner";
                }
                else if (radioButton == RadioButtonScrumMasterr)
                {
                    _fonctionMembre = "Scrum Masterr";
                }
                else if (radioButton == RadioButtonÉquipedeDéveloppement)
                {
                    _fonctionMembre = "Équipe de Développement";
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Récupérer les données des TextBox
            //_idMembre = int.Parse(TextBoxIdMembre.Text);
            _nomMembre = TextBoxNomMembre.Text.ToString();
            _prenomMembre = TextBoxPrenomMembre.Text.ToString();
        }

        // Assurez-vous de connecter ces méthodes aux événements Click de vos boutons si nécessaire
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            // Créez un nouvel objet Employee et ajoutez-le à la liste observable de MyData
            Membre newEmployee = new Membre
            {
                //IdMembre = _idMembre,
                Nom = _nomMembre,
                Prenom = _prenomMembre,
                Role = _fonctionMembre,
                Photo = _imageMembre // Supposons que vous avez stocké le chemin de l'image dans _imageMembre
            };

            // Ajoutez le nouvel employé à la liste observable de MyData
            Data.ListEmployee.Add(newEmployee);


            // Réinitialisez les champs après l'ajout

            _idMembre = 0;
            _nomMembre = string.Empty;
            _prenomMembre = string.Empty;
            _fonctionMembre = string.Empty;
            _imageMembre = null;

            MessageBox.Show("ajouté avec succes");
            TextBoxNomMembre.Text = "";
            TextBoxPrenomMembre.Text = "";
            RadioButtonStakeholders.IsChecked = false;
            RadioButtonScrumMasterr.IsChecked = false;
            RadioButtonProductOwner.IsChecked = false;
            RadioButtonÉquipedeDéveloppement.IsChecked = false;
            //openFileDialog.InitialDirectory = "";
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Logique pour supprimer un employé
            if (Data.CurrentEmployee != null)
            {
                // Accédez à l'employé actuellement sélectionné
                Membre selectedEmployee = Data.CurrentEmployee;

                // Implémentez la logique pour supprimer l'employé sélectionné de la liste
                // Par exemple :
                Data.ListEmployee.Remove(selectedEmployee);

                // Réinitialisez les champs après la suppression si nécessaire
                // Par exemple :
                _idMembre = 0;
                _nomMembre = string.Empty;
                _prenomMembre = string.Empty;
                _fonctionMembre = string.Empty;
                _imageMembre = null;
            }
            else
                MessageBox.Show("veuillez selectionner une ligne");
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
        private bool EnsureDetailsDisplayed()
        {
            if (Data.CurrentEmployee != null)
            {
                idMembre.Text = Data.CurrentEmployee.IdMembre.ToString();
                TextBoxNomMembre.Text = Data.CurrentEmployee.Nom;
                TextBoxPrenomMembre.Text = Data.CurrentEmployee.Prenom;

                switch (Data.CurrentEmployee.Role)
                {
                    case "Stake holders":
                        RadioButtonStakeholders.IsChecked = true;
                        _fonctionMembre = "Stake holders";
                        break;
                    case "Product Owner":
                        RadioButtonProductOwner.IsChecked = true;
                        _fonctionMembre = "Product Owner";
                        break;
                    case "Scrum Masterr":
                        RadioButtonScrumMasterr.IsChecked = true;
                        _fonctionMembre = "Scrum Masterr";
                        break;
                    case "Équipe de Développement":
                        RadioButtonÉquipedeDéveloppement.IsChecked = true;
                        _fonctionMembre = "Équipe de Développement";
                        break;
                    default:
                        RadioButtonStakeholders.IsChecked = false;
                        RadioButtonProductOwner.IsChecked = false;
                        RadioButtonScrumMasterr.IsChecked = false;
                        RadioButtonÉquipedeDéveloppement.IsChecked = false;
                        break;
                }

                string img = null;
                img = Data.CurrentEmployee.Photo.ImagePath;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(img, UriKind.RelativeOrAbsolute); // Supposons que le chemin de l'image est stocké dans la propriété "Chemin" de l'objet Image
                bitmap.EndInit();

                ImageAvatar.Source = bitmap;

                if (Data.CurrentEmployee.Photo == null)
                {
                    ImageAvatar.Source = null;
                }
                return true;

            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un employé pour afficher ses détails.");
                return false; // Les détails ne sont pas affichés
            }
        }

        private void Button_Modifier(object sender, RoutedEventArgs e)
        {
            if (Data.CurrentEmployee != null)
            {
                
                    // Mettre à jour directement l'objet employé actuel
                    Data.CurrentEmployee.Nom = TextBoxNomMembre.Text;
                    Data.CurrentEmployee.Prenom = TextBoxPrenomMembre.Text;
                    Data.CurrentEmployee.Role = DetermineRole(); // Méthode pour déterminer le rôle basé sur les boutons radio
                    if (_imageMembre != null)
                    {
                        Data.CurrentEmployee.Photo.ImagePath = _imageMembre.ImagePath; // Assurez-vous que _imageMembre est correctement mis à jour avant cette assignation
                    }

                    // Aucun besoin de retirer et ajouter de nouveau
                    // Data.ListEmployee.Remove(Data.CurrentEmployee);
                    // Data.ListEmployee.Add(Data.CurrentEmployee);

                    // Informer l'utilisateur que la modification a été effectuée avec succès
                    MessageBox.Show("Modification effectuée avec succès !");
                
            }
            else
                MessageBox.Show("Veuillez d'abord afficher les détails de l'employé.");
            
        }
        private string DetermineRole()
        {
            if (RadioButtonStakeholders.IsChecked == true)
                return "Stake holders";
            if (RadioButtonProductOwner.IsChecked == true)
                return "Product Owner";
            if (RadioButtonScrumMasterr.IsChecked == true)
                return "Scrum Master";
            if (RadioButtonÉquipedeDéveloppement.IsChecked == true)
                return "Équipe de Développement";

            return ""; // Renvoyer une valeur par défaut si aucun n'est sélectionné
        }

        private void ButtonSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (Data.CurrentEmployee != null)
            {
                Data.ListEmployee.Remove(Data.CurrentEmployee);

                MessageBox.Show("la ligne a été supprimé avec succès");
            }
        }

        private void ButtonAfficher_Click(object sender, RoutedEventArgs e)
        {
            EnsureDetailsDisplayed();
        }
    }
}
