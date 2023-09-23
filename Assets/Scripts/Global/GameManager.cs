using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int CoinsCollected;
    public int EnemiesKilled;
    public int amountOfAmmo;

    public bool reloaded;


    [SerializeField] TMPro.TMP_Text moneyField;
    int moneyOnMap;
    [SerializeField] TMPro.TMP_Text ammoField;

    void Start()
    {
        moneyOnMap = FindObjectsOfType<Money>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        DrawAmmo();
        DrawMoney();
    }

    void DrawMoney()
    {
        moneyField.text = CoinsCollected + "/" + moneyOnMap;
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
}
