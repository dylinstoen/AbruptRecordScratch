using _Project.Scripts.Core.Level.Interface;
using _Project.Scripts.Gameplay;
using TMPro;
using UnityEngine;

public class LevelCompletedScreen : MonoBehaviour
{
    [SerializeField] private GameObject levelCompletedScreen;
    [SerializeField] private TMP_Text coinText;
    private ILevelStateSource _levelController;
    private ICoinService _coinService;

    private void Start() {
        levelCompletedScreen.SetActive(false);
    }

    private void OnLevelCompleted() {
        levelCompletedScreen.SetActive(true);
        coinText.text = "You Scored: " + _coinService.GetCoins().ToString() + " Points";
    }
    public void Initialize(ILevelStateSource levelController, ICoinService coinService) {
        _levelController = levelController;
        _coinService = coinService;
        levelController.LevelCompleted += OnLevelCompleted;
    }
    private void OnDisable() {
        _levelController.LevelCompleted -= OnLevelCompleted;
    }

}
