
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CESI.MF.projet.classe
{
    class Enemies : Characters
    {
      

        public Enemies(double m, double x, double y, Canvas canvas)
        {

            this.location.X = x;
            this.location.Y = y;
            this.mass = m;
            this.velocity.X = 0;
            this.velocity.Y = 0;
            this.acceleration.X = 0;
            this.acceleration.Y = 0;
             this.e = new Ellipse();
            Color color = Color.FromArgb(255, 225, 80, 100); // définit la couleur RVB
            SolidColorBrush b = new SolidColorBrush();
            b.Color = color;
            e.Fill = b; // couleur de remplissage
            e.StrokeThickness = 2; // taille du contour
            e.Stroke = Brushes.Black; // couleur du contour
            e.Width = mass + 15;
            e.Height = mass + 15;
            canvas.Children.Add(e);
        }


        public override bool checkEdges(double hauteur, double largeur)
        {
            if (location.Y > hauteur)
            {
                velocity.Y *= -0.9; // A little dampening when hitting the bottom
                location.Y = 0;
                /* }else if(location.Y < 1){
                     velocity.Y *= -0.9; // A little dampening when hitting the bottom
                     location.Y = hauteur;*/
            }
            else if (location.X < 0)
            {
                location.X = largeur;
            }
            return true;
        }

        public override void update()
        {
            velocity += acceleration;
            velocity.Y = 2;
            velocity.X = -1;
            location += velocity;
            acceleration.X = 0;
            acceleration.Y = 0;
        }

    }
}

