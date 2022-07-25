using Snake_Game_OOP.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace Snake_Game_OOP.ConsoleSettings
{
    class ConsoleRenderer : IRenderer
    {
        public void Clear()
        {
            Console.Clear();
        }

        public void Render(Body body)
        {


            foreach (var item in body.BodyOutput)
            {
            Console.SetCursorPosition(item.Value.X, item.Value.Y);
            Console.ForegroundColor = item.Value.Color;
            Console.Write(item.Value.Symbol);

            }

            
            Console.SetCursorPosition(body.BodyOutput.Last.Value.Value.X, body.BodyOutput.Last.Value.Value.Y);
            Console.Write(' ');
        }

        public void Render(IFood food)
        {
            
            Console.SetCursorPosition(food.X, food.Y);
            Console.ForegroundColor = food.Color;
            Console.Write(food.Symbol);
                  




        }

        public void Render(Highscore highscore)
        {
            this.Clear();
            var highscorePos = GlobalConstants.consoleWidth / 3 - 5;
            Console.SetCursorPosition(highscorePos, 0);
            Console.WriteLine("|         HighScores        |");
            var jsonText = File.ReadAllText(GlobalConstants.highscoreFilePath);
            var jsonObj = JsonConvert.DeserializeObject<ICollection<highscoreEntryDTO>>(jsonText);
            var i = 1;
            foreach (var item in jsonObj)
            {

                Console.SetCursorPosition(highscorePos, i);
                Console.Write($"|{i}.{new string(' ',2-i.ToString().Length)}|{item.Username}{new string(' ',12-item.Username.Length)}|Score: {item.Score}{new string(' ',3-item.Score.ToString().Length)}|");
                i++;
            }
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, i + 1);
            Console.WriteLine("Press 'e' to go back to EndGameScreen");
            Console.SetCursorPosition(0 , i + 2);
            Console.WriteLine("Press ESC to EXIT");
            ConsoleKeyInfo input = Console.ReadKey(true);
            while (input.Key != ConsoleKey.E)
            {
                if (input.Key == ConsoleKey.Escape)
                {
                    Console.ResetColor();
                    Environment.Exit(0);

                }

                
                input = Console.ReadKey(true);
            }


        }

        
    }
}
