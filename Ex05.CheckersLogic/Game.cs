using System;
using System.Collections.Generic;

namespace Ex05.CheckersLogic
{
    public class Game
    {
        public enum ePlayerNum
        {
            Player1 = 1,
            Player2 = 2
        }

        public EventHandler AfterGameEnded;
        public EventHandler GameUpdatedBoard;
        public EventHandler NextPlayerTurn;
        private readonly Board r_Board;
        private bool m_OCanEat;
        private bool m_XCanEat;
        private bool m_SkipTurn;
        private bool m_GameEnded;
        private bool m_Tie;
        private bool m_XWin;
        private bool m_OWin;

        public Game(Player i_PlayerOne, Player i_PlayerTwo, Board i_Board, int i_CurrentPlayer)
        {
            PlayerOne = i_PlayerOne;
            PlayerTwo = i_PlayerTwo;
            r_Board = i_Board;
            CurrPlayer = (ePlayerNum)i_CurrentPlayer;
        }

        public Board BoardMatrix
        {
            get
            {
                return r_Board;
            }
        }

        public Player PlayerOne { get; set; }

        public Player PlayerTwo { get; set; }

        public string LastMove { get; set; }

        public ePlayerNum CurrPlayer { get; set; }

        public bool SkipTurn
        {
            get
            {
                return m_SkipTurn;
            }

            set
            {
                m_SkipTurn = value;
            }
        }

        public bool XWin
        {
            get
            {
                return m_XWin;
            }

            set
            {
                m_XWin = value;
            }
        }

        public bool OWin
        {
            get
            {
                return m_OWin;
            }

            set
            {
                m_OWin = value;
            }
        }

        public bool Tie
        {
            get
            {
                return m_Tie;
            }

            set
            {
                m_Tie = value;
            }
        }

        public bool Ended
        {
            get
            {
                return m_GameEnded;
            }

            set
            {
                m_GameEnded = value;
            }
        }


        // Checks if player one can move
        public bool CanPlayer1Move()
        {
            bool canMove = false;

            foreach(Pawn pawn in r_Board.PlayerOnePawns)
            {
                if(CanXEat(pawn) || CanMoveUpperLeft(pawn) || CanMoveUpperRight(pawn))
                {
                    canMove = true;
                }

                if((pawn.LetterOfPawn == Pawn.ePawnType.K)
                   && (CanKingXEat(pawn) || CanMoveLowerLeft(pawn) || CanMoveLowerRight(pawn)))
                {
                    canMove = true;
                }
            }

            return canMove;
        }

        // Checks if player 2 can move
        public bool CanPlayer2Move()
        {
            bool canMove = false;

            foreach(Pawn pawn in r_Board.PlayerTwoPawns)
            {
                if(CanOEat(pawn) || CanMoveLowerLeft(pawn) || CanMoveLowerRight(pawn))
                {
                    canMove = true;
                }

                if((pawn.LetterOfPawn == Pawn.ePawnType.U)
                   && (CanKingOEat(pawn) || CanMoveUpperLeft(pawn) || CanMoveUpperRight(pawn)))
                {
                    canMove = true;
                }
            }

            return canMove;
        }

        public bool UpdateIfSomeoneCanEat()
        {
            if(m_SkipTurn == false)
            {
                m_XCanEat = false;
                m_OCanEat = false;
                UpdateAllEatingPawns();
            }

            return CurrPlayer == ePlayerNum.Player1 ? m_XCanEat : m_OCanEat;
        }

        // Our main functions: gets player's move checks if it a legal one and move accordingly
        public bool IsLegalMove(Coordinate i_Current, Coordinate i_Possible)
        {
            OnNextPlayerTurn();
            Coordinate possible = i_Possible;
            Coordinate current = i_Current;
            bool canBeMoved = true;
            bool isCoordinateHasPawn = (r_Board.MatrixOfBoard[i_Current.CoordinateRow, i_Current.CoordinateCol] != null);
            bool isPossibleEmpty = (r_Board.MatrixOfBoard[i_Possible.CoordinateRow, i_Possible.CoordinateCol] == null);
            bool isSomeoneCanEat = UpdateIfSomeoneCanEat();

            if(isCoordinateHasPawn && isPossibleEmpty)
            {
                if(isSomeoneCanEat) // Enter the eating method only if you have someone to eat
                {
                    // Checks if the coordinate the user chose to move from can even eat
                    if(r_Board.MatrixOfBoard[current.CoordinateRow, current.CoordinateCol].CanEat)
                    {
                        if(!MoveWithEat(current, possible))
                        {
                            canBeMoved = false;
                        }
                    }
                    else
                    {
                        canBeMoved = false;
                    }
                }
                else
                {
                    if(!MoveWithoutEat(current, possible))
                    {
                        canBeMoved = false;
                    }
                }

                // If i moves successfully 
                if(canBeMoved)
                {
                    AfterTurnMoveUpdate(i_Possible, isSomeoneCanEat);
                }

                MakeComputerTurn();
            }
            else
            {
                canBeMoved = false;
            }

            return canBeMoved;
        }

        public void MakeComputerTurn()
        {
            if(CurrPlayer == ePlayerNum.Player2 && PlayerTwo.PlayerType == Player.ePlayerType.Computer
                                                && m_GameEnded == false)
            {
                string computerMove = MakeComputerMove();
                Coordinate startMoveComputer;
                Coordinate finishMoveComputer;
                ParseCoordinate(computerMove, out startMoveComputer, out finishMoveComputer);
                IsLegalMove(startMoveComputer, finishMoveComputer);
            }
        }

