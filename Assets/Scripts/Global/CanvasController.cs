using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject _restartScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RestartShow()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _restartScreen.SetActive(true);
    }
}
