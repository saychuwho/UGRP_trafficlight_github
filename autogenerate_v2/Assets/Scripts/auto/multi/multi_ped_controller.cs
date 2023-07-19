using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class multi_ped_controller : MonoBehaviour
{
    public GameObject ped_info;
    public GameObject signal_info;
    private GameObject[] pedestrian1;
    private GameObject[] pedestrian2;
    public int scene_num;
    // Start is called before the first frame update
    void Start()
    {
        
       
       
    }

    // Update is called once per frame
    void Update()
    {
        scene_num = ped_info.GetComponent<multi_ped_creater>().scene_num;
        pedestrian1 = ped_info.GetComponent<multi_ped_creater>().pedestrian1;
        pedestrian2 = ped_info.GetComponent<multi_ped_creater>().pedestrian2;
       
        int ped1_signal = signal_info.GetComponent<multi_signal_creater>().signals[0].GetComponent<TrafficLightController>().pedestrainLights[3].currentLightStateIndex;
        int ped2_signal = signal_info.GetComponent<multi_signal_creater>().signals[0].GetComponent<TrafficLightController>().pedestrainLights[0].currentLightStateIndex;

        if (ped1_signal == 1)
        {
            foreach (var p1 in pedestrian1)
            {
                p1.transform.Translate(Vector3.forward * Time.deltaTime * 2.5f, Space.World);
            }
                
        }
        else if (ped2_signal == 1)
        {
            foreach (var p2 in pedestrian2)
            {
                p2.transform.Translate(Vector3.left * Time.deltaTime * 2.5f, Space.World);
            }
                
        }

    }
}
