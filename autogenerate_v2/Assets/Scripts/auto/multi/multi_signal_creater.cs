using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class multi_signal_creater : MonoBehaviour
{
    public GameObject signal_prefab;
    public GameObject scenario_data;
    public GameObject[] signals;

    public int scene_num;
    public int traffic_light_duration;
    // Start is called before the first frame update
    void Start()
    {
        scenario_data = Instantiate(scenario_data);

        List<string> ped_signal1_condition = scenario_data.GetComponent<multi_data_reader>().ped_signal1_condition;
        List<string> ped_signal2_condition = scenario_data.GetComponent<multi_data_reader>().ped_signal2_condition;
        List<string> car_signal1_condition = scenario_data.GetComponent<multi_data_reader>().car_signal1_condition;
        List<string> car_signal2_condition = scenario_data.GetComponent<multi_data_reader>().car_signal2_condition;




        scene_num = scenario_data.GetComponent<multi_data_reader>().line_num;
        int environment_offset = scenario_data.GetComponent<multi_data_reader>().environment_offset;
        signals = new GameObject[scene_num];
        for (int i = 0; i < scene_num; i++)
        {
            signals[i] = (GameObject)PrefabUtility.InstantiatePrefab(signal_prefab);
            signals[i].transform.position = new Vector3(-2.5f, 20, 0) + new Vector3(0, 0, i * environment_offset);
            signals[i].transform.rotation = Quaternion.Euler(0, 0, 0);
            signals[i].transform.localScale = new Vector3(3, 3, 3);
            signals[i].GetComponent<TrafficLightController>().trafficLightDurationInit = traffic_light_duration;
            signals[i].name = "signal_" + i.ToString();

            int ped1_signal = signals[i].GetComponent<TrafficLightController>().pedestrainLights[3].currentLightStateIndex;
            int ped2_signal = signals[i].GetComponent<TrafficLightController>().pedestrainLights[0].currentLightStateIndex;
            if (car_signal1_condition[i] == "none")
            {
                signals[i].transform.GetChild(1).gameObject.SetActive(false);
            }

            if (car_signal2_condition[i] == "none")
            {
                signals[i].transform.GetChild(0).gameObject.SetActive(false);
            }

            if (ped_signal1_condition[i] == "none")
            {
                signals[i].transform.GetChild(7).gameObject.SetActive(false);
            }

            if (ped_signal2_condition[i] == "none")
            {
                signals[i].transform.GetChild(4).gameObject.SetActive(false);
            }


        }
    }
}
