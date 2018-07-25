namespace ConsoleApp2
{
    public static class MessageBus
    {

        static SoundManager sM;

        //Register the SoundManager to the MessageBus.
        public static void RegisterSM(SoundManager soundManager) => sM = soundManager;
        //Register the SoundManager to a Event.
        public static void RegisterEvent(object regis)
        {
            if(regis is IRegisterEvent rE)
            {
                rE.Play += sM.OnPlay;
            }
        }
    }
}