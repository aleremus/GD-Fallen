using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Entity : MonoBehaviour
{
    // Start is called before the first frame update

    public bool IsDead;
    public int CurrentHp { get; set; }

    [SerializeField] public int MaxHP { get; set; }
    public Action OnDamageReceive { get; set; }

    public Action OnDeath { get; set; }

    virtual public void DealDamage(Entity reciever, int damage) { }

    virtual public void ReceiveDamage(int damage) { }
    virtual public void Die() { }

}
