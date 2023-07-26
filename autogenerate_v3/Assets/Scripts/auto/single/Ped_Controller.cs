using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ped_Controller : MonoBehaviour
{
    public GameObject ped_info;
    public GameObject signal_info;
    private GameObject pedestrian1;
    private GameObject pedestrian2;

    // Start is called before the first frame update
    void Start()
    {
        pedestrian1= ped_info.GetComponent<Ped_creater>().pedestrian1;
        pedestrian2 = ped_info.GetComponent<Ped_creater>().pedestrian2;

    }

    // Update is called once per frame
    void Update()
    {
        pedestrian1 = ped_info.GetComponent<Ped_creater>().pedestrian1;
        pedestrian2 = ped_info.GetComponent<Ped_creater>().pedestrian2;
        int ped1_signal = signal_info.GetComponent<TrafficLightController>().pedestrainLights[0].currentLightStateIndex;
        int ped2_signal = signal_info.GetComponent<TrafficLightController>().pedestrainLights[1].currentLightStateIndex;

        if(ped1_signal == 1)
        {
            pedestrian1.transform.Translate(Vector3.forward * Time.deltaTime * 2.5f, Space.World);
        }
        else if(ped2_signal == 1)
        {
            pedestrian2.transform.Translate(Vector3.left * Time.deltaTime * 2.5f, Space.World);
        }
       
    }
}
