using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 1;
    public Transform player;
    public Vector3 offset;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Update is called once per frame
    void LateUpdate()
    {
        float horizontal = Input.GetAxis("Mouse X") * rotationSpeed;
        player.Rotate(0, horizontal, 0);

        float desiredYAngle = player.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredYAngle, 0);
        transform.position = player.position - (rotation * offset);

        transform.LookAt(player);
   }
}
