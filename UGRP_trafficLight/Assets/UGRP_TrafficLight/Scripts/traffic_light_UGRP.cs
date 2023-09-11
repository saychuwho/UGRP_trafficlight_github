using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class traffic_light_UGRP : MonoBehaviour
{
    // 현재 신호등이 어떤 type을 가지는가를 나타내는 변수
    public int trafficLightType = 0;

    // 현재 신호등이 어떤 위치에 있는지를 나타내는 변수
    [HideInInspector] public int trafficLightLocation = 0;

    // code idea's from "Animating Traffic lights / Street Lamps / Signs" by Kobra Game Studios
    // 여기에는 반드시 신호등 불 순서대로 집어넣어야 한다.
    public GameObject[] lights;

    // 기존 코드에서 시간 관련된 거를 뺐다.
    // 어떤 불이 들어와야 하는지가 아니라, 신호 체계에서 어떤 상태를 가지고 있어야 하는지를 나타내는 index이다.
    [HideInInspector] public int startingLightIndex = 0;

    // 현재 켜져있는 불이 여러개가 있을 수 있으니 이를 List로 관리
    List<int> m_iCurrentLightIndex = new List<int>();

    // 어떤 불이 어떤 순서대로 들어와야 하는지를 지정하는 List
    // 이거는 controller의 Start에서 지정해야 할지도...
    [HideInInspector] public List<int[]> lightStates = new List<int[]>();

    // 상태들이 어떤 길이 만큼 불이 들어올지를 결정
    [HideInInspector] public List<float> lightDuration = new List<float>();

    // 현재 어떤 상태로 불이 들어와야 하는지를 나타내는 변수
    [HideInInspector] public int currentLightStateIndex = 0;

    // 현재 상태가 얼마나 지속되어야 하는가를 나타내는 변수
    [HideInInspector] public float currentLightStateDuration;

    // 황색등이 들어오는 lightState가 얼마나 있는지를 나타내는 변수
    [HideInInspector] public int yellowNum = 0;

    // child light : 자신의 행동과 동일한 행동을 하는 light를 표현
    // public traffic_light_UGRP[] childLights;

    
    // Start is called before the first frame update
    void Start()
    {
        // all init of trafficlight will be done in TrafficLightController
    }

    public void ResetLight()
    {
        foreach (GameObject light in lights)
        {
            m_iCurrentLightIndex.Add(0);
            light.SetActive(false);
        }
    }

    // 현재 신호등의 상태를 List로 반환하는 함수
    public List<int> ReturnCurrentLight()
    {
        return m_iCurrentLightIndex;
    }

    // lightIndex에 해당하는 light를 키고, lightIndex를 m_iCurrentLightIndex에 넣는다.
    // 한꺼번에 여러개의 불이 들어올 수 있다
    public void LightOn(int lightIndex)
    {
        // Debug.Log("trafficLight name : " + this.name);
        // Debug.Log("lightIndex : " + lightIndex.ToString());
        // Debug.Log("length of m_iCurrentLightIndex : " + m_iCurrentLightIndex.Count.ToString());
        
        lights[lightIndex].SetActive(true);
        m_iCurrentLightIndex[lightIndex] = 1;
    }

    // lightIndex에 해당하는 light를 끄고, lightIndex를 m_iCurrentLightIndex에서 제거한다.
    // 한꺼번에 여러개의 불이 들어올 수 있으니 이 함수를 만들어둠
    public void LightOff(int lightIndex)
    {
        lights[lightIndex].SetActive(false);
        m_iCurrentLightIndex[lightIndex] = 0;
    }

    // lightIndex를 받으면 해당하는 lightstate를 켜는 함수
    public void TurnLightState(int lightstateindex)
    {
        int tempLightIndex = 0;
        foreach(int lightstate in lightStates[lightstateindex])
        {
            if (lightstate != 0)
            {
                LightOn(tempLightIndex);
            }
            else
            {
                LightOff(tempLightIndex);
            }
            tempLightIndex++;
        }
    }

    // Update는 TrafficLightController.cs에서 이루어질 예정이다.
    // Update is called once per frame
    void Update()
    {
        
    }
}

