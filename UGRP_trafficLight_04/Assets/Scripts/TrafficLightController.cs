using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    public traffic_light_UGRP []trafficLights;
    public traffic_light_UGRP []pedestrainLights;

    public int startingTrafficIndex = 0;

    int CurrentTrafficLightIndex;
    int CurrentPedestrainLightIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        CurrentTrafficLightIndex = startingTrafficIndex;
        CurrentPedestrainLightIndex = startingTrafficIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if(trafficLights.Length > 1)
        {
            foreach(traffic_light_UGRP trafficlight in trafficLights)
            {
                for(int i=0;i<trafficlight.lights.Length;i++)
                {
                    trafficlight.ChangeLights(i);
                }
            }
        }
    }
}
