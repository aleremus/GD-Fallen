using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PCameraController : MonoBehaviour
{
    [SerializeField] private PMoveController _pMoveController;
    [SerializeField]private CinemachineVirtualCamera _virtualCamera;
/*    [SerializeField]private CinemachineBasicPostProcessing _postProcessing;*/
    [SerializeField]private CinemachineBasicMultiChannelPerlin _perlinNoise;
    [SerializeField]private float walkShakeAmplitude;
    [SerializeField]private float walkShakeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        _perlinNoise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraBend(walkShakeSpeed * _pMoveController.GetPlayerSpeed(), walkShakeAmplitude*_pMoveController.GetPlayerSpeed()*Input.GetAxis("Horizontal"));
    }
    void CameraBend(float intensity,float amplitude)
    {
        _perlinNoise.m_FrequencyGain = intensity;
        _perlinNoise.m_AmplitudeGain = amplitude;
    }
    void CameraShake(float intensity, float time)
    {
        _perlinNoise.m_AmplitudeGain = intensity;
        StartCoroutine(ShakeOnTime(time));
    }
    IEnumerator ShakeOnTime(float time)
    {
        yield return new WaitForSeconds(time);
        ResetNoiseIntensity();
    }
    private void ResetNoiseIntensity()
    {
        _perlinNoise.m_AmplitudeGain = 0;
    }
    
}
