using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UICanvasView : MonoBehaviour
{
    [SerializeField] private UIWinnerTextView _uiWinnerTextView;
    [SerializeField] private Button _restartButton;
    
    public async UniTask ShowWinnerText(string text, Color textColor)
    {
        _uiWinnerTextView.SetupView(text, textColor);
        await _uiWinnerTextView.Show();
    }

    public void ShowRestartButton()
    {
        _restartButton.gameObject.SetActive(true);
    }

    public void Setup(Action onRestartButtonClicked)
    {
        _restartButton.onClick.AddListener(() => onRestartButtonClicked());
    }
}
