using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int CoinsCollected;
    private int EnemiesKilled;
    [SerializeField] private int amountOfAmmo;

    [SerializeField] int enemiesForSmash;
    [SerializeField] Image smashFill;
    [SerializeField] TMPro.TMP_Text smashField;
    public bool canSmash;
    public bool reloaded;

    private int enemiesBeforeSmash;

    [SerializeField] TMPro.TMP_Text moneyField;
    int moneyOnMap;
    [SerializeField] TMPro.TMP_Text ammoField;
    

    void Start()
    {
        moneyOnMap = FindObjectsOfType<Money>().Length;

        enemiesBeforeSmash = enemiesForSmash;
        DrawSmash();
        DrawAmmo();
        DrawMoney();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shot()
    {
        reloaded = false;
        DrawAmmo();
    }

    public void Reload()
    {
        
        reloaded = amountOfAmmo > 0;
        DrawAmmo();
    }


    private void DrawAmmo()
    {
        if (reloaded)
        {
            ammoField.text = 2 + "/" + (amountOfAmmo - 2);
        }
        else
        {
            ammoField.text = 0 + "/" + amountOfAmmo;
        }
    }
    void DrawMoney()
    {
        moneyField.text = CoinsCollected + "/" + moneyOnMap;
    }

    public void CollectCoin()
    {
        CoinsCollected++;
        DrawMoney();
    }

    public void KillEnemy()
    {
        EnemiesKilled++;
        enemiesBeforeSmash++;
        enemiesBeforeSmash = Mathf.Clamp(enemiesBeforeSmash ,0, enemiesForSmash);

        canSmash = enemiesBeforeSmash >= enemiesForSmash;
        DrawSmash();
    }

    public void CollectAmmo(int ammo)
    {
        amountOfAmmo += ammo;
        DrawAmmo();
    }

    void DrawSmash()
    {
        smashFill.fillAmount = ((float) enemiesBeforeSmash) / ((float)enemiesForSmash);
        smashField.text = enemiesBeforeSmash + "/" + enemiesForSmash;
    }

    public void Smash()
    {
        enemiesBeforeSmash = 0;
        canSmash = enemiesBeforeSmash >= enemiesForSmash;
        DrawSmash();
    }
}
