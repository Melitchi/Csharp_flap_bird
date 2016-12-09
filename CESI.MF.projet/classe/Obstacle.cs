﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CESI.MF.projet.classe
{
    class Obstacle{
        public double x, y, w, h; 
        private Rectangle rectangle;
        public Vector location;
        public double mass;
        public Vector acceleration;
        public Vector velocity;
        private const double scrolling_velocity = -2;

        public Obstacle(double x_, double y_, double w_, double h_, Canvas canvas) {
            //Définition de l'obstacle
            rectangle = new Rectangle();
            x = x_;
            y = y_;
            w = w_;
            h = h_;
            //Définitions des vecteurs
            location.X =x;
            location.Y = y;
            mass = 10;
            velocity.X = 0;
            velocity.Y = 0;
            acceleration.X = 0;
            acceleration.Y = 0;
            //Création de l'obstacle
            rectangle.Width = w;
            rectangle.Height = h;
            rectangle.Stroke = Brushes.Black;
            rectangle.Fill = Brushes.GreenYellow;
            Canvas.SetTop(rectangle, x);
            Canvas.SetLeft(rectangle, y);
            canvas.Children.Add(rectangle);
        }

        //Afficher les obstacles
        public void display()
        {
            Canvas.SetTop(rectangle, location.Y);
            Canvas.SetLeft(rectangle, location.X);
        }
        public void applyForce(Vector force)
        {
            Vector vec = force / mass;
            acceleration += vec;
        }


        public void update()
        {
            // vitesse constante de défilement
            velocity.X = scrolling_velocity;
            location += velocity;
            acceleration.X = 0;
            acceleration.Y = 0;
        }
    }
}
