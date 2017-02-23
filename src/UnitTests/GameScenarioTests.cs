using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using System.Diagnostics;
using static Logic.Exceptions;

namespace UnitTests
{
    [TestClass]
    public class GameScenarioTests
    {
        [TestMethod]
        public void Test_Yellow_Wins_Horizontal()
        {
            Game theGame = new Game(5, 5);
            // Let's play!
            for (int i = 0; i < 3; i++)
            {
                theGame.AddDisc(new Disc(Enums.Sides.Yellow), i);
                theGame.AddDisc(new Disc(Enums.Sides.Red), i);
            }

            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 3);

            Assert.IsTrue(theGame.CurrentState == Enums.GameStates.YellowWins);
            Debug.WriteLine(theGame.Board.ToString());
        }

        [TestMethod]
        public void Test_Red_Wins_Vertical()
        {
            Game theGame = new Game(5, 5);
            // Let's play!
            for (int i = 0; i < 6; i++)
            {
                if (i % 2 == 0)
                    theGame.AddDisc(new Disc(Enums.Sides.Yellow), 0);
                else
                    theGame.AddDisc(new Disc(Enums.Sides.Red), 1);
            }
            Debug.WriteLine(theGame.Board.ToString());
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 2);
            Debug.WriteLine(theGame.Board.ToString());
            theGame.AddDisc(new Disc(Enums.Sides.Red), 1);
            Debug.WriteLine(theGame.Board.ToString());
            Assert.IsTrue(theGame.CurrentState == Enums.GameStates.RedWins);

        }

        [TestMethod]
        public void Test_Yellow_Wins_Diagonal()
        {
            Game theGame = new Game(5, 5);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 0);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 1);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 1);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 2);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 0);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 2);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 2);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 3);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 0);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 3);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 1);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 3);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 3);

            Debug.WriteLine(theGame.Board.ToString());
            Assert.IsTrue(theGame.CurrentState == Enums.GameStates.YellowWins);
        }

        [TestMethod]
        public void Test_For_Draw()
        {
            Game theGame = new Game(5, 5);
            // Let's play!
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 0);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 0);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 1);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 2);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 1);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 1);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 2);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 3);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 4);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 4);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 3);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 1);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 1);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 2);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 3);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 4);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 3);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 3);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 2);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 4);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 4);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 0);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 0);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 2);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 0);
            Debug.WriteLine(theGame.Board.ToString());
            Assert.IsTrue(theGame.CurrentState == Enums.GameStates.Draw);
        }

        [TestMethod]
        public void Test_For_Invalid_Board_Dimensions()
        {
            try
            {
                Game theGame = new Game(-1, 0);
                Assert.Fail("Invalid board dimensions were not tested for.");
            }
            catch (InvalidBoardDimensionsException) { }

            try
            {
                Game theGame = new Game(2, 2);
                Assert.Fail("Board too small not tested for.");
            }
            catch (InvalidBoardDimensionsException) { }
        }

        [TestMethod]
        public void Test_Invalid_Move()
        {
            Game theGame = new Game(5, 5);


            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 0);

            try
            {
                theGame.AddDisc(new Disc(Enums.Sides.Yellow), 0);
                Assert.Fail("Invalid move - same player moved twice not tested for.");
            }
            catch (WrongPlayerMoveException)
            {

            }

            try
            {
                theGame.AddDisc(new Disc(Enums.Sides.Red), 5);
                Assert.Fail("Disc played out of bounds not tested for.");

            }
            catch (OutOfGameBoardBoundsException)
            { }
        }

        [TestMethod]
        public void Test_Add_To_Full_Board()
        {
            Game theGame = new Game(5, 5);
            // First, fill the board with a draw condition
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 0);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 0);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 1);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 2);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 1);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 1);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 2);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 3);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 4);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 4);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 3);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 1);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 1);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 2);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 3);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 4);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 3);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 3);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 2);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 4);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 4);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 0);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 0);
            theGame.AddDisc(new Disc(Enums.Sides.Red), 2);
            theGame.AddDisc(new Disc(Enums.Sides.Yellow), 0);
            Debug.WriteLine(theGame.Board.ToString());
            try
            {
                theGame.AddDisc(new Disc(Enums.Sides.Red), 2);
                Assert.Fail("Able to add more discs to a board that is already full.");
            }
            catch { }
        }
    }
}
