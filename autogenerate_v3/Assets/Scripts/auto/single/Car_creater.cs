using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Car_creater : MonoBehaviour
{
    public GameObject left_prefab;
    public GameObject ego_prefab;
    public GameObject scenario_data;
    public GameObject left_vehicle;
    public GameObject ego_vehicle;
    // Start is called before the first frame update
    void Start()
    {
       // GameObject left_vehicle = new GameObject();
       // GameObject ego_vehicle = new GameObject();

       
        left_vehicle = (GameObject)PrefabUtility.InstantiatePrefab(left_prefab);
        ego_vehicle = (GameObject)PrefabUtility.InstantiatePrefab(ego_prefab);

        
       
        string position_data = scenario_data.GetComponent<Data_reader>().ego_position;
        
        Vector3 ego_position_1 = new Vector3(25, 0, 4.5f);
        Vector3 ego_position_2 = new Vector3(13, 0, 5);
        if(position_data == "1")
        {
            ego_vehicle.transform.position = ego_position_1;
        }
        else if(position_data == "2")
        {
            ego_vehicle.transform.position = ego_position_2;
        }
        ego_vehicle.transform.rotation = Quaternion.Euler(0, -90, 0);
        ego_vehicle.name = "ego_vehicle";
        left_vehicle.transform.position = new Vector3(5, 0, -27);
        left_vehicle.transform.rotation = Quaternion.Euler(0, 0, 0);
        left_vehicle.name = "left_vehicle";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
