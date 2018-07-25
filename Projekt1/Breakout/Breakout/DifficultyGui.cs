using SFML.Graphics;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class DifficultyGui : GuiGroup
    {
        private List<Checkbox> _checkBoxes = new List<Checkbox>();
        private double _difficultiesDistances = 70;
        private Vector2D _checkboxSize = new Vector2D(24, 24);

        private Ai.Difficulty _difficultyLeft = Ai.Difficulty.Human;
        private Ai.Difficulty _difficultyRight = Ai.Difficulty.Human;

        public DifficultyGui(Vector2D position, Font font) : base(position)
        {
            Text up = new Text("UP", font, 20);
            Text down = new Text("DOWN", font, 20);

            MainMenu.SetTextOriginToMiddle(up);
            up.Position = new Vector2D(-150, 0);
            up.OutlineThickness = 1;
            up.OutlineColor = Color.White;
            up.FillColor = Color.Black;

            MainMenu.SetTextOriginToMiddle(down);
            down.Position = new Vector2D(150, 0);

            GuiGroup upDownGroup = new GuiGroup(new Vector2D(0, 0));
            upDownGroup.AddDrawable(up);
            upDownGroup.AddDrawable(down);


            List<GuiGroup> aiGroup = new List<GuiGroup>(3);
            Text[] newTexts = new Text[aiGroup.Capacity];

            for (int i = 0; i < aiGroup.Capacity; i++)
            {
                newTexts[i] = new Text("text", font, 20);
            }

            newTexts[0].DisplayedString = "HUMAN";
            newTexts[1].DisplayedString = "NONE";
            newTexts[2].DisplayedString = "AI";

            for (int i = 0; i < aiGroup.Capacity; i++)
            {
                MainMenu.SetTextOriginToMiddle(newTexts[i]);
                newTexts[i].Position = new Vector2D(0, 0);

                float yDiffernence = 3;
                Vector2D position1 = new Vector2D(-150, yDiffernence);
                Vector2D position2 = new Vector2D(150, yDiffernence);

                Checkbox checkbox1 = new Checkbox(position1, _checkboxSize, font, false, true, false);
                Checkbox checkbox2 = new Checkbox(position2, _checkboxSize, font, false, false, false);

                if (i == 0)
                {
                    checkbox1.IsChecked = true;
                    checkbox2.IsChecked = true;
                }


                Vector2D aiGroupPosition = new Vector2D(0, (i + 1) * _difficultiesDistances);

                aiGroup.Add(new GuiGroup(aiGroupPosition));

                aiGroup[i].AddDrawable(newTexts[i]);
                aiGroup[i].AddDrawable(checkbox1);
                aiGroup[i].AddDrawable(checkbox2);
                _checkBoxes.Add(checkbox1);
                _checkBoxes.Add(checkbox2);


            }

            this.AddDrawable(upDownGroup);

            foreach (GuiGroup ai in aiGroup)
            {
                this.AddDrawable(ai);
            }
        }
        
        public void Touched(Vector2D position)
        {
            for (int i = 0; i < _checkBoxes.Count; i++)
            {
                _checkBoxes[i].Touched(position);

                if (_checkBoxes[i].IsChecked)
                {
                    switch (i)
                    {
                        case 0:
                            _difficultyLeft = Ai.Difficulty.Human;
                            break;
                        case 1:
                            _difficultyRight = Ai.Difficulty.Human;
                            break;
                        case 2:
                            _difficultyLeft = Ai.Difficulty.None;
                            break;
                        case 3:
                            _difficultyRight = Ai.Difficulty.None;
                            break;
                        case 4:
                            _difficultyLeft = Ai.Difficulty.Normal;
                            break;
                        case 5:
                            _difficultyRight = Ai.Difficulty.Normal;
                            break;
                        default: return;
                    }


                    int jStart = 0;

                    if (i % 2 != 0)
                    {
                        jStart = 1;
                    }


                    for (int j = jStart; j < _checkBoxes.Count; j += 2)
                    {
                        if (i != j)
                        {
                            _checkBoxes[j].IsChecked = false;
                        }
                    }
                }

            }
        }


        public Ai.Difficulty GetDifficultyLeft()
        {
            return _difficultyLeft;
        }

        public Ai.Difficulty GetDifficultyRight()
        {
            return _difficultyRight;
        }

    }
}