using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlaneController : MonoBehaviour
{
    [Header("Plane Stats")]
    [Tooltip("How much the throttle ramps up or down.")]
    public float throttleIncrement = 0.1f;
    [Tooltip("Maximum engine thrust when at 100% throttle.")]
    public float maxThrust = 200f;
    [Tooltip("How responsive the plane is when rolling, pitching, and yawing.")]
    public float responsiveness = 100f;
    [Tooltip("How much lift force this plane generates as it gains speed.")]
    public float lift = 135f;

    public Klareh.ZeroController controllerAnim;

    private float throttle;
    private float roll;
    private float pitch;
    private float yaw;
    private float responseModifier
    {
        get
        {
            return (rb.mass / 10f) * responsiveness;
        }

    }

    Rigidbody rb;
    [SerializeField] TextMeshProUGUI hud;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controllerAnim = GetComponent<Klareh.ZeroController>();
    }

    private void HandleInputs()
    {
        // Set rotational values from our axis inputs.
        roll = Input.GetAxis("Horizontal");
        pitch = Input.GetAxis("Vertical");
        yaw = Input.GetAxis("Yaw");

        // Handle throttle value being sure to clamp it between 0 and 100.
        if (Input.GetKey(KeyCode.I)) throttle += throttleIncrement;
        else if (Input.GetKey(KeyCode.O)) throttle -= throttleIncrement;
        throttle = Mathf.Clamp(throttle, 0, 100f);
    }

    private void Update()
    {
        if (throttle >= 20)
            controllerAnim.Engine = true;
        else
            controllerAnim.Engine = false;

        if (transform.position.y > 50)
            controllerAnim.Wheels = false;
        else
            controllerAnim.Wheels = true;

        HandleInputs();
        UpdateHUD();
    }

    private void FixedUpdate()
    {
        // Apply forces to our planes
        rb.AddForce(transform.forward * maxThrust * throttle);
        rb.AddTorque(transform.up * yaw * responseModifier);
        rb.AddTorque(transform.right * pitch * responseModifier);
        rb.AddTorque(-transform.forward * roll * responseModifier);
        rb.AddForce(Vector3.up * rb.velocity.magnitude * lift);
    }

    private void UpdateHUD()
    {
        hud.text = "Throttle " + throttle.ToString("F0") + "%\n";
        hud.text += "Airspeed: " + (rb.velocity.magnitude * 3.6f).ToString("F0") + "km/h\n";
        hud.text += "Altitude: " + transform.position.y.ToString("F0") + " m";
    }
}
