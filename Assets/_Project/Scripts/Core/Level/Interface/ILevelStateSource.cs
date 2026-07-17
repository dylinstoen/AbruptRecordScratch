using _Project.Scripts.Gameplay.Enums;
using System;
using UnityEngine;

namespace _Project.Scripts.Core.Level.Interface
{
    public interface ILevelStateSource
    {
        LevelState State { get; }
        
        public event Action LevelCompleted;
        bool IsGameplayActive { get; }
        event Action<LevelState> StateChanged;
    }
}
