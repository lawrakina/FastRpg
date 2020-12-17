using System;
using Data;
using Enums;
using Extension;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Unit.Player
{
    public sealed class PlayerFactory : IPlayerFactory
    {
        public IPlayerView CreatePlayer(GameObject prefab, CharacterSettings characterSettings)
        {
            var player = Object.Instantiate(prefab);
            player.name = $"PlayerCharacter.{characterSettings.CharacterClass}";
            player.AddCapsuleCollider(radius: 0.5f, isTrigger: false, 
                      center: new Vector3(0.0f,0.9f,0.0f),
                      height: 1.8f)
                  .AddRigidBody(mass: 80, CollisionDetectionMode.ContinuousSpeculative, 
                      isKinematic: false, useGravity: true, 
                      constraints: RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezeRotationZ)
                  .AddCode<PlayerView>();
            
            var component = player.GetComponent<PlayerView>();
            component.CharAttributes.Speed = characterSettings.PlayerMoveSpeed;
            component.CharAttributes.AgroDistance = characterSettings.AgroDistance;
            component.CharAttributes.RotateSpeedPlayer = characterSettings.RotateSpeedPlayer;
            component.CharAttributes.CharacterGender = characterSettings.CharacterGender;
            component.CharAttributes.CharacterRace = characterSettings.CharacterRace;

            switch (characterSettings.CharacterClass)
            {
                case CharacterClass.Warrior:
                    component.CharacterClass = new CharacterClassWarrior();
                    break;
                case CharacterClass.Rogue:
                    component.CharacterClass = new CharacterClassRogue();
                    break;
                case CharacterClass.Hunter:
                    component.CharacterClass = new CharacterClassHunter();
                    break;
                case CharacterClass.Mage:
                    component.CharacterClass = new CharacterClassMage();
                    break;
                default:
                    throw new Exception("PlayerFactory. playerData.PlayerSettings.CharacterClass:Недопустимое значение");
            }

            return component;
        }
    }
}