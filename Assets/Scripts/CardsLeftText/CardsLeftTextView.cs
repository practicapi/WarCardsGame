using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CardsLeftTextView : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;
    
    public void SetupView(Color color)
    {
        Debug.Log("SetupView: "+ color);
        _text.color = color;
    }
    
    public void SetNumber(int number)
    {
        _text.text = number.ToString();
    }
}
