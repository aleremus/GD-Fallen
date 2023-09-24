using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
public class Spider : Enemy
{
    [SerializeField] float deathAnimationTime;
    [SerializeField] float attackCooldown;
    [SerializeField] int maxHP;
    [SerializeField] int damage;
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] List<AudioClip> DieSounds;
    [SerializeField] List<AudioClip> RecieveDamageSounds;
    [SerializeField] List<AudioClip> AttackSounds;

    AudioSource audioSource;
    Rigidbody rigidbody;
    Player player;
    float _nextAttack;
    


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        MaxHP = maxHP;
        CurrentHp = MaxHP;
        _nextAttack = Time.time;
    }

    public override void ReceiveDamage(int damage)
    {
        if (IsDead)
            return;

        particleSystem.Play();
        audioSource.PlayOneShot(RecieveDamageSounds[Random.Range(0, RecieveDamageSounds.Count)]);

        base.ReceiveDamage(damage);
        
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
        audioSource.PlayOneShot(DieSounds[Random.Range(0, DieSounds.Count)]);
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<MeshCollider>().enabled = true;
        rigidbody.constraints = RigidbodyConstraints.None;
        GetComponentInParent<Animator>().enabled = false;
        GetComponentInParent<NavMeshAgent>().enabled = false;
        GetComponentInParent<EnemyMovement>().enabled = false;
        
        yield return new WaitForSeconds(0.2f);
        float time = Time.time + deathAnimationTime;
        rigidbody.AddForceAtPosition(transform.TransformDirection(Vector3.up * 100), Vector3.up + transform.position);
    }

    void Attack()
    {
        if (IsDead)
            return;
        if (_nextAttack > Time.time)
            return;
        audioSource.PlayOneShot(AttackSounds[Random.Range(0, AttackSounds.Count)]);

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
    }private void OnCollisionEnter(Collision collision)
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
