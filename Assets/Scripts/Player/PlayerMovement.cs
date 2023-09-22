using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 1; 
    [SerializeField] float jumpForce = 1; 
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    void Move()
    {
        Vector3 direction = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")) * speed;

        direction = Vector3.ClampMagnitude(direction, speed);

        direction = transform.TransformDirection(direction);

        direction.y = rigidbody.velocity.y;
        rigidbody.velocity = direction;
    }

    void Jump()
    {
        rigidbody.velocity = rigidbody.velocity + Vector3.up * jumpForce;
    }
}
