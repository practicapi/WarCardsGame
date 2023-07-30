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
    [SerializeField] private float _jumpDuration = 1f;
    
    [Header("Move Turned Down")]
    [SerializeField] private float _moveTurnedDownDuration = 1f;
    [SerializeField] private float _turnDownJumpPower = 1f;
    [SerializeField] private MeshRenderer _renderer;
    private Material _material;
    private Transform _transform;
    public string ID { get; private set; }
    
    public void Setup(string id)
    {
        ID = id;
        _material = _renderer.material;
        _transform = transform;
    }

    public void SetBackFaceTexture(Texture2D cardBackFaceTexture)
    {
        _material.SetTexture(MainTex, cardBackFaceTexture);
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

    public void FaceUp()
    {
        // var localLeft = -transform.right;
        //
        // var a = VectorUtils.RotateVectorAroundAxis(pivotToStartingPositionVector, localLeft, 180);
        // var halfACircleAngles = new Vector3(HalfCircleRotation, 0, 0);
        // var endRotation = halfACircleAngles + transform.localRotation.eulerAngles;
        
        
        //_transform.localRotation = 180f.ToQuaternionAroundYAxis();
        
        _transform.localRotation = Quaternion.Euler(0,HalfCircleRotation,0);
    }
    
    public async UniTask MoveToPointFacedDown(Vector3 destinationPoint)
    {
        var rotationEulerAngles = _transform.localRotation.eulerAngles;
        transform.DOLocalJump(_transform.localPosition, _turnDownJumpPower, 1, _moveTurnedDownDuration);
        await transform.DOLocalRotate(new Vector3(rotationEulerAngles.x, rotationEulerAngles.y, HalfCircleRotation), _moveTurnedDownDuration);
        await transform.DOMove(destinationPoint, _moveTurnedDownDuration);
    }
}

[Serializable]
public class ColorToTexture
{
    public DeckColor DeckColor;
    public Texture2D Texture2D;
}
