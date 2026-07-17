using TMPro;
using UnityEngine;

public class CoinHud : MonoBehaviour
{
    private ICoinService _coinService;

    [SerializeField] private TMP_Text coinText; 
    
    public void Initalize(ICoinService coinService) {
        _coinService = coinService;
        _coinService.CoinsChanged += UpdateDisplay;
    }

    public void UpdateDisplay(int coinCount) {
        coinText.text = "Coins: " + coinCount.ToString("D8");
    }
}
