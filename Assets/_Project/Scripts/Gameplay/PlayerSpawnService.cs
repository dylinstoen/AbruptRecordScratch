using System;
using _Project.Scripts.Actors;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public sealed class PlayerSpawnService : MonoBehaviour {
        [SerializeField] private GameObject playerPrefab;

        public IPlayerFacade Spawn(Vector3 position, Quaternion rotation) {
            var playerGo = Instantiate(playerPrefab, position, rotation);
            return playerGo.GetComponent<PlayerFacade>();
        }

    }
}

