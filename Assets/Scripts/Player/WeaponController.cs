using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public int ammunitionCurrent;
    [SerializeField]private Weapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FillAmmunition(int amount)
    {
        ammunitionCurrent = amount;
    }
    public float ShootingDelay()
    {
        return weapon.shootingRate;
    }
}
