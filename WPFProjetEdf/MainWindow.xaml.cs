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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFProjetEdf
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        edfEntities gstBdd;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gstBdd = new edfEntities();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            controleur ctrl = gstBdd.controleur.ToList().Find(contr => contr.login == txtLogin.Text.ToLower() && contr.mdp == txtMdp.Text.ToLower());
            if (txtLogin.Text == "")
            {
                MessageBox.Show("Veiller insérer un login", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtMdp.Text == "")
            {
                MessageBox.Show("Veiller insérer un mot de passe", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
               
                if (ctrl == null)
                {
                    MessageBox.Show("Vos indentifient sont incorrects", "Erreur d'authentification", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
               // string mdp = gstBdd.controleur.ToList().Find(contr => contr.mdp == txtMdp.Text.ToLower()).ToString();
                //if (txtLogin.Text.ToLower() != login && txtLogin.Text.ToLower() != mdp)
                //{
                //   
                //}
                else
                {

                 
                       // string statut = gstBdd.controleur.ToList().Find(contro => contro.statut == "admin").ToString();
                        if (ctrl.statut == "admin")
                        {
                            GestionAdmin gstAdmin = new GestionAdmin();
                            gstAdmin.Show();
                        }
                        else
                        {
                            GestionCtrl gstCtrl = new GestionCtrl(ctrl);
                            gstCtrl.Show();
                        }
                  
                }
            }


           
        }
    }
}
