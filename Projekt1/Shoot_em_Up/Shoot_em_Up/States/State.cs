using SFML.Graphics;
using System;

namespace ConsoleApp2
{
    public abstract class State
    {
        public int ID;
        private Texture _texture;
        protected double _duration;
        public double Duration
        {
            get { return _duration; }
        }
        public Texture Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        public abstract void Update(GameObject player, double elapsedTime);
        public abstract void Finish(GameObject player);

        public State()
        {
            ID = 0;
        }
    }


}