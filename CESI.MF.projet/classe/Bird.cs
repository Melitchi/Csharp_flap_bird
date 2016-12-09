﻿using System;
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
            Color color = Color.FromArgb(255, 55, 200, 220); // définit la couleur RVB
            SolidColorBrush b = new SolidColorBrush();
            b.Color = color;
            e.Fill = b; // couleur de remplissage
            e.StrokeThickness = 2; // taille du contour
            e.Stroke = Brushes.Black; // couleur du contour
            e.Width = mass+20 ;
            e.Height = mass+20;
            display(); // centrer bird 
            canvas.Children.Add(e);
        }
       
        // Afficher bird
        public void display()
        {
            Canvas.SetTop(e, location.Y);
            Canvas.SetLeft(e, location.X);
        }

        // Vérifie si bird touche le sol
        public bool checkEdges(double hauteur)
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
                if(location.X>obs[i].location.X && location.X<obs[i].location.X+obs[i].w && location.Y>obs[i].location.Y && location.Y< obs[i].location.Y+obs[i].h)
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
        //Appliquer force
        public void applyForce(Vector force)
        {
            Vector vec = force / mass;
            acceleration += vec;
        }
        // Mise à jour des vecteurs
        public void update()
        {
            velocity += acceleration;
            location += velocity;
            acceleration.X = 0;
            acceleration.Y = 0;
        }
    }
}

