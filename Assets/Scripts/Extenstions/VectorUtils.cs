using UnityEngine;

public static class VectorUtils
{
    public static Vector3 GetMiddlePoint(Vector3 point1, Vector3 point2)
    {
        return (point2 - point1) * 0.5f + point1;
    }
    
    public static Vector3 RotateVectorAroundAxis(Vector3 vector, Vector3 axis, float angleInDegrees)
    {
        return Quaternion.AngleAxis(angleInDegrees, axis) * vector;
    }
    
    public static Quaternion ToQuaternionAroundYAxis(this float angleInDegrees)
    {
        return Quaternion.Euler(new Vector3(0, angleInDegrees, 0));
    }
}
