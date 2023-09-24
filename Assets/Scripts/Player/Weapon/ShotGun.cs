using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ShotGun : Weapon
{
    //public float shootingRate;
    [SerializeField] bool hasBulletHole;
    [SerializeField] int damage;
    [SerializeField] int cutDamage;
    [SerializeField] GameObject bulletHolePrefab;
    [SerializeField] float cooldown;
    [SerializeField] float radius;
    [SerializeField] Vector3 inAccuracy;
    [SerializeField] int rays;
    public override void Fire()
    {
        base.Fire();
        for (int i = 0; i < rays; i++)
        {
            RaycastHit raycastHit;

            Physics.Raycast(transform.position, transform.forward + inAccuracy * Random.Range(-1, 1) * i / rays, out raycastHit, radius);

            if (raycastHit.collider)
            {
                if (hasBulletHole)
                {
                    GameObject hole = Instantiate(bulletHolePrefab, raycastHit.point, new Quaternion(raycastHit.normal.x, raycastHit.normal.y, raycastHit.normal.z, 0));
                    hole.transform.LookAt(transform.position);
                    hole.transform.parent = raycastHit.collider.transform;
                }
                Debug.Log(raycastHit.collider.name);
                Entity entity;

                if (raycastHit.collider.gameObject.TryGetComponent<Entity>(out entity))
                {
                    entity.ReceiveDamage(damage);
                    Debug.Log(damage);
                }
            }
        }

    }
    public override void Cut()
    {

    }
}
