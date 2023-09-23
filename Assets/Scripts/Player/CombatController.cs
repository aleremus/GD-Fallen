using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [SerializeField] private int maxCombo;
    [SerializeField] private KeyCode key;
    [SerializeField] private KeyCode debugKey;
    [SerializeField] private LayerMask destructibleLayer;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _distance;
    [SerializeField] private GameObject particlesPrefab;
    [SerializeField] private GameObject shardsPrefab;
    private Rigidbody _rigidbody;
    private int currentCombo;
    private void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        currentCombo = maxCombo;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(debugKey)) AddCombo();
        if (Input.GetKeyDown(key) && currentCombo > 0)
        {
            GameObject target = TryToHit(_camera.position, _camera.forward, _distance).transform?.gameObject;
            if (TryToHit(_camera.position, _camera.forward, _distance).transform?.gameObject != null) TryToBreak(target);
            currentCombo--;
        }
    }
    private RaycastHit TryToHit(Vector3 from, Vector3 direction, float distance)
    {
        RaycastHit hit;
        Physics.Raycast(new Ray(from, direction),out hit , distance, destructibleLayer);
        return hit;
    }
    private void TryToBreak(GameObject target)
    {
        if (particlesPrefab != null)
        {
            GameObject particle = Instantiate(particlesPrefab);
            particle.transform.position = target.transform.position;
            particle.transform.rotation = target.transform.rotation;
            particle = Instantiate(shardsPrefab);
            particle.transform.position = target.transform.position;
            particle.transform.rotation = target.transform.rotation;
        }
        Destroy(target);
    }
    public void TryToBreak()
    {
        GameObject target = TryToHit(_camera.position, _camera.forward, _distance).transform?.gameObject;
        if (target != null) TryToBreak(target);
    }
    public void AddCombo()
    {
        if (currentCombo < maxCombo)
        {
            currentCombo++;
        }
    }
}
