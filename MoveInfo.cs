using System;
using System.Collections.Generic;
using System.Text;

namespace ChessClient
{
    public class MoveInfo
    {
        public int playerID;
        public int gameID;
        public string fenMove;
        public bool drawOffer;
        public bool resignOffer;
    }
}
