using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class StateFileOut : MonoBehaviour
{
    public TrafficLightControllerV2 InterManager;
    public string savefileDir;
    public float recordTime;

    car_light_UGRP[] carlights;
    ped_light_UGRP[] pedlights;
    rightTurn_light_UGRP[] rightTurnlights;

    float timeTracker;

    FileStream fs;
    StreamWriter sw;

    // Start is called before the first frame update
    void Start()
    {
        // get arrays from TrafficLightControllerV2
        carlights = InterManager.trafficLights;
        pedlights = InterManager.pedestrainLights;
        rightTurnlights = InterManager.rightTurnLights;

        string filename = savefileDir + "lightState_" + DateTime.Now.ToString("yyMMdd_HHmmss") + ".txt";
        fs = new FileStream(filename, FileMode.Create);
        sw = new StreamWriter(fs);

        // write basic infos first
        sw.Write("trLight type : ");
        foreach(int type in InterManager.trafficLightTypeInit)
        {
            sw.Write(type.ToString() + " ");
        }
        sw.Write("\n");
        sw.Write("yellowDuration : " + InterManager.yellowDurationInit + "\n");
        sw.Write("greenDuration : " + InterManager.greenDurationInit + "\n");
        sw.Write("leftOnlyDuration : " + InterManager.leftOnlyDurationInit + "\n");
        sw.Write("starting Index : " + InterManager.startingTrafficIndex + "\n");

        // write rows
        sw.Write("Time".PadRight(12, ' '));
        for(int i = 1; i <= 4; i++)
        {
            string tmpString = "Car" + i;
            sw.Write(tmpString.PadRight(12, ' '));
        }
        for(int i = 1; i <= 4; i++)
        {
            string tmpString = "Ped" + i;
            sw.Write(tmpString.PadRight(12, ' '));
        }
        for(int i = 1; i <= 4; i++)
        {
            string tmpString = "Right" + i;
            sw.Write(tmpString.PadRight(12, ' '));
        }
        sw.Write("\n");

        // time tracker
        timeTracker = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeTracker <= Time.time && recordTime > Time.time)
        {
            sw.Write(timeTracker.ToString().PadRight(12, ' '));
            foreach(car_light_UGRP light in carlights)
            {
                sw.Write(LightReader(light.ReturnCurrentLight(), light.trafficLightType));
            }
            foreach(ped_light_UGRP light in pedlights)
            {
                sw.Write(LightReader(light.ReturnCurrentLight(), light.trafficLightType));
            }
            foreach(rightTurn_light_UGRP light in rightTurnlights)
            {
                sw.Write(LightReader(light.ReturnCurrentLight(), light.trafficLightType));
            }
            sw.Write("\n");
            timeTracker = Time.time + 1;
        }
        if(recordTime <= Time.time)
        {
            Debug.Log("done writing");
            sw.Close();
        }
    }

    public string LightReader(List<int> lightstate, int trafficLightType)
    {
        string retString = "";
        // car light
        if (trafficLightType / 100 == 1 || trafficLightType / 100 == 2)
        {
            if (lightstate[0] != 0)
            {
                if (lightstate[1] != 0)
                {
                    retString = "red+yellow";
                }
                else if (lightstate[2] != 0)
                {
                    retString = "red+left";
                }
                else
                {
                    retString = "red";
                }
            }
            else if (lightstate[1] != 0)
            {
                retString = "yellow";
            }
            else
            {
                retString = "green";
            }
        }
        // ped light
        else if (trafficLightType / 100 == 3)
        {
            if(lightstate[0] != 0)
            {
                retString = "red";
            }
            else
            {
                retString = "green";
            }
        }
        // rightturn light
        else
        {
            if(lightstate[0] != 0)
            {
                retString = "red";
            }
            else if(lightstate[1] != 0)
            {
                retString = "yellow";
            }
            else
            {
                retString = "right";
            }
        }


        return retString.PadRight(12, ' ');
    }
}
