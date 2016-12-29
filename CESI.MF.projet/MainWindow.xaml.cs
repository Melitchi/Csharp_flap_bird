using CESI.MF.projet.classe;
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
        private Enemies en;
        private List <Obstacle> obstacles;
        public BitmapImage backgroundImage;
        public Image img;
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
            scoreLabel = new Label();
            statutLabel = new Label();
            lifeLabel = new Label();
            statutLabel.FontSize = 30;
            lives = 5;
            loose = false;
            rootRepertoiry = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(Directory.GetCurrentDirectory()) + System.IO.Path.DirectorySeparatorChar);
            backgroundImage = new BitmapImage(new Uri(rootRepertoiry + "/../img/game_background.png", UriKind.Absolute));
            // ajout du background
            img = new Image();
            img.Source = backgroundImage;
            Canvas.SetTop(img, mainCanvas.Height);
            Canvas.SetLeft(img, mainCanvas.Width);
            mainCanvas.Children.Add(img);
            mainCanvas.Width = 994;
            mainCanvas.Height = 571;

            mainCanvas.Focusable = true;
            // Handler pour clavier
            mainCanvas.KeyDown+= KeyAction;
            // Ajouter le canvas
            mainCanvas.Focus();
            mainWindow.Content = mainCanvas;
            mainWindow.Title = "Méli - Flap bird";
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
            en = new Enemies(10, mainCanvas.Width, 1, mainCanvas);
            generate();
            //Préparation de la musique
            player = new SoundPlayer();
            player.SoundLocation = rootRepertoiry + "/../sound/Bonetrousle.wav";
        }

        private void PlayLoopingBackgroundSoundFile()
        {
            player.PlayLooping();
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

        private void KeyAction(object sender, KeyEventArgs e)
        {
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
            bird.location.X = mainCanvas.Width / 2;
            bird.location.Y = mainCanvas.Height / 2;
            generate();
        }
        public void generate(){
            bird.display();
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
        }

        public void update(Object sender, EventArgs e){            
                // Mouvement des obstacles

            for (int i = 0; i < obstacles.Count; i++)
            {
                Vector oGravite = new Vector();
                oGravite.Y = 0.5;
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
            en.update();
            en.display();
            en.applyForce(eGravite);
            en.checkEdges(mainCanvas.Height, mainCanvas.Width);
            // Mouvements de bird 
            Vector bGravite = new Vector();
            bGravite.Y = 1;
            bird.update();
            bird.display();
            // Contrôle si bird à perdu
            if(!true == bird.checkEdges(mainCanvas.Height, mainCanvas.Width)&& !true == bird.checkObstacle(obstacles)) {
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
