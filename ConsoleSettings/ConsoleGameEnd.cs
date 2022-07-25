using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Snake_Game_OOP.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snake_Game_OOP.ConsoleSettings
{
    public class ConsoleGameEnd : IGameEnd
    {
        private ISoundPlayer soundPlayer;
        private Highscore highscore;

        public ConsoleGameEnd(ISoundPlayer soundPlayer, Highscore highscore)
        {
            this.soundPlayer = soundPlayer;
            this.highscore = highscore;
        }
        public bool End()
        {

            soundPlayer.Play(true);
            Thread.Sleep(GlobalConstants.gameEndDelay);
            GlobalConstants.highScore--;

            GenerateEnd();

            UpdateHighScore();

            ConsoleKeyInfo input = Console.ReadKey(true);
            while (input.Key != ConsoleKey.Y)
            {
                if (input.Key == ConsoleKey.N)
                {
                    Console.ResetColor();
                    Environment.Exit(0);

                }

                if (input.Key == ConsoleKey.H)
                {
                    highscore.Render();
                    GenerateEnd();
                }
                input = Console.ReadKey(true);
            }
            Console.ResetColor();
            return false;

        }

        private void UpdateHighScore()
        {
            if (!File.Exists(GlobalConstants.highscoreFilePath))
                File.Create(GlobalConstants.highscoreFilePath);

            var jsonObj = File.ReadAllText(GlobalConstants.highscoreFilePath);
            var highscoreObj = JsonConvert.DeserializeObject<ICollection<highscoreEntryDTO>>(jsonObj);

            if (highscoreObj == null)
            {
                highscoreObj = new List<highscoreEntryDTO>();
            }

            highscoreObj.Add(new highscoreEntryDTO
            {
                Username = GlobalConstants.username,
                Score = GlobalConstants.highScore
            });

            highscoreObj = highscoreObj.OrderByDescending(x => x.Score).ThenBy(x => x.Username).Take(15).ToList();
         
            var jsonOutput = JsonConvert.SerializeObject(highscoreObj, GlobalConstants.jsonSettings);
            File.WriteAllText(GlobalConstants.highscoreFilePath, jsonOutput);
        }

        private void GenerateEnd()
        {


            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(GlobalConstants.consoleWidth / 2 - 5, GlobalConstants.consoleHeight / 2 - 2);
            Console.Write("GAME OVER");
            Console.SetCursorPosition(GlobalConstants.consoleWidth / 2 - 7, GlobalConstants.consoleHeight / 2);
            Console.Write($"High Score: ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write($"{GlobalConstants.highScore}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(GlobalConstants.consoleWidth / 2 - 10, GlobalConstants.consoleHeight / 2 + 2);
            Console.Write("h = view High scores");
            Console.SetCursorPosition(GlobalConstants.consoleWidth / 2 - 6, GlobalConstants.consoleHeight / 2 + 4);
            Console.Write("Play Again?");
            Console.SetCursorPosition(GlobalConstants.consoleWidth / 2 - 10, GlobalConstants.consoleHeight / 2 + 5);
            Console.Write("y = yes     n = no");
        }
    }
}
