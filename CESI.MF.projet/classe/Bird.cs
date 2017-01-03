using CESI.MF.projet.classe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CESI.MF.projet.classe
{
    class Bird:Characters
    {

        public Bird(double m, double x, double y, Canvas canvas)
        {
            //initialisation des variables
            this.location.X = x;
            this.location.Y = y;
            this.mass = m;
            this.velocity.X = 0;
            this.velocity.Y = 0;
            this.acceleration.X = 0;
            this.acceleration.Y = 0;
            //Création de bird
            this.e = new Ellipse();
            /*Color color = Color.FromArgb(255, 55, 200, 220); // définit la couleur RVB
            SolidColorBrush b = new SolidColorBrush();
            b.Color = color;
            e.Fill = b; // couleur de remplissage
            e.StrokeThickness = 2; // taille du contour
            e.Stroke = Brushes.Black; // couleur du contour*/

            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(repertoireImg + "/../img/whiteBird.png", UriKind.Absolute));

            e.Fill = myBrush;
            e.Width = 80 ;
            e.Height = 58;
            canvas.Children.Add(e);
        }
       
        // Vérifie si bird touche le sol
        public override bool checkEdges(double hauteur, double largeur)
        {
            bool touch = false;
            if (location.Y  > hauteur ) { 
                touch = true;
                location.Y = hauteur;  
            }
            return touch;
        }

        // Vérifie si bird touche un obstacle
        public bool checkObstacle(List<Obstacle> obs) {
            bool touched = false;
            for (int i = 0; i < obs.Count; i++) {
                if(location.X+e.Width-20>obs[i].location.X && location.X<obs[i].location.X+obs[i].w && location.Y+e.Height - 20 > obs[i].location.Y && location.Y+e.Height-20< obs[i].location.Y+obs[i].h)
                {
                    touched= true;
                    break;
                }
                else
                {
                    touched= false;
                }
            }
            return touched;
        }
        //vérifie si on heurte un ennemi
        public bool checkEnnemies(List<Ennemies>list) {
            bool touched = false;
            for (int i = 0; i < list.Count; i++)
            {
                if (location.X + e.Width - 20 > list[i].location.X && location.X < list[i].location.X+list[i].width && location.Y + e.Height - 20 > list[i].location.Y && location.Y + e.Height - 20 < list[i].location.Y + list[i].height)
                {
                    touched = true;
                    break;
                }
                else
                {
                    touched = false;
                }
            }
            return touched;
        }
     
        // Mise à jour des vecteurs
        public override void update(){
            velocity += acceleration;
            location += velocity;
            acceleration.X = 0;
            acceleration.Y = 0;
        }
    }
}

