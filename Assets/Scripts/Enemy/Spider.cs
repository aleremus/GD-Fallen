using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{


    [SerializeField] int maxHP;

    private void Awake()
    {
        MaxHP = maxHP;
        CurrentHp = MaxHP;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Attack()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

        }
    }
}
