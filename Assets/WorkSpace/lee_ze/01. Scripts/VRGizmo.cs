using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRGizmo : MonoBehaviour
{
    public Color gizmoColor = Color.green;

    public float gizmoRadius = 0.1f;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        Vector3 tempPos = transform.position;

        tempPos.y = tempPos.y + 1.35f;

        Gizmos.DrawSphere(tempPos, gizmoRadius);
    }
}
