using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
[ExecuteInEditMode]
public class CardView : MonoBehaviour
{
    private const float JumpAnimationRotation = 180f; 
    
    [SerializeField] private float _jumpPower = 1f;
    [SerializeField] private float _jumpDuration = 1f;
    [SerializeField] private RotateMode _jumpRotationMode = RotateMode.Fast;
    [SerializeField] private float jumpDistance = 0.1f;
    public int ID { get; private set; }
    
    public void Setup(int id)
    {
        ID = id;
    }

    [ContextMenu("Jump")]
    public void JumpToPoint()
    {
        JumpToPoint(transform.position - transform.right*jumpDistance + transform.forward*jumpDistance);
    }

    public async UniTask JumpToPoint(Vector3 destinationPoint)
    {
        var halfACircleAngles = new Vector3(179, 0, 0);
        var endRotation = halfACircleAngles + transform.localRotation.eulerAngles;
        var tasks = new List<UniTask>();
        var tweenLocalRotation = transform.DOLocalRotate(endRotation, _jumpDuration).ToUniTask();
        
        tasks.Add(tweenLocalRotation);
        
        var startingPosition = transform.position;
        var pivotPoint = VectorUtils.GetMiddlePoint(startingPosition, destinationPoint);
        var currentDegrees = 0f;
        var localLeft = -transform.right;
        var pivotToStartingPositionVector = startingPosition - pivotPoint;

        var tweenPosition = DOTween.To(
            () => currentDegrees,
            x =>
            {
                currentDegrees = x;
                transform.position = VectorUtils.RotateVectorAroundAxis(pivotToStartingPositionVector, localLeft, currentDegrees) + pivotPoint;
            },
            JumpAnimationRotation
            , _jumpDuration).ToUniTask();

        tasks.Add(tweenPosition);
        await UniTask.WhenAll(tasks);
    }
}
