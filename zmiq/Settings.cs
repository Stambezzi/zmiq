using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zmiq
{
    public enum Direction
    {
        up, down, left, right
    };
    class Settings
    {
        public static int widht { set; get; }
        public static int height { set; get; }
        public static int speed { set; get; }
        public static int score { set; get; }
        public static int points_per_circle { set; get; }
        public static bool gameOver { set; get; }
        public static Direction direction { set; get; }
        public Settings()
        {
            widht = 16;
            height = 16;
            speed = 16;
            score = 0;
            points_per_circle = 100;
            gameOver = false;
            direction = Direction.down;
        }
    }

}
