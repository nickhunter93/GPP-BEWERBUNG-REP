using System.Collections.Generic;

namespace ConsoleApp2
{
    public abstract class WeaponScript : Script
    {
        private List<GameObject> _friendly;

        public List<GameObject> Friendly { get => _friendly; set => _friendly = value; }

        public abstract void Attack();
        public abstract RenderComponent GetTextureComponent();
    }
}