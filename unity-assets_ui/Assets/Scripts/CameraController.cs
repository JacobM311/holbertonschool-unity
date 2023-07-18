using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeedH = 1;
    public float rotationSpeedV = 1;
    public Transform player;
    public Vector3 offset;

    private float currentRotationV = 0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float horizontal = Input.GetAxis("Mouse X") * rotationSpeedH;
        float vertical = Input.GetAxis("Mouse Y") * rotationSpeedV;

        player.Rotate(0, horizontal, 0);
        currentRotationV -= vertical;
        currentRotationV = Mathf.Clamp(currentRotationV, -30f, 30f);

        float desiredYAngle = player.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(currentRotationV, desiredYAngle, 0);
        transform.position = player.position - (rotation * offset);

        transform.LookAt(player);
    }
}
