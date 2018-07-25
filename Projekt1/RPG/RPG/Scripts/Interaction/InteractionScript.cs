using System.Collections.Generic;

namespace ConsoleApp2
{
    public abstract class InteractionScript : Script
    {
        protected double[] _interactionIntervals;
        protected double[] _interactionTimer;

        public InteractionScript(double[] interactionIntervals)
        {
            _interactionIntervals = interactionIntervals;
            _interactionTimer = new double[interactionIntervals.Length];
        }
    }
}