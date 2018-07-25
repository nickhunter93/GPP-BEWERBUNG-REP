using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2
{
    public class GameObject
    {
        private string _objectName = "";
        private List<Component> _components;
        private List<GameObject> _childs;
        private List<Script> _scripts;
        private Transform _transform;
        private GameObject _parent;
        private bool collisionHappened = false;
        private List<GameObject> collidedWith = new List<GameObject>();
        private bool _lateRemoval = false;
        private List<GameObject> _toBeRemoved = new List<GameObject>();

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
        public List<GameObject> CollidedWith { get => collidedWith; set => collidedWith = value; }
        public string ObjectName { get => _objectName; set => _objectName = value; }

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

        internal void RemoveChildLate(GameObject gameObject)
        {
            _lateRemoval = true;
            _toBeRemoved.Add(gameObject);
        }

        public T GetComponent<T>()
        {
            return _components.OfType<T>().FirstOrDefault();
        }

        public List<T> GetComponents<T>()
        {
            return (List<T>)(_components.OfType<T>());
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
                collidedWith.Clear();
                CollisionHappened = false;
            }

            int childCount = _childs.Count;
            for (int i = 0; i < childCount; i++)
            {
                if (_childs[i] != null)
                {
                    _childs[i].Update(elapsedTime);
                    _childs[i].Update(0);
                }
                else
                {
                    break;
                }
            }

            if (_lateRemoval)
            {
                foreach (var item in _toBeRemoved)
                {
                    RemoveChild(item);
                }
                _lateRemoval = false;
                _toBeRemoved.Clear();
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

        public T GetScript<T>()
        {
            if (_scripts.Count != 0)
            {
                foreach (Script script in _scripts)
                {
                    if (script is T)
                    {
                        return (T)(object)script;
                    }
                }
            }


            return default(T);
        }

        public List<T> GetScriptsInChilds<T>()
        {
            List<T> myList = new List<T>();
            addToListScript<T>(myList);
            return myList;
        }

        private List<T> addToListScript<T>(List<T> myList)
        {
            foreach (GameObject gameObject in _childs)
            {
                gameObject.addToListScript<T>(myList);
            }
            myList.Add(this.GetScript<T>());
            return myList;
        }
    }
}