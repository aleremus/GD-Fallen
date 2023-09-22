using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Transform> patrolPonts;
    [SerializeField] float speed;
    [SerializeField] float fov;
    [SerializeField] float agroTime;

    NavMeshAgent agent;
    Player player;
    bool _agro;
    float _agroTime;
    int _nextPoint;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        _agroTime -= Time.deltaTime;
        _agro = _agroTime > 0;
        if (DoesSeePlayer())
            ResetAgro();

        Move();
    }

    void Move()
    {
        if (_agro)
            ChasePlayer();
        else
            Patrol();
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    void Patrol()
    {
        if (patrolPonts == null || patrolPonts.Count <= 0)
            return;

        if ((patrolPonts[_nextPoint].position - transform.position).sqrMagnitude < 1)
        {
            _nextPoint = (_nextPoint + 1) % patrolPonts.Count;
            
        }
        agent.SetDestination(patrolPonts[_nextPoint].position);
    }


    void ResetAgro()
    {
        _agroTime = agroTime;
        _agro = true;
    }
    

    bool DoesSeePlayer()
    {
        if (Vector3.Angle(transform.forward, player.transform.position - transform.position) > fov)
            return false;

        RaycastHit raycastHit;

        Physics.Raycast(transform.position, player.transform.position - transform.position, out raycastHit);

        if (raycastHit.collider)
        {
            Debug.Log(raycastHit.collider.name);
            Player entity;

            return raycastHit.collider.gameObject.TryGetComponent<Player>(out entity);
        }

        return false;
    }
}
