using SFML.Graphics;
using System;
using System.Collections.Generic;

namespace ConsoleApp2
{

    public class GuiGroup : Transformable, Drawable
    {
        private List<Drawable> _drawables = new List<Drawable>();
        private bool _isVisible = true;

        public GuiGroup(Vector2D position)
        {
            Position = position;
        }

        public bool IsVisible { get => _isVisible; set => _isVisible = value; }
        public new Vector2D Position
        {
            get { return base.Position; }
            set
            {
                ChangePositionOfAll(value - base.Position);
                base.Position = value;
            }
        }

        public void AddDrawable(Drawable drawable)
        {
            _drawables.Add(drawable);
            ChangePositionOfDrawable(drawable, Position);
        }

        public void RemoveDrawable(Drawable drawable)
        {
            _drawables.Remove(drawable);
        }

        public Drawable GetDrawable(int i)
        {
            return _drawables[i];
        }

        public List<Drawable> GetAllDrawables()
        {
            return _drawables;
        }

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

        private void ChangePositionOfAll(Vector2D translate)
        {
            foreach (Drawable drawable in _drawables)
            {
                ChangePositionOfDrawable(drawable, translate);
            }
            
        }

        private void ChangePositionOfDrawable(Drawable drawable, Vector2D translate)
        {
            if (drawable is GuiGroup guiGroup)
            {
                guiGroup.Position += translate;
                
            }
            else if (drawable is GuiElement guiElement)
            {
                guiElement.Position += translate;

            }
            else if (drawable is Transformable transformable)
            {
                transformable.Position += translate;
            }
        }
        
        
    }
}