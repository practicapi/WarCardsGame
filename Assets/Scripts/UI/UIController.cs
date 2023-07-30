using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController
{
    private const string WinnerTextFormat = "{0} Wins!"; 
    
    private UICreator _uiCreator;
    private UICanvasView _uiCanvasView;
    
    public UIController()
    {
        _uiCreator = new UICreator();
    }

    public void CreateUI()
    {
        _uiCanvasView = _uiCreator.CreateUI();
        _uiCanvasView.Setup(OnRestartButtonClicked);
    }

    private void OnRestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public async UniTask ShowWinnerText(GameWinner gameWinner, Color textColor)
    {
        var text = string.Format(WinnerTextFormat, gameWinner.ToString());
        await _uiCanvasView.ShowWinnerText(text, textColor);
    }
    
    public void ShowRestartButton()
    {
        _uiCanvasView.ShowRestartButton();
    }
}
