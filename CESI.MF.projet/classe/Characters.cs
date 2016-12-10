
using System.IO;
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
        public string repertoireImg = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(Directory.GetCurrentDirectory()) + System.IO.Path.DirectorySeparatorChar + "/../ img /");
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
