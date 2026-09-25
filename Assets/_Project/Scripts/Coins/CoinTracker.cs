using System;
using UnityEngine;

public class CoinTracker : MonoBehaviour, ICoinService
{
    private int coins;
    public event Action<int> CoinsChanged;

    public void ResetCoins() {
        coins = 0;
        CoinsChanged?.Invoke(coins);
    }

    public int GetCoins() { return coins; }

    public void AddCoins(int value) {
        coins += value;
        CoinsChanged?.Invoke(coins);
    }
    void Start()
    {
        ResetCoins();
    }
}
