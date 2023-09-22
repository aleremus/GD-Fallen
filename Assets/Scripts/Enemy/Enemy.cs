using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
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
        
    }
}
