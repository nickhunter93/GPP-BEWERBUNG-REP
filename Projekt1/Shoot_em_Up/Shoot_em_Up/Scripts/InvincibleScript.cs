using SFML.Graphics;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class InvincibleScript : Script
    {
        private double _invincibleTimer;
        private double _invincibleInterval = 400;
        private double _invincibleCooldown = 200;
        private Vector2D _direction;
        private int _playerCount;

        public bool IsInvincible
        {
            get { return _invincibleTimer > 0; }
            set
            {
                if (_invincibleTimer <= -_invincibleCooldown)
                {
                    _invincibleTimer = _invincibleInterval;
                    _direction = Vector2D.Up().Rotate(_parent.transform.Rotation * (System.Math.PI / 180)).Normalize() * 0.2;
                    
                }
            }
        }

        public InvincibleScript(int playerCount)
        {
            _playerCount = playerCount;
        }


        public override void Update(double elapsedTime)
        {
            _invincibleTimer -= elapsedTime;

            if (IsInvincible)
            {
                _parent.GetComponent<RenderComponent>().Shape.Scale = Vector2D.One() * 0.8f;
                //_parent.Parent.transform.Position -= _direction * elapsedTime;
                _parent.transform.Position += _direction * elapsedTime;
                _parent.GetScripts<LookScript>()[0].IsActive = false;

                List<MovementScript> movementScripts = _parent.Parent.GetScripts<MovementScript>();
                foreach (MovementScript movementScript in movementScripts)
                {
                    if (movementScript.Id == _parent.GetScripts<MovementScript>()[0].Id)
                    {
                        movementScript.IsActive = false;
                    }
                }
                _parent.GetScripts<MovementScript>()[0].IsActive = false;
            }
            else
            {
                _parent.GetComponent<RenderComponent>().Shape.Scale = Vector2D.One() * 1;
                _parent.GetScripts<LookScript>()[0].IsActive = true;

                List<MovementScript> movementScripts = _parent.Parent.GetScripts<MovementScript>();
                foreach (MovementScript movementScript in movementScripts)
                {
                    if (movementScript.Id == _parent.GetScripts<MovementScript>()[0].Id)
                    {
                        movementScript.IsActive = true;
                    }
                }
                _parent.GetScripts<MovementScript>()[0].IsActive = true;
            }
        }
    }
}