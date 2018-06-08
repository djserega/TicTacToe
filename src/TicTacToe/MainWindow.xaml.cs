﻿using System;
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

            BindingOperations.GetBindingExpression(LabelWinsXs, ContentProperty).UpdateTarget();
            BindingOperations.GetBindingExpression(LabelWinsOs, ContentProperty).UpdateTarget();

            _board.Clear();

            CheckValueBoard();
            ChangeGameStep();
        }

        private void CheckValueBoard()
        {
            ImageRow1Column1.ImageSource = _board.Row1Column1 == null ? null : ((bool)_board.Row1Column1 ? _imageSourceXs : _imageSourceOs);
            ImageRow1Column2.ImageSource = _board.Row1Column2 == null ? null : ((bool)_board.Row1Column2 ? _imageSourceXs : _imageSourceOs);
            ImageRow1Column3.ImageSource = _board.Row1Column3 == null ? null : ((bool)_board.Row1Column3 ? _imageSourceXs : _imageSourceOs);

            ImageRow2Column1.ImageSource = _board.Row2Column1 == null ? null : ((bool)_board.Row2Column1 ? _imageSourceXs : _imageSourceOs);
            ImageRow2Column2.ImageSource = _board.Row2Column2 == null ? null : ((bool)_board.Row2Column2 ? _imageSourceXs : _imageSourceOs);
            ImageRow2Column3.ImageSource = _board.Row2Column3 == null ? null : ((bool)_board.Row2Column3 ? _imageSourceXs : _imageSourceOs);

            ImageRow3Column1.ImageSource = _board.Row3Column1 == null ? null : ((bool)_board.Row3Column1 ? _imageSourceXs : _imageSourceOs);
            ImageRow3Column2.ImageSource = _board.Row3Column2 == null ? null : ((bool)_board.Row3Column2 ? _imageSourceXs : _imageSourceOs);
            ImageRow3Column3.ImageSource = _board.Row3Column3 == null ? null : ((bool)_board.Row3Column3 ? _imageSourceXs : _imageSourceOs);
        }

        private void ChangeGameStep()
        {
            _step = !_step;

            Step = _step ? _textStepXs : _textStepOs;

            BindingOperations.GetBindingExpression(LabelStep, ContentProperty).UpdateTarget();
        }

        #region Board

        private void CanvasRow1Column1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _board.Row1Column1 = _step;
            ChangeGameStep();
            CheckValueBoard();
        }

        private void CanvasRow1Column2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _board.Row1Column2 = _step;
            ChangeGameStep();
            CheckValueBoard();
        }

        private void CanvasRow1Column3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _board.Row1Column3 = _step;
            ChangeGameStep();
            CheckValueBoard();
        }

        private void CanvasRow2Column1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _board.Row2Column1 = _step;
            ChangeGameStep();
            CheckValueBoard();
        }

        private void CanvasRow2Column2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _board.Row2Column2 = _step;
            ChangeGameStep();
            CheckValueBoard();
        }

        private void CanvasRow2Column3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _board.Row2Column3 = _step;
            ChangeGameStep();
            CheckValueBoard();
        }

        private void CanvasRow3Column1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _board.Row3Column1 = _step;
            ChangeGameStep();
            CheckValueBoard();
        }

        private void CanvasRow3Column2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _board.Row3Column2 = _step;
            ChangeGameStep();
            CheckValueBoard();
        }

        private void CanvasRow3Column3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _board.Row3Column3 = _step;
            ChangeGameStep();
            CheckValueBoard();
        }

        #endregion

    }
}
