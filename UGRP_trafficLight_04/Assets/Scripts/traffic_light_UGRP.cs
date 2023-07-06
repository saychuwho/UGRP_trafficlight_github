using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class traffic_light_UGRP : MonoBehaviour
{
    // ���� ��ȣ���� � type�� �����°��� ��Ÿ���� ����
    public int trafficLightType = 0;

    // ���� ��ȣ���� � ��ġ�� �ִ����� ��Ÿ���� ����
    public int trafficLightLocation = 0;

    // code idea's from "Animating Traffic lights / Street Lamps / Signs" by Kobra Game Studios
    // ���⿡�� �ݵ�� ��ȣ�� �� ������� ����־�� �Ѵ�.
    public GameObject[] lights;

    // ���� �ڵ忡�� �ð� ���õ� �Ÿ� ����.
    // � ���� ���;� �ϴ����� �ƴ϶�, ��ȣ ü�迡�� � ���¸� ������ �־�� �ϴ����� ��Ÿ���� index�̴�.
    public int startingLightIndex = 0;

    // ���� �����ִ� ���� �������� ���� �� ������ �̸� List�� ����
    List<int> m_iCurrentLightIndex = new List<int>();

    // � ���� � ������� ���;� �ϴ����� �����ϴ� List
    // �̰Ŵ� controller�� Start���� �����ؾ� ������...
    public List<int[]> lightStates = new List<int[]>();

    // ���µ��� � ���� ��ŭ ���� �������� ����
    public List<float> lightDuration = new List<float>();

    // ���� � ���·� ���� ���;� �ϴ����� ��Ÿ���� ����
    public int currentLightStateIndex = 0;

    // ���� ���°� �󸶳� ���ӵǾ�� �ϴ°��� ��Ÿ���� ����
    public float currentLightStateDuration;

    // Ȳ������ ������ lightState�� �󸶳� �ִ����� ��Ÿ���� ����
    public int yellowNum = 0;


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

    // ���� ��ȣ���� ���¸� List�� ��ȯ�ϴ� �Լ�
    public List<int> ReturnCurrentLight()
    {
        return m_iCurrentLightIndex;
    }

    // lightIndex�� �ش��ϴ� light�� Ű��, lightIndex�� m_iCurrentLightIndex�� �ִ´�.
    // �Ѳ����� �������� ���� ���� �� �ִ�
    public void LightOn(int lightIndex)
    {
        // Debug.Log("trafficLight name : " + this.name);
        // Debug.Log("lightIndex : " + lightIndex.ToString());
        // Debug.Log("length of m_iCurrentLightIndex : " + m_iCurrentLightIndex.Count.ToString());
        
        lights[lightIndex].SetActive(true);
        m_iCurrentLightIndex[lightIndex] = 1;
    }

    // lightIndex�� �ش��ϴ� light�� ����, lightIndex�� m_iCurrentLightIndex���� �����Ѵ�.
    // �Ѳ����� �������� ���� ���� �� ������ �� �Լ��� ������
    public void LightOff(int lightIndex)
    {
        lights[lightIndex].SetActive(false);
        m_iCurrentLightIndex[lightIndex] = 0;
    }

    // lightIndex�� ������ �ش��ϴ� lightstate�� �Ѵ� �Լ�
    public void TurnLightState(int lightstateindex)
    {
        int tempLightIndex = 0;
        foreach(int lightstate in lightStates[lightstateindex])
        {
            if (lightstate == 1)
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

    // Update�� TrafficLightController.cs���� �̷���� �����̴�.
    // Update is called once per frame
    void Update()
    {
        
    }
}

