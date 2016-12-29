using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CESI.MF.projet.classe
{
    class Background : Characters
    {
        public Image img;
        public BitmapImage backgroundImage;
        private const double scrolling_velocity = -1;

        public Background(string imgUri) {
            backgroundImage = new BitmapImage(new Uri (imgUri, UriKind.Absolute));
            // ajout du background
            img = new Image();
            img.Source = backgroundImage;
           
        }
        public double imgSize() {
            return img.Width;
        }
        public new void display()
        {
            Canvas.SetTop(img, location.Y);
            Canvas.SetLeft(img, location.X);
        }
        public override bool checkEdges(double hauteur, double largeur)
        {
            throw new NotImplementedException();
        }

        public override void update()
        {
            // vitesse constante de défilement
            velocity.X = scrolling_velocity;
            location += velocity;
            acceleration.X = 0;
            acceleration.Y = 0;
        }
    }
}

