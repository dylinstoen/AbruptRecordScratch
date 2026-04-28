using _Project.Scripts.Actors;
using KBCore.Refs;
using System;
using UnityEngine;

public class EnemyHealthHandler : MonoBehaviour
{
    [SerializeField, Self] private Health health;
    private void Awake() {
        health.InternalDied += Death;
        health.HealthChanged += HealthChanged;
    }

    private void HealthChanged(int arg1, int arg2) {
        Debug.Log(arg1 + " " + arg2);
    }

    public void Death() {
        Destroy(gameObject);
    }
    private void OnDisable() {
        health.InternalDied -= Death;
        health.HealthChanged -= HealthChanged;
    }
    private void OnDestroy() {
        health.InternalDied -= Death;
        health.HealthChanged -= HealthChanged;
    }
}
