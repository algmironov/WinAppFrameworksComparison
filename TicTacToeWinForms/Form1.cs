namespace TicTacToeWinForms
{
    public partial class Form1 : Form
    {
        private Button[,] buttons = new Button[3, 3];
        private char currentPlayer = 'X';
        private TableLayoutPanel tableLayoutPanel;

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
            CenterToScreen();
        }

        private void InitializeGame()
        {
            // ������� � ����������� TableLayoutPanel
            tableLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 3
            };

            // ����������� ������ � �������
            for (int i = 0; i < 3; i++)
            {
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    buttons[i, j] = new Button
                    {
                        Dock = DockStyle.Fill,
                        Font = new Font("Arial", 36),
                        TabIndex = i * 3 + j
                    };
                    buttons[i, j].Click += Button_Click;
                    tableLayoutPanel.Controls.Add(buttons[i, j], j, i);
                }
            }

            this.Controls.Add(tableLayoutPanel);
            this.MinimumSize = new Size(300, 300); // ������������� ����������� ������ �����
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            if (string.IsNullOrEmpty(clickedButton.Text))
            {
                clickedButton.Text = currentPlayer.ToString();
                if (CheckForWinner())
                {
                    MessageBox.Show($"Player {currentPlayer} wins!");
                    ResetGame();
                }
                else if (IsBoardFull())
                {
                    MessageBox.Show("It's a draw!");
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
            // �������� �� ����������� � ���������
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

            // �������� �� ����������
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