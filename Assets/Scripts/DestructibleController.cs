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
    public void Destroy()
    {
        Instantiate(destroyedObjectPrefab, gameObject.transform.position, gameObject.transform.rotation);
        gameObject.layer = 0;
        var nmu = FindObjectOfType<NavMeshUpdater>();
        if (nmu)
            nmu.UpdateNavMesh();
        Destroy(gameObject);
    }

}
