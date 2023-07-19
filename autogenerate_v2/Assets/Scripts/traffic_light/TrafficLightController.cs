using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// �������� �� ��ȣ logic�� ��Ÿ������� �����ϵ��� ¥����
public class TrafficLightController : MonoBehaviour
{
    public traffic_light_UGRP[] trafficLights;
    public ped_light_UGRP[] pedestrainLights;
    public rightTurn_light_UGRP[] rightTurnLights;

    // ����, Ȳ�� �̿��� lightState���� �󸶳� ���� �������� ����.
    public float trafficLightDurationInit = 2;

    // ��ȸ�� ��ȣ�� �󸶳� ���� �Ҵ������� ���� : ������ ��ȣ�� ������ ��
    public float rightTurnTime = 1;

    // public int startingTrafficIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // trafficlight�� ���¸� init
        int temp_index = 0;
        foreach (traffic_light_UGRP trafficlight in trafficLights)
        {
            // trafficlight reset
            trafficlight.ResetLight();

            // lightState init
            LightStatesGenerator temp_light = new LightStatesGenerator(trafficlight.trafficLightType);
            // Debug.Log(trafficlight.trafficLightType);
            trafficlight.lightStates = temp_light.lightStates;
            trafficlight.yellowNum = temp_light.yellowNum;

            // Location init
            trafficlight.trafficLightLocation = temp_index;

            // child light init
            if(trafficlight.childLights.Length > 0)
            {
                foreach(traffic_light_UGRP childlight in trafficlight.childLights)
                {
                    childlight.ResetLight();
                    childlight.lightStates = temp_light.lightStates;
                    childlight.yellowNum = temp_light.yellowNum;
                    childlight.trafficLightLocation = 10 + temp_index;
                }
            }

            // trafficlight duration init
            // trafficlight�� duration�� trafficlight�� type�� ���� �޶�����.
            // ������ �Ǵ� 1�� ��ȣ�� duration�� for���� �̿��� ����

            // trafficlight type�� ���� �޶�����. ���� ��� ��ȣ�� �� ������ ��쿡�� duration ������ �ٸ��� �ؾ� �Ѵ�.
            // ��� ��ȣ�� ������ ���
            if (trafficlight.trafficLightType / 100 == 2 && (trafficlight.trafficLightType % 100) / 10 != 1)
            {
                if (temp_index == 0)
                {
                    for (int i = 0; i < trafficlight.lightStates.Count; i++)
                    {
                        if (trafficlight.lightStates[i][1] != 0 || trafficlight.lightStates[i][0] == 2)
                        {
                            trafficlight.lightDuration.Add(1);
                        }
                        else
                        {
                            trafficlight.lightDuration.Add(trafficLightDurationInit);
                        }
                    }
                }
                else
                {
                    trafficlight.lightDuration = trafficLights[0].lightDuration;
                }
            }
            // ��� ��ȣ�� �� ���°� �ƴ� ���
            else if (trafficlight.trafficLightType / 100 != 2 || (trafficlight.trafficLightType % 100) / 10 == 1)
            {
                if (temp_index == 0)
                {
                    for (int i = 1; i < trafficlight.lightStates.Count; i++)
                    {
                        // Ȳ������ �������� duration�� 1�ʷ� ��������.
                        // �������� 2�϶��� ����������.
                        if (trafficlight.lightStates[i][1] != 0 || trafficlight.lightStates[i][0] == 2)
                        {
                            trafficlight.lightDuration.Add(1);
                        }
                        else
                        {
                            trafficlight.lightDuration.Add(trafficLightDurationInit);
                        }
                    }
                    float temp_sum = trafficlight.lightDuration.Sum();
                    // lightState�� �׻� �� ù ��°�� ���� ��ȣ ��� �������� �Ǵ� ����̹Ƿ� �̷��� ����
                    trafficlight.lightDuration.Insert(0, temp_sum);
                }
                else if (temp_index == 1) // �ٸ� ��ȣ�� ����̴�.
                {
                    // ���� ��ȣ�� ����� ���� ��ȣ�� ���� ���ؽ�ȣ�� ��� ���� ���� ��ȣ �ð��� �հ� �����ϴ�.
                    trafficlight.lightDuration.Add(trafficLights[0].lightDuration[0]);
                    // �� �̿� �ð��� Ȳ������ ������ ����ִ�.
                    float tempTrafficLightDuration = (trafficLights[0].lightDuration[0] - trafficlight.yellowNum) / (trafficlight.lightStates.Count - trafficlight.yellowNum - 1);
                    for (int i = 1; i < trafficlight.lightStates.Count; i++)
                    {
                        // Ȳ������ �������� duration�� 1�ʷ� ��������.
                        // �������� 2�϶��� ����������.
                        if (trafficlight.lightStates[i][1] != 0 || trafficlight.lightStates[i][0] == 2)
                        {
                            trafficlight.lightDuration.Add(1);
                        }
                        else
                        {
                            trafficlight.lightDuration.Add(tempTrafficLightDuration);
                        }
                    }
                }
                else if (temp_index == 2)
                {
                    trafficlight.lightDuration = trafficLights[0].lightDuration;
                }
                else if (temp_index == 3)
                {
                    trafficlight.lightDuration = trafficLights[1].lightDuration;
                }

                // currentLightStateIndex init
                if (temp_index % 2 == 1)
                {
                    trafficlight.currentLightStateIndex = 1;
                }
            }


            // trafficlight current light init
            trafficlight.currentLightStateDuration = Time.time + trafficlight.lightDuration[trafficlight.currentLightStateIndex];
            
            // light on the first state
            trafficlight.TurnLightState(trafficlight.currentLightStateIndex);

            // child light current light init, light on the first state
            if (trafficlight.childLights.Length > 0)
            {
                foreach (traffic_light_UGRP childlight in trafficlight.childLights)
                {
                    childlight.currentLightStateDuration = trafficlight.currentLightStateDuration;
                    childlight.TurnLightState(trafficlight.currentLightStateIndex);
                }
            }

            temp_index++;
        }

