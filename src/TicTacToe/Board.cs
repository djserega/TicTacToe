using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class Board
    {
        internal bool? Row1Column1 { get; set; }
        internal bool? Row1Column2 { get; set; }
        internal bool? Row1Column3 { get; set; }
        internal bool? Row2Column1 { get; set; }
        internal bool? Row2Column2 { get; set; }
        internal bool? Row2Column3 { get; set; }
        internal bool? Row3Column1 { get; set; }
        internal bool? Row3Column2 { get; set; }
        internal bool? Row3Column3 { get; set; }
        
        internal void Clear()
        {
            Row1Column1 = null;
            Row1Column2 = null;
            Row1Column3 = null;

            Row2Column1 = null;
            Row2Column2 = null;
            Row2Column3 = null;

            Row3Column1 = null;
            Row3Column2 = null;
            Row3Column3 = null;
        }
    }
}
