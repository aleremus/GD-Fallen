using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class Torch : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] AnimationCurve intensityCurve;
    [SerializeField] float blinkRadius;
    [SerializeField] float blinksPerSecond;
    Vector3 startPosition;
    Light light;
    float _nextBlink;
    float randStart;
    void Start()
    {
        randStart = Random.Range(0f, intensityCurve.keys[intensityCurve.length - 1].time);
        startPosition = transform.localPosition;
        light = GetComponent<Light>();
        _nextBlink = Time.time + 1 / blinksPerSecond;
    }

    // Update is called once per frame
    void Update()
    {
        light.intensity = intensityCurve.Evaluate((Time.time + randStart) % intensityCurve.keys[intensityCurve.length - 1].time);
        if (_nextBlink > Time.time)
            return;
        _nextBlink = Time.time + 1 / blinksPerSecond;
        transform.localPosition = startPosition + Random.insideUnitSphere * blinkRadius;
    }



}
