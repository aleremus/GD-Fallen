using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    [Range(0, 1000)] public float mouseSensitivity = 100;

    [SerializeField] float minVerticalView = 25f;
    [SerializeField] float maxVerticalView = 85f;
    [SerializeField] Transform player;

    private float _Xrotation = 0;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        Vector2 mouse = new Vector2(
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y")) * Time.deltaTime * mouseSensitivity;
        _Xrotation -= mouse.y;
        _Xrotation = Mathf.Clamp(_Xrotation, -maxVerticalView, minVerticalView);

        transform.localRotation = Quaternion.Euler(_Xrotation, 0, 0);

        player.Rotate(mouse.x * Vector3.up);

    }
}
