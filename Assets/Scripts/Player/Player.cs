using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] Weapon weapon;
    [SerializeField] int maxHP;

    private void Awake()
    {
        MaxHP = maxHP;
        CurrentHp = MaxHP;

    }
    override public void DealDamage(Entity reciever, int damage)
    {
        
    }

    override public void ReceiveDamage(int damage)
    {
        CurrentHp -= damage;
        if (damage > 0)
        {
        }
        else if (damage < 0)
        {
            
        }
        CurrentHp = Mathf.Clamp(CurrentHp, 0, maxHP);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
            weapon.Fire();
        if (Input.GetKeyDown(KeyCode.E))
            ReceiveDamage(1);
        if (Input.GetKeyDown(KeyCode.Q))
            ReceiveDamage(-1);
    }
}
