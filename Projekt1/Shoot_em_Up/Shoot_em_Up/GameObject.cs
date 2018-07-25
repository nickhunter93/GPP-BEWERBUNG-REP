using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class GameObject
    {
        private List<Component> _components;
        private List<GameObject> _childs;
        private List<Script> _scripts;
        private Transform _transform;
        private GameObject _parent;
        private bool collisionHappened = false;
        private ICollider collidedWith = null;

        public GameObject()
        {
            _components = new List<Component>();
            _transform = new Transform(this);
            _childs = new List<GameObject>();
            _scripts = new List<Script>();
        }

        public GameObject(Vector2D position) :this()
        {
            _transform.Position = position;
        }
        
        public GameObject Parent { get => _parent; set => _parent = value; }
        public Transform transform { get => _transform;}
        public bool CollisionHappened { get => collisionHappened; set => collisionHappened = value; }
        public ICollider CollidedWith { get => collidedWith; set => collidedWith = value; }

        public void SetChild(GameObject gameObject)
        {
            if(gameObject.Parent != null)
            {
                gameObject.Parent.RemoveChild(gameObject);
            }
            gameObject.Parent = this;
            _childs.Add(gameObject);
        }

        public void RemoveChild(GameObject gameObject)
        {
            gameObject.Parent = null;
            _childs.Remove(gameObject);
        }

        public void AddComponent(Component component)
        {
            if (component.GetType() == typeof(Transform))
               throw new ArgumentException("Transform can´t be added manually.<3");

            foreach (Component item in _components)
            {
                
                if (item.GetType() == component.GetType())
                {
                    return;
                }
            }
            component.Parent = this;
            _components.Add(component);
        }

        public T GetComponent<T>()
        {
            if(_components.Count != 0)
            {
                foreach (Component component in _components)
                {
                    if(component is T)
                    {
                        return (T)(object)component;
                    }
                }
            }
            

            return default(T);
        }

        public List<T> GetComponents<T>()
        {
            List<T> myList = new List<T>();

            if (_components.Count != 0)
            {
                foreach (Component component in _components)
                {
                    if (component is T)
                    {
                        myList.Add((T)(object)component);
                    }
                }
            }

            return myList;
        }

        public List<T> GetComponentsInChilds<T>()
        {
            List<T> myList = new List<T>();
            addToList<T>(myList);
            return myList;
        }

        private List<T> addToList<T>(List<T> myList)
        {
            foreach (GameObject gameObject in _childs)
            {
                gameObject.addToList<T>(myList);
            }
            myList.Add(this.GetComponent<T>());
            return myList;
        }

        public void Update(double elapsedTime)
        {
            transform.Update(elapsedTime);
            foreach (Component component in _components)
            {
                component.Update(elapsedTime);
            }
            foreach (Script script in _scripts)
            {
                if (script.IsActive)
                    script.Update(elapsedTime);
                if (CollisionHappened)
                {
                    script.OnCollide(collidedWith);
                }
            }
            if (CollisionHappened)
            {
                collidedWith = null;
                CollisionHappened = false;
            }
            foreach (GameObject child in _childs)
            {
                child.Update(elapsedTime);
            }
        }

        public List<GameObject> GetChilds()
        {
            return _childs;
        }

        public void AddScript(Script script)
        {
            script.gameObject = this;
            _scripts.Add(script);
        }

        public List<T> GetScripts<T>()
        {
            List<T> scripts = new List<T>();

            foreach (Script script in _scripts)
            {
                if (script is T s)
                {
                    scripts.Add(s);
                }
            }

            return scripts;
        }
    }
}