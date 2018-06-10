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
    /// Логика взаимодействия для UserControlIP.xaml
    /// </summary>
    public partial class UserControlIP : UserControl
    {
        #region class variables and properties

        #region public variables and properties
        public TextBox FirstBox { get { return Part1; } }
        public TextBox SecondBox { get { return Part2; } }
        public TextBox ThirdBox { get { return Part3; } }
        public TextBox FourthBox { get { return Part4; } }

        public bool IsReadOnly { get; set; }

        public string IP { get => GetIP(); set => ParseStringIP(value); }
        public bool IsFilled { get => IPIsFilled(); }
        #endregion

        #region private variables and properties
        private const string _errorMessage = "Введите число от 0 до 255.";
        #endregion

        #endregion

        #region constructors

        public UserControlIP()
        {
            InitializeComponent();
        }
      
        #endregion

        #region methods

        #region public methods
        public byte[] GetByteArray()
        {
            byte[] userInput = new byte[4];

            userInput[0] = Convert.ToByte(Part1.Text);
            userInput[1] = Convert.ToByte(Part2.Text);
            userInput[2] = Convert.ToByte(Part3.Text);
            userInput[3] = Convert.ToByte(Part4.Text);

            return userInput;
        }
        #endregion

        #region private methods
        private void JumpRight(TextBox rightNeighborBox, KeyEventArgs e)
        {
            rightNeighborBox.Focus();
            rightNeighborBox.CaretIndex = 0;
            e.Handled = true;
        }

        private void JumpLeft(TextBox leftNeighborBox, KeyEventArgs e)
        {
            leftNeighborBox.Focus();
            if (leftNeighborBox.Text != "")
            {
                leftNeighborBox.CaretIndex = leftNeighborBox.Text.Length;
            }
            e.Handled = true;
        }

        //checks for backspace, arrow and decimal key presses and jumps boxes if needed.
        //returns true when key was matched, false if not.
        private bool CheckJumpRight(TextBox previousBox, TextBox currentBox, TextBox rightNeighborBox, KeyEventArgs e)
        {
            bool downShift = ((KeyboardDevice)e.Device).Modifiers == ModifierKeys.Shift;

            switch (e.Key)
            {
                case Key.Tab:
                    if (downShift)
                        JumpLeft(previousBox, e);
                    else
                       if (currentBox.CaretIndex == currentBox.Text.Length || string.IsNullOrEmpty(currentBox.Text))
                        JumpRight(rightNeighborBox, e);
                    return true;
                case Key.Right:
                    if (currentBox.CaretIndex == currentBox.Text.Length || string.IsNullOrEmpty(currentBox.Text))
                        JumpRight(rightNeighborBox, e);
                    return true;
                case Key.OemPeriod:
                case Key.Decimal:
                case Key.Space:
                    JumpRight(rightNeighborBox, e);
                    rightNeighborBox.SelectAll();
                    return true;
                default:
                    return false;
            }
        }

        private bool CheckJumpLeft(TextBox currentBox, TextBox leftNeighborBox, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    if (currentBox.CaretIndex == 0 || currentBox.Text == "")
                    {
                        JumpLeft(leftNeighborBox, e);
                    }
                    return true;
                case Key.Back:
                    if ((currentBox.CaretIndex == 0 || currentBox.Text == "") && currentBox.SelectionLength == 0)
                    {
                        JumpLeft(leftNeighborBox, e);
                    }
                    return true;
                default:
                    return false;
            }
        }

        //discards non digits, prepares IPMaskedBox for textchange.
        private void HandleTextInput(TextBox currentBox, TextBox rightNeighborBox, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(Convert.ToChar(e.Text)))
            {
                e.Handled = true;
                //SystemSounds.Beep.Play();
                return;
            }

            if (currentBox.Text.Length == 3 && currentBox.SelectionLength == 0)
            {
                e.Handled = true;
                //SystemSounds.Beep.Play();
                if (currentBox != Part4)
                {
                    rightNeighborBox.Focus();
                    rightNeighborBox.SelectAll();
                }
            }
        }

        //checks whether textbox content > 255 when 3 characters have been entered.
        //clears if > 255, switches to next textbox otherwise 
        private void HandleTextChange(TextBox currentBox, TextBox rightNeighborBox)
        {
            if (currentBox.Text.Length == 3)
            {
                try
                {
                    Convert.ToByte(currentBox.Text);
                }
                catch (Exception exception) when (exception is FormatException || exception is OverflowException)
                {
                    currentBox.Clear();
                    currentBox.Focus();
                    //SystemSounds.Beep.Play();
                    MessageBox.Show(_errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (currentBox.CaretIndex != 2 && currentBox != Part4)
                {
                    rightNeighborBox.CaretIndex = rightNeighborBox.Text.Length;
                    rightNeighborBox.SelectAll();
                    rightNeighborBox.Focus();
                }
            }
        }
        #endregion

        #endregion

        #region Events
        //jump right, left or stay. 
        private void FirstByte_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (IsReadOnly)
                if (!IsKeyDigit(e.Key))
                {
                    e.Handled = true;
                    return;
                }

            CheckJumpRight(Part1, Part1, Part2, e);
        }

        private void SecondByte_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (IsReadOnly)
                if (!IsKeyDigit(e.Key))
                {
                    e.Handled = true;
                    return;
                }

            if (CheckJumpRight(Part1, Part2, Part3, e))
                return;

            CheckJumpLeft(Part2, Part1, e);
        }

        private void ThirdByte_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (IsReadOnly)
                if (!IsKeyDigit(e.Key))
                {
                    e.Handled = true;
                    return;
                }

            if (CheckJumpRight(Part2, Part3, Part4, e))
                return;

            CheckJumpLeft(Part3, Part2, e);
        }

        private void FourthByte_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (IsReadOnly)
                if (!IsKeyDigit(e.Key))
                {
                    e.Handled = true;
                    return;
                }

            CheckJumpLeft(Part4, Part3, e);

            if (e.Key == Key.Space)
            {
                //SystemSounds.Beep.Play();
                e.Handled = true;
            }
        }


        //discards non digits, prepares IPMaskedBox for textchange.
        private void FirstByte_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            HandleTextInput(Part1, Part2, e);
        }

        private void SecondByte_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            HandleTextInput(Part2, Part3, e);
        }

        private void ThirdByte_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            HandleTextInput(Part3, Part4, e);
        }

        private void FourthByte_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            HandleTextInput(Part4, Part4, e); //pass fourthbyte twice because no right neighboring box.
        }


        //checks whether textbox content > 255 when 3 characters have been entered.
        //clears if > 255, switches to next textbox otherwise 
        private void FirstByte_TextChanged(object sender, TextChangedEventArgs e)
        {
            HandleTextChange(Part1, Part2);
        }

        private void SecondByte_TextChanged(object sender, TextChangedEventArgs e)
        {
            HandleTextChange(Part2, Part3);
        }

        private void ThirdByte_TextChanged(object sender, TextChangedEventArgs e)
        {
            HandleTextChange(Part3, Part4);
        }

        private void FourthByte_TextChanged(object sender, TextChangedEventArgs e)
        {
            HandleTextChange(Part4, Part4);
        }
        #endregion

        private bool IsKeyDigit(Key key)
            => !((key >= Key.D0 && key <= Key.D9) && (key >= Key.NumPad0 && key <= Key.NumPad9));

        private void ParseStringIP(string value)
        {
            string[] partsIP = value.Split('.');

            if (partsIP.Count() == 4)
            {
                Part1.Text = partsIP[0];
                Part2.Text = partsIP[1];
                Part3.Text = partsIP[2];
                Part4.Text = partsIP[3];
            }
            else
            {
                Part1.Text = "";
                Part2.Text = "";
                Part3.Text = "";
                Part4.Text = "";
            }
        }

        private string GetIP()
        {
            StringBuilder ip = new StringBuilder();

            ip.Append(Part1.Text);
            ip.Append(".");
            ip.Append(Part2.Text);
            ip.Append(".");
            ip.Append(Part3.Text);
            ip.Append(".");
            ip.Append(Part4.Text);

            return ip.ToString();
        }

        private bool IPIsFilled()
        {
            return TextBoxtTextIsFilled(Part1) && TextBoxtTextIsFilled(Part2) && TextBoxtTextIsFilled(Part3) && TextBoxtTextIsFilled(Part4);
        }

        private bool TextBoxtTextIsFilled(TextBox textBox)
        {
            return !string.IsNullOrWhiteSpace(textBox.Text);
        }
    }
}
