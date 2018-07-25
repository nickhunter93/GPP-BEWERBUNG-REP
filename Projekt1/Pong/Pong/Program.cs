
using SFML.Graphics;
using SFML.System;

namespace ConsoleApp2
{
    public class Program
    {
        private Game _game;
        private Menu _menu;

        public static Vector2f windowSize = new Vector2f(1280, 800);
        private Font _font;

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Run();
        }


        public void Run()
        {

            _font = new Font("sayso chic.ttf");

            
            _menu = new Menu(windowSize, _font);
            _menu.Start();
            

            _game = new Game(windowSize, _font);
            _game.Start(_menu.GetDifficultyLeft(), _menu.GetDifficultyRight());
        }


    }
}
