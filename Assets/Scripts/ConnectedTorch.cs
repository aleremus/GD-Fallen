using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectedTorch : MonoBehaviour
{
    [SerializeField] GameObject parent;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (parent == null)
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
