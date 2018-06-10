using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace TicTacToe
{
    internal class Board
    {
        #region Fields board
        private int _row1Column1;
        private int _row1Column2;
        private int _row1Column3;
        private int _row2Column1;
        private int _row2Column2;
        private int _row2Column3;
        private int _row3Column1;
        private int _row3Column2;
        private int _row3Column3;
        #endregion

        #region Properties board
        internal int Row1Column1 { get => _row1Column1; set { if (_row1Column1 != value) { _row1Column1 = value; CheckAllBoard(); } } }
        internal int Row1Column2 { get => _row1Column2; set { if (_row1Column2 != value) { _row1Column2 = value; CheckAllBoard(); } } }
        internal int Row1Column3 { get => _row1Column3; set { if (_row1Column3 != value) { _row1Column3 = value; CheckAllBoard(); } } }
        internal int Row2Column1 { get => _row2Column1; set { if (_row2Column1 != value) { _row2Column1 = value; CheckAllBoard(); } } }
        internal int Row2Column2 { get => _row2Column2; set { if (_row2Column2 != value) { _row2Column2 = value; CheckAllBoard(); } } }
        internal int Row2Column3 { get => _row2Column3; set { if (_row2Column3 != value) { _row2Column3 = value; CheckAllBoard(); } } }
        internal int Row3Column1 { get => _row3Column1; set { if (_row3Column1 != value) { _row3Column1 = value; CheckAllBoard(); } } }
        internal int Row3Column2 { get => _row3Column2; set { if (_row3Column2 != value) { _row3Column2 = value; CheckAllBoard(); } } }
        internal int Row3Column3 { get => _row3Column3; set { if (_row3Column3 != value) { _row3Column3 = value; CheckAllBoard(); } } }
        #endregion

        #region Properties state of               
        internal bool FilledAllCells { get; private set; }

        internal bool Win { get; private set; }
        internal bool WinXs { get; private set; }
        internal bool WinOs { get; private set; }

        internal bool? Row1Win { get; private set; }
        internal bool? Row2Win { get; private set; }
        internal bool? Row3Win { get; private set; }
        internal bool? Column1Win { get; private set; }
        internal bool? Column2Win { get; private set; }
        internal bool? Column3Win { get; private set; }
        internal bool? Diagonal1Win { get; private set; }
        internal bool? Diagonal2Win { get; private set; }
        #endregion

        private int _countValueRow1;
        private int _countValueRow2;
        private int _countValueRow3;
        private int _countValueColumn1;
        private int _countValueColumn2;
        private int _countValueColumn3;
        private int _countValueDiagonal1;
        private int _countValueDiagonal2;

        private bool _initializeBoard;

        private void CheckWinState()
        {
            if (_initializeBoard)
                return;

            #region Rows win
            if (_countValueRow1 == 0 || _countValueRow1 == 3)
            {
                Row1Win = _countValueRow1 == 3 ? true : false;
                SetStateWin((bool)Row1Win);
            }
            else
                Row1Win = null;

            if (_countValueRow2 == 0 || _countValueRow2 == 3)
            {
                Row2Win = _countValueRow2 == 3 ? true : false;
                SetStateWin((bool)Row2Win);
            }
            else
                Row2Win = null;

            if (_countValueRow3 == 0 || _countValueRow3 == 3)
            {
                Row3Win = _countValueRow3 == 3 ? true : false;
                SetStateWin((bool)Row3Win);
            }
            else
                Row1Win = null;
            #endregion

            #region Columns win
            if (_countValueColumn1 == 0 || _countValueColumn1 == 3)
            {
                Column1Win = _countValueColumn1 == 3 ? true : false;
                SetStateWin((bool)Column1Win);
            }
            else
                Column1Win = null;

            if (_countValueColumn2 == 0 || _countValueColumn2 == 3)
            {
                Column2Win = _countValueColumn2 == 3 ? true : false;
                SetStateWin((bool)Column2Win);
            }
            else
                Column2Win = null;

            if (_countValueColumn3 == 0 || _countValueColumn3 == 3)
            {
                Column3Win = _countValueColumn3 == 3 ? true : false;
                SetStateWin((bool)Column3Win);
            }
            else
                Column3Win = null;
            #endregion

            #region Diagoanl win
            if (_countValueDiagonal1 == 0 || _countValueDiagonal1 == 3)
            {
                Diagonal1Win = _countValueDiagonal1 == 3 ? true : false;
                SetStateWin((bool)Diagonal1Win);
            }
            else
                Diagonal1Win = null;

            if (_countValueDiagonal2 == 0 || _countValueDiagonal2 == 3)
            {
                Diagonal2Win = _countValueDiagonal2 == 3 ? true : false;
                SetStateWin((bool)Diagonal2Win);
            }
            else
                Diagonal2Win = null;
            #endregion

            FilledAllCells = _countValueRow1 >= 0 && _countValueRow2 >= 0 && _countValueRow3 >= 0;
        }

        private void SetStateWin(bool value)
        {
            Win = true;

            if (value)
                WinXs = true;
            else
                WinOs = true;
        }

        internal void Clear()
        {
            _initializeBoard = true;

            Row1Column1 = -3; Row1Column2 = -3; Row1Column3 = -3;
            Row2Column1 = -3; Row2Column2 = -3; Row2Column3 = -3;
            Row3Column1 = -3; Row3Column2 = -3; Row3Column3 = -3;

            _initializeBoard = false;

            Row1Win = null; Row2Win = null; Row3Win = null;
            Column1Win = null; Column2Win = null; Column3Win = null;
            Diagonal1Win = null; Diagonal2Win = null;
            FilledAllCells = false;

            Win = false; WinOs = false; WinXs = false;
        }

        internal void NextGame()
        {
            Clear();
        }

        internal bool SetValueCell(string propertyName, bool stateBool)
        {
            int state = stateBool ? 1 : 0;

            switch (propertyName)
            {
                case "Row1Column1":
                    if (Row1Column1 < 0) { Row1Column1 = state; return true; }
                    break;
                case "Row1Column2":
                    if (Row1Column2 < 0) { Row1Column2 = state; return true; }
                    break;
                case "Row1Column3":
                    if (Row1Column3 < 0) { Row1Column3 = state; return true; }
                    break;
                case "Row2Column1":
                    if (Row2Column1 < 0) { Row2Column1 = state; return true; }
                    break;
                case "Row2Column2":
                    if (Row2Column2 < 0) { Row2Column2 = state; return true; }
                    break;
                case "Row2Column3":
                    if (Row2Column3 < 0) { Row2Column3 = state; return true; }
                    break;
                case "Row3Column1":
                    if (Row3Column1 < 0) { Row3Column1 = state; return true; }
                    break;
                case "Row3Column2":
                    if (Row3Column2 < 0) { Row3Column2 = state; return true; }
                    break;
                case "Row3Column3":
                    if (Row3Column3 < 0) { Row3Column3 = state; return true; }
                    break;

            }
            return false;
        }

        private void CheckAllBoard()
        {
            CheckRow1();
            CheckRow2();
            CheckRow3();
            CheckColumn1();
            CheckColumn2();
            CheckColumn3();
            CheckDiagonal1();
            CheckDiagonal2();

            CheckWinState();
        }

        private void CheckRow1() => _countValueRow1 = _row1Column1 + _row1Column2 + _row1Column3;
        private void CheckRow2() => _countValueRow2 = _row2Column1 + _row2Column2 + _row2Column3;
        private void CheckRow3() => _countValueRow3 = _row3Column1 + _row3Column2 + _row3Column3;
        private void CheckColumn1() => _countValueColumn1 = _row1Column1 + _row2Column1 + _row3Column1;
        private void CheckColumn2() => _countValueColumn2 = _row1Column2 + _row2Column2 + _row3Column2;
        private void CheckColumn3() => _countValueColumn3 = _row1Column3 + _row2Column3 + _row3Column3;
        private void CheckDiagonal1() => _countValueDiagonal1 = _row1Column1 + _row2Column2 + _row3Column3;
        private void CheckDiagonal2() => _countValueDiagonal2 = _row1Column3 + _row2Column2 + _row3Column1;

        public override string ToString()
        {
            string[] cellData = new string[9]
            {
                _row1Column1.ToString(),
                _row1Column2.ToString(),
                _row1Column3.ToString(),
                _row2Column1.ToString(),
                _row2Column2.ToString(),
                _row2Column3.ToString(),
                _row3Column1.ToString(),
                _row3Column2.ToString(),
                _row3Column3.ToString()
            };

            return new JavaScriptSerializer().Serialize(cellData);
        }

        public void Parse(string text)
        {
            string[] cellData = new JavaScriptSerializer().Deserialize<string[]>(text);
            _row1Column1 = int.Parse(cellData[0]);
            _row1Column2 = int.Parse(cellData[1]);
            _row1Column3 = int.Parse(cellData[2]);
            _row2Column1 = int.Parse(cellData[3]);
            _row2Column2 = int.Parse(cellData[4]);
            _row2Column3 = int.Parse(cellData[5]);
            _row3Column1 = int.Parse(cellData[6]);
            _row3Column2 = int.Parse(cellData[7]);
            _row3Column3 = int.Parse(cellData[8]);

            CheckAllBoard();
            CheckWinState();
        }
    }
}
