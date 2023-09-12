using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightTurn_light_UGRP : traffic_light_UGRP
{
    // rightlight�� Active ���θ� ��Ÿ��
    [HideInInspector] public bool isRightlightActive = true;

    // right turn traffic sign�� ��Ÿ�� ���θ� ����
    [HideInInspector] public bool isRightProhibitSignActive = true;

    // right turn light�� ��쿡��, �ڽ��� ����ؾ� �ϴ� ��ȣ�� �������� �����ص� �ʿ䰡 �ִ�.
    [HideInInspector] public int rightFrontTrafficlight = 0;
    [HideInInspector] public int rightSidePedlight = 0;

    [HideInInspector] public float rightDuration = 0;

    [HideInInspector] public int rightIndicator01 = 0;

    // right turn light�� ���������� �����Ƿ�, �� �̿ܿ��� ���� ������ ���� ������ ������ �־�� �Ѵ�.
    public GameObject bodystructure;

    // traffic sign
    public GameObject trafficSign;
}
