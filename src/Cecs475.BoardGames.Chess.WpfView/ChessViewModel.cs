﻿using Cecs475.BoardGames.Chess.Model;
using Cecs475.BoardGames.Model;
using Cecs475.BoardGames.WpfView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecs475.BoardGames.Chess.WpfView
{
    public class ChessSquare : INotifyPropertyChanged
    {
        private ChessPiece mChessPiece;

        public ChessPiece chessPiece
        {
            get
            {
                return mChessPiece;
            }
            set
            {
                if (!value.Equals(mChessPiece))
                {
                    mChessPiece = value;
                    OnPropertyChanged(nameof(chessPiece));
                }
            }
        }
        public BoardPosition Position
        {
            get; set;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        private bool mIsHighlighted;
        /// <summary>
        /// Whether the square should be highlighted because of a user action.
        /// </summary>
        public bool IsHighlighted
        {
            get { return mIsHighlighted; }
            set
            {
                if (value != mIsHighlighted)
                {
                    mIsHighlighted = value;
                    OnPropertyChanged(nameof(IsHighlighted));
                }
            }
        }
    }
    public class ChessViewModel : INotifyPropertyChanged, IGameViewModel
    {
        private ChessBoard mChessBoard;
        private ObservableCollection<ChessSquare> mSquares;

        public event EventHandler GameFinished;
        public event PropertyChangedEventHandler PropertyChanged;

        public ChessViewModel()
        {
            mChessBoard = new ChessBoard();
            mSquares = new ObservableCollection<ChessSquare>(
                BoardPosition.GetRectangularPositions(8, 8)
                .Select(pos => new ChessSquare()
                {
                    Position = pos,
                    chessPiece = mChessBoard.GetPieceAtPosition(pos)
                })
            );


            //might  not be select from m.startposition
            PossibleMoves = new HashSet<BoardPosition>(
                from ChessMove m in mChessBoard.GetPossibleMoves()
                select (m.StartPosition)
            ) ;
        }
        public ObservableCollection<ChessSquare> ChessSquares
        {
            get { return mSquares; }
        }
        public int CurrentPlayer
        {
            get { return mChessBoard.CurrentPlayer; }
        }
        public HashSet<BoardPosition> PossibleMoves { get; private set; }

        public GameAdvantage BoardAdvantage => mChessBoard.CurrentAdvantage;


        public bool CanUndo => true;

        public void UndoMove()
        {
            throw new NotImplementedException();
        }
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}