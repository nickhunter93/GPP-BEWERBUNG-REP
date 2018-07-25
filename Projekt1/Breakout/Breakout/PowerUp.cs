using SFML.Graphics;
using SFML.System;

namespace ConsoleApp2
{
    public class PowerUp : Sprite
    {
        private StateManager _stateManager;
        private Vector2D _direction;
        private double _speed;
        private RectangleObject _rectangleObject;
        private Vector2D _size;
        private State _state; 


        public StateManager StateManager
        {
            set { _stateManager = value; }
        }
        public Vector2D Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }
        public double Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        public RectangleObject RectangleObject
        {
            get { return _rectangleObject; }
            set { _rectangleObject = value; }
        }
        public Vector2D Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public PowerUp()
        {
            this.Texture = new Texture("Prefab.png");
            this.Size = (Vector2f)this.Texture.Size;
            this.Origin = new Vector2D(this.Size.X / 2, this.Size.Y / 2);
        }

        public PowerUp(State state)
        {
            _state = state;
            this.Texture = state.Texture;
            this.Size = (Vector2f)this.Texture.Size;
            this.Origin = new Vector2D(this.Size.X / 2, this.Size.Y / 2);
        }

        public void Spawn(Vector2D position, Vector2D direction, double speed)
        {
            this.Position = position;
            this.Direction = direction;
            this.Speed = speed;

        }
        
        public void ExecutePowerUp(RectangleObject rectangleObject)
        {
            this.RectangleObject = rectangleObject;
            Execute(rectangleObject);
        }
        protected void Execute(RectangleObject rectangleObject)
        {
            _stateManager.AddState(rectangleObject, _state, _state.Duration);
        }
        public PowerUp Clone()
        {
            PowerUp clone = new PowerUp(_state);
            clone.StateManager = this._stateManager;
            return clone;
        }

        public void Update(double elapsedTime)
        {
            this.Position += _direction * _speed * elapsedTime;
        }
    }
}