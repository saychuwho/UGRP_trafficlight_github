using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ped_light_UGRP : traffic_light_UGRP
{
    // ped light�� ���, �ڽ��� ����ؾ� �ϴ� ���� ��ȣ���� ������ ������ �ʿ䰡 �ִ�.
    // ������ ��ȣ���� �������� ���� ��ȣ��
    [HideInInspector] public int pedFrontTrafficlight = 0;
    // ������ ��ȣ���� �������� ���� ���� ��ȣ��
    [HideInInspector] public int pedSideTrafficlight01 = 0;
    // ������ ��ȣ���� �������� ������ ���� ��ȣ��
    [HideInInspector] public int pedSideTrafficlight02 = 0;

    [HideInInspector] public int pedIndicator01 = 0;

    // ������ ��ȣ���� �������� ������ ��ȸ�� ��ȣ��
    [HideInInspector] public int pedControlRightlight = 0;

    // child light
    public ped_light_UGRP[] pedChildLights;
}