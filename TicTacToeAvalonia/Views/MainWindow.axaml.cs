using Avalonia.Controls;
using Avalonia.Interactivity;

using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

using System;
using System.Threading.Tasks;

namespace TicTacToeAvalonia
{
    public partial class MainWindow : Window
    {
        private char currentPlayer = 'X';
        private Button[,] buttons;

        public MainWindow()
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
                    buttons[i, j] = this.FindControl<Button>($"Button_{i}_{j}");
                }
            }
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
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

        private async Task ShowMessage(string message)
        {
            var messageBox = MessageBoxManager
                        .GetMessageBoxStandard("Игра завершена", $"{message}", ButtonEnum.Ok);
            await messageBox.ShowAsync();

            // В реальном приложении здесь должно быть диалоговое окно
            Console.WriteLine(message);
        }
    }
}
