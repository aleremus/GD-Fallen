using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float maximalSpeed;
    [SerializeField, Range(0,1)] private float acceleration;
    private Rigidbody _rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Jump"),Input.GetAxis("Vertical"));
        Move(direction.normalized, maximalSpeed);
    }
    public void Move(Vector3 direction, float maximalSpeed)
    {

        if (_rigidBody.velocity.magnitude <= maximalSpeed)
        {
            _rigidBody.velocity += direction * acceleration;
        }
    }

}
