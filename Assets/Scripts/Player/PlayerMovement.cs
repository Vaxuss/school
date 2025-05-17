using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] float _moveSpeed = 4500;
    [SerializeField] float _maxSpeed = 20;

    [SerializeField] float _counterMovement = 0.175f;
    private float _threshold = 0.01f;

    float x, y;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.down * Time.deltaTime * 10);

        Vector2 mag = FindVelRelativeToLook();
        float xMag = mag.x, yMag = mag.y;

        CounterMovement(x, y, mag);

        float maxSpeed = this._maxSpeed;

        if (x > 0 && xMag > maxSpeed) x = 0;
        if (x < 0 && xMag < -maxSpeed) x = 0;
        if (y > 0 && yMag > maxSpeed) y = 0;
        if (y < 0 && yMag < -maxSpeed) y = 0;

        float multiplier = 1f, multiplierV = 1f;

        rb.AddForce(transform.forward * y * _moveSpeed * Time.deltaTime * multiplier * multiplierV);
        rb.AddForce(transform.right * x * _moveSpeed * Time.deltaTime * multiplier);
    }

    private void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
    }

    private void CounterMovement(float x, float y, Vector2 mag)
    {
        if (Mathf.Abs(mag.x) > _threshold && Mathf.Abs(x) < 0.05f || (mag.x < _threshold && x > 0) || (mag.x > _threshold && x < 0))
        {
            rb.AddForce(_moveSpeed * transform.right * Time.deltaTime * -mag.x * _counterMovement);
        }
        if (Mathf.Abs(mag.y) > _threshold && Mathf.Abs(y) < 0.05f || (mag.y < _threshold && y > 0) || (mag.y > _threshold && y < 0))
        {
            rb.AddForce(_moveSpeed * transform.forward * Time.deltaTime * -mag.y * _counterMovement);
        }

        if (Mathf.Sqrt((Mathf.Pow(rb.linearVelocity.x, 2) + Mathf.Pow(rb.linearVelocity.z, 2))) > _maxSpeed)
        {
            float fallspeed = rb.linearVelocity.y;
            Vector3 n = rb.linearVelocity.normalized * _maxSpeed;
            rb.linearVelocity = new Vector3(n.x, fallspeed, n.z);
        }
    }

    public Vector2 FindVelRelativeToLook()
    {
        float lookAngle = transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.linearVelocity.x, rb.linearVelocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitue = rb.linearVelocity.magnitude;
        float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xMag, yMag);
    }
}
