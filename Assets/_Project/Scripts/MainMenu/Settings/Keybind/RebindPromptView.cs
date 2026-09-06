using System;
using TMPro;
using UnityEngine;

public class RebindPromptView : MonoBehaviour {
    [SerializeField] private TMP_Text _countdownText;
    private int _lastDisplayedSecond = -1;

    public void Show(float timeoutSeconds) {
        gameObject.SetActive(true);

        SetTime(timeoutSeconds);
    }

    public void SetTime(float secondsRemaining) {
        int seconds = Mathf.CeilToInt(secondsRemaining);

        if (seconds == _lastDisplayedSecond)
            return;

        _lastDisplayedSecond = seconds;
        _countdownText.text = seconds.ToString();
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
