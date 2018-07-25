using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class PlayerGui : GuiGroup
    {
        
        private Vector2D _checkboxSize = new Vector2D(24, 24);
        private List<Checkbox> _checkboxes = new List<Checkbox>();
        private Font _font;
        private double _settingsDistances = 70;


        public PlayerGui(Vector2D position, Font font) : base(position)
        {
            _font = font;

            PlayerCount();
            //Difficulty();
        }

        private void PlayerCount()
        {

            Text onePlayer = new Text("1 PLAYER", _font, 20);
            MainMenu.SetTextOriginToMiddle(onePlayer);
            onePlayer.Position = new Vector2D(-60, -3);

            Checkbox onePlayerCheck = new Checkbox(new Vector2D(60, 0), _checkboxSize, _font, true, false, false);

            GuiGroup onePlayerGroup = new GuiGroup(new Vector2D(0, 0));
            onePlayerGroup.AddDrawable(onePlayer);
            onePlayerGroup.AddDrawable(onePlayerCheck);
            _checkboxes.Add(onePlayerCheck);
            
            this.AddDrawable(onePlayerGroup);

            
            Text twoPlayers = new Text("2 PLAYERS", _font, 20);
            MainMenu.SetTextOriginToMiddle(twoPlayers);
            twoPlayers.Position = new Vector2D(-60, -3);

            Checkbox twoPlayersCheck = new Checkbox(new Vector2D(60, 0), _checkboxSize, _font, false, false, false);

            GuiGroup twoPlayersGroup = new GuiGroup(new Vector2D(0, _settingsDistances));
            twoPlayersGroup.AddDrawable(twoPlayers);
            twoPlayersGroup.AddDrawable(twoPlayersCheck);
            _checkboxes.Add(twoPlayersCheck);
            
            this.AddDrawable(twoPlayersGroup);
        }

        private void Difficulty()
        {

            Text easy = new Text("EASY", _font, 20);
            MainMenu.SetTextOriginToMiddle(easy);
            easy.Position = new Vector2D(-60, -3);

            Checkbox easyCheck = new Checkbox(new Vector2D(60, 0), _checkboxSize, _font, true, false, false);

            GuiGroup easyGroup = new GuiGroup(new Vector2D(0, _settingsDistances * 2));
            easyGroup.AddDrawable(easy);
            easyGroup.AddDrawable(easyCheck);
            _checkboxes.Add(easyCheck);

            this.AddDrawable(easyGroup);


            Text hard = new Text("HARD", _font, 20);
            MainMenu.SetTextOriginToMiddle(hard);
            hard.Position = new Vector2D(-60, -3);

            Checkbox hardCheck = new Checkbox(new Vector2D(60, 0), _checkboxSize, _font, false, false, false);

            GuiGroup hardGroup = new GuiGroup(new Vector2D(0, _settingsDistances * 3));
            hardGroup.AddDrawable(hard);
            hardGroup.AddDrawable(hardCheck);
            _checkboxes.Add(hardCheck);

            this.AddDrawable(hardGroup);
        }

        public void TouchedOnce(Vector2D position)
        {

            for (int i = 0; i < _checkboxes.Count; i++)
            {
                _checkboxes[i].Touched(position);

                if (_checkboxes[i].IsChecked)
                {
                    switch (i)
                    {
                        case 0:
                            _checkboxes[1].IsChecked = false;
                            break;
                        case 1:
                            _checkboxes[0].IsChecked = false;
                            break;
                        case 2:
                            _checkboxes[3].IsChecked = false;
                            break;
                        case 3:
                            _checkboxes[2].IsChecked = false;
                            break;
                        default: return;
                    }
                }

            }
        }

        public bool IsEasy()
        {
            //if (_checkboxes[2].IsChecked)
            //    return true;
            return false;
        }

        public int GetPlayerCount()
        {
            if (_checkboxes[0].IsChecked)
                return 1;
            return 2;
        }

        public void PlayerSet()
        {
            _checkboxes[0].IsCheckable = false;
            _checkboxes[1].IsCheckable = false;
        }

    }
}