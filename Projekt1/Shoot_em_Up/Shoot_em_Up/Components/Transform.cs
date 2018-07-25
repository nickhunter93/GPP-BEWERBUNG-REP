using SFML.Window;

namespace ConsoleApp2
{
    public class Transform : Component
    {
        private Vector2D _position;
        private double _rotation;
        public Transform(GameObject gameObject)
        {
            Parent = gameObject;
            Position = Vector2D.Zero();
        }
        public Transform(Vector2D position,GameObject gameObject)
        {
            Parent = gameObject;
            Position = position;
        }

        public GameObject gameObject { get => Parent; }
        public Vector2D Position { get => _position; set => _position = value; }
        public double Rotation { get => _rotation; set => _rotation = value; }

        public override void Update(double elapsedTime)
        {
            if(Parent.Parent != null)
            {

            }
        }
    }
}