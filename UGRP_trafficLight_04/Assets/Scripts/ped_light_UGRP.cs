using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ped_light_UGRP : traffic_light_UGRP
{
    // ped light의 경우, 자신이 고려해야 하는 차량 신호등의 정보를 저장할 필요가 있다.
    public int pedFrontTrafficlight = 0;
    public int pedSideTrafficlight01 = 0;
    public int pedSideTrafficlight02 = 0;

    public int pedIndicator01 = 0;

    public int pedControlRightlight = 0;
}