        public void AfterTurnMoveUpdate(Coordinate i_Possible, bool i_IsSomeoneCanEat)
        {
            MakePawnKing(r_Board.MatrixOfBoard[i_Possible.CoordinateRow, i_Possible.CoordinateCol]);
            OnGameUpdatedBoard();

            // Checks if the user needs to continue eating
            if(EatingPerType(
                   i_Possible,
                   r_Board.MatrixOfBoard[i_Possible.CoordinateRow, i_Possible.CoordinateCol].LetterOfPawn)
               && i_IsSomeoneCanEat == true)
            {
                // enable only my soldier so only he can continue eating
                EnablePawnOnlyForThisPawn(r_Board.MatrixOfBoard[i_Possible.CoordinateRow, i_Possible.CoordinateCol]);
                m_SkipTurn = true;
            }
            else
            {
                AfterMove();
            }
        }

        public void OnGameUpdatedBoard()
        {
            if(GameUpdatedBoard != null)
            {
                GameUpdatedBoard.Invoke(this, EventArgs.Empty);
            }
        }

        public void AfterMove()
        {
            CurrPlayer = (CurrPlayer == ePlayerNum.Player1) ? ePlayerNum.Player2 : ePlayerNum.Player1;

            if(GetCurrentUser().PlayerLetterType == Player.eLetterType.X)
            {
                if(CanPlayer1Move() == false)
                {
                    m_GameEnded = true;
                    if(CanPlayer2Move() == false)
                    {
                        m_Tie = true;
                    }
                    else
                    {
                        m_OWin = true;
                        CalculatePoints();
                    }
                }
            }

            if(GetCurrentUser().PlayerLetterType == Player.eLetterType.O)
            {
                if(CanPlayer2Move() == false)
                {
                    m_GameEnded = true;
                    if(CanPlayer1Move() == false)
                    {
                        m_Tie = true;
                    }
                    else
                    {
                        m_XWin = true;
                        CalculatePoints();
                    }
                }
            }

            m_SkipTurn = false;

            if(m_GameEnded == true)
            {
                OnAfterGameEnded();
            }
            else
            {
                OnNextPlayerTurn();
            }
        }

        public void OnNextPlayerTurn()
        {
            if(NextPlayerTurn != null)
            {
                NextPlayerTurn.Invoke(this, EventArgs.Empty);
            }
        }

        public void OnAfterGameEnded()
        {
            if(AfterGameEnded != null)
            {
                AfterGameEnded.Invoke(this, EventArgs.Empty);
            }
        }

        // Calculated points after winning
        public void CalculatePoints()
        {
            int playerOnePoints = 0;
            int playerTwoPoints = 0;

            foreach(Pawn pawn in r_Board.PlayerOnePawns)
            {
                if(pawn.LetterOfPawn == Pawn.ePawnType.X)
                {
                    playerOnePoints++;
                }

                else
                {
                    playerOnePoints += 4;
                }
            }

            foreach(Pawn pawn in r_Board.PlayerTwoPawns)
            {
                if(pawn.LetterOfPawn == Pawn.ePawnType.O)
                {
                    playerTwoPoints++;
                }

                else
                {
                    playerTwoPoints += 4;
                }
            }

            if(m_OWin == true)
            {
                PlayerTwo.PlayerScore += (playerTwoPoints - playerOnePoints);
            }

            else
            {
                PlayerOne.PlayerScore += (playerOnePoints - playerTwoPoints);
            }
        }

        // Checks if pawn can be made king after moving
        public void MakePawnKing(Pawn i_Pawn)
        {
            if(i_Pawn.LetterOfPawn == Pawn.ePawnType.X && i_Pawn.Location.CoordinateRow == 0)
            {
                i_Pawn.LetterOfPawn = Pawn.ePawnType.K;
            }

            else if(i_Pawn.LetterOfPawn == Pawn.ePawnType.O && i_Pawn.Location.CoordinateRow == r_Board.SizeOfBoard - 1)
            {
                i_Pawn.LetterOfPawn = Pawn.ePawnType.U;
            }
        }

        // Enable only the pawn that can double eat for the next round
        public void EnablePawnOnlyForThisPawn(Pawn i_CurrentPawn)
        {
            if(i_CurrentPawn.LetterOfPlayer == Player.eLetterType.X)
            {
                foreach(Pawn pawn in r_Board.PlayerOnePawns)
                {
                    if(i_CurrentPawn != pawn)
                    {
                        pawn.CanEat = false;
                    }
                }
            }
            else
            {
                foreach(Pawn pawn in r_Board.PlayerTwoPawns)
                {
                    if(i_CurrentPawn != pawn)
                    {
                        pawn.CanEat = false;
                    }
                }
            }
        }

