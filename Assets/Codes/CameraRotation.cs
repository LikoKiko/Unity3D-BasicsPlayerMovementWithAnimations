using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 150f;
    [SerializeField] private Transform playerBody;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        playerBody.Rotate(Vector3.up, mouseX);
    }
}
