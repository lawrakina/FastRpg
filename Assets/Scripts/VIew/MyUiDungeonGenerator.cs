using System;
using DungeonArchitect;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MyUiDungeonGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _generator;
    [SerializeField] private InputField _seedNumber;

    public void GenerateRandomSeed()
    {
        _seedNumber.text = Random.Range(0, Int32.MaxValue).ToString();
    }

    public void SetSeed()
    {
        var dungeon = _generator.GetComponent<DungeonConfig>();
        var seed = Int32.Parse(_seedNumber.text);
        dungeon.Seed = (uint) seed;
    }
}
