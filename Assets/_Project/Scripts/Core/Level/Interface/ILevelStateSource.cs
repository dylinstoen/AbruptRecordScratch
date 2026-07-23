using _Project.Scripts.Gameplay.Enums;
using System;
using UnityEngine;

namespace _Project.Scripts.Core.Level.Interface
{
    public interface ILevelStateSource
    {
        public LevelState CurrentState { get; }
        public event Action LevelCompleted;
        event Action<LevelState> StateChanged;
    }
}
