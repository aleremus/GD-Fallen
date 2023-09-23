using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private Animator _shotgunAnimator;
    [SerializeField] private GameObject flash;
    [SerializeField] Weapon weapon;
    [SerializeField] int maxHP;
    [SerializeField] private CanvasController _canvasController;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        MaxHP = maxHP;
        CurrentHp = MaxHP;
        _rigidbody = GetComponent<Rigidbody>();
    }
    override public void DealDamage(Entity reciever, int damage)
    {
        
    }

    override public void ReceiveDamage(int damage)
    {
        CurrentHp -= damage;
        if (damage > 0)
        {
        }
        else if (damage < 0)
        {
            
        }
        CurrentHp = Mathf.Clamp(CurrentHp, 0, maxHP);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            Fire();
        if (Input.GetKeyDown(KeyCode.E))
            ReceiveDamage(1);
        if (Input.GetKeyDown(KeyCode.Q))
            ReceiveDamage(-1);
        if (_rigidbody.velocity.magnitude > 0.1 && !_shotgunAnimator.GetCurrentAnimatorStateInfo(0).IsName("Shoot")) _shotgunAnimator.Play("Walk");
        else if (!_shotgunAnimator.GetCurrentAnimatorStateInfo(0).IsName("Shoot")) _shotgunAnimator.Play("ShotgunIdle");
        if (_shotgunAnimator.GetCurrentAnimatorStateInfo(0).IsName("Shoot") && _shotgunAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime * _shotgunAnimator.GetCurrentAnimatorStateInfo(0).length <= 0.4 && _shotgunAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime * _shotgunAnimator.GetCurrentAnimatorStateInfo(0).length >= 0.2) flash.SetActive(true);
        else flash.SetActive(false);

    }
    public void Death()
    {
        _canvasController.RestartShow();
    }
    private void Fire()
    {
        weapon.Fire();
        _shotgunAnimator.Play("Shoot");
    }
}
