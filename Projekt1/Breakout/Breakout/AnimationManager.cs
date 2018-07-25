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
                    animation.GoToEndPosition();
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

        public void RemoveAnimationsToObject(Transformable transformable)
        {
            List<Animation> removeAnimations = new List<Animation>();

            foreach (Animation animation in _animations)
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

            foreach (Animation animation in removeAnimations)
            {
                _animations.Remove(animation);
            }
        }

    }
}