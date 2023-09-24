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
    bool _isCoroutine;

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


        if (entity is not Player)
        {
            transform.LookAt(player.transform, Vector3.up);
            
        }
        
        if (entity.CurrentHp > hps.Count)
            StartCoroutine(GainHP());
        else if (entity.CurrentHp < hps.Count)
            StartCoroutine(LoseHp());
    }

   


    IEnumerator LoseHp()
    {
        _isCoroutine = true;
        (GameObject hp, var _) = hps[hps.Count - 1];
        hps.RemoveAt(hps.Count - 1);
        

        float time = Time.time + 0.5f;

        while (time > Time.time)
        {
            hp.transform.Translate(Vector3.up * Time.deltaTime * 1000, Space.World);
            yield return new WaitForEndOfFrame();
        }

        DestroyObject(hp);
        _isCoroutine = false;
    }

    IEnumerator GainHP()
    {
        
        _isCoroutine = true;
        
        var hp = Instantiate(hpPrefab, transform);
        //hp.GetComponent<Image>().sprite = sprites[Random.Range(0, sprites.Count)];
        var hpRectT = hp.GetComponent<RectTransform>();
        int i = hps.Count;
        hps.Add((hp, hpRectT));
        
        yield return new WaitForSeconds(1f);
        float time = Time.time + 0.5f;
        hpRectT.position = transform.position + new Vector3(70  * GetComponentInParent<Canvas>().pixelRect.width /800 * (i), 500 , 0);

        while (time > Time.time&&hp.transform.position.y >= transform.position.y)
        {
            hp.transform.Translate(Vector3.down * Time.deltaTime * 1000, Space.World);
            yield return new WaitForEndOfFrame();
        }
        if (transform.position.y >= hp.transform.position.y) hp.transform.position = new Vector3(hp.transform.position.x,transform.position.y, hp.transform.position.z);
        _isCoroutine = false;

    }

    public void Shot()
    {
        if(!_isCoroutine)
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.2f);
        foreach (var hp in hps)
        {
            hp.hp.GetComponent<Animator>().Play("heart_shot", -1, 0);
            hp.hp.GetComponent<Animator>().SetFloat("Rand", Random.Range(1, 1.4f));

        }
    }
}
