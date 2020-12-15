using Unit.Player;
using UnityEngine;

namespace Interface
{
    public interface IFightCamera
    {
        Vector3 OffsetTopPosition();
        Vector3 OffsetThirdPosition();
        Transform TopTarget { get; set; }
        Transform ThirdTarget { get; set; }
    }
}