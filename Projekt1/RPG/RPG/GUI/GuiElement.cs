﻿using SFML.Graphics;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public abstract class GuiElement : Transformable, Drawable
    {
        private Vector2D _originalPosition;
        protected Vector2D _size;
        protected List<Drawable> _drawables = new List<Drawable>();
        private bool _isVisible = true;

        public GuiElement(Vector2D position, Vector2D size, Font font)
        {
            Position = position;
            _size = size;
            Origin = _size / 2;
            _originalPosition = position;
        }

        public bool IsVisible { get => _isVisible; set => _isVisible = value; }
        public new Vector2D Position
        {
            get { return base.Position; }
            set
            {
                ChangePosition(value - base.Position);
                base.Position = value;
            }
        }
        public Vector2D OriginalPosition { get => _originalPosition; }

        public abstract bool Touched(Vector2D position);

        public abstract void Update(double elapsedTime);

        void Drawable.Draw(RenderTarget target, RenderStates states)
        {
            if (IsVisible)
            {
                foreach (Drawable drawable in _drawables)
                {
                    target.Draw(drawable);
                }
            }
        }

        public void ChangePositionOfDrawable(Drawable drawable, Vector2D translate)
        {
            if (drawable is Transformable transformable)
            {
                transformable.Position += translate;
            }

        }

        public void ChangePosition(Vector2D translate)
        {
            foreach (Drawable drawable in _drawables)
            {
                ChangePositionOfDrawable(drawable, translate);
            }
            
        }
        
    }
}