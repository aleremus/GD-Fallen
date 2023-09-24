using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cep : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float angle;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3( Mathf.Sin(Time.time * speed) * angle, transform.eulerAngles.y, 0);
    }
}
