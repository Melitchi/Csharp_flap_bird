using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CESI.MF.projet.classe
{
    class Bird
    {
        public Vector velocity; // vitesse
        public Vector acceleration; // acceleration
        public double mass; // masse
        public Vector location; // position du centre
        public Ellipse e;
        public Canvas canvas;

        public Bird(double m, double x, double y, Canvas canvas)
        {

            this.location.X = x;
            this.location.Y = y;
            this.mass = m;
            this.velocity.X = 0;
            this.velocity.Y = 0;
            this.acceleration.X = 0;
            this.acceleration.Y = 0;
            this.canvas = canvas;
            this.e = new Ellipse();
            Color color = Color.FromArgb(255, 55, 200, 220); // définit la couleur RVB
            SolidColorBrush b = new SolidColorBrush();
            b.Color = color;
            e.Fill = b; // couleur de remplissage
            e.StrokeThickness = 2; // taille du contour
            e.Stroke = Brushes.Black; // couleur du contour
            e.Width = mass + 15;
            e.Height = mass + 15;
            canvas.Children.Add(e);
        }

        public Vector getEllipsePosition()
        {
            location.X = location.X - (e.Width / 2);
            location.Y = location.Y - (e.Height / 2);
            return location;
        }

        public void display()
        {
            Canvas.SetTop(e, location.Y);
            Canvas.SetLeft(e, location.X);

        }
        // Vérifie si bird touche le sol ou un obstacle
        public void checkEdges(double hauteur)
        {
            if (location.Y > hauteur)
            {
                velocity.Y *= -0.9; // A little dampening when hitting the bottom
                location.Y = hauteur;
                // stop on a perdu
            }

        }
        public void applyForce(Vector force)
        {
            Vector vec = force / mass;
            acceleration += vec;
        }

        public void update()
        {
            velocity += acceleration;
            location += velocity;
            acceleration.X = 0;
            acceleration.Y = 0;
        }
    }
}

