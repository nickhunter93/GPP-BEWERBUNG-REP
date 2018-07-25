using SFML.Graphics;
using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class ActionScript : Script, IRegisterEvent
    {
        private DataManager _dataManager;
        private double _minDistance;
        

        public ActionScript(double minDistance)
        {
            _dataManager = DataManager.GetInstance();
            _minDistance = minDistance;
            MessageBus.RegisterEvent(this);
        }

        public event EventHandler Play;

        public void OnPlay(String sound)
        {
            if (sound == "chestopen")
                Play?.Invoke(this, new SoundsEventArgs(SoundManager.SoundNumbers.Chestopen));
            else if (sound == "chestmimic")
                Play?.Invoke(this, new SoundsEventArgs(SoundManager.SoundNumbers.Chestmimic));
            else if (sound == "bonfire")
                Play?.Invoke(this, new SoundsEventArgs(SoundManager.SoundNumbers.Bonfire));
        }

        public void Action()
        {
            List<GameObject> actionObjects = new List<GameObject>();
            actionObjects.AddRange(_dataManager.Bonfires);
            actionObjects.AddRange(_dataManager.Chests);

            double closestDistance = _minDistance;
            GameObject nearestObject = null;

            for (int i = 0; i < actionObjects.Count; i++)
            {
                double distance = actionObjects[i].transform.Position.GetDistance(_parent.transform.Position);

                if (distance < _minDistance && distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestObject = actionObjects[i];
                }
            }

            if (nearestObject != null)
            {
                if (_dataManager.Bonfires.Contains(nearestObject))
                {
                    if (!nearestObject.GetScript<BonfireScript>().IsLit)
                    {
                        nearestObject.GetScript<BonfireScript>().Lit();
                        _parent.GetScript<CharacterScript>().Souls += nearestObject.GetScript<BonfireScript>().Souls;
                        OnPlay("bonfire");
                    }
                }

                if (_dataManager.Chests.Contains(nearestObject))
                {
                    if (nearestObject.GetScript<MimicAi>() == null)
                    {
                        new Factory().CreatePowerUp(nearestObject.transform.Position, new System.Random().Next(0, 3), _dataManager.PrefabPowerUps);
                        _dataManager.Chests.Remove(nearestObject);
                        _dataManager.BackgroundObjects.Remove(nearestObject);
                        _dataManager.Environment.RemoveChild(nearestObject);
                        OnPlay("chestopen");
                    }
                    else
                    {
                        nearestObject.GetScript<MimicAi>().Attack(_parent);
                        nearestObject.GetComponent<RenderComponent>().Update(0);
                        nearestObject.GetComponent<RenderComponent>().DrawSprite();
                        OnPlay("chestmimic");

                        Program.windowState = Program.WindowState.GameOver;
                    }


                }
            }
        }
    }
}