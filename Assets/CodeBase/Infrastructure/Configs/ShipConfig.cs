using UnityEngine;

namespace CodeBase.Infrastructure.Configs
{
    [CreateAssetMenu(menuName = "Configs/Ship", fileName = "ShipConfig")]
    public class ShipConfig : ScriptableObject
    {
        [Range(0.1f, 4f)] public float ThrustingForce;
        [Range(0.1f, 1f)] public float TurnSpeed;
    }
}