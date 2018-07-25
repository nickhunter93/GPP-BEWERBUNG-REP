using System.Collections.Generic;

namespace ConsoleApp2
{
    public class EnemyGui : Script
    {
        private HealthBar _healthBar;
        private CharacterScript _characterScript = null;
        private List<GuiElement> _guiElements = new List<GuiElement>();

        public EnemyGui(HealthBar healthBar)
        {
            _healthBar = healthBar;

            _guiElements.Add(healthBar);
        }

        public HealthBar HealthBar { get => _healthBar; set => _healthBar = value; }

        public override void Update(double elapsedTime)
        {
            if (_characterScript == null)
                _characterScript = _parent.GetScript<CharacterScript>();

            foreach (GuiElement guiElement in _guiElements)
            {
                guiElement.Update(elapsedTime);
                guiElement.Position = _parent.transform.Position + new Vector2D(-25, -40);
            }

            _healthBar.ChangeLife(_characterScript.Life);
        }

        public List<GuiElement> GetGuiElements()
        {
            return _guiElements;
        }

    }
}