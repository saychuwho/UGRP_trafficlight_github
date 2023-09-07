using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car_light_UGRP : traffic_light_UGRP
{
    // child lights
    public car_light_UGRP[] carChildLights;

    // straight_left_together / no protect left sign
    public GameObject trafficSign;
}
