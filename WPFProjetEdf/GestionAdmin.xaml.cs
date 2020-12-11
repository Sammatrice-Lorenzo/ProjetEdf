using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFProjetEdf
{
    /// <summary>
    /// Logique d'interaction pour GestionAdmin.xaml
    /// </summary>
    public partial class GestionAdmin : Window
    {
        public GestionAdmin()
        {
            InitializeComponent();
        }

        edfEntities gstBdd;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gstBdd = new edfEntities();
            lstControleurs.ItemsSource = gstBdd.controleur.ToList();
        }

        private void lstControleurs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstControleurs.SelectedItem != null)
            {
                int numControleur = (lstControleurs.SelectedItem as controleur).id;
                var query = from cli in gstBdd.client
                            where cli.idcontroleur == numControleur
                            select new ClientPerso
                            {
                                IdControleur = cli.idcontroleur,
                                NomClient = cli.nom,
                                NumClient = cli.identifiant,
                                PrenomClient = cli.prenom,
                                AnceinReleve = cli.ancienReleve,
                                DernierReleve = cli.dernierReleve,
                                Montant = cli.dernierReleve - cli.ancienReleve
                            };
                lstClients.ItemsSource = query.ToList();
            }
        }

        private void btnInscire_Click(object sender, RoutedEventArgs e)
        {
           
            if (txtNomControleur.Text == "")
            {
                MessageBox.Show("Saisir le nom du controleur", "Erreur d saise", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            else if (txtPrenomControleur.Text == "")
            {
                MessageBox.Show("Saisir  le prenom du controleur", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            else 
            {
                int dernierNumControleur = gstBdd.controleur.Max(contro => contro.id);
                controleur newControleur = new controleur()
                {
                    id = dernierNumControleur + 1,
                    login = txtNomClient.Text.Substring(0, 1).ToLower() + txtPrenomControleur.Text.Substring(0, 1),
                    mdp = txtNomClient.Text.Substring(0, 1).ToLower() + txtPrenomControleur.Text.Substring(0, 1) + "123",
                    statut = "ctrl"
                };
                gstBdd.controleur.Add(newControleur);
                gstBdd.SaveChanges();

                MessageBox.Show("Controleur enregistrée", "Enregistrement", MessageBoxButton.OK, MessageBoxImage.Information);
                lstControleurs.ItemsSource = gstBdd.controleur.ToList();
               
            }
        }

        private void btnInscireClient_Click(object sender, RoutedEventArgs e)
        {
            if (lstControleurs.SelectedItem == null)
            {
                MessageBox.Show("Saisir le contoleur", "Erreur de sélction", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            else if (txtNomClient.Text == "")
            {
                MessageBox.Show("Saisir le nom du client", "Erreur de saise", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            else if (txtPrenomClient.Text == "")
            {
                MessageBox.Show("Saisir  le prenom du client", "Erreur de saise", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            else
            {
                int numContro = (lstControleurs.SelectedItem as controleur).id;
                int dernierNumClient = gstBdd.client.Max(cli => cli.identifiant);
                client newClient = new client()
                {
                    identifiant = dernierNumClient + 1,
                    nom = txtNomClient.Text,
                    prenom = txtPrenomClient.Text,
                    ancienReleve = 0,
                    dernierReleve = 0,
                    idcontroleur = numContro
                };
                gstBdd.client.Add(newClient);
                gstBdd.SaveChanges();
                MessageBox.Show("Client enregistrée", "Enregistrement", MessageBoxButton.OK, MessageBoxImage.Information);
                int numControleur = (lstControleurs.SelectedItem as controleur).id;
                var query = from cli in gstBdd.client
                            where cli.idcontroleur == numControleur
                            select new ClientPerso
                            {
                                IdControleur = cli.idcontroleur,
                                NomClient = cli.nom,
                                NumClient = cli.identifiant,
                                PrenomClient = cli.prenom,
                                AnceinReleve = cli.ancienReleve,
                                DernierReleve = cli.dernierReleve,
                                Montant = cli.dernierReleve - cli.ancienReleve
                            };
                lstClients.ItemsSource = query.ToList();
            }
        }

    }
}
