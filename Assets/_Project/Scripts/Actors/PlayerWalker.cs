using System;
using _Project.Scripts.Audio.Interfaces;
using _Project.Scripts.Gameplay;
using KinematicCharacterController;
using UnityEngine;

namespace _Project.Scripts.Actors {
    public class PlayerWalker : MonoBehaviour {
        private IImpactService _impactService;
        [SerializeField] private KinematicCharacterMotor kinematicCharacterMotor;
        
        public void Initialize(IImpactService impactService) {
            _impactService = impactService;
        }
        // TODO: Walking takes in input and kinematic controller
        // TODO: if the input is > 0 and the velocity > 0 and the character is grounded.
        //          Then, they are walking, generate walk effect
        //          Else if, Check if there sliding (vel > 0), generate slide effect
        //          Else, return
        private void Update() {
            
        }
    }
}