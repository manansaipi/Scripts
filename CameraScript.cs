using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class CameraScript : MonoBehaviour
{
    public float mouseSensivity = 100f;
    public Transform player;

    float xRotation;

    // [SerializeField] private float cameraSensivity = 100f;

 
    private void Start() {
    }
    private void Update() {
        float mouseX = 0;
        float mouseY = 0;

        if(Touchscreen.current.touches.Count > 0 && Touchscreen.current.touches[0].isInProgress) {
            mouseX = Touchscreen.current.touches[0].delta.ReadValue().x;
            mouseY = Touchscreen.current.touches[0].delta.ReadValue().y;    
        }   
        mouseX *= mouseSensivity;
        mouseY *= mouseSensivity;
        
        xRotation -= mouseY * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -80, 50);

        transform.localRotation = Quaternion.Euler(xRotation,0,0);

        player.Rotate(Vector3.up * mouseX * Time.deltaTime);
    }
}
