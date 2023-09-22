using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] private float _destroyTime;
    [SerializeField] private float _power;
    // Start is called before the first frame update
    private Rigidbody _rigidbody;
    private Transform _player;
    private float initialTime;
    void Start()
    {
        _player = GameObject.Find("Player").gameObject.transform;
        for (int i = 0; i < transform.childCount; i++)
        {
            _rigidbody = gameObject.transform.GetChild(0).GetComponent<Rigidbody>();
            _rigidbody.AddForce(_player.position - transform.position.normalized * -_power, ForceMode.Impulse);
        }
        initialTime = Time.realtimeSinceStartup;

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup - initialTime >= _destroyTime) Destroy(gameObject);
    }
}
