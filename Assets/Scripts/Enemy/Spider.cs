using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{

    [SerializeField] float attackCooldown;
    [SerializeField] int maxHP;
    [SerializeField] int damage;
    Player player;
    float _nextAttack;
    

    private void Awake()
    {
        MaxHP = maxHP;
        CurrentHp = MaxHP;
        _nextAttack = Time.time;
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Attack()
    {
        if (_nextAttack > Time.time)
            return;

        _nextAttack = Time.time + attackCooldown;
        player.ReceiveDamage(damage);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            return;
        }
        Attack();
    }


}
