using _Project.Scripts.Utilities;
using UnityEngine;

namespace _Project.Scripts.Combat.BaseEnemy {
    public interface IDetectionStrategy {
        bool Execute(Transform player, Transform detector, CountdownTimer timer);
    }
}