using System.Collections.Generic;

namespace ConsoleApp2
{
    public class ArrowScript : Script
    {
        private List<GameObject> _friendly;
        private Vector2D _direction;
        private double _lifeTime = 10000;
        private double _speed = 1;

        public ArrowScript(Vector2D direction, double speed, List<GameObject> friendly)
        {
            _direction = direction.Normalize();
            Speed = speed;
            Friendly = friendly;
        }

        public double LifeTime { get => _lifeTime; }
        public double Speed { get => _speed; set => _speed = value; }
        public List<GameObject> Friendly { get => _friendly; set => _friendly = value; }

        public override void Update(double elapsedTime)
        {
            _parent.transform.Position += _direction * Speed;
            _lifeTime -= elapsedTime;
        }

        public override void OnCollide(ICollider collider)
        {
            if (collider is SphereCollider)
            {
                SphereCollider sCollider = (SphereCollider)collider;
                if (Friendly.Contains(sCollider.Parent))
                {
                    if (gameObject.GetComponent<RectangleCollider>() != null)
                    {
                        gameObject.GetComponent<RectangleCollider>().SetActiveTimer(200);
                    }
                    return;
                }
            }
            Speed = 0;
        }
    }
}