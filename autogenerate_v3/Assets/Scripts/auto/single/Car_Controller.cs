using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Controller : MonoBehaviour
{
    public GameObject car_info;
    public GameObject signal_info;
    private GameObject left_car;

    // Start is called before the first frame update
    void Start()
    {
        left_car = car_info.GetComponent<Car_creater>().left_vehicle;
    }

    // Update is called once per frame
    void Update()
    {
        left_car = car_info.GetComponent<Car_creater>().left_vehicle;
        int left_car_signal = signal_info.GetComponent<TrafficLightController>().trafficLights[1].currentLightStateIndex;
        if (left_car_signal == 1 )
        {
            left_car.transform.Translate(Vector3.forward * Time.deltaTime * 7.0f, Space.World);
        }
    }
}