        // Update in the list of pawns if the pawn can eat
        public void UpdateAllEatingPawns()
        {
            if(GetCurrentUser().PlayerLetterType == Player.eLetterType.X)
            {
                foreach(Pawn pawn in r_Board.PlayerOnePawns)
                {
                    if((pawn.LetterOfPawn == Pawn.ePawnType.X) && CanXEat(pawn))
                    {
                        pawn.CanEat = true;
                        m_XCanEat = true;
                    }

                    else if((pawn.LetterOfPawn == Pawn.ePawnType.K) && CanKingXEat(pawn))
                    {
                        pawn.CanEat = true;
                        m_XCanEat = true;
                    }
                }
            }

            else
            {
                foreach(Pawn pawn in r_Board.PlayerTwoPawns)
                {
                    if((pawn.LetterOfPawn == Pawn.ePawnType.O) && CanOEat(pawn))
                    {
                        pawn.CanEat = true;
                        m_OCanEat = true;
                    }

                    else if((pawn.LetterOfPawn == Pawn.ePawnType.U) && CanKingOEat(pawn))
                    {
                        pawn.CanEat = true;
                        m_OCanEat = true;
                    }
                }
            }
        }

        // Checks the possibility of eating per the pawn the user chose
        public bool EatingPerType(Coordinate i_Pawn, Pawn.ePawnType i_PawnLetter)
        {
            bool isPossibleEat = false;
            switch(i_PawnLetter)
            {
                case Pawn.ePawnType.O:
                    isPossibleEat = CanOEat(r_Board.MatrixOfBoard[i_Pawn.CoordinateRow, i_Pawn.CoordinateCol]);
                    break;
                case Pawn.ePawnType.X:
                    isPossibleEat = CanXEat(r_Board.MatrixOfBoard[i_Pawn.CoordinateRow, i_Pawn.CoordinateCol]);
                    break;
                case Pawn.ePawnType.K:
                    isPossibleEat = CanKingXEat(r_Board.MatrixOfBoard[i_Pawn.CoordinateRow, i_Pawn.CoordinateCol]);
                    break;
                case Pawn.ePawnType.U:
                    isPossibleEat = CanKingOEat(r_Board.MatrixOfBoard[i_Pawn.CoordinateRow, i_Pawn.CoordinateCol]);
                    break;
            }

            return isPossibleEat;
        }

        // Checks if a regular pawn X has the possibility to eat at all
        public bool CanXEat(Pawn i_PawnX)
        {
            return CanXEatRight(i_PawnX, Player.eLetterType.O) || CanXEatLeft(i_PawnX, Player.eLetterType.O);
        }

        // Checks if a regular pawn O has the possibility to eat at all
        public bool CanOEat(Pawn i_PawnO)
        {
            return CanOEatRight(i_PawnO, Player.eLetterType.X) || CanOEatLeft(i_PawnO, Player.eLetterType.X);
        }

        // Checks if X king has the possibility to eat at all
        public bool CanKingXEat(Pawn i_PawnX)
        {
            return CanXEatRight(i_PawnX, Player.eLetterType.O) || CanXEatLeft(i_PawnX, Player.eLetterType.O)
                                                               || CanOEatRight(i_PawnX, Player.eLetterType.O)
                                                               || CanOEatLeft(i_PawnX, Player.eLetterType.O);
        }

        // Checks if a O king has the possibility to eat at all
        public bool CanKingOEat(Pawn i_PawnO)
        {
            return CanXEatRight(i_PawnO, Player.eLetterType.X) || CanXEatLeft(i_PawnO, Player.eLetterType.X)
                                                               || CanOEatRight(i_PawnO, Player.eLetterType.X)
                                                               || CanOEatLeft(i_PawnO, Player.eLetterType.X);
        }

        // Checks if coordinate has the possibly to eat upper right
        public bool CanXEatRight(Pawn i_PawnX, Player.eLetterType i_LetterType)
        {
            bool isPossibleToEat = false;
            if(IsInRange(i_PawnX.Location.CoordinateRow - 1, i_PawnX.Location.CoordinateCol + 1) && IsInRange(
                   i_PawnX.Location.CoordinateRow - 2,
                   i_PawnX.Location.CoordinateCol + 2) && r_Board.MatrixOfBoard[i_PawnX.Location.CoordinateRow - 1,
                   i_PawnX.Location.CoordinateCol + 1] != null)
            {
                if(r_Board.MatrixOfBoard[i_PawnX.Location.CoordinateRow - 1, i_PawnX.Location.CoordinateCol + 1]
                   != null)
                {
                    isPossibleToEat =
                        r_Board.MatrixOfBoard[i_PawnX.Location.CoordinateRow - 1, i_PawnX.Location.CoordinateCol + 1]
                            .LetterOfPlayer == i_LetterType && r_Board.MatrixOfBoard[i_PawnX.Location.CoordinateRow - 2,
                            i_PawnX.Location.CoordinateCol + 2] == null;
                }
            }

            return isPossibleToEat;
        }

        // Checks if coordinate has the possibly to move upper right
        public bool CanMoveUpperRight(Pawn i_PawnX)
        {
            bool isPossibleToMove = false;
            if(IsInRange(i_PawnX.Location.CoordinateRow - 1, i_PawnX.Location.CoordinateCol + 1))
            {
                isPossibleToMove =
                    r_Board.MatrixOfBoard[i_PawnX.Location.CoordinateRow - 1, i_PawnX.Location.CoordinateCol + 1]
                    == null;
            }

            return isPossibleToMove;
        }

