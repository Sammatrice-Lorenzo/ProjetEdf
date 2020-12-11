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
    /// Logique d'interaction pour GestionCtrl.xaml
    /// </summary>
    public partial class GestionCtrl : Window
    {
        controleur contro;
        public GestionCtrl(controleur unContro)
        {
            InitializeComponent();
            contro = unContro;
        }
        edfEntities gstBdd;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gstBdd = new edfEntities();
            int numControleur = contro.id;

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

        private void btnInserer_Click(object sender, RoutedEventArgs e)
        {
            int dernierRevele = gstBdd.client.ToList().Find(cli => cli.idcontroleur == contro.id).dernierReleve;
            if (lstClients.SelectedItem == null)
            {
                MessageBox.Show("Sélectionner un client", "Erreur de sélction", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            else if(txtReleve.Text == "")
            {
                MessageBox.Show("Saisir le montant du client", "Erreur de saise", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            else if(Convert.ToInt16(txtReleve.Text)< dernierRevele )
            {
                MessageBox.Show("Vous pouvez pas insérer montatn plus garnd que celui d'avant", "Erreur de saise", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            else
            {
                int nouveau = Convert.ToInt16(txtReleve.Text);
                gstBdd.client.First(cl => cl.idcontroleur == contro.id).dernierReleve = nouveau;
                gstBdd.client.First(cl => cl.idcontroleur == contro.id).ancienReleve = dernierRevele;
                gstBdd.SaveChanges();
                MessageBox.Show("Insértion enregistrée", "Enregistrement", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        
        }
    }
}
