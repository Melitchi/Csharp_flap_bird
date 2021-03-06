﻿using CESI.MF.projet.classe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CESI.MF.projet
{
    public partial class MainWindow : Window
    {
       
        private Bird bird;
        private Canvas mainCanvas;
        private bool started;
        private bool loose ;
        public double distance;
        public Label scoreLabel;
        public Label lifeLabel;
        public Label statutLabel;
        public int lives;
        private Background backgrd;
        private List <Obstacle> obstacles;
        private List<Ennemies> ennemies;
        private string rootRepertoiry;
        public SoundPlayer player;
        public MainWindow()
       
        {
            InitializeComponent();
            started = false;
            distance = 0;
           
            //initialisation du canvas
            mainCanvas = new Canvas();
            obstacles = new List<Obstacle>();
            ennemies = new List<Ennemies>();
            scoreLabel = new Label();
            statutLabel = new Label();
            lifeLabel = new Label();
            statutLabel.FontSize = 30;
            lives = 5;
            loose = false;
            rootRepertoiry = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(Directory.GetCurrentDirectory()) + System.IO.Path.DirectorySeparatorChar);
            backgrd = new Background(rootRepertoiry + "/../img/game_background.png");
            Canvas.SetTop(backgrd.img, mainCanvas.Height);
            Canvas.SetLeft(backgrd.img, mainCanvas.Width);
            mainCanvas.Children.Add(backgrd.img);
            mainCanvas.Width = 994;
            mainCanvas.Height = 571;

            mainCanvas.Focusable = true;
            // Handler pour clavier
            mainCanvas.KeyDown+= KeyAction;
            // Ajouter le canvas
            mainCanvas.Focus();
            mainWindow.Content = mainCanvas;
            mainWindow.Title = "Méli - Flappy bird";
            //labels
            scoreLabel.Content = "";
            mainCanvas.Children.Add(scoreLabel);

            Canvas.SetTop(statutLabel, mainCanvas.Height / 2);
            Canvas.SetLeft(statutLabel, (mainCanvas.Width / 2));
            Canvas.SetZIndex(statutLabel, 10);
            statutLabel.Content = "Touche entrée pour commencer";
            mainCanvas.Children.Add(statutLabel);
            mainCanvas.Children.Add(lifeLabel);
            // Création du personnage
            bird = new Bird(10, mainCanvas.Width/2, mainCanvas.Height/2, mainCanvas);
           
            generate();
            //Préparation de la musique
            player = new SoundPlayer();
            player.SoundLocation = rootRepertoiry + "/../sound/Bonetrousle.wav";
        }

        private void PlayLoopingBackgroundSoundFile()
        {
            //player.PlayLooping();
        }

        public void startGame() {
            statutLabel.Content = "";
            PlayLoopingBackgroundSoundFile();
            CompositionTarget.Rendering += update;
        }
        public void stopGame() {
            CompositionTarget.Rendering -= update;
            if (loose) {
                showGameOverScreen();
            }else {
                showGamePauseScreen();
            }
        }

        private void KeyAction(object sender, KeyEventArgs e){
            // quitter le jeu
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
            // sauter
            if (e.Key == Key.Space)
            {
                bird.velocity.Y = -2;
            }
            //Démarrer ou mettre le jeu en pause
            if (e.Key == Key.Enter)
            {
                if (!started)
                {
                    startGame();
                    started = true;
                    if (loose)
                    {
                        if (lives > 0) { resetGame(false); }
                        else { lives = 5; resetGame(true); }
                    }
                }
                else
                {
                    stopGame();
                    started = false;
                }
            }
        }
        public void resetGame(bool newGame){
            if(obstacles.Count>0) { 
                obstacles.Clear();
                for (int i = 0; i < 8; i++)
                {
                    Rectangle rectangle = (Rectangle)LogicalTreeHelper.FindLogicalNode(mainCanvas, "rectangle" + i);
                    mainCanvas.Children.Remove(rectangle);
                }
             }
            if (ennemies.Count > 0)
            {
                ennemies.Clear();
                for (int i = 0; i < 8; i++)
                {
                    Ellipse todie = (Ellipse)LogicalTreeHelper.FindLogicalNode(mainCanvas, "ennemi" + i);
                    mainCanvas.Children.Remove(todie);
                }
            }
            bird.location.X = mainCanvas.Width / 2;
            bird.location.Y = mainCanvas.Height / 2;
            generate();
        }

        public void generate(){
            bird.display();
            loose = false;
            lifeLabel.Content = "Vies: " + lives;
            Canvas.SetTop(lifeLabel, 10);
            Canvas.SetLeft(lifeLabel, 10);
            Canvas.SetZIndex(lifeLabel, 10);

            // Génération des obstacles
            Random rand = new Random();
            double hauteur = (mainCanvas.Height / 2);
            double largeur = 100;
            double altitude = 0;

            // 8 obstacles peuvent être affichés au max à l'écran
            for (int i = 0; i < 8; i++)
            { 
                altitude = rand.Next(Convert.ToInt32(Convert.ToInt32(mainCanvas.Height / 1.8)), Convert.ToInt32(mainCanvas.Height + 1));
                if (obstacles.Count < 1) {
                    distance = (mainCanvas.Width);
                }
                else
                {
                    distance += 120;
                }
                obstacles.Add(new Obstacle(distance, altitude, largeur, hauteur, mainCanvas, "rectangle"+i));
            }
            //Génération des ennemis
            for (int i = 0; i < 2; i++)
            {
                ennemies.Add(new Ennemies(10, mainCanvas.Width, 1, mainCanvas,"ennemi"+i));
           }
        }

        public void update(Object sender, EventArgs e){
            // Mouvement des obstacles
            Vector oGravite = new Vector();
            oGravite.Y = 5;
            for (int i = 0; i < obstacles.Count; i++)
            { 
                obstacles[i].update();
                obstacles[i].display();
                obstacles[i].applyForce(oGravite);
                // si l'obstacle est hors de l'écran on le décale
                if((obstacles[i].location.X + obstacles[i].w) <=0) {
                    obstacles[i].location.X = mainCanvas.Width;
                }
            }
            // Mouvements ennemies
            Vector eGravite = new Vector();
            eGravite.Y = 2;
            for (int i = 0; i < ennemies.Count; i++)
            {
                ennemies[i].update();
                ennemies[i].display();
                ennemies[i].applyForce(eGravite);
                ennemies[i].checkEdges(mainCanvas.Height, mainCanvas.Width);
            }
            // Mouvements de bird 
            Vector bGravite = new Vector();
            bGravite.Y = 1;
            bird.update();
            bird.display();
         
            // Contrôle si bird à perdu
            if (!true == bird.checkEdges(mainCanvas.Height, mainCanvas.Width)&& !true == bird.checkObstacle(obstacles) && !true == bird.checkEnnemies(ennemies)) {
                bird.applyForce(bGravite);
            }else {
                loose = true;
                started = false;
                statutLabel.Content = "loose";
                lives--;
                stopGame();
            }    
        }

        public void showStartSceen() {

        }

        private void showGamePauseScreen()
        {
            statutLabel.Content = "PAUSE";
        }

        public void showGameOverScreen() {
            if(lives>0) {
                statutLabel.Content = "RETRY";
               
            }else { 
                statutLabel.Content = "GAME OVER";
            }
        }
    }
}
