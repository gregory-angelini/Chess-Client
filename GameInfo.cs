using System;
using System.Collections.Generic;
using System.Text;

namespace ChessClient
{
    struct GameInfo
    {
        public int gameId;
        public string FEN;
        public string status;
        public string whitePlayer;
        public string blackPlayer;
        public string lastMove;
        //public string yourColor;
        //public bool isYourMove;
        public string gameResult;// draw, resign, checkmate, stalemate
        public string winnerPlayer;
    }
}
