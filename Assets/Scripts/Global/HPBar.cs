using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HPBar : MonoBehaviour
{
    RectTransform rectTransform;
    [SerializeField] GameObject hpPrefab;
    [SerializeField] Entity entity;

    List<(GameObject hp, RectTransform rectTransform)> hps;
    void Start()
    {
        hps = new();
        rectTransform = GetComponent<RectTransform>();
        GenerateHP();
    }

    // Update is called once per frame
    void Update()
    {

        if (entity.CurrentHp > hps.Count)
            GainHP();
        else if (entity.CurrentHp < hps.Count)
            LoseHp();

    }

    void GenerateHP()
    {
        var hp = Instantiate(hpPrefab, rectTransform);
        var hpRectT = hp.GetComponent<RectTransform>();

        hps.Add((hp, hpRectT)); 
        for (int i = 1; i < entity.MaxHP; i++)
        {
            hp = Instantiate(hpPrefab, rectTransform);
            hpRectT = hp.GetComponent<RectTransform>();
            hpRectT.Translate(Vector3.right * hpRectT.rect.width * i);
            hps.Add((hp, hpRectT));
        }
    }

    void LoseHp()
    {
        (GameObject hp, var _) = hps[hps.Count - 1];
        hps.RemoveAt(hps.Count - 1);
        DestroyObject(hp);
    }

    void GainHP()
    {
        var hp = Instantiate(hpPrefab, rectTransform);
        var hpRectT = hp.GetComponent<RectTransform>();
        hps.Add((hp, hpRectT));
        hpRectT.Translate(Vector3.right * hpRectT.rect.width * (hps.Count - 1));

    }
}
