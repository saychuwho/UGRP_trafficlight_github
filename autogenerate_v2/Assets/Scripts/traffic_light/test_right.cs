using EVP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test_right : MonoBehaviour
{
    public GameObject front_signal;
    public GameObject ego_vehicle;
    public BoxCollider bound;
    public Text failureText;
    private string car_signal;
    private string people_signal;
    private Vector3 Start_point;
    private Vector3 Start_angle;
    public bool Is_right;
    // Start is called before the first frame update
    void Start()
    {
        Start_point=ego_vehicle.transform.position;
        Start_angle=ego_vehicle.transform.rotation.eulerAngles;
        car_signal = front_signal.GetComponent<check_signal>().car_signal;
        people_signal = front_signal.GetComponent<check_signal>().peole_signal;   
        if (car_signal == "rightarrow") Is_right = true;
        else Is_right=false;
    }

    // Update is called once per frame
    void Update()
    {
        Bounds bounds = bound.bounds;
        car_signal = front_signal.GetComponent<check_signal>().car_signal;
        people_signal = front_signal.GetComponent<check_signal>().peole_signal;
        if (car_signal == "rightarrow") Is_right = true;
        else Is_right = false;

        if(bounds.Contains(ego_vehicle.transform.position)==true)
        {
            if(Is_right==false) {
                failureText.text = "FAIL TO TEST";
                ego_vehicle.transform.position = Start_point;
                ego_vehicle.transform.rotation =Quaternion.Euler(Start_angle);
                ego_vehicle.GetComponent<Rigidbody>().velocity = Vector3.zero;
                //ego_vehicle.GetComponent<VehicleController>().steerInput = 0;
                //ego_vehicle.GetComponent<VehicleController>().brakeInput = 0;
            }
            else
            {
                failureText.text = "PASS TO TEST";
            }
        }
    }
}
