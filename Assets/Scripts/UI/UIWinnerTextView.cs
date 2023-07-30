using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIWinnerTextView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _winnerText;
    [SerializeField] private float _showAnimationDuration = 1f;

    public void SetupView(string text, Color color)
    {
        _winnerText.text = text;
        _winnerText.color = color;
    }

    public async UniTask Show()
    {
        gameObject.SetActive(true);
        Debug.Log("Show!");
        _winnerText.transform.localScale = Vector3.zero;
        await _winnerText.transform.DOScale(Vector3.one, _showAnimationDuration).SetEase(Ease.OutElastic);
    }
}
