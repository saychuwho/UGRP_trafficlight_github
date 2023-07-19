using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// 전적으로 이 신호 logic은 사거리에서만 동작하도록 짜여짐
public class TrafficLightController : MonoBehaviour
{
    public traffic_light_UGRP[] trafficLights;
    public ped_light_UGRP[] pedestrainLights;
    public rightTurn_light_UGRP[] rightTurnLights;

    // 적색, 황색 이외의 lightState에서 얼마나 불이 들어올지를 결정.
    public float trafficLightDurationInit = 2;

    // 우회전 신호에 얼마나 불을 할당할지를 결정 : 보행자 신호에 영향을 줌
    public float rightTurnTime = 1;

    // public int startingTrafficIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // trafficlight의 상태를 init
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
            // trafficlight의 duration은 trafficlight의 type에 따라서 달라진다.
            // 기준이 되는 1번 신호의 duration은 for문을 이용해 설정

            // trafficlight type에 따라서 달라진다. 만약 모든 신호가 다 직좌인 경우에는 duration 설정을 다르게 해야 한다.
            // 모든 신호가 직좌인 경우
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
            // 모든 신호가 다 직좌가 아닌 경우
            else if (trafficlight.trafficLightType / 100 != 2 || (trafficlight.trafficLightType % 100) / 10 == 1)
            {
                if (temp_index == 0)
                {
                    for (int i = 1; i < trafficlight.lightStates.Count; i++)
                    {
                        // 황색등이 켜질때는 duration을 1초로 설정하자.
                        // 적색등이 2일때도 마찬가지로.
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
                    // lightState의 항상 맨 첫 번째는 양쪽 신호 모두 빨간색이 되는 경우이므로 이렇게 설정
                    trafficlight.lightDuration.Insert(0, temp_sum);
                }
                else if (temp_index == 1) // 다른 신호의 경우이다.
                {
                    // 기준 신호의 양방향 적색 신호의 합은 기준신호의 모든 진행 관련 신호 시간의 합과 동일하다.
                    trafficlight.lightDuration.Add(trafficLights[0].lightDuration[0]);
                    // 그 이외 시간은 황색등의 개수와 상관있다.
                    float tempTrafficLightDuration = (trafficLights[0].lightDuration[0] - trafficlight.yellowNum) / (trafficlight.lightStates.Count - trafficlight.yellowNum - 1);
                    for (int i = 1; i < trafficlight.lightStates.Count; i++)
                    {
                        // 황색등이 켜질때는 duration을 1초로 설정하자.
                        // 적색등이 2일때도 마찬가지로.
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

        // pedlight의 상태를 init
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
                    // Location 설정이 무엇보다 중요하다! List에 순서대로 집어넣는게 무엇보다 중요함.
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

                    // child light init : 대각선 신호등은 모두 동일하게 움직이니 child light이 필요 X
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
                // 대각선 보행자 신호등
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

            // rightTurn light의 상태를 init
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

                    // rightlight가 보이면 안되니까 가려준다.
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
            // 차량 신호등을 update
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

            // 보행자 신호등을 update
            foreach (ped_light_UGRP pedlight in pedestrainLights)
            {
                // Duration 기반이 아니라, 주변 신호등의 state 조건에 맞춰서 불을 켜고 꺼야 한다.
                if (pedlight.lights.Length > 1)
                {
                    traffic_light_UGRP tempFrontLight = trafficLights[pedlight.pedFrontTrafficlight];
                    traffic_light_UGRP tempSideLight01 = trafficLights[pedlight.pedSideTrafficlight01];
                    traffic_light_UGRP tempSideLight02 = trafficLights[pedlight.pedSideTrafficlight02];
                    traffic_light_UGRP tempThisLight = trafficLights[pedlight.trafficLightLocation];
                    rightTurn_light_UGRP tempRightlight = rightTurnLights[pedlight.pedControlRightlight];

                    // 직좌 동시신호가 아닌 경우에는 양방향 직진이 반드시 있으니까, Side 신호 둘이 동시에 직진일때 불이 들어오도록 하자.
                    // 이때, side duration은 신호등의 lightDuration과 currentLightStateIndex로 계산해볼 수 있다. 
                    // 불 켜는 방식은 일반적인 등과 동일.
                    // 비보호 좌회전도 logic은 동일하나, 조건문이 달라져야 한다. 좌회전 신호가 없고, 직진 신호가 2에 있다.
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
                        // turn on the green light when 비보호 좌회전 신호
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

                        // 자신에게 할당받은 rightTurnlight를 control하는 section
                        if (tempRightlight.lights.Length > 1 && tempRightlight.isRightlightActive)
                        {
                            traffic_light_UGRP tempFrontTrafficlight = trafficLights[tempRightlight.rightFrontTrafficlight];
                            traffic_light_UGRP tempThisTrafficlight = trafficLights[tempRightlight.trafficLightLocation];

                            if (tempFrontTrafficlight.trafficLightType / 100 == 1 ||
                                tempFrontTrafficlight.trafficLightType / 100 == 5)
                            {
                                // 기본적인 logic은 보행자 신호와 유사. 특정 조건이 맞춰지면 녹색등을 켜고, 황색등, 적색등 순으로 켜고 끈다. 
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
                                // turn on the green light if FrontLight is 비보호 좌회전
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

                    // 직좌 동시신호일 때 보행자 신호등과 우회전 신호등의 행동을 결정
                    // 직좌 동시신호일때 보행자 신호등이 나중에 빨간불일때만 들어오는 곳이 아닐때(220이 아닐때)
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

                        // 자신에게 할당받은 rightTurnlight를 control하는 section
                        if (tempRightlight.lights.Length > 1 && tempRightlight.isRightlightActive)
                        {
                            traffic_light_UGRP tempFrontTrafficlight = trafficLights[tempRightlight.rightFrontTrafficlight];
                            traffic_light_UGRP tempThisTrafficlight = trafficLights[tempRightlight.trafficLightLocation];
                            // 우회전 신호가 속한 곳이 직좌 동시신호일 때
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
                    // 모든 교차로가 직좌 동시신호이고 보행자 신호는 나중에 모든 차량 신호가 적색일때 들어오는 경우
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

                        // 자신에게 할당받은 rightTurnlight를 control하는 section
                        if (tempRightlight.lights.Length > 1 && tempRightlight.isRightlightActive)
                        {
                            traffic_light_UGRP tempFrontTrafficlight = trafficLights[tempRightlight.rightFrontTrafficlight];
                            traffic_light_UGRP tempThisTrafficlight = trafficLights[tempRightlight.trafficLightLocation];
                            // 우회전 신호가 속한 곳이 직좌 동시신호일 때
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
