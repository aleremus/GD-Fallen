using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Weapon
{
    //public float shootingRate;
    [SerializeField] int damage;
    [SerializeField] float cooldown;
    [SerializeField] float radius;
    public override void Fire()
    {
        base.Fire();

        RaycastHit raycastHit;

        Physics.Raycast(transform.position, transform.forward, out raycastHit, radius);

        if (raycastHit.collider)
        {
            Debug.Log(raycastHit.collider.name);
            Entity entity;
            
            if (raycastHit.collider.gameObject.TryGetComponent<Entity>(out entity))
            {
                entity.ReceiveDamage(damage);
            }
        }

    }
}
