
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;
    public float xSensivity = 30f;
    public float ySensivity = 30f;
    // Start is called before the first frame update

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mousey = input.y;
        xRotation-= (mousey*Time.deltaTime)* ySensivity;
        xRotation = Mathf.Clamp(xRotation,-80,80f);

        cam.transform.localRotation= Quaternion.Euler(xRotation,0,0);
        transform.Rotate(Vector3.up *(mouseX*Time.deltaTime)*xSensivity);

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
