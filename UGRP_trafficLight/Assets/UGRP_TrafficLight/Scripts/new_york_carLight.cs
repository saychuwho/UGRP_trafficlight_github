using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// capsule light to support korean light state 
/// need to mimic all methods in traffic_light_UGRP
/// </summary>

public class new_york_carLight : MonoBehaviour
{
    public car_light_UGRP left_light;
    public car_light_UGRP straight_light;

    [HideInInspector] public int trafficLightType = 0;
    [HideInInspector] public int trafficLightLocation = 0;
    [HideInInspector] public List<int[]> lightStates = new List<int[]>();
    [HideInInspector] public List<float> lightDuration = new List<float>();
    [HideInInspector] public int currentLightStateIndex = 0;
    [HideInInspector] public float currentLightStateDuration;
    [HideInInspector] List<int> m_iCurrentLightIndex = new List<int>();

    /// <summary>
    /// Initialize lightstate of left_light and straight_light
    /// </summary>
    public void InitNewYorkLight()
    {
        LightStatesGeneratorV2 temp_light = new LightStatesGeneratorV2(500);
        left_light.lightStates = temp_light.lightStates;
        straight_light.lightStates = temp_light.lightStates;
    }

    /// <summary>
    /// get lightstateindex from TrafficLightController and change it into new york traffic light state
    /// change mechanism is needed in separate function
    /// </summary>
    /// <param name="lightstateindex"> lightstateindex get from TrafficLightControllerV2 </param>
    public void TurnLightState(int lightstateindex)
    {
        List<int> changed_index = TranslateLightState(lightstateindex);
        left_light.TurnLightState(changed_index[0]);
        straight_light.TurnLightState(changed_index[1]);
    }

    /// <summary>
    /// change lightStates's component into left_light state index and straight_light state index
    /// </summary>
    /// <param name="lightstateindex"></param>
    /// <returns></returns>
    List<int> TranslateLightState(int lightstateindex)
    {
        int left_index = 0;
        int straight_index = 0;
        List<int> ret_list = new List<int>();

        
        if (lightStates[lightstateindex][2] > 0)
        {
            left_index = 2;
            straight_index = 0;
        }
        else if(lightStates[lightstateindex][3] > 0)
        {
            left_index = 0;
            straight_index = 2;

        }
        else if(lightStates[lightstateindex][1] > 0 && lightStates[lightstateindex][0] > 0)
        {
            left_index = 1;
            straight_index = 0;
        }
        else if(lightStates[lightstateindex][1] > 0 && lightStates[lightstateindex][0] == 0)
        {
            left_index = 0;
            straight_index = 1;
        }
        else
        {
            left_index = 0;
            straight_index = 0;
        }

        SetCurrentLightIndex(lightstateindex);

        ret_list.Add(left_index);
        ret_list.Add(straight_index);
        return ret_list;
    }

    /// <summary>
    /// reset the light
    /// </summary>
    public void ResetLight()
    {
        left_light.ResetLight();
        straight_light.ResetLight();
        for(int i = 0; i < 4; i++)
        {
            m_iCurrentLightIndex.Add(0);
        }
    }

    /// <summary>
    /// return m_iCurrentLightIndex. give compatibility with car_light_UGRP
    /// </summary>
    /// <returns> m_iCurrentLightIndex </returns>
    public List<int> ReturnCurrentLight()
    {
        return m_iCurrentLightIndex;
    }

    /// <summary>
    /// set m_iCurrentLightIndex
    /// </summary>
    /// <param name="lightindex"> index of lightStates </param>
    void SetCurrentLightIndex(int lightindex)
    {
        int temp_index = 0;
        foreach(int light in lightStates[lightindex])
        {
            if(light > 0)
            {
                m_iCurrentLightIndex[temp_index] = 1;
            }
            else
            {
                m_iCurrentLightIndex[temp_index] = 0;
            }
            temp_index++;
        }
    }
}
