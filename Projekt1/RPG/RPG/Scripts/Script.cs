using System.Collections.Generic;

namespace ConsoleApp2
{
    public abstract class Script
    {
        protected GameObject _parent;
        private bool _isActive = true;

        public Script()
        {

        }

        public GameObject gameObject { get => _parent; set => _parent = value; }        //small because of unity ;)
        public bool IsActive { get => _isActive; set => _isActive = value; }

        public virtual void OnStart()
        {

        }

        public virtual void Update(double elapsedTime)
        {

        }

        public virtual void OnCollide(List<GameObject> collider)
        {

        }
    }
}