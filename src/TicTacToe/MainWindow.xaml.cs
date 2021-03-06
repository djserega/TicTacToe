﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private ReceivedMessageEvents _receivedMessage;
        internal ReceivedMessageEvents ReceivedMessage { get => _receivedMessage; }

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

        private bool IsActiveMultiplayer { get => _connector != null; }
        private MultiplayerClientServer _connector;
        private TransportObjects _message;

        internal Board Board { get => _board; }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            _receivedMessage = new ReceivedMessageEvents();
            _receivedMessage.ReceivedMessage += _receivedMessage_ReceivedMessage;
            StartOver();
        }

        private void _receivedMessage_ReceivedMessage()
        {
            if (!string.IsNullOrEmpty(_receivedMessage.Message.Text))
            {
                _message = _receivedMessage.Message;

                _step = _message.TypeTransport == TypeTransportObject.StepXs;
                Step = _message.Text;
                _board = _message.Board;
                WinsXs = _message.WinsXs;
                WinsOs = _message.WinsOs;

                Dispatcher.Invoke(new ThreadStart(delegate
                {
                    UpdateBoard();
                    BindingOperations.GetBindingExpression(LabelStep, ContentProperty).UpdateTarget();
                    UpdateTargetWins();
                }));
            }
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

            SendMessage();
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

                if (!IsActiveMultiplayer)
                {
                    MessageBoxResult result = MessageBox.Show(this, "Следующая игра?", "Игра закончена", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                    if (result == MessageBoxResult.Yes)
                        NextGame();
                }
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

        private void ButtonMultiplayer_Click(object sender, RoutedEventArgs e)
        {
            Multiplayer form = new Multiplayer() { Owner = this };
            form.ShowDialog();

            if (form.IsServer)
                _connector = form.Server;
            else if (form.IsClient)
                _connector = form.Client;

            if (_connector != null)
            {
                _connector.ReceivedMessage = _receivedMessage;
                _message = new TransportObjects();
                ReadMessage();
            }
        }

        private void SendMessage()
        {
            if (_connector == null || _message == null)
                return;

            _message.Clear();

            _message.TypeTransport = _step ? TypeTransportObject.StepXs : TypeTransportObject.StepOs;
            _message.Text = _step ? _textStepXs : _textStepOs;
            _message.Board = Board;

            _connector.SendMessage(_message.ToString());
        }

        private async void ReadMessage()
        {
            await _connector.ReadMessageAsync();
        }

    }
}
