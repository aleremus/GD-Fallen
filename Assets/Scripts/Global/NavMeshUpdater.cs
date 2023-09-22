using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class NavMeshUpdater : MonoBehaviour
{
    [SerializeField] NavMeshSurface surface;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && Input.GetKey(KeyCode.N))
            UpdateNavMesh();
    }

    public void UpdateNavMesh()
    {
        Debug.Log("NavMesh Updated!");
        surface.BuildNavMesh();
    }
}
