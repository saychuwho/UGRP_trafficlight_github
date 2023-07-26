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

        for(int i = 0 ; i < scene_num; i++)
        {
            int ped1_signal = signal_info.GetComponent<multi_signal_creater>().signals[i].GetComponent<TrafficLightControllerV2>().pedestrainLights[2].currentLightStateIndex;
            int ped2_signal = signal_info.GetComponent<multi_signal_creater>().signals[i].GetComponent<TrafficLightControllerV2>().pedestrainLights[3].currentLightStateIndex;
            string ped1_condition = signal_info.GetComponent<multi_signal_creater>().ped_signal1_condition[i];
            string ped2_condition = signal_info.GetComponent<multi_signal_creater>().ped_signal2_condition[i];
            if (ped1_signal == 1 && ped1_condition != "none")
            {
                pedestrian1[i].transform.Translate(Vector3.forward * Time.deltaTime * 2.5f, Space.World);
            }
            else if (ped2_signal == 1 && ped2_condition != "none")
            {
                pedestrian2[i].transform.Translate(Vector3.left * Time.deltaTime * 2.5f, Space.World);
            }
        }
        

        

    }
}
