﻿using UnityEngine;


namespace Windows
{
    public sealed class CharacterWindow : BaseWindow
    {
        #region Fields

        [SerializeField]
        private Transform _characterPositionSpawn;

        #endregion


        public override void Ctor()
        {
            base.Ctor();

            CharacterSpawn = GetPositionCharacter;
        }

        private Transform GetPositionCharacter()
        {
            return _characterPositionSpawn;
        }
    }
}