using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;
    [SerializeField] float oscilationSpeed;
    [SerializeField] float oscilationHeight;
    [SerializeField] float rotationSpeed;
    [SerializeField] AudioSource audioSource;


    float _startY;
    bool _goingUp;


    void Start()
    {
        _startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Oscilate();
        Rotate();
    }

    void Rotate()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
    void Oscilate()
    {
        if (transform.position.y < _startY)
        {
            _goingUp = true;
        }
        else if (transform.position.y > _startY + oscilationHeight)
        {
            _goingUp = false;
        }

        float speed = curve.Evaluate((transform.position.y - _startY) / oscilationHeight) * oscilationSpeed * Time.deltaTime;
        if (_goingUp)
        {
            transform.Translate(Vector3.up * speed);
        }
        else
        {
            transform.Translate(Vector3.down * speed);
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag != "Player")
        {
            return;
        }
        audioSource.Play();

        FindObjectOfType<GameManager>().CollectCoin();
        GameObject.Destroy(gameObject);
    }
}
