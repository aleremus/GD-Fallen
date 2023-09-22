using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HPBar : MonoBehaviour
{
    RectTransform rectTransform;
    [SerializeField] GameObject hpPrefab;
    [SerializeField] Entity entity;
    [SerializeField] List<Sprite> sprites;
    Player player;

    List<(GameObject hp, RectTransform rectTransform)> hps;
    void Start()
    {
        player = FindObjectOfType<Player>();
        hps = new();
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (entity.CurrentHp > hps.Count)
            GainHP();
        else if (entity.CurrentHp < hps.Count)
            LoseHp();
        if (entity is not Player)
        {
            transform.LookAt(player.transform, Vector3.up);
            
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
        hp.GetComponent<Image>().sprite = sprites[Random.Range(0, sprites.Count)];
        var hpRectT = hp.GetComponent<RectTransform>();
        hps.Add((hp, hpRectT));
        hpRectT.Translate(Vector3.right * hpRectT.rect.width * transform.localScale.x * (hps.Count - 1));

    }
}
