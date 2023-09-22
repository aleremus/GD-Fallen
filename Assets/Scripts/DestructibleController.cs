using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleController : MonoBehaviour
{
    [SerializeField]private GameObject destroyedObjectPrefab;
    [SerializeField]private ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnDestroy()
    {
        Instantiate(destroyedObjectPrefab, gameObject.transform.position, gameObject.transform.rotation);
        
/*        Destroy(gameObject);*/
    }
}
