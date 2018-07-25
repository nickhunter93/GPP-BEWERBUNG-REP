using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class PauseMenu
    {
        private RenderWindow _window;
        private Font _font;

        private Text _pause;
        private Text _pressToContinue;

        private CircleShape _mouseCircle;

        private Vector2D _textfieldSize = new Vector2D(70, 20);
        private GuiGroup _resolutionGroup = new GuiGroup(new Vector2D(Program.windowSize.X / 2, 300));
        private List<Textfield> _textfields = new List<Textfield>();

        public PauseMenu(RenderWindow window, Font font)
        {
            _window = window;
            _font = font;
        }
        public void Start()
        {
            Initialise();
        }

        public void Run()
        {
            Input();
            Update();
            Redraw();
        }

        private void Initialise()
        {

            _pause = new Text("PAUSE", _font, 60);
            SetOriginToMiddle(_pause);
            _pause.Position = new Vector2D(Program.windowSize.X / 2, 100);

            _pressToContinue = new Text("PRESS ENTER TO CONTINUE", _font, 40);
            SetOriginToMiddle(_pressToContinue);
            _pressToContinue.Position = new Vector2D(Program.windowSize.X / 2, Program.windowSize.Y - 100);

            
        }

        private void Update()
        {
            
        }

        private void Input()
        {
            
            
        }
        

        private void Redraw()
        {

            _window.Clear();
            _window.DispatchEvents();
            
            _window.Draw(_pause);
            _window.Draw(_pressToContinue);
            _window.Draw(_resolutionGroup);

            _window.Display();
        }

        private void SetOriginToMiddle(Text text)
        {
            text.Origin = new Vector2D(text.GetGlobalBounds().Width / 2, text.GetGlobalBounds().Height / 2);
        }
    }
}