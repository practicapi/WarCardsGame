using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class CardView : MonoBehaviour
{
    private const float HalfCircleRotation = 180f;
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");

    [Header("Jump")]
    [SerializeField] private float _jumpPower = 1f;
    [SerializeField] private float _jumpDuration = 1f;
    [SerializeField] private RotateMode _jumpRotationMode = RotateMode.Fast;
    
    [Header("Move Turned Down")]
    [SerializeField] private float _moveTurnedDownDuration = 1f;

    [SerializeField] private List<ColorToTexture> _colorToTextureList;
    [SerializeField] private MeshRenderer _renderer;
    private Material _material;
    public string ID { get; private set; }
    
    public void Setup(string id)
    {
        ID = id;
        _material = _renderer.material;
    }

    public async UniTask TurnFaceUp()
    {
        
    }

    public void SetColor(DeckColor color)
    {
        _material.SetTexture(MainTex, _colorToTextureList.Find(x => x.DeckColor == color).Texture2D);
    }
    
    public async UniTask JumpToPoint(Vector3 destinationPoint, bool shouldFaceUp = true)
    {
        var tasks = new List<UniTask>();

        if (shouldFaceUp)
        {
            var halfACircleAngles = new Vector3(HalfCircleRotation, 0, 0);
            var endRotation = halfACircleAngles + transform.localRotation.eulerAngles;
            var tweenLocalRotation = transform.DOLocalRotate(endRotation, _jumpDuration).ToUniTask();
            
            tasks.Add(tweenLocalRotation);
        }

        var startingPosition = transform.position;
        var pivotPoint = VectorUtils.GetMiddlePoint(startingPosition, destinationPoint);
        var currentDegrees = 0f;
        var localLeft = -transform.right;
        var pivotToStartingPositionVector = startingPosition - pivotPoint;
        var xOffset = (destinationPoint - startingPosition).x;
        var tweenPosition = DOTween.To(
            () => currentDegrees,
            x =>
            {
                currentDegrees = x;
                var vectorXOffset = xOffset * (currentDegrees / HalfCircleRotation) * Vector3.right;
                transform.position = vectorXOffset+VectorUtils.RotateVectorAroundAxis(pivotToStartingPositionVector, localLeft, currentDegrees) + pivotPoint;
            },
            HalfCircleRotation
            , _jumpDuration).ToUniTask();
        tasks.Add(tweenPosition);
        await UniTask.WhenAll(tasks);
    }

    public async UniTask MoveToPointFacedDown(Vector3 destinationPoint)
    {
        var rotationEulerAngles = transform.rotation.eulerAngles;
        await transform.DOLocalRotate(new Vector3(rotationEulerAngles.x, rotationEulerAngles.y, HalfCircleRotation), _moveTurnedDownDuration * 0.25f);
        await transform.DOMove(destinationPoint, _moveTurnedDownDuration);
    }
}

[Serializable]
public class ColorToTexture
{
    public DeckColor DeckColor;
    public Texture2D Texture2D;
}
