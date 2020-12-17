using System;
using System.Collections.Generic;
using Data;
using Enums;
using Extension;
using Interface;
using UniRx;
using UnityEngine;


namespace Unit.Player
{
    public sealed class CustomizingCharacter: ICleanup
    {
        #region Fields

        protected CompositeDisposable _subscriptions;
        private IPlayerView _player;
        private readonly PrototypePlayerModel _prototype;
        private CharacterSettingsData _characterSettingsData;

        #endregion

        public CustomizingCharacter(CharacterSettingsData characterSettingsData, IPlayerView player, PrototypePlayerModel prototype)
        {
            _subscriptions = new CompositeDisposable();
            _prototype = prototype;
            _player = player;
            _characterSettingsData = characterSettingsData;

            _prototype.CharacterClass.Subscribe(charClass =>
            {
                Dbg.Log($"_prototype.CharacterClass:{charClass}");
                switch (charClass)
                {
                    case CharacterClass.Warrior:
                        break;
                    case CharacterClass.Rogue:
                        break;
                    case CharacterClass.Hunter:
                        break;
                    case CharacterClass.Mage:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(charClass), charClass, null);
                }
            }).AddTo(_subscriptions);
            
            // rebuild all lists
            BuildLists();

            // disable any enabled objects before clear
            if (_characterSettingsData.enabledObjects.Count != 0)
            {
                foreach (GameObject g in _characterSettingsData.enabledObjects)
                {
                    g.SetActive(false);
                }
            }

            // clear enabled objects list
            _characterSettingsData.enabledObjects.Clear();

            // set default male character
            ActivateItem(_characterSettingsData.male.headAllElements[0]);
            ActivateItem(_characterSettingsData.male.eyebrow[0]);
            ActivateItem(_characterSettingsData.male.facialHair[0]);
            ActivateItem(_characterSettingsData.male.torso[0]);
            ActivateItem(_characterSettingsData.male.arm_Upper_Right[0]);
            ActivateItem(_characterSettingsData.male.arm_Upper_Left[0]);
            ActivateItem(_characterSettingsData.male.arm_Lower_Right[0]);
            ActivateItem(_characterSettingsData.male.arm_Lower_Left[0]);
            ActivateItem(_characterSettingsData.male.hand_Right[0]);
            ActivateItem(_characterSettingsData.male.hand_Left[0]);
            ActivateItem(_characterSettingsData.male.hips[0]);
            ActivateItem(_characterSettingsData.male.leg_Right[0]);
            ActivateItem(_characterSettingsData.male.leg_Left[0]);
        }

