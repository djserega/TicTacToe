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

namespace TicTacToe
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int WinsXs { get; private set; }
        public int WinsOs { get; private set; }
        public string Step { get; private set; }

        private Board _board = new Board();
        private bool _step;

        private readonly ImageSource _imageSourceXs = new BitmapImage(new Uri("pack://application:,,,/TicTacToe;component/_Xs.png"));
        private readonly ImageSource _imageSourceOs = new BitmapImage(new Uri("pack://application:,,,/TicTacToe;component/_Os.png"));
        private readonly string _textStepXs = "Ход крестика";
        private readonly string _textStepOs = "Ход нолика";
        private readonly string _textStepWinXs = "Победа крестиков";
        private readonly string _textStepWinOs = "Победа ноликов";
        private readonly string _textStepFilledAllCells = "Ничья";

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            StartOver();
        }

        private void FormBoard_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void ButtonStartOver_Click(object sender, RoutedEventArgs e)
        {
            StartOver();
        }

        private void StartOver()
        {
            _step = false;

            WinsXs = 0;
            WinsOs = 0;

            UpdateTargetWins();

            _board.Clear();

            CheckValueBoard();
            ChangeGameStep();
        }

        private void UpdateTargetWins()
        {
            BindingOperations.GetBindingExpression(LabelWinsXs, ContentProperty).UpdateTarget();
            BindingOperations.GetBindingExpression(LabelWinsOs, ContentProperty).UpdateTarget();
        }

        private void CheckValueBoard()
        {
            ImageRow1Column1.ImageSource = _board.Row1Column1 < 0 ? null : (_board.Row1Column1 == 1 ? _imageSourceXs : _imageSourceOs);
            ImageRow1Column2.ImageSource = _board.Row1Column2 < 0 ? null : (_board.Row1Column2 == 1 ? _imageSourceXs : _imageSourceOs);
            ImageRow1Column3.ImageSource = _board.Row1Column3 < 0 ? null : (_board.Row1Column3 == 1 ? _imageSourceXs : _imageSourceOs);

            ImageRow2Column1.ImageSource = _board.Row2Column1 < 0 ? null : (_board.Row2Column1 == 1 ? _imageSourceXs : _imageSourceOs);
            ImageRow2Column2.ImageSource = _board.Row2Column2 < 0 ? null : (_board.Row2Column2 == 1 ? _imageSourceXs : _imageSourceOs);
            ImageRow2Column3.ImageSource = _board.Row2Column3 < 0 ? null : (_board.Row2Column3 == 1 ? _imageSourceXs : _imageSourceOs);

            ImageRow3Column1.ImageSource = _board.Row3Column1 < 0 ? null : (_board.Row3Column1 == 1 ? _imageSourceXs : _imageSourceOs);
            ImageRow3Column2.ImageSource = _board.Row3Column2 < 0 ? null : (_board.Row3Column2 == 1 ? _imageSourceXs : _imageSourceOs);
            ImageRow3Column3.ImageSource = _board.Row3Column3 < 0 ? null : (_board.Row3Column3 == 1 ? _imageSourceXs : _imageSourceOs);
        }

        private void ChangeGameStep()
        {
            if (_board.Win)
            {
                if (_board.WinXs)
                {
                    WinsXs++;
                    Step = _textStepWinXs;
                }
                else
                {
                    WinsOs++;
                    Step = _textStepWinOs;
                }
                UpdateTargetWins();
            }
            else if (_board.FilledAllCells)
            {
                Step = _textStepFilledAllCells;
            }
            else
            {
                _step = !_step;

                Step = _step ? _textStepXs : _textStepOs;
            }

            BindingOperations.GetBindingExpression(LabelStep, ContentProperty).UpdateTarget();
        }

        #region Board

        private void UserStep(object canvas)
        {
            if (canvas is Canvas cell)
                if (!CheckWin())
                {
                    if (_board.SetValueCell(cell.Name.Substring(6), _step))
                        ChangeGameStep();
                    UpdateBoard();
                }
        }

        private void UpdateBoard()
        {
            CheckValueBoard();
            CheckWin();
        }

        private void CanvasRow1Column1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => UserStep(sender);
        private void CanvasRow1Column2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => UserStep(sender);
        private void CanvasRow1Column3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => UserStep(sender);
        private void CanvasRow2Column1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => UserStep(sender);
        private void CanvasRow2Column2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => UserStep(sender);
        private void CanvasRow2Column3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => UserStep(sender);
        private void CanvasRow3Column1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => UserStep(sender);
        private void CanvasRow3Column2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => UserStep(sender);
        private void CanvasRow3Column3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => UserStep(sender);

        #endregion

        private bool CheckWin()
        {
            if (_board.Win || _board.FilledAllCells)
            {
                MessageBox.Show(Step);

                MessageBoxResult result = MessageBox.Show(this,"Следующая игра?", "Игра закончена", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                if (result == MessageBoxResult.Yes)
                    NextGame();
            }

            return _board.Win;
        }

        private void ButtonNextGame_Click(object sender, RoutedEventArgs e)
        {
            NextGame();
        }

        private void NextGame()
        {
            _board.NextGame();
            UpdateBoard();
        }
    }
}
