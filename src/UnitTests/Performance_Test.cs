using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class Performance_Test
    {
        [TestMethod]
        public void Test_Big_Game()
        {
            Game theGame = new Game(100, 100);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Random r = new Random(DateTime.Now.Millisecond);
            while (theGame.CurrentState == Enums.GameStates.YellowsTurn || theGame.CurrentState == Enums.GameStates.RedsTurn)
            {
                try
                {
                    if (theGame.CurrentState == Enums.GameStates.YellowsTurn)
                        theGame.AddDisc(new Disc(Enums.Sides.Yellow), r.Next(0, 100));
                    else

                        theGame.AddDisc(new Disc(Enums.Sides.Red), r.Next(0, 100));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            sw.Stop();
            Debug.WriteLine("Game state: " + theGame.CurrentState.ToString());
            Debug.WriteLine(theGame.Board.ToString());
        }

        [TestMethod]
        public void Test_Many_Games()
        {
            int taskCount = 0;

            for (int i = 0; i < Environment.ProcessorCount * 2; i++)
            {
                Task.Run(() =>
                {
                    Interlocked.Increment(ref taskCount);
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    for (int x = 0; x < 1000; x++)
                    {
                        Game theGame = new Game(50, 50);
                        Random r = new Random(DateTime.Now.Millisecond);
                        while (theGame.CurrentState == Enums.GameStates.YellowsTurn || theGame.CurrentState == Enums.GameStates.RedsTurn)
                        {
                            try
                            {
                                if (theGame.CurrentState == Enums.GameStates.YellowsTurn)
                                    theGame.AddDisc(new Disc(Enums.Sides.Yellow), r.Next(0, 50));
                                else

                                    theGame.AddDisc(new Disc(Enums.Sides.Red), r.Next(0, 50));
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex.Message);
                            }
                        }
                    }
                    sw.Stop();
                    Debug.WriteLine("1000 games completed in " + sw.Elapsed.ToString());
                }).ContinueWith((previousTask) =>
                {
                    Interlocked.Decrement(ref taskCount);
                });
            }
            Thread.Sleep(5000);
            while (taskCount > 0)
                Thread.Sleep(0);
        }
    }
}
