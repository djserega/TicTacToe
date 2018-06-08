using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class Board
    {
        #region Fields board
        private bool? _row1Column1;
        private bool? _row1Column2;
        private bool? _row1Column3;
        private bool? _row2Column1;
        private bool? _row2Column2;
        private bool? _row2Column3;
        private bool? _row3Column1;
        private bool? _row3Column2;
        private bool? _row3Column3;
        #endregion

        #region Properties board
        internal bool? Row1Column1 { get => _row1Column1; set { if (_row1Column1 != value) { _row1Column1 = value; CheckWinState(); } } }
        internal bool? Row1Column2 { get => _row1Column2; set { if (_row1Column2 != value) { _row1Column2 = value; CheckWinState(); } } }
        internal bool? Row1Column3 { get => _row1Column3; set { if (_row1Column3 != value) { _row1Column3 = value; CheckWinState(); } } }
        internal bool? Row2Column1 { get => _row2Column1; set { if (_row2Column1 != value) { _row2Column1 = value; CheckWinState(); } } }
        internal bool? Row2Column2 { get => _row2Column2; set { if (_row2Column2 != value) { _row2Column2 = value; CheckWinState(); } } }
        internal bool? Row2Column3 { get => _row2Column3; set { if (_row2Column3 != value) { _row2Column3 = value; CheckWinState(); } } }
        internal bool? Row3Column1 { get => _row3Column1; set { if (_row3Column1 != value) { _row3Column1 = value; CheckWinState(); } } }
        internal bool? Row3Column2 { get => _row3Column2; set { if (_row3Column2 != value) { _row3Column2 = value; CheckWinState(); } } }
        internal bool? Row3Column3 { get => _row3Column3; set { if (_row3Column3 != value) { _row3Column3 = value; CheckWinState(); } } }
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

        private void CheckWinState()
        {
            #region Rows win
            if (_row1Column1 == null || _row1Column2 == null || _row1Column3 == null)
                Row1Win = null;
            else
            {
                Row1Win = (bool)_row1Column1 && (bool)_row1Column2 && (bool)_row1Column3;
                if ((bool)Row1Win)
                    SetStateWin((bool)Row1Win);
                else
                    Row1Win = null;
            }

            if (_row2Column1 == null || _row2Column2 == null || _row2Column3 == null)
                Row2Win = null;
            else
            {
                Row2Win = (bool)_row2Column1 && (bool)_row2Column2 && (bool)_row2Column3;
                if ((bool)Row2Win)
                    SetStateWin((bool)Row2Win);
                else
                    Row2Win = null;
            }

            if (_row3Column1 == null || _row3Column2 == null || _row3Column3 == null)
                Row1Win = null;
            else
            {
                Row3Win = (bool)_row3Column1 && (bool)_row3Column2 && (bool)_row3Column3;
                if ((bool)Row3Win)
                    SetStateWin((bool)Row3Win);
                else
                    Row3Win = null;
            }
            #endregion

            #region Columns win
            if (_row1Column1 == null || _row2Column1 == null || _row3Column1 == null)
                Column1Win = null;
            else
            {
                Column1Win = (bool)_row1Column1 && (bool)_row2Column1 && (bool)_row3Column1;
                if ((bool)Column1Win)
                    SetStateWin((bool)Column1Win);
                else
                    Column1Win = null;
            }

            if (_row1Column2 == null || _row2Column2 == null || _row3Column2 == null)
                Column2Win = null;
            else
            {
                Column2Win = (bool)_row1Column2 && (bool)_row2Column2 && (bool)_row3Column2;
                if ((bool)Column2Win)
                    SetStateWin((bool)Column2Win);
                else
                    Column2Win = null;

            }

            if (_row1Column3 == null || _row2Column3 == null || _row3Column3 == null)
                Column3Win = null;
            else
            {
                Column3Win = (bool)_row1Column3 && (bool)_row2Column3 && (bool)_row3Column3;
                if ((bool)Column3Win)
                    SetStateWin((bool)Column3Win);
                else
                    Column3Win = null;

            }
            #endregion

            #region Diagoanl win
            if (_row1Column1 == null || _row2Column2 == null || _row3Column3 == null)
                Diagonal1Win = null;
            else
            {
                Diagonal1Win = (bool)_row1Column1 && (bool)_row2Column2 && (bool)_row3Column3;
                if ((bool)Diagonal1Win)
                    SetStateWin((bool)Diagonal1Win);
                else
                    Diagonal1Win = null;
            }

            if (_row1Column3 == null || _row2Column2 == null || _row3Column1 == null)
                Diagonal2Win = null;
            else
            {
                Diagonal2Win = (bool)_row1Column3 && (bool)_row2Column2 && (bool)_row3Column1;
                if ((bool)Diagonal2Win)
                    SetStateWin((bool)Diagonal2Win);
                else
                    Diagonal2Win = null;
            }
            #endregion

            FilledAllCells = _row1Column1 != null && _row1Column2 != null && _row1Column3 != null
                && _row1Column1 != null && _row2Column1 != null && _row3Column1 != null
                && _row1Column1 != null && _row2Column2 != null && _row3Column3 != null;
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
            Row1Column1 = null; Row1Column2 = null; Row1Column3 = null;
            Row2Column1 = null; Row2Column2 = null; Row2Column3 = null;
            Row3Column1 = null; Row3Column2 = null; Row3Column3 = null;

            Row1Win = null; Row2Win = null; Row3Win = null;
            Column1Win = null; Column2Win = null; Column3Win = null;
            Diagonal1Win = null; Diagonal2Win = null;

            Win = false; WinOs = false; WinXs = false;
        }

        internal void NextGame()
        {
            Clear();
        }

        internal bool SetValueCell(string propertyName, bool state)
        {
            switch (propertyName)
            {
                case "Row1Column1":
                    if (Row1Column1 == null) { Row1Column1 = state; return true; }
                    break;
                case "Row1Column2":
                    if (Row1Column2 == null) { Row1Column2 = state; return true; }
                    break;
                case "Row1Column3":
                    if (Row1Column3 == null) { Row1Column3 = state; return true; }
                    break;
                case "Row2Column1":
                    if (Row2Column1 == null) { Row2Column1 = state; return true; }
                    break;
                case "Row2Column2":
                    if (Row2Column2 == null) { Row2Column2 = state; return true; }
                    break;
                case "Row2Column3":
                    if (Row2Column3 == null) { Row2Column3 = state; return true; }
                    break;
                case "Row3Column1":
                    if (Row3Column1 == null) { Row3Column1 = state; return true; }
                    break;
                case "Row3Column2":
                    if (Row3Column2 == null) { Row3Column2 = state; return true; }
                    break;
                case "Row3Column3":
                    if (Row3Column3 == null) { Row3Column3 = state; return true; }
                    break;

            }
            return false;
        }

    }
}