        // Checks if coordinate has the possibly to move upper left
        public bool CanMoveUpperLeft(Pawn i_PawnX)
        {
            bool isPossibleToMove = false;
            if(IsInRange(i_PawnX.Location.CoordinateRow - 1, i_PawnX.Location.CoordinateCol - 1))
            {
                isPossibleToMove =
                    r_Board.MatrixOfBoard[i_PawnX.Location.CoordinateRow - 1, i_PawnX.Location.CoordinateCol - 1]
                    == null;
            }

            return isPossibleToMove;
        }

        // Checks if coordinate has the possibly to move lower right
        public bool CanMoveLowerRight(Pawn i_PawnX)
        {
            bool isPossibleToMove = false;
            if(IsInRange(i_PawnX.Location.CoordinateRow + 1, i_PawnX.Location.CoordinateCol + 1))
            {
                isPossibleToMove =
                    r_Board.MatrixOfBoard[i_PawnX.Location.CoordinateRow + 1, i_PawnX.Location.CoordinateCol + 1]
                    == null;
            }

            return isPossibleToMove;
        }

        // Checks if coordinate has the possibly to move lower left
        public bool CanMoveLowerLeft(Pawn i_PawnX)
        {
            bool isPossibleToMove = false;
            if(IsInRange(i_PawnX.Location.CoordinateRow + 1, i_PawnX.Location.CoordinateCol - 1))
            {
                isPossibleToMove =
                    r_Board.MatrixOfBoard[i_PawnX.Location.CoordinateRow + 1, i_PawnX.Location.CoordinateCol - 1]
                    == null;
            }

            return isPossibleToMove;
        }

        // Checks if coordinate has the possibly to eat upper left
        public bool CanXEatLeft(Pawn i_PawnX, Player.eLetterType i_LetterType)
        {
            bool isPossibleToEat = false;
            if(IsInRange(i_PawnX.Location.CoordinateRow - 1, i_PawnX.Location.CoordinateCol - 1) && IsInRange(
                   i_PawnX.Location.CoordinateRow - 2,
                   i_PawnX.Location.CoordinateCol - 2) && r_Board.MatrixOfBoard[i_PawnX.Location.CoordinateRow - 1,
                   i_PawnX.Location.CoordinateCol - 1] != null)
            {
                if(r_Board.MatrixOfBoard[i_PawnX.Location.CoordinateRow - 1, i_PawnX.Location.CoordinateCol - 1]
                   != null)
                {
                    isPossibleToEat =
                        r_Board.MatrixOfBoard[i_PawnX.Location.CoordinateRow - 1, i_PawnX.Location.CoordinateCol - 1]
                            .LetterOfPlayer == i_LetterType && r_Board.MatrixOfBoard[i_PawnX.Location.CoordinateRow - 2,
                            i_PawnX.Location.CoordinateCol - 2] == null;
                }
            }

            return isPossibleToEat;
        }

        // Checks if coordinate has the possibly to eat lower right
        public bool CanOEatRight(Pawn i_PawnO, Player.eLetterType i_LetterType)
        {
            bool isPossibleToEat = false;
            if(IsInRange(i_PawnO.Location.CoordinateRow + 1, i_PawnO.Location.CoordinateCol + 1) && IsInRange(
                   i_PawnO.Location.CoordinateRow + 2,
                   i_PawnO.Location.CoordinateCol + 2))
            {
                if(r_Board.MatrixOfBoard[i_PawnO.Location.CoordinateRow + 1, i_PawnO.Location.CoordinateCol + 1]
                   != null)
                {
                    isPossibleToEat =
                        r_Board.MatrixOfBoard[i_PawnO.Location.CoordinateRow + 1, i_PawnO.Location.CoordinateCol + 1]
                            .LetterOfPlayer == i_LetterType && r_Board.MatrixOfBoard[i_PawnO.Location.CoordinateRow + 2,
                            i_PawnO.Location.CoordinateCol + 2] == null;
                }
            }

            return isPossibleToEat;
        }

        // Checks if coordinate has the possibly to eat lower left
        public bool CanOEatLeft(Pawn i_PawnO, Player.eLetterType i_LetterType)
        {
            bool isPossibleToEat = false;
            if(IsInRange(i_PawnO.Location.CoordinateRow + 1, i_PawnO.Location.CoordinateCol - 1) && IsInRange(
                   i_PawnO.Location.CoordinateRow + 2,
                   i_PawnO.Location.CoordinateCol - 2))
            {
                if(r_Board.MatrixOfBoard[i_PawnO.Location.CoordinateRow + 1, i_PawnO.Location.CoordinateCol - 1]
                   != null)
                {
                    isPossibleToEat =
                        r_Board.MatrixOfBoard[i_PawnO.Location.CoordinateRow + 1, i_PawnO.Location.CoordinateCol - 1]
                            .LetterOfPlayer == i_LetterType && r_Board.MatrixOfBoard[i_PawnO.Location.CoordinateRow + 2,
                            i_PawnO.Location.CoordinateCol - 2] == null;
                }
            }

            return isPossibleToEat;
        }


        // Checks if coordinate is in range
        public bool IsInRange(int i_Row, int i_Col)
        {
            return i_Col > -1 && i_Row > -1 && i_Row < r_Board.SizeOfBoard && i_Col < r_Board.SizeOfBoard;
        }

