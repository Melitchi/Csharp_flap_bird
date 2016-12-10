
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace CESI.MF.projet.classe
{
    abstract class Characters
    {
        public Vector velocity; // vitesse
        public Vector acceleration; // acceleration
        public double mass; // masse
        public Vector location; // position du centre
        public Ellipse e;

        abstract public bool checkEdges(double hauteur, double largeur);
        abstract public void update();

        public void applyForce(Vector force)
        {
            Vector vec = force / mass;
            acceleration += vec;
        }
        public void display()
        {
            Canvas.SetTop(e, location.Y);
            Canvas.SetLeft(e, location.X);
        }

        // Vérifie si bird touche le sol
    }
}
