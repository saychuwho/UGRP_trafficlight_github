using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightTurn_light_UGRP : traffic_light_UGRP
{
    // rightlight�� Active ���θ� ��Ÿ��
    public bool isRightlightActive = true;

    // right turn light�� ��쿡��, �ڽ��� ����ؾ� �ϴ� ��ȣ�� �������� �����ص� �ʿ䰡 �ִ�.
    public int rightFrontTrafficlight = 0;
    public int rightSidePedlight = 0;

    public float rightDuration = 0;

    public int rightIndicator01 = 0;

    // right turn light�� ���������� �����Ƿ�, �� �̿ܿ��� ���� ������ ���� ������ ������ �־�� �Ѵ�.
    public GameObject bodystructure;
}
