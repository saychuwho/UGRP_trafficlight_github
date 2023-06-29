using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class traffic_light_UGRP : MonoBehaviour
{

    // code idea's from "Animating Traffic lights / Street Lamps / Signs" by Kobra Game Studios

    public GameObject[] lights;

    // 기존 코드에서 시간 관련된 거를 뺐다.
    public int startingLightIndex = 0;

    int m_iCurrentLightIndex;
  
    

    // Start is called before the first frame update
    void Start()
    {
        // code from TrafficLights.cs
        m_iCurrentLightIndex = startingLightIndex;
        SetCurrentLight();
    }

    // code from TrafficLights.cs
    void SetCurrentLight()
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(false);
        }
        lights[m_iCurrentLightIndex].SetActive(true);
    }

    // 기존에 켜진 light를 끄고, 켜진 light를 바꾸는 함수
    public void ChangeLights(int lightIndex)
    {
        lights[m_iCurrentLightIndex].SetActive(false);
        m_iCurrentLightIndex = lightIndex;
        lights[m_iCurrentLightIndex].SetActive(true);
    }

    // Update는 TrafficLightController.cs에서 이루어질 예정이다.
    // Update is called once per frame
    void Update()
    {
        
    }
}
