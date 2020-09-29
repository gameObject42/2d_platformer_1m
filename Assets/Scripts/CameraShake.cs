using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    private CinemachineVirtualCamera cmVcam;
    private float shakeTimer;
    private float startingIntensity;
    private float endTime;

    private void Start()
    {
        Instance = this;
        cmVcam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cmbmcp =
                    cmVcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cmbmcp.m_AmplitudeGain = 
                    Mathf.Lerp(startingIntensity, 0f, (1-(shakeTimer / endTime)));

            }
        }
    }
    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cmbmcp =
            cmVcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cmbmcp.m_AmplitudeGain = intensity;

        startingIntensity = intensity;
        endTime = time;
        shakeTimer = time;
    }
}
