using ClassLibrarygame2048;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;
using System.Collections.ObjectModel;
using System.Drawing;


namespace game2048
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Model model = new Model(4);
        Button[,] buttons;
      
        public MainWindow()
        {
            InitializeComponent();
            buttons = new Button[model.size, model.size];
            Button button = new Button();
            int count = 0;
            for (int x = 0; x < model.size; x++)
            {
                for (int y = 0; y < model.size; y++)
                {

                    button = (Button)gridMap.Children[count];
                    buttons[x, y] = button;
                    buttons[x, y].Opacity = 0;
                    count++;

                }
            }

        }
        private void RepeatButton_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }
        void StartGame()
        {
            textblockstate.Text = "Still play ";
            Model.colectScore = 0;
            score.Text = Model.colectScore.ToString();
            bestscore.Text = Model.bestScore.ToString();
            textblockstate.Foreground = Brushes.Green;
            model.Start();
            ShowModel();
        }
        public void ShowModel()
        {
            for (int x = 0; x < model.size; x++)
            {
                for (int y = 0; y < model.size; y++)
                {
                    ShowButtonText("b" + x + y, model.GetMap(x, y));
                   
                }
            }
        }
        void ShowButtonText(string name, int namber)
        {
            for (int x = 0; x < model.size; x++)
            {
                for (int y = 0; y < model.size; y++)
                {
                    if (buttons[x, y].Name.ToString() == name)
                    {
                        string tmp = namber.ToString();
                        buttons[x, y].Content = tmp;
                        buttons[x, y].Opacity = 0;
                        if (buttons[x, y].Content.ToString() != "0")
                        {
                            buttons[x, y].Opacity = 100;
                        }
                    }

                }
            }


        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            if (!(model.IsGameOver()))
            {
                switch (e.Key)
                {
                    case Key.Left: model.Left(); break;
                    case Key.Right: model.Right(); break;
                    case Key.Up: model.Up(); break;
                    case Key.Down: model.Down(); break;
                    case Key.Escape: this.Close(); break;
                }
                score.Text = Model.colectScore.ToString();
                ShowModel();
              
            }
            else
            {
                textblockstate.Foreground = Brushes.Red;
                textblockstate.Text = "Game Over !!!" ;
                MessageBoxResult result = MessageBox.Show(" Do you want try again ?", "RESTART ?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Model.bestScore = Model.colectScore > Model.bestScore ? Model.colectScore : Model.bestScore;
                    Model.colectScore = 0;
                    score.Text = Model.colectScore.ToString();
                    bestscore.Text = Model.bestScore.ToString();
                    Model.isGameOver = false;
                    StartGame();
                }
                else
                {
                    this.Close();
                }
            }

        }
       

           
        
        
       
    }
  
} 