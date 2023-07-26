using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ped_creater : MonoBehaviour
{
    public GameObject ped_prefab;
    public GameObject scenario_data;
    public GameObject pedestrian1;
    public GameObject pedestrian2;
    private Vector3 destination;
    private Vector3 ped1_position_1;
    private Vector3 ped2_position_1;
    void Start()
    {
        pedestrian1 = (GameObject)PrefabUtility.InstantiatePrefab(ped_prefab);
        pedestrian2 = (GameObject)PrefabUtility.InstantiatePrefab(ped_prefab);
        scenario_data = Instantiate(scenario_data);
        string ped1_position = scenario_data.GetComponent<Data_reader>().ped1_position;
        string ped2_position = scenario_data.GetComponent<Data_reader>().ped2_position;
        string car_action = scenario_data.GetComponent<Data_reader>().car_action;
        //Vector3 ped_position_0 = new Vector3(25, 0, 4.5f);

        if(ped1_position == "exist"){
            if (car_action == "right_turn")
            {
                ped1_position_1  = new Vector3(18, 0, -11);
                //destination = new Vector3(-10, 0, 18);
                pedestrian1.transform.position =  ped1_position_1;
                pedestrian1.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            pedestrian1.transform.position = new Vector3(4, -100, 18);
        }

        if (ped2_position == "exist")
        {
            if (car_action == "right_turn")
            {
                ped2_position_1 = new Vector3(10f, 0, 18);
                //destination = new Vector3(-10, 0, 18);
                pedestrian2.transform.position = ped2_position_1;
                pedestrian2.transform.rotation = Quaternion.Euler(0, -90, 0);
            }
        }
        else
        {
            pedestrian2.transform.position = new Vector3(4, -100, 18);
        }

        pedestrian1.name = "walking_ped_1";
        pedestrian2.name = "walking_ped_2";
    }

    void Update()
    {
        
    }
}