        // Checks if pawn can move upper left on the board and the possible coordinate is correct one
        public bool IsUpperLeft(Coordinate i_Current, Coordinate i_Possible)
        {
            bool isMatches = false;
            if(i_Current.CoordinateRow - 1 > -1 && i_Current.CoordinateCol - 1 > -1)
            {
                if(i_Current.CoordinateRow - 1 == i_Possible.CoordinateRow
                   && i_Current.CoordinateCol - 1 == i_Possible.CoordinateCol)
                {
                    isMatches = true;
                }
            }

            return isMatches;
        }

        // Checks if pawn can move upper right on the board and the possible coordinate is correct one
        public bool IsUpperRight(Coordinate i_Current, Coordinate i_Possible)
        {
            bool isMatches = false;
            if(i_Current.CoordinateRow - 1 > -1 && i_Current.CoordinateCol + 1 < r_Board.SizeOfBoard)
            {
                if(i_Current.CoordinateRow - 1 == i_Possible.CoordinateRow
                   && i_Current.CoordinateCol + 1 == i_Possible.CoordinateCol)
                {
                    isMatches = true;
                }
            }

            return isMatches;
        }

        // Checks if pawn can move lower right on the board and the possible coordinate is correct one
        public bool IsLowerRight(Coordinate i_Current, Coordinate i_Possible)
        {
            bool isMatches = false;
            if(i_Current.CoordinateRow + 1 < r_Board.SizeOfBoard && i_Current.CoordinateCol + 1 < r_Board.SizeOfBoard)
            {
                if(i_Current.CoordinateRow + 1 == i_Possible.CoordinateRow
                   && i_Current.CoordinateCol + 1 == i_Possible.CoordinateCol)
                {
                    isMatches = true;
                }
            }

            return isMatches;
        }

        // Checks if pawn can move lower left on the board and the possible coordinate is correct one
        public bool IsLowerLeft(Coordinate i_Current, Coordinate i_Possible)
        {
            bool isMatches = false;
            if(i_Current.CoordinateRow + 1 < r_Board.SizeOfBoard && i_Current.CoordinateCol - 1 > -1)
            {
                if(i_Current.CoordinateRow + 1 == i_Possible.CoordinateRow
                   && i_Current.CoordinateCol - 1 == i_Possible.CoordinateCol)
                {
                    isMatches = true;
                }
            }

            return isMatches;
        }

        // Checks if pawn can eat upper right and the possible coordinate is correct one
        public bool IsUpperRightEat(Coordinate i_Current, Coordinate i_Possible)
        {
            bool isMatches = false;
            if(CanXEatRight(
                r_Board.MatrixOfBoard[i_Current.CoordinateRow, i_Current.CoordinateCol],
                GetCurrentOpponent().PlayerLetterType))
            {
                if(IsInRange(i_Possible.CoordinateRow, i_Possible.CoordinateCol) == true)
                {
                    if(i_Current.CoordinateRow - 2 == i_Possible.CoordinateRow
                       && i_Current.CoordinateCol + 2 == i_Possible.CoordinateCol)
                    {
                        isMatches = true;
                    }
                }
            }

            return isMatches;
        }

        // Checks if pawn can eat upper left and the possible coordinate is correct one
        public bool IsUpperLeftEat(Coordinate i_Current, Coordinate i_Possible)
        {
            bool isMatches = false;
            if(CanXEatLeft(
                r_Board.MatrixOfBoard[i_Current.CoordinateRow, i_Current.CoordinateCol],
                GetCurrentOpponent().PlayerLetterType))
            {
                if(IsInRange(i_Possible.CoordinateRow, i_Possible.CoordinateCol) == true)
                {
                    if(i_Current.CoordinateRow - 2 == i_Possible.CoordinateRow
                       && i_Current.CoordinateCol - 2 == i_Possible.CoordinateCol)
                    {
                        isMatches = true;
                    }
                }
            }

            return isMatches;
        }

        // Checks if pawn can eat lower right and the possible coordinate is correct one
        public bool IsLowerRightEat(Coordinate i_Current, Coordinate i_Possible)
        {
            bool isMatches = false;
            if(CanOEatRight(
                r_Board.MatrixOfBoard[i_Current.CoordinateRow, i_Current.CoordinateCol],
                GetCurrentOpponent().PlayerLetterType))
            {
                if(IsInRange(i_Possible.CoordinateRow, i_Possible.CoordinateCol) == true)
                {
                    if(i_Current.CoordinateRow + 2 == i_Possible.CoordinateRow
                       && i_Current.CoordinateCol + 2 == i_Possible.CoordinateCol)
                    {
                        isMatches = true;
                    }
                }
            }

            return isMatches;
        }

        // Checks if pawn can eat lower left and the possible coordinate is correct one
        public bool IsLowerLeftEat(Coordinate i_Current, Coordinate i_Possible)
        {
            bool isMatches = false;
            if(CanOEatLeft(
                r_Board.MatrixOfBoard[i_Current.CoordinateRow, i_Current.CoordinateCol],
                GetCurrentOpponent().PlayerLetterType))
            {
                if(IsInRange(i_Possible.CoordinateRow, i_Possible.CoordinateCol) == true)
                {
                    if(i_Current.CoordinateRow + 2 == i_Possible.CoordinateRow
                       && i_Current.CoordinateCol - 2 == i_Possible.CoordinateCol)
                    {
                        isMatches = true;
                    }
                }
            }

            return isMatches;
        }

