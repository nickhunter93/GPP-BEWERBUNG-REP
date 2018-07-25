using System;
using SFML.Window;

namespace ConsoleApp2
{
    public class KeyboardInteractionScript : InteractionScript
    {
        private Keyboard.Key[] _interactions;

        public KeyboardInteractionScript(Keyboard.Key[] interactions, double[] interactionIntervals) : base(interactionIntervals)
        {
            _interactions = interactions;
        }

        public override void Update(double elapsedTime)
        {
            for (int i = 0; i < _interactions.Length; i++)
            {
                _interactionTimer[i] -= elapsedTime;

                if (Keyboard.IsKeyPressed(_interactions[i]))
                {
                    if (_interactionTimer[i] < 0)
                    {
                        _interactionTimer[i] = _interactionIntervals[i];

                        switch (i)
                        {
                            case 0:
                                _parent.GetScript<ActionScript>().Action();
                                break;
                            case 1:
                                _parent.GetScript<EstusScript>().UseEstus();
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            
        }
    }
}