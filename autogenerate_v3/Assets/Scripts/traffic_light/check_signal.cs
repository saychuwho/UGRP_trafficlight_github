using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class check_signal : MonoBehaviour
{
    public ViewPeopleSemaphore people_smaphore;
    public ViewCarSemaphore car_smaphore;
    public string peole_signal;
    public string car_signal;   
    // Start is called before the first frame update
    void Start()
    {
        peole_signal = people_smaphore.tag;
        car_signal=car_smaphore.tag;
    }

    // Update is called once per frame
    void Update()
    {
        peole_signal = people_smaphore.tag;
        car_signal = car_smaphore.tag;
    }
}