        // Moves pawn without eating the opponent 
        public bool MoveWithoutEat(Coordinate i_Current, Coordinate i_Possible)
        {
            bool isMoved = false;
            Player currentPlayer = CurrPlayer == ePlayerNum.Player1 ? PlayerOne : PlayerTwo;
            if(currentPlayer.PlayerLetterType == Player.eLetterType.X)
            {
                if(IsUpperRight(i_Current, i_Possible))
                {
                    isMoved = true;
                    r_Board.MovePawn(i_Current, i_Possible);

                }
                else if(IsUpperLeft(i_Current, i_Possible))
                {
                    isMoved = true;
                    r_Board.MovePawn(i_Current, i_Possible);
                }
                else if(r_Board.MatrixOfBoard[i_Current.CoordinateRow, i_Current.CoordinateCol].LetterOfPawn
                        == Pawn.ePawnType.K)
                {
                    if(IsLowerRight(i_Current, i_Possible))
                    {
                        isMoved = true;
                        r_Board.MovePawn(i_Current, i_Possible);
                    }
                    else if(IsLowerLeft(i_Current, i_Possible))
                    {
                        isMoved = true;
                        r_Board.MovePawn(i_Current, i_Possible);
                    }
                }
            }

            else
            {
                if(IsLowerRight(i_Current, i_Possible))
                {
                    isMoved = true;
                    r_Board.MovePawn(i_Current, i_Possible);
                }
                else if(IsLowerLeft(i_Current, i_Possible))
                {
                    isMoved = true;
                    r_Board.MovePawn(i_Current, i_Possible);
                }

                else if(r_Board.MatrixOfBoard[i_Current.CoordinateRow, i_Current.CoordinateCol].LetterOfPawn
                        == Pawn.ePawnType.U)
                {
                    if(IsUpperRight(i_Current, i_Possible))
                    {
                        isMoved = true;
                        r_Board.MovePawn(i_Current, i_Possible);
                    }
                    else if(IsUpperLeft(i_Current, i_Possible))
                    {
                        isMoved = true;
                        r_Board.MovePawn(i_Current, i_Possible);
                    }
                }
            }

            return isMoved;
        }


        // Makes a move with a pawn including eating a pawn
        public bool MoveWithEat(Coordinate i_Current, Coordinate i_Possible)
        {
            bool isMoved = false;
            Player currentPlayer = GetCurrentUser();
            if(currentPlayer.PlayerLetterType == Player.eLetterType.X)
            {
                if(IsUpperRightEat(i_Current, i_Possible))
                {
                    isMoved = true;
                    r_Board.DeletePawn(i_Current.CoordinateRow - 1, i_Current.CoordinateCol + 1, currentPlayer);
                    r_Board.MovePawn(i_Current, i_Possible);
                }
                else if(IsUpperLeftEat(i_Current, i_Possible))
                {
                    isMoved = true;
                    r_Board.DeletePawn(i_Current.CoordinateRow - 1, i_Current.CoordinateCol - 1, currentPlayer);
                    r_Board.MovePawn(i_Current, i_Possible);
                }
                else if(r_Board.MatrixOfBoard[i_Current.CoordinateRow, i_Current.CoordinateCol].LetterOfPawn
                        == Pawn.ePawnType.K)
                {
                    if(IsLowerRightEat(i_Current, i_Possible))
                    {
                        isMoved = true;
                        r_Board.DeletePawn(i_Current.CoordinateRow + 1, i_Current.CoordinateCol + 1, currentPlayer);
                        r_Board.MovePawn(i_Current, i_Possible);
                    }
                    else if(IsLowerLeftEat(i_Current, i_Possible))
                    {
                        isMoved = true;
                        r_Board.DeletePawn(i_Current.CoordinateRow + 1, i_Current.CoordinateCol - 1, currentPlayer);
                        r_Board.MovePawn(i_Current, i_Possible);
                    }
                }
            }

            else
            {
                if(IsLowerRightEat(i_Current, i_Possible))
                {
                    isMoved = true;
                    r_Board.DeletePawn(i_Current.CoordinateRow + 1, i_Current.CoordinateCol + 1, currentPlayer);
                    r_Board.MovePawn(i_Current, i_Possible);
                }
                else if(IsLowerLeftEat(i_Current, i_Possible))
                {
                    isMoved = true;
                    r_Board.DeletePawn(i_Current.CoordinateRow + 1, i_Current.CoordinateCol - 1, currentPlayer);
                    r_Board.MovePawn(i_Current, i_Possible);
                }

                else if(r_Board.MatrixOfBoard[i_Current.CoordinateRow, i_Current.CoordinateCol].LetterOfPawn
                        == Pawn.ePawnType.U)
                {
                    if(IsUpperRightEat(i_Current, i_Possible))
                    {
                        isMoved = true;
                        r_Board.DeletePawn(i_Current.CoordinateRow - 1, i_Current.CoordinateCol + 1, currentPlayer);
                        r_Board.MovePawn(i_Current, i_Possible);
                    }
                    else if(IsUpperLeftEat(i_Current, i_Possible))
                    {
                        isMoved = true;
                        r_Board.DeletePawn(i_Current.CoordinateRow - 1, i_Current.CoordinateCol - 1, currentPlayer);
                        r_Board.MovePawn(i_Current, i_Possible);
                    }
                }
            }

            return isMoved;
        }

