namespace TicTacToeMAUIApp
{
    public partial class MainPage : ContentPage
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
            var grid = this.Content as Grid;
            if (grid != null)
            {
                foreach (var child in grid.Children.OfType<Button>())
                {
                    int row = Grid.GetRow(child);
                    int col = Grid.GetColumn(child);
                    if (row < 3 && col < 3)
                    {
                        buttons[row, col] = child;
                    }
                }
            }
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            if (string.IsNullOrEmpty(clickedButton.Text))
            {
                clickedButton.Text = currentPlayer.ToString();
                if (CheckForWinner())
                {
                    DisplayAlert("Game Over", $"Player {currentPlayer} wins!", "OK");
                    ResetGame();
                }
                else if (IsBoardFull())
                {
                    DisplayAlert("Game Over", "It's a draw!", "OK");
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
                if (buttons[i, 0].Text == currentPlayer.ToString() &&
                    buttons[i, 1].Text == currentPlayer.ToString() &&
                    buttons[i, 2].Text == currentPlayer.ToString())
                    return true;

                if (buttons[0, i].Text == currentPlayer.ToString() &&
                    buttons[1, i].Text == currentPlayer.ToString() &&
                    buttons[2, i].Text == currentPlayer.ToString())
                    return true;
            }

            // Проверка по диагоналям
            if (buttons[0, 0].Text == currentPlayer.ToString() &&
                buttons[1, 1].Text == currentPlayer.ToString() &&
                buttons[2, 2].Text == currentPlayer.ToString())
                return true;

            if (buttons[0, 2].Text == currentPlayer.ToString() &&
                buttons[1, 1].Text == currentPlayer.ToString() &&
                buttons[2, 0].Text == currentPlayer.ToString())
                return true;

            return false;
        }

        private bool IsBoardFull()
        {
            foreach (Button button in buttons)
            {
                if (string.IsNullOrEmpty(button.Text))
                    return false;
            }
            return true;
        }

        private void ResetGame()
        {
            foreach (Button button in buttons)
            {
                button.Text = "";
            }
            currentPlayer = 'X';
        }
    }

}
