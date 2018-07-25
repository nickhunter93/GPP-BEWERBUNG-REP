using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class SettingsGui : GuiGroup
    {

        private Vector2D _textfieldSize = new Vector2D(100, 20);
        private List<Textfield> _textfields = new List<Textfield>();
        private Vector2D _checkboxSize = new Vector2D(24, 24);
        private List<Checkbox> _checkboxes = new List<Checkbox>();
        private Vector2D _sliderSize = new Vector2D(100, 5);
        private List<Slider> _sliders = new List<Slider>();
        private RenderWindow _window;
        private Font _font;
        private double _settingsDistances = 70;


        public SettingsGui(Vector2D position, Font font, RenderWindow window) : base(position)
        {
            _window = window;
            _font = font;

            Resolution();
            Fullscreen();
            Sound();
            Life();
        }

        private void Resolution()
        {

            Text resolutionX = new Text("RESOLUTION X", _font, 20);
            MainMenu.SetTextOriginToMiddle(resolutionX);
            resolutionX.Position = new Vector2D(-60, -3);

            Vector2D resolutionXInputPosition = new Vector2D(100, 0);
            Textfield resolutionXInput = new Textfield(resolutionXInputPosition, _textfieldSize, _font, 20, false, ">=" + Program.windowSize.X.ToString(), _window, true);

            GuiGroup resolutionXGroup = new GuiGroup(new Vector2D(0, 0));
            resolutionXGroup.AddDrawable(resolutionX);
            resolutionXGroup.AddDrawable(resolutionXInput);
            _textfields.Add(resolutionXInput);

            Text resolutionY = new Text("RESOLUTION Y", _font, 20);
            MainMenu.SetTextOriginToMiddle(resolutionY);
            resolutionY.Position = new Vector2D(-60, -3);

            Vector2D resolutionYInputPosition = new Vector2D(100, 0);
            Textfield resolutionYInput = new Textfield(resolutionYInputPosition, _textfieldSize, _font, 20, false, ">=" + Program.windowSize.Y.ToString(), _window, true);

            GuiGroup resolutionYGroup = new GuiGroup(new Vector2D(0, _settingsDistances));
            resolutionYGroup.AddDrawable(resolutionY);
            resolutionYGroup.AddDrawable(resolutionYInput);
            _textfields.Add(resolutionYInput);

            resolutionXInput.IsVisible = !Program.fullscreen;
            resolutionYInput.IsVisible = !Program.fullscreen;

            this.AddDrawable(resolutionXGroup);
            this.AddDrawable(resolutionYGroup);
        }

        private void Fullscreen()
        {
            Text fullscreen = new Text("FULLSCREEN", _font, 20);
            MainMenu.SetTextOriginToMiddle(fullscreen);
            fullscreen.Position = new Vector2D(-60, -3);
            
            Checkbox fullscreenCheck = new Checkbox(new Vector2D(60, 0), _checkboxSize, _font, Program.fullscreen, false, true);

            GuiGroup fullscreenGroup = new GuiGroup(new Vector2D(0, _settingsDistances * 2));
            fullscreenGroup.AddDrawable(fullscreen);
            fullscreenGroup.AddDrawable(fullscreenCheck);
            _checkboxes.Add(fullscreenCheck);


            this.AddDrawable(fullscreenGroup);
        }

        private void Sound()
        {
            Text sound = new Text("VOLUME", _font, 20);
            MainMenu.SetTextOriginToMiddle(sound);
            sound.Position = new Vector2D(-60, -3);

            //Checkbox soundCheck = new Checkbox(new Vector2D(60, 0), _checkboxSize, _font, Program.muted, false, true);
            Vector2D volumePosition = new Vector2D(100, 0);
            Slider volume = new Slider(volumePosition, _sliderSize, _font);

            GuiGroup soundGroup = new GuiGroup(new Vector2D(0, _settingsDistances * 3));
            soundGroup.AddDrawable(sound);
            soundGroup.AddDrawable(volume);
            _sliders.Add(volume);


            this.AddDrawable(soundGroup);

        }

        private void Life()
        {
            Text lifes = new Text("LIFES", _font, 20);
            MainMenu.SetTextOriginToMiddle(lifes);
            lifes.Position = new Vector2D(-60, -3);

            Vector2D lifesInputPosition = new Vector2D(100, 0);
            Textfield lifesInput = new Textfield(lifesInputPosition, _textfieldSize, _font, 20, false, Program.lifes.ToString(), _window, true);

            GuiGroup lifeGroup = new GuiGroup(new Vector2D(0, _settingsDistances * 4));
            lifeGroup.AddDrawable(lifes);
            lifeGroup.AddDrawable(lifesInput);
            _textfields.Add(lifesInput);


            this.AddDrawable(lifeGroup);
        }

        public void TouchedMulitiple(Vector2D position)
        {
            foreach (Slider slider in _sliders)
            {
                slider.Touched(position);
                Program.volume = slider.Value;
            }
        }

        public void TouchedOnce(Vector2D position)
        {
            foreach (Textfield textfield in _textfields)
            {
                textfield.Touched(position);
            }

            for (int i = 0; i < _checkboxes.Count; i++)
            {
                bool oldState = _checkboxes[i].IsChecked;
                _checkboxes[i].Touched(position);
                
                switch (i)
                {
                    case 0:
                            Program.fullscreen = _checkboxes[i].IsChecked;
                            if (oldState != _checkboxes[i].IsChecked)
                                Program.Restart(false);
                        break;
                    case 1:
                            Program.muted = _checkboxes[i].IsChecked;
                        break;
                    default: return;
                }
                    

            }
        }

        public void Update()
        {
            foreach (Textfield textfield in _textfields)
            {
                textfield.Update();
            }


            if (_textfields[0].IsChecked || _textfields[1].IsChecked)
            {
                double newXSize = _textfields[0].GetNumbersInText();
                double newYSize = _textfields[1].GetNumbersInText();

                if (Program.windowSize.X != newXSize || Program.windowSize.Y != newYSize)
                {
                    if (newXSize >= Program.minWindowSize.X && newYSize >= Program.minWindowSize.Y)
                    {
                        Program.userWindowSize = new Vector2D(_textfields[0].GetNumbersInText(), _textfields[1].GetNumbersInText());
                        Program.Restart(false);
                    }
                }
            }


            if (_textfields[2].IsChecked)
            {
                Program.lifes = _textfields[2].GetNumbersInText();

                if (Program.maxLifes < Program.lifes)
                {
                    Program.maxLifes = Program.lifes;
                }
            }
            else
            {
                _textfields[2].SetText(Program.lifes.ToString());
            }
        }
    }
}