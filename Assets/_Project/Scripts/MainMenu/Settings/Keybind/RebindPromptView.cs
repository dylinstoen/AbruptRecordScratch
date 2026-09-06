using System;
using TMPro;
using UnityEngine;

public class RebindPromptView : MonoBehaviour {
    [SerializeField] private TMP_Text _countdownText;

    public void Show(float timeoutSeconds) {
        gameObject.SetActive(true);

        SetTime(timeoutSeconds);
    }

    public void SetTime(float secondsRemaining) {
        _countdownText.text = Mathf.CeilToInt(secondsRemaining).ToString();
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
