using UnityEngine;
using System.Collections;

public class countdownLight : MonoBehaviour
{
    public GameObject   []lights;
    public float        []lightDuration;

    public Material     []countdownTimerMaterials;
    public MeshRenderer countdownTimer = null;
    public int          Element_LightToCountdownOn = -1;
    public int          countdownTimerStartingIndex = 0;

    public int          startingLightIndex = 0;

    int                 m_iCurrentLightIndex;
    int                 m_iCurrentCountdownTimerIndex;
    float               m_flChangeLightTime;
    float               m_flChangeCountdownNumberTime;
    float               m_flCountdownTimerDelta;     // time between changing each countdown number

	void Start ()
    {
	    m_iCurrentLightIndex = startingLightIndex;
        m_iCurrentCountdownTimerIndex = countdownTimerStartingIndex;

        if (countdownTimer != null)
        {
            countdownTimer.material = countdownTimerMaterials[m_iCurrentCountdownTimerIndex];
            countdownTimer.gameObject.SetActive( false );
        }

        m_flChangeLightTime =  Time.time + lightDuration[m_iCurrentLightIndex];
        

        if (Element_LightToCountdownOn == m_iCurrentLightIndex)
        {
            m_flCountdownTimerDelta = lightDuration[ m_iCurrentLightIndex ] / ( countdownTimerStartingIndex + 1);
            m_flChangeCountdownNumberTime = Time.time + m_flCountdownTimerDelta;
            m_iCurrentCountdownTimerIndex = countdownTimerStartingIndex;

            if (countdownTimer != null)
            {
                countdownTimer.material = countdownTimerMaterials[m_iCurrentCountdownTimerIndex];
                countdownTimer.gameObject.SetActive(true);
            }
        }

        SetCurrentLight();
	}

	void SetCurrentLight()
    {
        foreach (GameObject light in lights)
            light.SetActive(false);

        lights[ m_iCurrentLightIndex ].SetActive ( true );
        foreach (GameObject light in lights)
        {
            string name = lights[m_iCurrentLightIndex].name;
            if (name.Contains("no"))
            {
                light.gameObject.tag = "red";
                light.transform.parent.gameObject.tag = "red";
            }
            else {
                light.gameObject.tag = "green";
                light.transform.parent.gameObject.tag = "green";
            }
        }
        if (Element_LightToCountdownOn == m_iCurrentLightIndex)
            countdownTimer.gameObject.SetActive( true );
    }

    void UpdateCountdownTimer()
    {
        if (m_flChangeCountdownNumberTime <= Time.time)
        {
            m_flChangeCountdownNumberTime = Time.time + m_flCountdownTimerDelta;

            if (m_iCurrentCountdownTimerIndex > 0)
            {
                m_iCurrentCountdownTimerIndex--;
                countdownTimer.material = countdownTimerMaterials[ m_iCurrentCountdownTimerIndex ];
            }
        }        
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

                m_flChangeLightTime = Time.time + lightDuration[m_iCurrentLightIndex];

                if (Element_LightToCountdownOn == m_iCurrentLightIndex)
                {
                    m_flCountdownTimerDelta = lightDuration[m_iCurrentLightIndex] / (countdownTimerStartingIndex + 1);
                    m_flChangeCountdownNumberTime = Time.time + m_flCountdownTimerDelta;
                    m_iCurrentCountdownTimerIndex = countdownTimerStartingIndex;
                    countdownTimer.material = countdownTimerMaterials[m_iCurrentCountdownTimerIndex];
                    countdownTimer.gameObject.SetActive(true);
                }
                else
                {
                    if ( countdownTimer != null )
                        countdownTimer.gameObject.SetActive(false);
                }
            }
        }

        if ( Element_LightToCountdownOn == m_iCurrentLightIndex )
            UpdateCountdownTimer();
        	
	}
}
