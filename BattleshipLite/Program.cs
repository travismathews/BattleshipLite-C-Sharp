﻿using BattleshipLiteLibrary;
using BattleshipLiteLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLite
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WelcomeMessage();
            PlayerInfoModel activePlayer = CreatePlayer("Player 1");
            PlayerInfoModel opponent = CreatePlayer("Player 2");
            PlayerInfoModel winner = null;

            do
            {
                
                DisplayShotGrid(activePlayer);

                RecordPlayerShot(activePlayer, opponent);


                bool doesGameContinue = GameLogic.PlayerStillActive(opponent);

                if (doesGameContinue == true)
                {
                    // Swap using a temp variable
                    // PlayerInfoModel tempHolder = opponent;
                    // opponent = activePlayer;
                    // activePlayer = tempHolder;

                    // As of c# 7.0 you can use a TUPLE
                    // Swapping positions
                    (activePlayer, opponent) = (opponent, activePlayer);


                } else
                {
                    winner = activePlayer;
                }



            } while (winner == null);

            IdentifyWinner(winner);

            Console.ReadLine();
        }

        private static void IdentifyWinner(PlayerInfoModel winner)
        {
            Console.WriteLine($"Congratulations to { winner.UsersName} for winning!");
            Console.WriteLine($"{winner.UsersName} took { GameLogic.GetShotCount(winner) } shots.");
        }

        private static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to BattleshipLite");
            Console.WriteLine("Created by: Travis Mathews");
            Console.WriteLine();
        }

        private static PlayerInfoModel CreatePlayer(string playerTitle)
        {
            PlayerInfoModel output = new PlayerInfoModel();

            Console.WriteLine($"Player information for {playerTitle}");


            // Ask the user for their name
            output.UsersName = AskForUsersName();

            // Load up the shot grid
            GameLogic.InitializeGrid(output);

            // Ask the user for their ship placements
            PlaceShips(output);

            // Clear
            Console.Clear();


            return output;
        }

        private static string AskForUsersName()
        {
            Console.Write("What is your name: ");
            string output = Console.ReadLine();

            return output;
        }

        private static void PlaceShips(PlayerInfoModel model)
        {
            do
            {
                Console.Write($"Where do you want to place ship number {model.ShipLocations.Count + 1}: ");
                string location = Console.ReadLine();

                bool isValidLocation = false;

                try
                {
                    isValidLocation = GameLogic.PlaceShip(model, location);
                }
                catch (Exception ex)
                {

                    Console.WriteLine("Error: " + ex.Message);
                }

                if(isValidLocation == false)
                {
                    Console.WriteLine("That was not a valid location. Please try again.");
                }

            } while (model.ShipLocations.Count < 5);
        }

        private static void DisplayShotGrid(PlayerInfoModel activePlayer)
        {

            string currentRow = activePlayer.ShotGrid[0].SpotLetter;

            foreach (var gridSpot in activePlayer.ShotGrid)
            {
                if (gridSpot.SpotLetter != currentRow)
                {
                    Console.WriteLine();
                    currentRow = gridSpot.SpotLetter;
                }

                if(gridSpot.Status == GridSpotStatus.Empty)
                {
                    Console.Write($" {gridSpot.SpotLetter}{gridSpot.SpotNumber} ");
                }

                else if (gridSpot.Status == GridSpotStatus.Hit)
                {
                    Console.Write("  X ");
                }

                else if (gridSpot.Status == GridSpotStatus.Miss)
                {
                    Console.Write("  O ");
                } else
                {
                    Console.Write("  ? ");
                }
            }

            Console.WriteLine();
            Console.WriteLine();
        }

        private static void RecordPlayerShot(PlayerInfoModel activePlayer, PlayerInfoModel opponent)
        {
            bool isValidShot = false;
            string row = "";
            int column = 0;

            do
            {
                string shot = AskForShot(activePlayer);
                try
                {
                    (row, column) = GameLogic.SplitShotIntoRowAndColumn(shot);
                    isValidShot = GameLogic.ValidateShot(activePlayer, row, column);
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("Error: " + ex.Message);
                    isValidShot = false;
                }

                if (isValidShot == false)
                {
                    Console.WriteLine("Invalid shot location, please try again. ");
                }

            } while (isValidShot == false);

            bool isAHit = GameLogic.IdentifyShotResult(opponent, row, column);

            GameLogic.MarkShotResult(activePlayer, row, column, isAHit);

            DisplayShotResults(row, column, isAHit);

        }

        private static void DisplayShotResults(string row, int column, bool isAHit)
        {
            if (isAHit)
            {
                Console.WriteLine($"{ row }{ column } is a Hit!");
            } else
            {
                Console.WriteLine($"{ row }{ column } is a miss.");
            }
            Console.WriteLine();
            
        }

        private static string AskForShot(PlayerInfoModel activePlayer)
        {
            Console.Write($"Please enter your shot selection {activePlayer.UsersName}: ");
            string output = Console.ReadLine();

            return output;
        }
    }
}
