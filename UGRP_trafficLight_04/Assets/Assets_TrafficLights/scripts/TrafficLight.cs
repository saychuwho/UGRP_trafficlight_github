using UnityEngine;
using System.Collections;

public class TrafficLight : MonoBehaviour
{
    public GameObject   []lights;
    public float        []lightDuration;
    public float        []blinkLightsTime;

    public int          startingLightIndex = 0;

    int                 m_iCurrentLightIndex;
    float               m_flChangeLightTime;
    float               m_flBlinkLightTime;

	void Start ()
    {
	    m_iCurrentLightIndex = startingLightIndex;
        m_flChangeLightTime =  Time.time + lightDuration[m_iCurrentLightIndex];
        m_flBlinkLightTime = Time.time;

        SetCurrentLight();
	}

    void SetCurrentLight()
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(false);
            
        }
            
            
        

        lights[ m_iCurrentLightIndex ].SetActive ( true );
    }
	
	void Update ()
    {
        // change to next light
        if (lights.Length > 1)
        {
            if (m_flChangeLightTime <= Time.time)
            {
                m_iCurrentLightIndex++;

                if (m_iCurrentLightIndex >= lights.Length)
                    m_iCurrentLightIndex = 0;

                SetCurrentLight();
                /*
                foreach (GameObject light in lights)
                {
                    string name = lights[m_iCurrentLightIndex].name;
                    if (name.Contains("red"))
                    {
                        light.gameObject.tag = "red";
                        light.transform.parent.gameObject.tag = "red";
                    }
                    else if (name.Contains("yellow"))
                    {
                        light.gameObject.tag = "yellow";
                        light.transform.parent.gameObject.tag = "yellow";
                    }
                    else if (name.Contains("green-left"))
                    {
                        light.gameObject.tag = "green-left";
                        light.transform.parent.gameObject.tag = "green-left";
                    }
                    else if (name.Contains("green"))
                    {
                        light.gameObject.tag = "green";
                        light.transform.parent.gameObject.tag = "green";
                    }
                }
                */

                m_flChangeLightTime = Time.time + lightDuration[m_iCurrentLightIndex];
            }
        }

        // handle light blinking
        if (blinkLightsTime.Length > 0)
        {
            if (blinkLightsTime[m_iCurrentLightIndex] > 0.0f)
            {
                if (m_flBlinkLightTime <= Time.time)
                {
                    m_flBlinkLightTime = Time.time + blinkLightsTime[m_iCurrentLightIndex];

                    if (lights[m_iCurrentLightIndex].activeSelf)
                        lights[m_iCurrentLightIndex].SetActive(false);
                    else
                        lights[m_iCurrentLightIndex].SetActive(true);
                }
            }
        }
	}
}
