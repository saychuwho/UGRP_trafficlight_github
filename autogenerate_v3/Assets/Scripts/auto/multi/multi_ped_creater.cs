using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class multi_ped_creater : MonoBehaviour
{
    public GameObject ped_prefab;
    public GameObject scenario_data;
    public GameObject[] pedestrian1;
    public GameObject[] pedestrian2;
    private Vector3 ped1_position_1;
    private Vector3 ped2_position_1;
    public int scene_num;
    void Start()
    {
        scenario_data = Instantiate(scenario_data);
        scene_num = scenario_data.GetComponent<multi_data_reader>().line_num;
        int environment_offset = scenario_data.GetComponent<multi_data_reader>().environment_offset;
        pedestrian1 = new GameObject[scene_num];
        pedestrian2 = new GameObject[scene_num];
        for (int i = 0; i < scene_num; i++)
        {
            pedestrian1[i] = (GameObject)PrefabUtility.InstantiatePrefab(ped_prefab);
            pedestrian2[i] = (GameObject)PrefabUtility.InstantiatePrefab(ped_prefab);
            string ped1_position = scenario_data.GetComponent<multi_data_reader>().ped1_position[i];
            string ped2_position = scenario_data.GetComponent<multi_data_reader>().ped2_position[i];
            string car_action = scenario_data.GetComponent<multi_data_reader>().car_action[i];
          
            if (ped1_position == "exist")
            {
                if (car_action == "right_turn")
                {
                    ped1_position_1 = new Vector3(18, 0, -11) + new Vector3(0, 0, i * environment_offset); ;
                    //destination = new Vector3(-10, 0, 18);
                    pedestrian1[i].transform.position = ped1_position_1;
                    pedestrian1[i].transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
            else
            {
                pedestrian1[i].transform.position = new Vector3(4, -100, 18);
            }

            if (ped2_position == "exist")
            {
                if (car_action == "right_turn")
                {
                    ped2_position_1 = new Vector3(10f, 0, 18) + new Vector3(0, 0, i * environment_offset); ;
                    pedestrian2[i].transform.position = ped2_position_1;
                    pedestrian2[i].transform.rotation = Quaternion.Euler(0, -90, 0);
                }
            }
            else
            {
                pedestrian2[i].transform.position = new Vector3(4, -100, 18) ;
            }

            pedestrian1[i].name = "walking_ped1_"+i.ToString();
            pedestrian2[i].name = "walking_ped2_"+i.ToString();
        }


    }
}
