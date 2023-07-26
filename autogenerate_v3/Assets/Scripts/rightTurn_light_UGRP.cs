using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightTurn_light_UGRP : traffic_light_UGRP
{
    // rightlight의 Active 여부를 나타냄
    public bool isRightlightActive = true;

    // right turn light의 경우에도, 자신이 고려해야 하는 신호가 무엇인지 저장해둘 필요가 있다.
    public int rightFrontTrafficlight = 0;
    public int rightSidePedlight = 0;

    public float rightDuration = 0;

    public int rightIndicator01 = 0;

    // right turn light는 없어질수도 있으므로, 등 이외에도 하위 구조에 대한 정보도 가지고 있어야 한다.
    public GameObject bodystructure;
}
