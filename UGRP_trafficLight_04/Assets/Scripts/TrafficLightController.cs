using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    public traffic_light_UGRP []trafficLights;
    public traffic_light_UGRP []pedestrainLights;

    public int startingTrafficIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach(traffic_light_UGRP trafficlight in trafficLights)
        {
            trafficlight.lightDuration.Add(3);
            trafficlight.lightDuration.Add(3);
            trafficlight.lightDuration.Add(3);
            trafficlight.lightDuration.Add(3);
            trafficlight.lightStates = new LightStates(trafficlight.trafficLightType).lightStates;

            trafficlight.currentLightStateDuration = Time.time + trafficlight.lightDuration[trafficlight.currentLightStateIndex];
               
            int tempLightIndex = 0;
            foreach(int lightstate in trafficlight.lightStates[trafficlight.currentLightStateIndex])
            {
                if(lightstate == 1)
                {
                    trafficlight.LightOn(tempLightIndex);
                }
                else
                {
                    trafficlight.LightOff(tempLightIndex);
                }
                tempLightIndex++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(trafficLights.Length >= 1)
        {
            foreach(traffic_light_UGRP trafficlight in trafficLights)
            {
                if(trafficlight.lights.Length > 1)
                {
                    if(trafficlight.currentLightStateDuration <= Time.time)
                    {
                        trafficlight.currentLightStateIndex++;
                        if (trafficlight.currentLightStateIndex >= trafficlight.lightStates.Count)
                            trafficlight.currentLightStateIndex = 0;

                        int tempLightIndex = 0;
                        foreach (int lightstate in trafficlight.lightStates[trafficlight.currentLightStateIndex])
                        {
                            if (lightstate == 1)
                            {
                                trafficlight.LightOn(tempLightIndex);
                            }
                            else
                            {
                                trafficlight.LightOff(tempLightIndex);
                            }
                            tempLightIndex++;
                        }

                        trafficlight.currentLightStateDuration = Time.time + trafficlight.lightDuration[trafficlight.currentLightStateIndex];
                    }
                }       
            }
        }
    }
}