        // Parses the coordinated from string format to points format
        public bool ParseCoordinate(string i_PlayerMove, out Coordinate o_Current, out Coordinate o_Possible)
        {
            bool isInputLegal = true;

            if(i_PlayerMove.Length != 5)
            {
                isInputLegal = false;
                o_Current = new Coordinate(-1, -1);
                o_Possible = new Coordinate(-1, -1);
            }
            else
            {
                o_Current = new Coordinate(i_PlayerMove[1] - 'a', i_PlayerMove[0] - 'A');
                o_Possible = new Coordinate(i_PlayerMove[4] - 'a', i_PlayerMove[3] - 'A');

                if(IsNotLegalCoordinate(o_Current, o_Possible))
                {
                    isInputLegal = false;
                }
            }

            return isInputLegal;
        }

        private bool isCoordinateInRange(Coordinate i_Current, Coordinate i_Possible)
        {
            return i_Current.CoordinateCol < 0 || i_Possible.CoordinateCol < 0 || i_Current.CoordinateRow < 0
                   || i_Possible.CoordinateRow < 0 || i_Current.CoordinateCol > r_Board.SizeOfBoard - 1
                   || i_Possible.CoordinateCol > r_Board.SizeOfBoard - 1
                   || i_Current.CoordinateRow > r_Board.SizeOfBoard - 1
                   || i_Possible.CoordinateRow > r_Board.SizeOfBoard - 1;
        }

        // Checks if the coordinates is illegal
        public bool IsNotLegalCoordinate(Coordinate i_Current, Coordinate i_Possible)
        {
            bool isNotLegal = false;
            Player currentPlayer = GetCurrentUser();
            if(isCoordinateInRange(i_Current, i_Possible))
            {
                isNotLegal = true;
            }
            else
            {
                if(r_Board.MatrixOfBoard[i_Possible.CoordinateRow, i_Possible.CoordinateCol] != null
                   || r_Board.MatrixOfBoard[i_Current.CoordinateRow, i_Current.CoordinateCol] == null)
                {
                    isNotLegal = true;
                }

                else if(r_Board.MatrixOfBoard[i_Current.CoordinateRow, i_Current.CoordinateCol].LetterOfPlayer
                        != currentPlayer.PlayerLetterType)
                {
                    isNotLegal = true;
                }
            }

            return isNotLegal;
        }

        public Player GetCurrentUser()
        {
            return (CurrPlayer == ePlayerNum.Player1) ? PlayerOne : PlayerTwo;
        }

        public Player GetCurrentOpponent()
        {
            return CurrPlayer == ePlayerNum.Player1 ? PlayerTwo : PlayerOne;
        }

        // Makes all the computer moves and add the to a list if there are eating moves the computer has to eat first
        public string MakeComputerMove()
        {
            List<string> computerMoves = new List<string>();
            List<string> computerEatingMoves = new List<string>();

            foreach(Pawn pawn in r_Board.PlayerTwoPawns)
            {
                if((pawn.LetterOfPawn == Pawn.ePawnType.O) && (CanOEat(pawn)))
                {
                    AddAllEatingMoves(computerEatingMoves, pawn);
                }
                else if((pawn.LetterOfPawn == Pawn.ePawnType.U) && CanKingOEat((pawn)))
                {
                    AddAllEatingMoves(computerEatingMoves, pawn);
                }
            }

            if(computerEatingMoves.Count == 0)
            {
                foreach(Pawn pawn in r_Board.PlayerTwoPawns)
                {
                    if((pawn.LetterOfPawn == Pawn.ePawnType.O) && (CanMoveLowerLeft(pawn) || CanMoveLowerRight(pawn)))
                    {
                        AddAllMoves(computerMoves, pawn);
                    }
                    else if((pawn.LetterOfPawn == Pawn.ePawnType.U)
                            && (CanMoveLowerLeft(pawn) || CanMoveLowerRight(pawn) || CanMoveUpperRight(pawn)
                                || CanMoveUpperLeft(pawn)))
                    {
                        AddAllMoves(computerMoves, pawn);
                    }
                }
            }

            string moveOfComputer = null;
            int placeInList;
            Random rnd = new Random();

            if(computerEatingMoves.Count != 0)
            {
                placeInList = rnd.Next(0, computerEatingMoves.Count);
                moveOfComputer = computerEatingMoves[placeInList];
            }
            else if(computerMoves.Count != 0)
            {
                placeInList = rnd.Next(0, computerMoves.Count);
                moveOfComputer = computerMoves[placeInList];
            }

            return moveOfComputer;
        }

        // Parses coordinates to a Aa>Bb format
        public static string MakeStringCoordinates(int i_CurrentCol, int i_CurrentRow, int i_FutureCol, int i_FutureRow)
        {
            return Convert.ToChar(i_CurrentCol) + "" + Convert.ToChar(i_CurrentRow) + ">" + Convert.ToChar(i_FutureCol)
                   + Convert.ToChar(i_FutureRow);
        }