        public void Cleanup()
        {
            _subscriptions?.Dispose();
        }
        
        
        private void BuildLists()
        {
            //build out male lists
            BuildList(_characterSettingsData.male.headAllElements, "Male_Head_All_Elements");
            BuildList(_characterSettingsData.male.headNoElements, "Male_Head_No_Elements");
            BuildList(_characterSettingsData.male.eyebrow, "Male_01_Eyebrows");
            BuildList(_characterSettingsData.male.facialHair, "Male_02_FacialHair");
            BuildList(_characterSettingsData.male.torso, "Male_03_Torso");
            BuildList(_characterSettingsData.male.arm_Upper_Right, "Male_04_Arm_Upper_Right");
            BuildList(_characterSettingsData.male.arm_Upper_Left, "Male_05_Arm_Upper_Left");
            BuildList(_characterSettingsData.male.arm_Lower_Right, "Male_06_Arm_Lower_Right");
            BuildList(_characterSettingsData.male.arm_Lower_Left, "Male_07_Arm_Lower_Left");
            BuildList(_characterSettingsData.male.hand_Right, "Male_08_Hand_Right");
            BuildList(_characterSettingsData.male.hand_Left, "Male_09_Hand_Left");
            BuildList(_characterSettingsData.male.hips, "Male_10_Hips");
            BuildList(_characterSettingsData.male.leg_Right, "Male_11_Leg_Right");
            BuildList(_characterSettingsData.male.leg_Left, "Male_12_Leg_Left");

            //build out female lists
            BuildList(_characterSettingsData.female.headAllElements, "Female_Head_All_Elements");
            BuildList(_characterSettingsData.female.headNoElements, "Female_Head_No_Elements");
            BuildList(_characterSettingsData.female.eyebrow, "Female_01_Eyebrows");
            BuildList(_characterSettingsData.female.facialHair, "Female_02_FacialHair");
            BuildList(_characterSettingsData.female.torso, "Female_03_Torso");
            BuildList(_characterSettingsData.female.arm_Upper_Right, "Female_04_Arm_Upper_Right");
            BuildList(_characterSettingsData.female.arm_Upper_Left, "Female_05_Arm_Upper_Left");
            BuildList(_characterSettingsData.female.arm_Lower_Right, "Female_06_Arm_Lower_Right");
            BuildList(_characterSettingsData.female.arm_Lower_Left, "Female_07_Arm_Lower_Left");
            BuildList(_characterSettingsData.female.hand_Right, "Female_08_Hand_Right");
            BuildList(_characterSettingsData.female.hand_Left, "Female_09_Hand_Left");
            BuildList(_characterSettingsData.female.hips, "Female_10_Hips");
            BuildList(_characterSettingsData.female.leg_Right, "Female_11_Leg_Right");
            BuildList(_characterSettingsData.female.leg_Left, "Female_12_Leg_Left");

            // build out all gender lists
            BuildList(_characterSettingsData.allGender.all_Hair, "All_01_Hair");
            BuildList(_characterSettingsData.allGender.all_Head_Attachment, "All_02_Head_Attachment");
            BuildList(_characterSettingsData.allGender.headCoverings_Base_Hair, "HeadCoverings_Base_Hair");
            BuildList(_characterSettingsData.allGender.headCoverings_No_FacialHair, "HeadCoverings_No_FacialHair");
            BuildList(_characterSettingsData.allGender.headCoverings_No_Hair, "HeadCoverings_No_Hair");
            BuildList(_characterSettingsData.allGender.chest_Attachment, "All_03_Chest_Attachment");
            BuildList(_characterSettingsData.allGender.back_Attachment, "All_04_Back_Attachment");
            BuildList(_characterSettingsData.allGender.shoulder_Attachment_Right, "All_05_Shoulder_Attachment_Right");
            BuildList(_characterSettingsData.allGender.shoulder_Attachment_Left, "All_06_Shoulder_Attachment_Left");
            BuildList(_characterSettingsData.allGender.elbow_Attachment_Right, "All_07_Elbow_Attachment_Right");
            BuildList(_characterSettingsData.allGender.elbow_Attachment_Left, "All_08_Elbow_Attachment_Left");
            BuildList(_characterSettingsData.allGender.hips_Attachment, "All_09_Hips_Attachment");
            BuildList(_characterSettingsData.allGender.knee_Attachement_Right, "All_10_Knee_Attachement_Right");
            BuildList(_characterSettingsData.allGender.knee_Attachement_Left, "All_11_Knee_Attachement_Left");
            BuildList(_characterSettingsData.allGender.elf_Ear, "Elf_Ear");
        }

        // called from the BuildLists method
        void BuildList(List<GameObject> targetList, string characterPart)
        {
            Transform[] rootTransform = _player.Transform.gameObject.GetComponentsInChildren<Transform>();

            // declare target root transform
            Transform targetRoot = null;

            // find character parts parent object in the scene
            foreach (Transform t in rootTransform)
            {
                if (t.gameObject.name == characterPart)
                {
                    targetRoot = t;
                    break;
                }
            }

            // clears targeted list of all objects
            targetList.Clear();

            // cycle through all child objects of the parent object
            for (int i = 0; i < targetRoot.childCount; i++)
            {
                // get child gameobject index i
                GameObject go = targetRoot.GetChild(i).gameObject;

                // disable child object
                go.SetActive(false);

                // add object to the targeted object list
                targetList.Add(go);

                // collect the material for the random character, only if null in the inspector;
                if (!_characterSettingsData.mat)
                {
                    if (go.GetComponent<SkinnedMeshRenderer>())
                        _characterSettingsData.mat = go.GetComponent<SkinnedMeshRenderer>().material;
                }
            }
        }
        
        void ActivateItem(GameObject go)
        {
            // enable item
            go.SetActive(true);

            // add item to the enabled items list
            _characterSettingsData.enabledObjects.Add(go);
        }
    }
}