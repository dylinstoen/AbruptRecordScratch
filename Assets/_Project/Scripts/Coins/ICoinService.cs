using System;
using UnityEngine;

public interface ICoinService {
    public void AddCoins(int value);

    public int GetCoins();
    public void ResetCoins();

    event Action<int> CoinsChanged;
}
