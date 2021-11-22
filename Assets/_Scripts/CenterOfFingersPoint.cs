using System.Collections.Generic;
using UnityEngine;

public class CenterOfFingersPoint : MonoBehaviour
{
    [SerializeField] private List<Transform> _fingerPositions;

    void Update()
    {
        var t = transform;
        t.position = GetCenterOfFingers();
        t.LookAt(GetEulerAngles());
        DrawArrow.ForDebug(t.position, t.forward, 10);
    }

    private Vector3 GetEulerAngles()
    {
        Vector3 a = _fingerPositions[0].position;
        Vector3 b = _fingerPositions[1].position;
        Vector3 c = _fingerPositions[2].position;
        Vector3 side1 = b - a;
        Vector3 side2 = c - a;
        Vector3 perp = Vector3.Cross(side1, side2);

        return perp;
    }

    private Vector3 GetCenterOfFingers()
    {
        float totalX = 0f;
        float totalY = 0f;
        float totalZ = 0f;
        
        foreach(Transform p in _fingerPositions)
        {
            Vector3 position = p.transform.position;
            totalX += position.x;
            totalY += position.y;
            totalZ += position.z;
        }
        
        float centerX = totalX / _fingerPositions.Count;
        float centerY = totalY / _fingerPositions.Count;
        float centerZ = totalZ / _fingerPositions.Count;

        return new Vector3(centerX, centerY, centerZ);
    } 
}
