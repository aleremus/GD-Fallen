using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ShotGun : Weapon
{
    //public float shootingRate;
    [SerializeField] bool hasBulletHole;
    [SerializeField] int damage;
    [SerializeField] GameObject bulletHolePrefab;
    [SerializeField] float cooldown;
    [SerializeField] float radius;
    public override void Fire()
    {
        base.Fire();

        RaycastHit raycastHit;

        Physics.Raycast(transform.position, transform.forward, out raycastHit, radius);

        if (raycastHit.collider)
        {
            if (hasBulletHole){
                GameObject hole = Instantiate(bulletHolePrefab, raycastHit.point, new Quaternion(raycastHit.normal.x, raycastHit.normal.y, raycastHit.normal.z, 0));
                hole.transform.LookAt(transform.position);
                hole.transform.parent = raycastHit.collider.transform;
            }
            Debug.Log(raycastHit.collider.name);
            Entity entity;
            
            if (raycastHit.collider.gameObject.TryGetComponent<Entity>(out entity))
            {
                entity.ReceiveDamage(damage);
            }
        }

    }
}
