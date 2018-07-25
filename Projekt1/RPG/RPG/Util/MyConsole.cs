namespace ConsoleApp2
{
    public static class MyConsole
    {
        public static void Out(string msg)
        {
            System.Console.Out.WriteLine(msg);
        }
        public static void Debug(object obj, string msg)
        {
            System.Console.Out.WriteLine(obj.GetType().ToString() + " - " + msg);
        }
    }
}