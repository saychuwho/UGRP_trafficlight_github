using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class multi_car_creater : MonoBehaviour
{
    public GameObject left_prefab;
    public GameObject ego_prefab;
    public GameObject scenario_data;
    public GameObject[] left_vehicle;
    public GameObject[] ego_vehicle;
    public int scene_num;
    // Start is called before the first frame update
    void Start()
    {
        

        scene_num = scenario_data.GetComponent<multi_data_reader>().line_num;
        int environment_offset = scenario_data.GetComponent<multi_data_reader>().environment_offset;
        left_vehicle = new GameObject[scene_num]; // 배열 초기화
        ego_vehicle = new GameObject[scene_num]; // 배열 초기화
        for (int i = 0; i < scene_num; i++)
        {
            left_vehicle[i] = (GameObject)PrefabUtility.InstantiatePrefab(left_prefab);
            ego_vehicle[i] = (GameObject)PrefabUtility.InstantiatePrefab(ego_prefab);

            string position_data = scenario_data.GetComponent<multi_data_reader>().ego_position[i];

            Vector3 ego_position_1 = new Vector3(25, 0, 4.5f) + new Vector3(0, 0, i * environment_offset);
            Vector3 ego_position_2 = new Vector3(13, 0, 5) + new Vector3(0, 0, i * environment_offset);
            if (position_data == "1")
            {
                ego_vehicle[i].transform.position = ego_position_1;
            }
            else if (position_data == "2")
            {
                ego_vehicle[i].transform.position = ego_position_2;
            }
            ego_vehicle[i].transform.rotation = Quaternion.Euler(0, -90, 0);
            ego_vehicle[i].name = "ego_vehicle_" + i.ToString();
            left_vehicle[i].transform.position = new Vector3(5, 0, -27) + new Vector3(0, 0, i * environment_offset);
            left_vehicle[i].transform.rotation = Quaternion.Euler(0, 0, 0);
            left_vehicle[i].name = "left_vehicle_" + i.ToString();
        }
    }
}
