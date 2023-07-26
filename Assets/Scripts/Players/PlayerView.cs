using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [field: SerializeField] public Transform DeckParentTransform { get; private set; }
    [field: SerializeField] public Transform PileParentTransform { get; private set; }
    [field: SerializeField] public Transform CardsLeftTextParentTransform { get; private set; }
}
