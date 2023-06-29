using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class traffic_light_UGRP : MonoBehaviour
{

    // code idea's from "Animating Traffic lights / Street Lamps / Signs" by Kobra Game Studios

    public GameObject[] lights;

    // ���� �ڵ忡�� �ð� ���õ� �Ÿ� ����.
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

    // ������ ���� light�� ����, ���� light�� �ٲٴ� �Լ�
    public void ChangeLights(int lightIndex)
    {
        lights[m_iCurrentLightIndex].SetActive(false);
        m_iCurrentLightIndex = lightIndex;
        lights[m_iCurrentLightIndex].SetActive(true);
    }

    // Update�� TrafficLightController.cs���� �̷���� �����̴�.
    // Update is called once per frame
    void Update()
    {
        
    }
}
