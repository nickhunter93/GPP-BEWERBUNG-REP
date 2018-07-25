namespace ConsoleApp2
{
    public abstract class Component
    {
        private GameObject _parent;

        public GameObject Parent { get => _parent; set => _parent = value; }

        public abstract void Update(double elapsedTime);
        
    }
}