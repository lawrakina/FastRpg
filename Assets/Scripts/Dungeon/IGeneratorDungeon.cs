using UnityEngine;

namespace Dungeon
{
    public interface IGeneratorDungeon
    {
        void      BuildDungeon();
        void      DestroyDungeon();
        Transform GetPlayerPosition();
        void      SetRandomSeed();
    }
}