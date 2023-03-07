using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendJokesV2.Models
{
    public class JokesModel
    {
        public bool error { get; set; }
        public string category { get; set; }
        public string type { get; set; }
        public string joke { get; set; }

        public string setup { get; set; }
        public string delivery { get; set; }
        public Flags flags { get; set; }
        public int id { get; set; }
        public bool safe { get; set; }
        public string lang { get; set; }
    }
    public static class JokesOutput
    {
        public static string title { get; set; }
        public static string message { get; set; }

        public static void JokeFormatter(JokesModel JokeResponse)
        {
            if (JokeResponse != null)
            {
                if (JokeResponse.joke != null)
                {
                    title = JokeResponse.category;
                    message = JokeResponse.joke;
                }
                else
                {
                    title = JokeResponse.type;
                    message = $"{JokeResponse.setup} \n{JokeResponse.delivery}";
                }
            }
            else
            {
                title = "Jokes on you!!";
                message = "Oops no jokes this week!";
            }
        }

    }
    public class Flags
    {
        public bool nsfw { get; set; }
        public bool religious { get; set; }
        public bool political { get; set; }
        public bool racist { get; set; }
        public bool sexist { get; set; }
        public bool _explicit { get; set; }
    }
}
