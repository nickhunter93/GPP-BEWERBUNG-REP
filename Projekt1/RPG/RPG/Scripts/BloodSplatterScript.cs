using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class BloodSplatterScript : Script
    {
        private double _timer = 0;
        private double _timerAtStart;
        private RenderComponent _renderComponent;
        public BloodSplatterScript(double timer)
        {
            _timer = timer;
            _timerAtStart = timer;
        }

        public override void Update(double elapsedTime)
        {
            if(_timer > 0)
            {
                _timer -= elapsedTime;
                if(_renderComponent == null)
                {
                    _renderComponent = gameObject.GetComponent<RenderComponent>();
                }
                if(_renderComponent != null)
                {
                    _renderComponent.Sprite.Color = new SFML.Graphics.Color(255,255,255,(byte)((255/_timerAtStart) * _timer));
                }

            }
            if(_timer <= 0)
            {
                DataManager dm = DataManager.GetInstance();
                dm.Environment.RemoveChildLate(gameObject);
                dm.BackgroundObjects.Remove(gameObject);
            }
        }
    }
}
