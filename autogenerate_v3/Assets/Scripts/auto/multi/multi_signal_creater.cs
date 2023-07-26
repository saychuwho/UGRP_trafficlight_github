using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class multi_signal_creater : MonoBehaviour
{
    public GameObject signal_prefab;
    public GameObject scenario_data;
    public GameObject[] signals;
    public List<string> ped_signal1_condition;
    public List<string> ped_signal2_condition;
    public int scene_num;
    public int traffic_light_duration;
    // Start is called before the first frame update
    void Start()
    {
        scenario_data = Instantiate(scenario_data);

        ped_signal1_condition = scenario_data.GetComponent<multi_data_reader>().ped_signal1_condition;
        ped_signal2_condition = scenario_data.GetComponent<multi_data_reader>().ped_signal2_condition;
        List<string> ped_signal1_light = scenario_data.GetComponent<multi_data_reader>().ped_signal1_light;
        List<string> ped_signal2_light = scenario_data.GetComponent<multi_data_reader>().ped_signal2_light;
        List<string> car_signal1_light = scenario_data.GetComponent<multi_data_reader>().car_signal1_light;
        List<string> car_signal2_light = scenario_data.GetComponent<multi_data_reader>().car_signal2_light;
        List<string> right_signal_light = scenario_data.GetComponent<multi_data_reader>().right_signal_light;




        scene_num = scenario_data.GetComponent<multi_data_reader>().line_num;
        int environment_offset = scenario_data.GetComponent<multi_data_reader>().environment_offset;
        signals = new GameObject[scene_num];
        for (int i = 0; i < scene_num; i++)
        {
            signals[i] = (GameObject)PrefabUtility.InstantiatePrefab(signal_prefab);
            signals[i].transform.position = new Vector3(0, 0, 0) + new Vector3(0, 0, i * environment_offset);
            signals[i].transform.rotation = Quaternion.Euler(0, 90, 0);
            signals[i].transform.localScale = new Vector3(1, 1, 1);
            signals[i].GetComponent<TrafficLightControllerV2>().greenDurationInit = traffic_light_duration;
            signals[i].name = "signal_" + i.ToString();

            
            string car1_signal = car_signal1_light[i];

            if(car1_signal == "green")
            {
                signals[i].GetComponent<TrafficLightControllerV2>().startingTrafficIndex = 0;
            }
            else if (car1_signal == "yellow")
            {
                signals[i].GetComponent<TrafficLightControllerV2>().startingTrafficIndex = 1;
            }
            else if (car1_signal == "red+left")
            {
                signals[i].GetComponent<TrafficLightControllerV2>().startingTrafficIndex = 2;
            }
            else if (car1_signal == "red+yellow")
            {
                signals[i].GetComponent<TrafficLightControllerV2>().startingTrafficIndex = 3;
            }
            

            if (ped_signal1_condition[i] == "none")
            {
                signals[i].transform.GetChild(5).GetChild(6).gameObject.SetActive(false);
                signals[i].transform.GetChild(5).GetChild(7).gameObject.SetActive(false);
                signals[i].transform.GetChild(5).GetChild(11).gameObject.SetActive(false);

                signals[i].transform.GetChild(8).GetChild(6).gameObject.SetActive(false);
                signals[i].transform.GetChild(8).GetChild(7).gameObject.SetActive(false);
                signals[i].transform.GetChild(8).GetChild(11).gameObject.SetActive(false);
            }

            if (ped_signal2_condition[i] == "none")
            {
                signals[i].transform.GetChild(10).GetChild(6).gameObject.SetActive(false);
                signals[i].transform.GetChild(10).GetChild(7).gameObject.SetActive(false);
                signals[i].transform.GetChild(10).GetChild(11).gameObject.SetActive(false);

                signals[i].transform.GetChild(7).GetChild(6).gameObject.SetActive(false);
                signals[i].transform.GetChild(7).GetChild(7).gameObject.SetActive(false);
                signals[i].transform.GetChild(7).GetChild(11).gameObject.SetActive(false);
            }

            if (right_signal_light[i] == "none")
            {
                signals[i].transform.GetChild(0).gameObject.SetActive(false);
                signals[i].transform.GetChild(1).gameObject.SetActive(false);
                signals[i].transform.GetChild(2).gameObject.SetActive(false);
                signals[i].transform.GetChild(3).gameObject.SetActive(false);
            }


        }
    }
}
