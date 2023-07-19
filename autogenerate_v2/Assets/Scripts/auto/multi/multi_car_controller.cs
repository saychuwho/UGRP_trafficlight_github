using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multi_car_controller : MonoBehaviour
{
    public GameObject car_info;
    public GameObject signal_info;
    private GameObject[] left_car;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int scene_num = car_info.GetComponent<multi_car_creater>().scene_num;
        left_car = new GameObject[scene_num];
        left_car = car_info.GetComponent<multi_car_creater>().left_vehicle;
        int left_car_signal = signal_info.GetComponent<multi_signal_creater>().signals[0].GetComponent<TrafficLightController>().trafficLights[0].currentLightStateIndex;
        if (left_car_signal == 6)
        {
            foreach(var lc in left_car)
            {
                lc.transform.Translate(Vector3.forward * Time.deltaTime * 7.0f, Space.World);
            }
            
        }
    }
}
