using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
[ExecuteInEditMode]
public class CardView : MonoBehaviour
{
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
        RotateToPoint(transform.position + Vector3.left*jumpDistance + Vector3.forward*jumpDistance);
    }

    public async UniTask RotateToPoint(Vector3 destinationPoint)
    {
        var tasks = new List<UniTask>();
        var startingPosition = transform.position;
        var currentRotation = Quaternion.Euler(0, 0, 0);
        var halfACircleAngles = new Vector3(180, 0, 0);
        var endRotation = halfACircleAngles + transform.rotation.eulerAngles;

        // tween local rotation
        tasks.Add(transform.DOLocalRotate(endRotation, _jumpDuration).ToUniTask());
        
        // pivotPoint = middle point between destination and starting position
        var pivotPoint = (destinationPoint - startingPosition) * 0.5f + startingPosition;
        
        // tween position
        tasks.Add(DOTween.To(() => currentRotation, (x) =>
        {
            currentRotation = x;
            transform.position = currentRotation * (startingPosition - pivotPoint) + pivotPoint;
        }, halfACircleAngles, _jumpDuration).ToUniTask());
        
        // wait to all
        await UniTask.WhenAll(tasks);
    }

    // static Quaternion GetRotateAroundDelta (Quaternion startingRotation, Vector3 startingPosition, Vector3 pivotPoint, Quaternion byRotation, out Vector3 newPosition)
    // {
    //     newPosition = byRotation * (startingPosition - pivotPoint) + pivotPoint;
    //     return byRotation * startingRotation;
    // }
}
