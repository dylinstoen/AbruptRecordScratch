using System;
using UnityEngine;
using FPS.Input;
namespace FPS.WeaponSystem {
    /// <summary>
    /// Wire Trigger and Emit modules, passing the input info into its respective modules 
    /// </summary>
    public class Weapon : MonoBehaviour {
        [Header("Magazine")]
        public Magazine magazine;
        [Header("Fire")] 
        public Trigger trigger;
        public Emitter emitter;
        public Impact impact;
    }  
}

