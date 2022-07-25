using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace Snake_Game_OOP
{

    public class highscoreEntryDTO
    {
        public string Username { get; set; }

        public int Score { get; set; }

    }
}
