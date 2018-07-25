using SFML.Graphics;

namespace ConsoleApp2
{
    public class BonfireScript : Script
    {
        private int _souls;
        private AnimationManager _animationManager;
        private bool _isLit = false;

        public BonfireScript(int souls)
        {
            _souls = souls;
            _animationManager = new AnimationManager();
        }

        public int Souls { get => _souls; }
        public bool IsLit { get => _isLit; }

        public void Lit()
        {
            if (!_animationManager.ExistAnimationToTextureComponent(_parent.GetComponent<RenderComponent>()) && !_isLit)
            {
                RenderComponent textureComponent = _parent.GetComponent<RenderComponent>();
                _animationManager.AddAnimation(new TextureAnimation(textureComponent, new Texture[] { new Texture("Pictures/bonfire2_2.png"), new Texture("Pictures/bonfire3_2.png") }, -1, 0, 500, Vector2D.One(), Vector2D.One() * -1));
                _animationManager.AddAnimation(new TextureAnimation(textureComponent, null, 200, 0, 0, Vector2D.One() * 4, Vector2D.One() * -1));
                _animationManager.AddAnimation(new TextureAnimation(textureComponent, null, 200, 200, 0, Vector2D.One(), Vector2D.One() * 4));
                _isLit = true;
            }
        }

        public override void Update(double elapsedTime)
        {
            _animationManager.Update(elapsedTime);
        }
    }
}