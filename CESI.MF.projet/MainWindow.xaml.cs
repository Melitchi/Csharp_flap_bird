using CESI.MF.projet.classe;
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

namespace CESI.MF.projet
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Canvas mainCanvas;
        private Bird bird;
        public MainWindow()
        {
            InitializeComponent();

            //initialisation du canvas
            mainCanvas = new Canvas();
            mainCanvas.Background=Brushes.Azure;
            //même taille que la fenêtre
            mainCanvas.Width = gameContainer.Width;
            mainCanvas.Height = gameContainer.Height;
            // Ajouter le canvas
            mainWindow.Content = mainCanvas;
            mainWindow.Title = "Canvas Sample";
            mainWindow.Show();

            // Création du personnage
            bird = new Bird(10,500 ,500,mainCanvas);
        }
    }
}
