namespace ConsoleApp2
{
    public class EnemyMovementScript : MovementScript
    {
        private Vector2D _targetPosition = Vector2D.One();

        public Vector2D TargetPosition { get => _targetPosition; set { _targetPosition = value; IsActive = true; } }

        public EnemyMovementScript(double speed)
        {
            Speed = speed;
            IsActive = false;
        }
                
        public override void Update(double elapsedTime)
        {
            Vector2D direction = TargetPosition - (_parent.transform.Position + _parent.Parent.transform.Position);
            direction = direction.Normalize() * Speed * elapsedTime;
            _parent.transform.Position += direction;
        }

        public override MovementScript Clone()
        {
            return new EnemyMovementScript(Speed);
        }
    }
}