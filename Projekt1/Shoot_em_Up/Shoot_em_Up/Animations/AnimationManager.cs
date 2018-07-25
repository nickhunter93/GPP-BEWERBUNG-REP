using SFML.Graphics;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class AnimationManager
    {
        private List<Animation> _animations = new List<Animation>();


        public void Update(double elapsedTime)
        {
            List<Animation> deleteAnimations = new List<Animation>();

            foreach (Animation animation in _animations)
            {
                animation.Update(elapsedTime);

                if (animation.DurationLeft < 0)
                {
                    deleteAnimations.Add(animation);

                    if (animation is MoveAnimation a)
                        a.GoToEndPosition();
                }
            }

            foreach (Animation delete in deleteAnimations)
            {
                _animations.Remove(delete);
            }
        }

        public void AddAnimation(Animation animation)
        {
            if (Program.playAnimations)
                _animations.Add(animation);
        }

        public bool ExistAnimationToTextureComponent(RenderComponent textureComponent)
        {
            foreach (TextureAnimation animation in _animations)
            {
                if (animation.TextureComponent == textureComponent)
                    return true;
            }

            return false;
        }

        public void RemoveAnimationsToTextureComponent(RenderComponent textureComponent)
        {
            List<TextureAnimation> removeAnimations = new List<TextureAnimation>();

            foreach (TextureAnimation animation in _animations)
            {
                if (animation.TextureComponent == textureComponent)
                {
                    if (!removeAnimations.Contains(animation))
                        removeAnimations.Add(animation);
                }
            }

            foreach (TextureAnimation animation in removeAnimations)
            {
                _animations.Remove(animation);
            }
        }

        public void RemoveAnimationsToTransformable(Transformable transformable)
        {
            List<MoveAnimation> removeAnimations = new List<MoveAnimation>();

            foreach (MoveAnimation animation in _animations)
            {
                foreach (Transformable animationTransformable in animation.Transformables)
                {
                    if (animationTransformable == transformable)
                    {
                        if (!removeAnimations.Contains(animation))
                            removeAnimations.Add(animation);
                    }
                }
            }

            foreach (MoveAnimation animation in removeAnimations)
            {
                _animations.Remove(animation);
            }
        }

    }
}