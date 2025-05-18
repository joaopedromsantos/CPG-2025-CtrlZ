using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // jogador
    public Vector3 offset;   // ajuste da posição da câmera
    public float smoothSpeed = 0.125f; // suavidade do movimento

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
