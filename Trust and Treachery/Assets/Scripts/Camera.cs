using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform player; // GameObject yerine Transform kullandık, çünkü kamerayı doğrudan karakterin içinde konumlandıracağız.
    [SerializeField]
    [Range(0.5f, 2f)]
    float mouseSense = 1f;
    [SerializeField]
    [Range(-90f, 90f)]
    float lookUp = -90f;  // First Person için, yukarı ve aşağı bakış açısını genişlettik.
    [SerializeField]
    [Range(-90f, 90f)]
    float lookDown = 90f;

    float cameraVerticalRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float rotateX = Input.GetAxis("Mouse X") * mouseSense;
        float rotateY = Input.GetAxis("Mouse Y") * mouseSense;

        cameraVerticalRotation -= rotateY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, lookUp, lookDown);

        transform.localRotation = Quaternion.Euler(cameraVerticalRotation, 0f, 0f);
        player.Rotate(Vector3.up * rotateX);
    }
}