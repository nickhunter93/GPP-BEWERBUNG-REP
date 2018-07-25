using SFML.Graphics;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class PlayerGui : Script
    {
        private GuiElement _background;
        private HealthBar _healthBar;
        private SimpleText _souls;
        private TextureGui _helmet;
        private TextureGui _estus;
        private SimpleText _estusCount;
        private int _estusCounter;

        private DataManager _dataManager;
        private List<GuiElement> _guiElements = new List<GuiElement>();
        private CharacterScript _characterScript = null;
        private EstusScript _estusScript = null;

        public PlayerGui(HealthBar healthBar, SimpleText souls, TextureGui helmet, TextureGui estus, GuiElement background, SimpleText estusCount)
        {
            _dataManager = DataManager.GetInstance();

            _healthBar = healthBar;
            _souls = souls;
            _helmet = helmet;
            _estus = estus;
            _background = background;
            _estusCount = estusCount;

            _guiElements.Add(background);
            _guiElements.Add(healthBar);
            _guiElements.Add(souls);
            _guiElements.Add(helmet);
            _guiElements.Add(estus);
            _guiElements.Add(estusCount);
        }

        public HealthBar HealthBar { get => _healthBar; }
        public SimpleText SimpleText { get => _souls; }
        public TextureGui Helmet { get => _helmet; set => _helmet = value; }
        public TextureGui Estus { get => _estus; set => _estus = value; }
        public GuiElement Background { get => _background; set => _background = value; }
        public SimpleText EstusCount { get => _estusCount; set => _estusCount = value; }

        public override void Update(double elapsedTime)
        {
            if (_characterScript == null)
                _characterScript = _parent.GetScript<CharacterScript>();

            if (_estusScript == null)
            {
                _estusScript = _parent.GetScript<EstusScript>();
            }

            foreach (GuiElement guiElement in _guiElements)
            {
                guiElement.Update(elapsedTime);
                guiElement.Position = guiElement.OriginalPosition + _dataManager.Window.GetView().Center - Program.windowSize / 2;
            }

            _healthBar.ChangeLife(_characterScript.Life);

            ChangeScore(_characterScript.Souls, int.Parse(_souls.Text.DisplayedString), 20);

            if (_estusCounter != _estusScript.EstusCount)
            {
                _estusCounter = _estusScript.EstusCount;

                _estusCount.ChangeText(_estusCounter.ToString());

                string estusPath = "";

                switch (_estusCounter)
                {
                    case 3:
                        estusPath = "Pictures/Estus/estus3.png";
                        break;
                    case 2:
                        estusPath = "Pictures/Estus/estus2.png";
                        break;
                    case 1:
                        estusPath = "Pictures/Estus/estus1.png";
                        break;
                    case 0:
                        estusPath = "Pictures/Estus/estus0.png";
                        break;
                    default:
                        estusPath = "Pictures/Estus/estus3.png";
                        break;
                }
                
                _estus.ChangeTexture(new Texture(estusPath));
            }

            //if (SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.Y))
            //    _parent.GetScript<CharacterScript>().Life--;
        }

        private void ChangeScore(int score, int _displayedScore, int countSpeed)
        {
            if (score >= _displayedScore)
            {
                int add = (score - _displayedScore) / countSpeed;
                if (add == 0)
                    add = score - _displayedScore;
                _displayedScore += add;
            }
            
            _souls.ChangeText(_displayedScore.ToString());
        }

        public List<GuiElement> GetGuiElements()
        {
            return _guiElements;
        }
    }
}