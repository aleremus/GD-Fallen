using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAddition : MonoBehaviour
{
    public Vector4 MinMaxXYOffset;   
    public Vector4 MaxMinYZRotationOffset;
    public GameObject Target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(Mathf.Clamp(Target.transform.position.x, Target.transform.position.x + MinMaxXYOffset.w, Target.transform.position.x + MinMaxXYOffset.x) , gameObject.transform.position.y, Mathf.Clamp(Target.transform.position.z, Target.transform.position.z + MinMaxXYOffset.y, Target.transform.position.z + MinMaxXYOffset.z));
       // gameObject.transform.rotation = new Quaternion();
    }
}
