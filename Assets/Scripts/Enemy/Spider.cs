using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spider : Enemy
{
    [SerializeField] float deathAnimationTime;
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

    public override void Die()
    {
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        IsDead = true;
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager)
            gameManager.KillEnemy();
        //GetComponent<Collider>().enabled = false;
        GetComponentInParent<Animator>().enabled = false;
        GetComponentInParent<NavMeshAgent>().enabled = false;
        GetComponentInParent<EnemyMovement>().enabled = false;
        float time = Time.time + deathAnimationTime;
        while (time > Time.time)
        {
            transform.parent.Rotate(Vector3.left * 90 /  deathAnimationTime * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    void Attack()
    {
        if (IsDead)
            return;
        if (_nextAttack > Time.time)
            return;

        _nextAttack = Time.time + attackCooldown;
        player.ReceiveDamage(damage);
    }

    private void OnCollisionStay(Collision collision)
    {

        if (IsDead)
            return;
        if (collision.gameObject.tag != "Player")
        {
            return;
        }
        Attack();
    }


}
