using SFML.Window;

namespace ConsoleApp2
{
    public class ControllerInteractionScript : InteractionScript
    {
        private uint[] _interactions;
        private uint _id;

        public ControllerInteractionScript(uint id, uint[] interactions, double[] interactionIntervals) : base(interactionIntervals)
        {
            _interactions = interactions;
            _id = id;
        }

        public override void Update(double elapsedTime)
        {
            for (int i = 0; i < _interactions.Length; i++)
            {
                _interactionTimer[i] -= elapsedTime;

                if (Joystick.IsButtonPressed(_id, _interactions[i]))
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
