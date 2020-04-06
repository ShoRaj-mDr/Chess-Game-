﻿using Cecs475.BoardGames.WpfView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Cecs475.BoardGames.Chess.WpfView
{
    public class ChessGameFactory : IWpfGameFactory
    {
        public string GameName
        {
            get
            {
                return "Chess";
            }
        }

        public IValueConverter CreateBoardAdvantageConverter()
        {
            return null;
        }

        public IValueConverter CreateCurrentPlayerConverter()
        {
            return null;
        }

        public IWpfGameView CreateGameView()
        {
            return new ChessView();
        }
    }
}