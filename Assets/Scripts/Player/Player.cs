using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

    [SerializeField] private int ammoPerShot;
    [SerializeField] private Animator _shotgunAnimator;
    [SerializeField] private AudioSource _shotgunAudioSorce;
    [SerializeField] private GameObject flash;
    [SerializeField] private GameManager gameManager;
    [SerializeField] Weapon weapon;
    [SerializeField] int maxHP;
    [SerializeField] private CanvasController _canvasController;
    [SerializeField, Tooltip("0 - Empty shot\n1 - Shot")] List<AudioClip> sounds;
    private Rigidbody _rigidbody;
    private HPBar hPBar;

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
        if (IsDead)
            return;
        CurrentHp -= damage;
        if (damage > 0)
        {
        }
        else if (damage <= 0)
        {

        }
        CurrentHp = Mathf.Clamp(CurrentHp, 0, maxHP);
        if(CurrentHp <= 0)
        {
            Death();
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        hPBar = FindObjectOfType<HPBar>();
    }

    // Update is called once per frame
    void Update()
    {

        if (IsDead)
            return;

        if (_shotgunAnimator.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
        {
            return;
        }
            if (_rigidbody.velocity.magnitude > 0.1 && !_shotgunAnimator.GetCurrentAnimatorStateInfo(0).IsName("ShotgunReload")) _shotgunAnimator.Play("Walk");
            else if (!_shotgunAnimator.GetCurrentAnimatorStateInfo(0).IsName("ShotgunReload")) _shotgunAnimator.Play("ShotgunIdle");

            if (Input.GetKeyDown(KeyCode.Mouse0) && !_shotgunAnimator.GetCurrentAnimatorStateInfo(0).IsName("ShotgunReload"))
            {
                Fire();
                return;
            }
            if (Input.GetKeyDown(KeyCode.E))
                ReceiveDamage(1);
            if (Input.GetKeyDown(KeyCode.Q))
                ReceiveDamage(-1);
        if (/*Input.GetKeyDown(KeyCode.R)||*/!gameManager.reloaded&&gameManager.GetAmountOfAmmo()>0)
        {
            gameManager.Reload();
            _shotgunAnimator.Play("ShotgunReload");
        }
            if (_shotgunAnimator.GetCurrentAnimatorStateInfo(0).IsName("Shoot") && _shotgunAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime * _shotgunAnimator.GetCurrentAnimatorStateInfo(0).length <= 0.3 && _shotgunAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime * _shotgunAnimator.GetCurrentAnimatorStateInfo(0).length >= 0.2) flash.SetActive(true);
            else flash.SetActive(false);
        

    }
    public void Death()
    {
        _canvasController.RestartShow();
    }
    private void Fire()
    {
        if (!gameManager.reloaded)
        {
            
            if (sounds[0])
            {
                _shotgunAudioSorce.PlayOneShot(sounds[0]);
            }
            return;
        }
        gameManager.Shot();
        if (hPBar)
            hPBar.Shot();
        if (sounds[1])
        {
            _shotgunAudioSorce.PlayOneShot(sounds[1]);
        }
        _shotgunAnimator.Play("Shoot");
        gameManager.CollectAmmo(-ammoPerShot);
        weapon.Fire();

    }
}
