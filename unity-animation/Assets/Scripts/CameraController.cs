using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeedH = 1;
    public float rotationSpeedV = 1;
    public Transform player;
    public Vector3 offset;

    private float currentRotationV;
    private float currentRotationH;

    void Start()
    {
        currentRotationH = 180;
        currentRotationV = 10;
    }

    void Update()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float horizontal = Input.GetAxis("Mouse X") * rotationSpeedH;
        float verticalMultiplier = SettingsManager.instance.isInverted ? -1 : 1;
        float vertical = Input.GetAxis("Mouse Y") * rotationSpeedV * verticalMultiplier;

        currentRotationH += horizontal;
        currentRotationV -= vertical;
        currentRotationV = Mathf.Clamp(currentRotationV, -30f, 30f);

        Quaternion rotation = Quaternion.Euler(currentRotationV, currentRotationH, 0);
        transform.position = player.position - (rotation * offset);
        transform.LookAt(player);
    }
}
