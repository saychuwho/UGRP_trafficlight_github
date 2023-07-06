using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trafficControllerDemo : MonoBehaviour
{
    public traffic_light_UGRP[] trafficLights;
    public traffic_light_UGRP[] pedestrainLights;
    // Start is called before the first frame update
    void Start()
    {
        // trafficlight의 상태를 init
        int temp_index = 0;
        foreach (traffic_light_UGRP trafficlight in trafficLights)
        {
            // trafficlight reset
            trafficlight.ResetLight();

            // lightState init
            LightStatesGenerator temp_light = new LightStatesGenerator(trafficlight.trafficLightType);
            trafficlight.lightStates = temp_light.lightStates;

            // trafficlight duration init
            for (int i = 0; i < trafficlight.lightStates.Count; i++)
            {
                trafficlight.lightDuration.Add(2);
            }

            // trafficlight current light init
            trafficlight.currentLightStateDuration = Time.time + trafficlight.lightDuration[trafficlight.currentLightStateIndex];

            // light on the first state
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

            temp_index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (trafficLights.Length >= 1)
        {
            foreach (traffic_light_UGRP trafficlight in trafficLights)
            {
                if (trafficlight.lights.Length > 1)
                {
                    if (trafficlight.currentLightStateDuration <= Time.time)
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
