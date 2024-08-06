using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Popups;
using System.Threading.Tasks;
using System;

namespace TicTacToeUno
{
    public sealed partial class MainPage : Page
    {
        private char currentPlayer = 'X';
        private Button[,] buttons;

        public MainPage()
        {
            InitializeComponent();
            InitializeButtons();
        }

        private void InitializeButtons()
        {
            buttons = new Button[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    buttons[i, j] = (Button)FindName($"Button_{i}_{j}");
                }
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            if (string.IsNullOrEmpty(clickedButton.Content?.ToString()))
            {
                clickedButton.Content = currentPlayer;
                if (CheckForWinner())
                {
                    await ShowMessageAsync($"Player {currentPlayer} wins!");
                    ResetGame();
                }
                else if (IsBoardFull())
                {
                    await ShowMessageAsync("It's a draw!");
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

        private async Task ShowMessageAsync(string message)
        {
            var dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }
    }
}