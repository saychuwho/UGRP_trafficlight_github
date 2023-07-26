using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class multi_road_creater : MonoBehaviour
{
    public GameObject road_prefab;
    public GameObject scenario_data;
    public GameObject[] roads;

    public int scene_num;
    // Start is called before the first frame update
    void Start()
    {
        scenario_data = Instantiate(scenario_data);
        scene_num = scenario_data.GetComponent<multi_data_reader>().line_num;
        int environment_offset = scenario_data.GetComponent<multi_data_reader>().environment_offset;
        roads = new GameObject[scene_num];    
        for (int i = 0; i < scene_num; i++)
        {
            roads[i] = (GameObject)PrefabUtility.InstantiatePrefab(road_prefab);
            roads[i].transform.position = new Vector3(0, 0, 0) + new Vector3(0, 0, i* environment_offset);
            roads[i].transform.rotation = Quaternion.Euler(0, 0, 0);
            roads[i].name = "road_" + i.ToString();

        }
    }
}