        // pedlight�� ���¸� init
        temp_index = 0;
        if (pedestrainLights.Length > 1)
        {
            foreach (ped_light_UGRP pedlight in pedestrainLights)
            {
                // trafficlight reset
                pedlight.ResetLight();

                // lightState init
                LightStatesGenerator temp_light = new LightStatesGenerator(pedlight.trafficLightType);
                pedlight.lightStates = temp_light.lightStates;
                pedlight.yellowNum = temp_light.yellowNum;

                if ((pedlight.trafficLightType % 100) / 10 == 1)
                {
                    // Location init
                    // Location ������ �������� �߿��ϴ�! List�� ������� ����ִ°� �������� �߿���.
                    pedlight.trafficLightLocation = temp_index;

                    // front traffic light, side traffic light init;
                    pedlight.pedFrontTrafficlight = (pedlight.trafficLightLocation + 2) % 4;
                    pedlight.pedSideTrafficlight01 = (pedlight.trafficLightLocation + 1) % 4;
                    pedlight.pedSideTrafficlight02 = (pedlight.trafficLightLocation + 3) % 4;

                    // rightTurnlight to init
                
                    pedlight.pedControlRightlight = (pedlight.trafficLightLocation + 3) % 4;
                    if (rightTurnLights[pedlight.pedControlRightlight].trafficLightType == 0)
                    {
                        pedlight.pedControlRightlight = -1;
                    }

                    // child light init : �밢�� ��ȣ���� ��� �����ϰ� �����̴� child light�� �ʿ� X
                    if(pedlight.childLights.Length > 0)
                    {
                        foreach(ped_light_UGRP childlight in pedlight.childLights)
                        {
                            childlight.ResetLight();
                            childlight.lightStates = temp_light.lightStates;
                            childlight.yellowNum = temp_light.yellowNum;
                            childlight.trafficLightLocation = 10 + temp_index;
                        }
                    }
                }
                // �밢�� ������ ��ȣ��
                else
                {
                    pedlight.trafficLightLocation = 0;
                    pedlight.pedFrontTrafficlight = 1;
                    pedlight.pedSideTrafficlight01 = 2;
                    pedlight.pedSideTrafficlight02 = 3;
                }


                // light on the first state
                pedlight.TurnLightState(pedlight.currentLightStateIndex);

                pedlight.currentLightStateDuration = Time.time;
                temp_index++;
            }

            // rightTurn light�� ���¸� init
            temp_index = 0;
            if (rightTurnLights.Length > 1)
            {
                foreach (rightTurn_light_UGRP rightlight in rightTurnLights)
                {
                    // trafficlight reset
                    rightlight.ResetLight();

                    // lightState init
                    LightStatesGenerator temp_light = new LightStatesGenerator(rightlight.trafficLightType);
                    rightlight.lightStates = temp_light.lightStates;
                    rightlight.yellowNum = temp_light.yellowNum;

                    // Location init
                    rightlight.trafficLightLocation = temp_index;

                    if (rightlight.isRightlightActive)
                    {
                        // right light duration init
                        rightlight.rightDuration = rightTurnTime;

                        // front traffic light, side ped light init
                        rightlight.rightFrontTrafficlight = (rightlight.trafficLightLocation + 2) % 4;
                        rightlight.rightSidePedlight = (rightlight.trafficLightLocation + 1) % 4;

                        // light on the first state
                        rightlight.TurnLightState(rightlight.currentLightStateIndex);

                        rightlight.currentLightStateDuration = Time.time;
                    }

                    // rightlight�� ���̸� �ȵǴϱ� �����ش�.
                    else
                    {
                        rightlight.bodystructure.SetActive(false);
                        for (int i = 0; i < rightlight.lights.Length; i++)
                        {
                            rightlight.LightOff(i);
                        }
                    }
                    temp_index++;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (trafficLights.Length >= 1)
        {
            // ���� ��ȣ���� update
            foreach (traffic_light_UGRP trafficlight in trafficLights)
            {
                if (trafficlight.lights.Length > 1)
                {
                    if (trafficlight.currentLightStateDuration <= Time.time)
                    {
                        trafficlight.currentLightStateIndex++;
                        if (trafficlight.currentLightStateIndex >= trafficlight.lightStates.Count)
                            trafficlight.currentLightStateIndex = 0;

                        trafficlight.TurnLightState(trafficlight.currentLightStateIndex);
                        trafficlight.currentLightStateDuration = Time.time + trafficlight.lightDuration[trafficlight.currentLightStateIndex];

                        // child light update
                        if(trafficlight.childLights.Length > 0)
                        {
                            foreach(traffic_light_UGRP childlight in trafficlight.childLights)
                            {
                                childlight.TurnLightState(trafficlight.currentLightStateIndex);
                                childlight.currentLightStateDuration = trafficlight.currentLightStateDuration;
                            }
                        }
                    }
                }
            }

            // ������ ��ȣ���� update
            foreach (ped_light_UGRP pedlight in pedestrainLights)
            {
                // Duration ����� �ƴ϶�, �ֺ� ��ȣ���� state ���ǿ� ���缭 ���� �Ѱ� ���� �Ѵ�.
                if (pedlight.lights.Length > 1)
                {
                    traffic_light_UGRP tempFrontLight = trafficLights[pedlight.pedFrontTrafficlight];
                    traffic_light_UGRP tempSideLight01 = trafficLights[pedlight.pedSideTrafficlight01];
                    traffic_light_UGRP tempSideLight02 = trafficLights[pedlight.pedSideTrafficlight02];
                    traffic_light_UGRP tempThisLight = trafficLights[pedlight.trafficLightLocation];
                    rightTurn_light_UGRP tempRightlight = rightTurnLights[pedlight.pedControlRightlight];

                    // ���� ���ý�ȣ�� �ƴ� ��쿡�� ����� ������ �ݵ�� �����ϱ�, Side ��ȣ ���� ���ÿ� �����϶� ���� �������� ����.
                    // �̶�, side duration�� ��ȣ���� lightDuration�� currentLightStateIndex�� ����غ� �� �ִ�. 
                    // �� �Ѵ� ����� �Ϲ����� ��� ����.
                    // ��ȣ ��ȸ���� logic�� �����ϳ�, ���ǹ��� �޶����� �Ѵ�. ��ȸ�� ��ȣ�� ����, ���� ��ȣ�� 2�� �ִ�.
                    if (tempSideLight01.trafficLightType / 100 == 1 || tempSideLight01.trafficLightType / 100 == 5)
                    {
                        // turn on the green light
                        if (tempSideLight01.trafficLightType / 100 == 1 && tempSideLight02.trafficLightType / 100 == 1 &&
                            tempSideLight01.ReturnCurrentLight()[3] == 1 && tempSideLight02.ReturnCurrentLight()[3] == 1
                            && pedlight.ReturnCurrentLight()[1] == 0 && tempFrontLight.ReturnCurrentLight()[2] != 1 && pedlight.pedIndicator01 == 0)
                        {
                            pedlight.currentLightStateIndex++;
                            if (pedlight.currentLightStateIndex >= pedlight.lightStates.Count)
                                pedlight.currentLightStateIndex = 0;

                            pedlight.TurnLightState(pedlight.currentLightStateIndex);
                            pedlight.currentLightStateDuration = Time.time + tempSideLight01.lightDuration[tempSideLight01.currentLightStateIndex];

                            tempRightlight.rightIndicator01 = 0;

                            // child light update
                            if (pedlight.childLights.Length > 0)
                            {
                                foreach (traffic_light_UGRP childlight in pedlight.childLights)
                                {
                                    childlight.TurnLightState(pedlight.currentLightStateIndex);
                                    childlight.currentLightStateDuration = pedlight.currentLightStateDuration;
                                }
                            }
                        }
                        // turn on the green light when ��ȣ ��ȸ�� ��ȣ
                        else if (tempSideLight01.trafficLightType / 100 == 5 && tempSideLight01.ReturnCurrentLight()[2] == 1 &&
                            tempSideLight02.ReturnCurrentLight()[2] == 1 && pedlight.ReturnCurrentLight()[1] == 0 &&
                            pedlight.pedIndicator01 == 0)
                        {
                            pedlight.currentLightStateIndex++;
                            if (pedlight.currentLightStateIndex >= pedlight.lightStates.Count)
                                pedlight.currentLightStateIndex = 0;

                            pedlight.TurnLightState(pedlight.currentLightStateIndex);
                            pedlight.currentLightStateDuration = Time.time + tempSideLight01.lightDuration[tempSideLight01.currentLightStateIndex];

                            tempRightlight.rightIndicator01 = 0;

                            // child light update
                            if (pedlight.childLights.Length > 0)
                            {
                                foreach (traffic_light_UGRP childlight in pedlight.childLights)
                                {
                                    childlight.TurnLightState(pedlight.currentLightStateIndex);
                                    childlight.currentLightStateDuration = pedlight.currentLightStateDuration;
                                }
                            }
                        }

                        // turn on the red light
                        else if (pedlight.currentLightStateDuration - rightTurnTime <= Time.time && pedlight.ReturnCurrentLight()[1] == 1)
                        {
                            pedlight.currentLightStateIndex++;
                            if (pedlight.currentLightStateIndex >= pedlight.lightStates.Count)
                                pedlight.currentLightStateIndex = 0;

                            pedlight.TurnLightState(pedlight.currentLightStateIndex);

                            pedlight.pedIndicator01 = 1;
                            tempRightlight.rightIndicator01 = 1;

                            // child light update
                            if (pedlight.childLights.Length > 0)
                            {
                                foreach (traffic_light_UGRP childlight in pedlight.childLights)
                                {
                                    childlight.TurnLightState(pedlight.currentLightStateIndex);
                                }
                            }
                        }
                        else if (pedlight.currentLightStateDuration <= Time.time && pedlight.ReturnCurrentLight()[1] == 0)
                        {
                            pedlight.pedIndicator01 = 0;
                        }

                        // �ڽſ��� �Ҵ���� rightTurnlight�� control�ϴ� section
                        if (tempRightlight.lights.Length > 1 && tempRightlight.isRightlightActive)
                        {
                            traffic_light_UGRP tempFrontTrafficlight = trafficLights[tempRightlight.rightFrontTrafficlight];
                            traffic_light_UGRP tempThisTrafficlight = trafficLights[tempRightlight.trafficLightLocation];

                            if (tempFrontTrafficlight.trafficLightType / 100 == 1 ||
                                tempFrontTrafficlight.trafficLightType / 100 == 5)
                            {
                                // �⺻���� logic�� ������ ��ȣ�� ����. Ư�� ������ �������� ������� �Ѱ�, Ȳ����, ������ ������ �Ѱ� ����. 
                                // turn on the green light
                                if (tempThisTrafficlight.trafficLightType / 100 == 1 &&
                                    pedlight.ReturnCurrentLight()[1] == 0 && tempRightlight.ReturnCurrentLight()[0] == 1
                                    && tempFrontTrafficlight.ReturnCurrentLight()[3] == 1 &&
                                    tempThisTrafficlight.ReturnCurrentLight()[3] == 1)
                                {
                                    tempRightlight.currentLightStateIndex = 2;

                                    tempRightlight.TurnLightState(tempRightlight.currentLightStateIndex);

                                    tempRightlight.currentLightStateDuration = Time.time + tempRightlight.rightDuration - 1;

                                    tempRightlight.rightIndicator01 = 1;
                                }
                                // turn on the green light if FrontLight is ��ȣ ��ȸ��
                                else if (tempFrontTrafficlight.trafficLightType / 100 == 5 &&
                                    pedlight.ReturnCurrentLight()[1] == 0 && tempRightlight.ReturnCurrentLight()[0] == 1
                                    && tempFrontTrafficlight.ReturnCurrentLight()[2] == 1 &&
                                    tempThisTrafficlight.ReturnCurrentLight()[2] == 1)
                                {
                                    tempRightlight.currentLightStateIndex = 2;

                                    tempRightlight.TurnLightState(tempRightlight.currentLightStateIndex);

                                    tempRightlight.currentLightStateDuration = Time.time + tempRightlight.rightDuration - 1;

                                    tempRightlight.rightIndicator01 = 1;
                                }


                                // turn on the yellow light
                                if (tempRightlight.currentLightStateDuration <= Time.time && pedlight.ReturnCurrentLight()[1] != 1
                                    && tempRightlight.rightIndicator01 == 1 && tempRightlight.ReturnCurrentLight()[2] == 1)
                                {
                                    tempRightlight.currentLightStateIndex = 1;

                                    tempRightlight.TurnLightState(tempRightlight.currentLightStateIndex);

                                    tempRightlight.currentLightStateDuration = Time.time + 1;
                                }

                                // turn on the red light
                                else if (tempFrontTrafficlight.ReturnCurrentLight()[0] == 1 ||
                                    (tempRightlight.currentLightStateDuration <= Time.time))
                                {
                                    tempRightlight.currentLightStateIndex = 0;

                                    tempRightlight.TurnLightState(tempRightlight.currentLightStateIndex);

                                    tempRightlight.rightIndicator01 = 0;
                                }
                            }
                        }

                    }

                    // ���� ���ý�ȣ�� �� ������ ��ȣ��� ��ȸ�� ��ȣ���� �ൿ�� ����
                    // ���� ���ý�ȣ�϶� ������ ��ȣ���� ���߿� �������϶��� ������ ���� �ƴҶ�(220�� �ƴҶ�)
                    else if (tempSideLight01.trafficLightType / 100 == 2 && (tempSideLight01.trafficLightType % 100) / 10 != 2)
                    {
                        // turn on the green light
                        if (tempSideLight01.ReturnCurrentLight()[3] == 1 && pedlight.ReturnCurrentLight()[1] == 0 &&
                            tempFrontLight.ReturnCurrentLight()[2] != 1 && pedlight.pedIndicator01 == 0)
                        {
                            pedlight.currentLightStateIndex++;
                            if (pedlight.currentLightStateIndex >= pedlight.lightStates.Count)
                                pedlight.currentLightStateIndex = 0;

                            pedlight.TurnLightState(pedlight.currentLightStateIndex);
                            pedlight.currentLightStateDuration = Time.time + tempSideLight01.lightDuration[tempSideLight01.currentLightStateIndex];

                            tempRightlight.rightIndicator01 = 0;

                            // child light update
                            if (pedlight.childLights.Length > 0)
                            {
                                foreach (traffic_light_UGRP childlight in pedlight.childLights)
                                {
                                    childlight.TurnLightState(pedlight.currentLightStateIndex);
                                    childlight.currentLightStateDuration = pedlight.currentLightStateDuration;
                                }
                            }
                        }
                        // turn on the red light
                        else if (pedlight.currentLightStateDuration - rightTurnTime <= Time.time && pedlight.ReturnCurrentLight()[1] == 1)
                        {
                            pedlight.currentLightStateIndex++;
                            if (pedlight.currentLightStateIndex >= pedlight.lightStates.Count)
                                pedlight.currentLightStateIndex = 0;

                            pedlight.TurnLightState(pedlight.currentLightStateIndex);

                            pedlight.pedIndicator01 = 1;
                            tempRightlight.rightIndicator01 = 1;

                            // child light update
                            if (pedlight.childLights.Length > 0)
                            {
                                foreach (traffic_light_UGRP childlight in pedlight.childLights)
                                {
                                    childlight.TurnLightState(pedlight.currentLightStateIndex);
                                }
                            }
                        }
                        else if (pedlight.currentLightStateDuration <= Time.time && pedlight.ReturnCurrentLight()[1] == 0)
                        {
                            pedlight.pedIndicator01 = 0;
                        }

                        // �ڽſ��� �Ҵ���� rightTurnlight�� control�ϴ� section
                        if (tempRightlight.lights.Length > 1 && tempRightlight.isRightlightActive)
                        {
                            traffic_light_UGRP tempFrontTrafficlight = trafficLights[tempRightlight.rightFrontTrafficlight];
                            traffic_light_UGRP tempThisTrafficlight = trafficLights[tempRightlight.trafficLightLocation];
                            // ��ȸ�� ��ȣ�� ���� ���� ���� ���ý�ȣ�� ��
                            if (tempThisTrafficlight.trafficLightType / 100 == 2)
                            {
                                // turn on the green light
                                if (pedlight.ReturnCurrentLight()[1] == 0 && tempRightlight.ReturnCurrentLight()[0] == 1
                                    && tempFrontTrafficlight.ReturnCurrentLight()[3] == 1)
                                {
                                    tempRightlight.currentLightStateIndex = 2;

                                    tempRightlight.TurnLightState(tempRightlight.currentLightStateIndex);

                                    tempRightlight.currentLightStateDuration = Time.time + tempRightlight.rightDuration - 1;

                                    tempRightlight.rightIndicator01 = 1;
                                }
                                // turn on the yellow light
                                if (tempRightlight.currentLightStateDuration <= Time.time && pedlight.ReturnCurrentLight()[1] != 1
                                    && tempRightlight.rightIndicator01 == 1 && tempRightlight.ReturnCurrentLight()[2] == 1)
                                {
                                    tempRightlight.currentLightStateIndex = 1;

                                    tempRightlight.TurnLightState(tempRightlight.currentLightStateIndex);

                                    tempRightlight.currentLightStateDuration = Time.time + 1;
                                }

                                // turn on the red light
                                else if (tempFrontTrafficlight.ReturnCurrentLight()[0] == 1 ||
                                    (tempRightlight.currentLightStateDuration <= Time.time))
                                {
                                    tempRightlight.currentLightStateIndex = 0;

                                    tempRightlight.TurnLightState(tempRightlight.currentLightStateIndex);

                                    tempRightlight.rightIndicator01 = 0;
                                }
                            }
                        }
                    }
                    // ��� �����ΰ� ���� ���ý�ȣ�̰� ������ ��ȣ�� ���߿� ��� ���� ��ȣ�� �����϶� ������ ���
                    else if (tempSideLight01.trafficLightType / 100 == 2 && (tempSideLight01.trafficLightType % 100) / 10 == 2)
                    {
                        // turn on the green light
                        if (tempSideLight01.ReturnCurrentLight()[0] == 1 && tempFrontLight.ReturnCurrentLight()[0] == 1 &&
                            tempSideLight02.ReturnCurrentLight()[0] == 1 && tempThisLight.ReturnCurrentLight()[0] == 1 &&
                            pedlight.pedIndicator01 == 0 && pedlight.ReturnCurrentLight()[1] == 0)
                        {
                            pedlight.currentLightStateIndex++;
                            if (pedlight.currentLightStateIndex >= pedlight.lightStates.Count)
                                pedlight.currentLightStateIndex = 0;

                            pedlight.TurnLightState(pedlight.currentLightStateIndex);
                            pedlight.currentLightStateDuration = Time.time + tempSideLight01.lightDuration[tempSideLight01.currentLightStateIndex];

                            tempRightlight.rightIndicator01 = 0;

                            // child light update
                            if (pedlight.childLights.Length > 0)
                            {
                                foreach (traffic_light_UGRP childlight in pedlight.childLights)
                                {
                                    childlight.TurnLightState(pedlight.currentLightStateIndex);
                                    childlight.currentLightStateDuration = pedlight.currentLightStateDuration;
                                }
                            }
                        }
                        // turn on the red light
                        else if (pedlight.currentLightStateDuration - rightTurnTime <= Time.time && pedlight.ReturnCurrentLight()[1] == 1)
                        {
                            pedlight.currentLightStateIndex++;
                            if (pedlight.currentLightStateIndex >= pedlight.lightStates.Count)
                                pedlight.currentLightStateIndex = 0;

                            pedlight.TurnLightState(pedlight.currentLightStateIndex);

                            pedlight.pedIndicator01 = 1;
                            tempRightlight.rightIndicator01 = 1;

                            // child light update
                            if (pedlight.childLights.Length > 0)
                            {
                                foreach (traffic_light_UGRP childlight in pedlight.childLights)
                                {
                                    childlight.TurnLightState(pedlight.currentLightStateIndex);
                                }
                            }
                        }
                        else if (pedlight.currentLightStateDuration <= Time.time && pedlight.ReturnCurrentLight()[1] == 0)
                        {
                            pedlight.pedIndicator01 = 0;
                        }

                        // �ڽſ��� �Ҵ���� rightTurnlight�� control�ϴ� section
                        if (tempRightlight.lights.Length > 1 && tempRightlight.isRightlightActive)
                        {
                            traffic_light_UGRP tempFrontTrafficlight = trafficLights[tempRightlight.rightFrontTrafficlight];
                            traffic_light_UGRP tempThisTrafficlight = trafficLights[tempRightlight.trafficLightLocation];
                            // ��ȸ�� ��ȣ�� ���� ���� ���� ���ý�ȣ�� ��
                            if (tempThisTrafficlight.trafficLightType / 100 == 2)
                            {
                                // turn on the green light
                                if (pedlight.ReturnCurrentLight()[1] == 0 && tempRightlight.ReturnCurrentLight()[0] == 1
                                    && tempFrontTrafficlight.ReturnCurrentLight()[3] == 1)
                                {
                                    tempRightlight.currentLightStateIndex = 2;

                                    tempRightlight.TurnLightState(tempRightlight.currentLightStateIndex);

                                    tempRightlight.currentLightStateDuration = Time.time + 
                                        tempFrontTrafficlight.lightDuration[tempFrontTrafficlight.currentLightStateIndex];

                                    tempRightlight.rightIndicator01 = 1;
                                }
                                // turn on the yellow light
                                if (tempRightlight.currentLightStateDuration <= Time.time && pedlight.ReturnCurrentLight()[1] != 1
                                    && tempRightlight.rightIndicator01 == 1 && tempRightlight.ReturnCurrentLight()[2] == 1)
                                {
                                    tempRightlight.currentLightStateIndex = 1;

                                    tempRightlight.TurnLightState(tempRightlight.currentLightStateIndex);

                                    tempRightlight.currentLightStateDuration = Time.time + 1;
                                }

                                // turn on the red light
                                else if (tempFrontTrafficlight.ReturnCurrentLight()[0] == 1 ||
                                    (tempRightlight.currentLightStateDuration <= Time.time))
                                {
                                    tempRightlight.currentLightStateIndex = 0;

                                    tempRightlight.TurnLightState(tempRightlight.currentLightStateIndex);

                                    tempRightlight.rightIndicator01 = 0;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
