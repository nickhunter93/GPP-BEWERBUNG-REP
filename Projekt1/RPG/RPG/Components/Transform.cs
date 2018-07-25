using SFML.Window;

namespace ConsoleApp2
{
    public class Transform : Component
    {
        private Vector2D _position;
        private Vector2D _worldPosition;
        private double _rotation;
        private double _worldRotation;

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
        public Vector2D WorldPosition { get => _worldPosition; set => _worldPosition = value; }
        public double WorldRotation { get => _worldRotation; set => _worldRotation = value; }

        public override void Update(double elapsedTime)
        {
            GameObject parent = this.Parent;
            Vector2D position = Vector2D.Zero();// parent.transform.Position;
            double rotation = 0;// parent.transform.Rotation;
            while (parent != null)
            {
                //position =  position.Rotate((Math.PI / 180) * parent.transform.Rotation) + parent.transform.Position;
                position += parent.transform.Position;
                position = position.RotateAround((System.Math.PI * parent.transform.Rotation / 180), parent.transform.Position);

                rotation += parent.transform.Rotation;
                parent = parent.Parent;
            }
            WorldPosition = position;
            WorldRotation = rotation;
        }
    }
}