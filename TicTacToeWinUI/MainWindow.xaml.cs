using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

using System;

namespace TicTacToeWinUI
{
    public sealed partial class MainWindow : Window
    {
        private char currentPlayer = 'X';
        private Button[,] buttons;

        public MainWindow()
        {
            this.InitializeComponent();
            this.AppWindow.Resize(size: new Windows.Graphics.SizeInt32(800, 800));
            InitializeButtons();
        }

        private void InitializeButtons()
        {
            buttons = new Button[3, 3];
            var rootGrid = (Grid)this.Content;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    buttons[i, j] = (Button)rootGrid.FindName($"Button_{i}_{j}");
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            if (string.IsNullOrEmpty(clickedButton.Content?.ToString()))
            {
                clickedButton.Content = currentPlayer;
                if (CheckForWinner())
                {
                    ShowMessage($"Player {currentPlayer} wins!");
                    ResetGame();
                }
                else if (IsBoardFull())
                {
                    ShowMessage("It's a draw!");
                    ResetGame();
                }
                else
                {
                    currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                }
            }
        }

        private bool CheckForWinner()
        {
            // Проверка по горизонтали и вертикали
            for (int i = 0; i < 3; i++)
            {
                if (buttons[i, 0].Content?.ToString() == currentPlayer.ToString() &&
                    buttons[i, 1].Content?.ToString() == currentPlayer.ToString() &&
                    buttons[i, 2].Content?.ToString() == currentPlayer.ToString())
                    return true;

                if (buttons[0, i].Content?.ToString() == currentPlayer.ToString() &&
                    buttons[1, i].Content?.ToString() == currentPlayer.ToString() &&
                    buttons[2, i].Content?.ToString() == currentPlayer.ToString())
                    return true;
            }

            // Проверка по диагоналям
            if (buttons[0, 0].Content?.ToString() == currentPlayer.ToString() &&
                buttons[1, 1].Content?.ToString() == currentPlayer.ToString() &&
                buttons[2, 2].Content?.ToString() == currentPlayer.ToString())
                return true;

            if (buttons[0, 2].Content?.ToString() == currentPlayer.ToString() &&
                buttons[1, 1].Content?.ToString() == currentPlayer.ToString() &&
                buttons[2, 0].Content?.ToString() == currentPlayer.ToString())
                return true;

            return false;
        }

        private bool IsBoardFull()
        {
            foreach (Button button in buttons)
            {
                if (string.IsNullOrEmpty(button.Content?.ToString()))
                    return false;
            }
            return true;
        }

        private void ResetGame()
        {
            foreach (Button button in buttons)
            {
                button.Content = "";
            }
            currentPlayer = 'X';
        }

        private async void ShowMessage(string message)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "Game Over",
                Content = message,
                CloseButtonText = "OK"
            };

            dialog.XamlRoot = this.Content.XamlRoot;
            await dialog.ShowAsync();
        }
    }
}
