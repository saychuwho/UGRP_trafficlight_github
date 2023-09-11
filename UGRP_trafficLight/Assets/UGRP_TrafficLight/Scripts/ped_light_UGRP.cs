using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ped_light_UGRP : traffic_light_UGRP
{
    // ped light의 경우, 자신이 고려해야 하는 차량 신호등의 정보를 저장할 필요가 있다.
    // 보행자 신호등을 기준으로 정면 신호등
    [HideInInspector] public int pedFrontTrafficlight = 0;
    // 보행자 신호등을 기준으로 왼쪽 차량 신호등
    [HideInInspector] public int pedSideTrafficlight01 = 0;
    // 보행자 신호등을 기준으로 오른쪽 차량 신호등
    [HideInInspector] public int pedSideTrafficlight02 = 0;

    [HideInInspector] public int pedIndicator01 = 0;

    // 보행자 신호등을 기준으로 오른쪽 우회전 신호등
    [HideInInspector] public int pedControlRightlight = 0;

    // child light
    public ped_light_UGRP[] pedChildLights;
}