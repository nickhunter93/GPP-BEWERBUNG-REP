using SFML.Graphics;
using SFML.Window;
using System;
using System.Diagnostics;
using System.Threading;

namespace ConsoleApp2
{
    public class PopUp : IRegisterEvent
    {
        private RenderWindow _window;
        private Font _font;

        private Stopwatch _stopwatch = new Stopwatch();
        private TextureGui _controls;
        private bool _isMousePressed = true;

        private View camera;

        public PopUp(RenderWindow window, Font font)
        {
            _window = window;
            _font = font;
            MessageBus.RegisterEvent(this);
            
            
            Sprite sprite = new Sprite(new Texture("Pictures/controls.png"));
            sprite.Color = new Color(255, 255, 255, 10);
            _controls = new TextureGui(new Vector2D(Program.windowSize.X / 2 - sprite.Texture.Size.X / 2, Program.windowSize.Y / 2 - sprite.Texture.Size.Y / 2), Vector2D.Zero(), _font, sprite, true);
        }

        public event EventHandler Play;

        public void OnPlay(String sound)
        {
            if (sound == "menuselect")
                Play?.Invoke(this, new SoundsEventArgs(SoundManager.SoundNumbers.Menuselect));
        }

        public void Start()
        {
            Initialise();
        }

        public void Run()
        {
            if (camera == null)
            {
                camera = new View(Program.windowSize / 2, Program.windowSize);
            }

            _window.SetView(camera);
            double elapsedTime = (double)_stopwatch.ElapsedTicks / (double)Stopwatch.Frequency * 1000;

            _stopwatch.Restart();

            Input();
            Update(elapsedTime);
            Redraw();

            _stopwatch.Stop();
        }

        private void Initialise()
        {

        }


        private void Input()
        {


            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (!_isMousePressed)
                {
                    _isMousePressed = true;
                    Program.windowState = Program.lastWindowState;
                    OnPlay("menuselect");
                }
            }
            else
            {
                _isMousePressed = false;
            }
        }

        private void Update(double elapsedTime)
        {
            /*if (camera == null)
            {
                camera = new View(Program.windowSize / 2, Program.windowSize);
                _window.SetView(camera);
            }*/

        }

        private void Redraw()
        {
            //_window.Clear();
            //_window.DispatchEvents();

            _window.Draw(_controls);

            _window.Display();
        }



    }
}