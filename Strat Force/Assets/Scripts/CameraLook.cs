using UnityEngine;

public class FreeLookOrbit : MonoBehaviour
{
    public Transform target;                
    public Vector3 offset = new Vector3(0, 2, -6); 
    public float rotationSpeed = 5f;
    public float minYAngle = 10f;
    public float maxYAngle = 80f;
    public float distance = 6f;

    private float currentYaw = 0f;
    private float currentPitch = 20f;

    void LateUpdate()
    {
        if (!target) return;

        // Handle mouse input for orbiting
        if (Input.GetMouseButton(1)) // Right mouse button
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            currentYaw += mouseX;
            currentPitch -= mouseY;
            currentPitch = Mathf.Clamp(currentPitch, minYAngle, maxYAngle);
        }

        // Convert angles to rotation
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);

        // Set camera position based on rotation and distance
        Vector3 desiredPosition = target.position - rotation * Vector3.forward * distance;

        // Apply position and rotation
        transform.position = desiredPosition;
        transform.LookAt(target.position + Vector3.up * 1.5f); 
    }
}
