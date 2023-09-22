using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Entity : MonoBehaviour
{
    // Start is called before the first frame update

    public int CurrentHp { get; set; }

    [SerializeField] public int MaxHP { get; set; }
    public Action OnDamageReceive { get; set; }

    public Action OnDeath { get; set; }

    abstract public void DealDamage(Entity reciever, int damage);

    abstract public void ReceiveDamage(int damage);

}