        // Adds all regular moves the computer can do to a list
        public void AddAllMoves(List<string> i_ComputerMoves, Pawn i_Pawn)
        {
            int currentCol;
            int currentRow;
            int futurePossibleCol;
            int futurePossibleRow;

            if(CanMoveLowerLeft(i_Pawn) == true)
            {
                currentCol = i_Pawn.Location.CoordinateCol + 'A';
                currentRow = i_Pawn.Location.CoordinateRow + 'a';
                futurePossibleCol = i_Pawn.Location.CoordinateCol - 1 + 'A';
                futurePossibleRow = i_Pawn.Location.CoordinateRow + 1 + 'a';

                string tempForList = MakeStringCoordinates(
                    currentCol,
                    currentRow,
                    futurePossibleCol,
                    futurePossibleRow);
                i_ComputerMoves.Add(tempForList);
            }

            if(CanMoveLowerRight(i_Pawn) == true)
            {
                currentCol = i_Pawn.Location.CoordinateCol + 'A';
                currentRow = i_Pawn.Location.CoordinateRow + 'a';
                futurePossibleCol = i_Pawn.Location.CoordinateCol + 1 + 'A';
                futurePossibleRow = i_Pawn.Location.CoordinateRow + 1 + 'a';

                string tempForList = MakeStringCoordinates(
                    currentCol,
                    currentRow,
                    futurePossibleCol,
                    futurePossibleRow);
                i_ComputerMoves.Add(tempForList);
            }

            if(i_Pawn.LetterOfPawn == Pawn.ePawnType.U)
            {
                if(CanMoveUpperLeft(i_Pawn) == true)
                {
                    currentCol = i_Pawn.Location.CoordinateCol + 'A';
                    currentRow = i_Pawn.Location.CoordinateRow + 'a';
                    futurePossibleCol = i_Pawn.Location.CoordinateCol - 1 + 'A';
                    futurePossibleRow = i_Pawn.Location.CoordinateRow - 1 + 'a';

                    string tempForList = MakeStringCoordinates(
                        currentCol,
                        currentRow,
                        futurePossibleCol,
                        futurePossibleRow);
                    i_ComputerMoves.Add(tempForList);
                }

                if(CanMoveUpperRight(i_Pawn) == true)
                {
                    currentCol = i_Pawn.Location.CoordinateCol + 'A';
                    currentRow = i_Pawn.Location.CoordinateRow + 'a';
                    futurePossibleCol = i_Pawn.Location.CoordinateCol + 1 + 'A';
                    futurePossibleRow = i_Pawn.Location.CoordinateRow - 1 + 'a';

                    string tempForList = MakeStringCoordinates(
                        currentCol,
                        currentRow,
                        futurePossibleCol,
                        futurePossibleRow);
                    i_ComputerMoves.Add(tempForList);
                }
            }
        }

        // Adds all the eating moves the computer can doo to a list
        public void AddAllEatingMoves(List<string> i_ComputerEatingMoves, Pawn i_Pawn)
        {
            int currentCol;
            int currentRow;
            int futurePossibleCol;
            int futurePossibleRow;

            if(CanOEatLeft(i_Pawn, Player.eLetterType.X) == true)
            {
                currentCol = i_Pawn.Location.CoordinateCol + 'A';
                currentRow = i_Pawn.Location.CoordinateRow + 'a';
                futurePossibleCol = i_Pawn.Location.CoordinateCol - 2 + 'A';
                futurePossibleRow = i_Pawn.Location.CoordinateRow + 2 + 'a';

                string tempForList = MakeStringCoordinates(
                    currentCol,
                    currentRow,
                    futurePossibleCol,
                    futurePossibleRow);
                i_ComputerEatingMoves.Add(tempForList);
            }

            if(CanOEatRight(i_Pawn, Player.eLetterType.X) == true)
            {
                currentCol = i_Pawn.Location.CoordinateCol + 'A';
                currentRow = i_Pawn.Location.CoordinateRow + 'a';
                futurePossibleCol = i_Pawn.Location.CoordinateCol + 2 + 'A';
                futurePossibleRow = i_Pawn.Location.CoordinateRow + 2 + 'a';

                string tempForList = MakeStringCoordinates(
                    currentCol,
                    currentRow,
                    futurePossibleCol,
                    futurePossibleRow);
                i_ComputerEatingMoves.Add(tempForList);
            }

            if(i_Pawn.LetterOfPawn == Pawn.ePawnType.U)
            {
                if(CanXEatLeft(i_Pawn, Player.eLetterType.X) == true)
                {
                    currentCol = i_Pawn.Location.CoordinateCol + 'A';
                    currentRow = i_Pawn.Location.CoordinateRow + 'a';
                    futurePossibleCol = i_Pawn.Location.CoordinateCol - 2 + 'A';
                    futurePossibleRow = i_Pawn.Location.CoordinateRow - 2 + 'a';

                    string tempForList = MakeStringCoordinates(
                        currentCol,
                        currentRow,
                        futurePossibleCol,
                        futurePossibleRow);
                    i_ComputerEatingMoves.Add(tempForList);
                }

                if(CanXEatRight(i_Pawn, Player.eLetterType.X) == true)
                {
                    currentCol = i_Pawn.Location.CoordinateCol + 'A';
                    currentRow = i_Pawn.Location.CoordinateRow + 'a';
                    futurePossibleCol = i_Pawn.Location.CoordinateCol + 2 + 'A';
                    futurePossibleRow = i_Pawn.Location.CoordinateRow - 2 + 'a';

                    string tempForList = MakeStringCoordinates(
                        currentCol,
                        currentRow,
                        futurePossibleCol,
                        futurePossibleRow);
                    i_ComputerEatingMoves.Add(tempForList);
                }
            }
        }
    }
}