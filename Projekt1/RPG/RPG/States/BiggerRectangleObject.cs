using SFML.Graphics;
using SFML.System;
using System;
using System.Threading;

namespace ConsoleApp2
{
    public class BiggerRectangleObject : State
    {
        private Vector2D _originalScale;
        private Vector2D _originalSize;
        private Vector2D _bigSpriteFactor;
        private Sprite _sprite;
        private bool _isBig = false;
        private RectangleCollider _rectangleCollider;

        public BiggerRectangleObject()
        {
            this.Texture = new Texture("Pictures/BiggerRectangleObject.png");
            this._duration = 10000;
            _bigSpriteFactor = new Vector2D(1.5, 2);
        }
        
        public override void Update(GameObject player, double elapsedTime)
        {
            if (!_isBig)
            {
                if (player.GetChilds()[0].GetChilds()[0].GetComponent<RenderComponent>() != null)
                {
                    _sprite = player.GetChilds()[0].GetChilds()[0].GetComponent<RenderComponent>().Sprite;
                }
                else
                    return;

                GameObject weapon = player.GetScript<CharacterScript>().Weapon;
                _sprite = weapon.GetComponent<RenderComponent>().Sprite;
                _rectangleCollider = weapon.GetComponent<RectangleCollider>();
                


                _originalScale = _sprite.Scale;
                if (_rectangleCollider != null)
                    _originalSize = _rectangleCollider.Size;

                _sprite.Scale = _originalScale * _bigSpriteFactor;

                if (_rectangleCollider != null)
                    _rectangleCollider.Size = _rectangleCollider.Size * _bigSpriteFactor;
                
                _isBig = true;
            }
            
        }

        public override void Finish(GameObject player)
        {
            _sprite.Scale = _originalScale;
            if (_rectangleCollider != null)
                _rectangleCollider.Size = _originalSize;
            _isBig = false;
        }
        
        public PowerUpScript Clone()
        {
            return new PowerUpScript(this);
        }
    }
}