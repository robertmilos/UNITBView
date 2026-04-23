using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 500f;
    public Transform playerCamera;
    public float interactRange = 3f; 
    private CharacterController controller;
    private float xRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // --- Movement & Look ---
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.SimpleMove(move * speed);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // --- The Interaction Logic ---
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            Ray ray = new Ray(playerCamera.position, playerCamera.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactRange))
            {
                // 1. Try to find a Door component
                Door door = hit.collider.GetComponentInParent<Door>();
                if (door != null)
                {
                    door.ToggleDoor();
                    return; // Stop looking if we found a door
                }

                // 2. Try to find a Window component
                Window window = hit.collider.GetComponentInParent<Window>();
                if (window != null)
                {
                    window.ToggleWindow();
                    return; // Stop looking if we found a window
                }

                Window1 window1 = hit.collider.GetComponentInParent<Window1>();
                if (window1 != null) { window1.ToggleWindow(); return; }
                
                // Optional: Useful for debugging why something isn't opening
                Debug.Log("Hit " + hit.collider.name + " but no Door/Window script found.");
            }
        }
    }
}