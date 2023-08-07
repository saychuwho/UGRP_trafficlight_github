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

    FileStream fs2;
    StreamWriter swCSV;

    int padnum = 20;

    // Start is called before the first frame update
    void Start()
    {
        // get arrays from TrafficLightControllerV2
        carlights = InterManager.trafficLights;
        pedlights = InterManager.pedestrainLights;
        rightTurnlights = InterManager.rightTurnLights;

        string filename = savefileDir + "lightState_" + DateTime.Now.ToString("yyMMdd_HHmmss") + ".txt";
        string filename2 = savefileDir + "lightState_" + DateTime.Now.ToString("yyMMdd_HHmmss") + ".csv";
        fs = new FileStream(filename, FileMode.Create);
        sw = new StreamWriter(fs);

        // add csv FileStream
        fs2 = new FileStream(filename2, FileMode.Create);
        swCSV = new StreamWriter(fs2);

        // write basic infos first >> csv don't need this. read the extra infos from txt
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
        sw.Write("Time".PadRight(padnum, ' '));
        swCSV.Write("Time,");
        for(int i = 1; i <= 4; i++)
        {
            string tmpString = "Car" + i;
            sw.Write(tmpString.PadRight(padnum, ' '));
            swCSV.Write(tmpString + ",");
        }
        for(int i = 1; i <= 4; i++)
        {
            string tmpString = "Ped" + i;
            sw.Write(tmpString.PadRight(padnum, ' '));
            swCSV.Write(tmpString + ",");
        }
        for(int i = 1; i <= 4; i++)
        {
            string tmpString = "Right" + i;
            sw.Write(tmpString.PadRight(padnum, ' '));
            swCSV.Write(tmpString + ",");
        }
        sw.Write("\n");
        swCSV.Write("\n");

        // time tracker
        timeTracker = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeTracker <= Time.time && recordTime > Time.time)
        {
            int tmptimeTracker = (int)timeTracker;
            int tmpIndexTracker = 0;
            List<bool> tmpRightExist = new List<bool>();
            foreach(rightTurn_light_UGRP light in rightTurnlights)
            {
                if (light.isRightlightActive)
                    tmpRightExist.Add(true);
                else
                    tmpRightExist.Add(false);
            }

            sw.Write(tmptimeTracker.ToString().PadRight(padnum, ' '));
            swCSV.Write(tmptimeTracker.ToString() + ",");
            

            foreach (car_light_UGRP light in carlights)
            {
                if (tmpRightExist[tmpIndexTracker])
                {
                    sw.Write(LightReader(light.ReturnCurrentLight(), light.trafficLightType));
                    swCSV.Write(LightReader(light.ReturnCurrentLight(), light.trafficLightType, true) + ",");
                }
                else
                {
                    sw.Write(LightReader(light.ReturnCurrentLight(), light.trafficLightType, false, false));
                    swCSV.Write(LightReader(light.ReturnCurrentLight(), light.trafficLightType, true, false) + ",");
                }
                tmpIndexTracker++;
            }

            foreach(ped_light_UGRP light in pedlights)
            {
                sw.Write(LightReader(light.ReturnCurrentLight(), light.trafficLightType));
                swCSV.Write(LightReader(light.ReturnCurrentLight(), light.trafficLightType, true) + ",");
            }

            tmpIndexTracker = 0;
            foreach(rightTurn_light_UGRP light in rightTurnlights)
            {
                if (tmpRightExist[tmpIndexTracker])
                {
                    sw.Write(LightReader(light.ReturnCurrentLight(), light.trafficLightType));
                    swCSV.Write(LightReader(light.ReturnCurrentLight(), light.trafficLightType, true) + ",");
                }
                else
                {
                    sw.Write(LightReader(light.ReturnCurrentLight(), light.trafficLightType, false, false));
                    swCSV.Write(LightReader(light.ReturnCurrentLight(), light.trafficLightType, true, false) + ",");
                }
                tmpIndexTracker++;
            }
            sw.Write("\n");
            swCSV.Write("\n");
            timeTracker = Time.time + 1;
        }
        if(recordTime <= Time.time)
        {
            Debug.Log("done writing");
            sw.Close();
            swCSV.Close();
        }
    }

    /// <summary>
    /// read the lightstate list and return string lightstate
    /// index of the direction is like this :
    ///     - Straight : (S)
    ///     - Right turn : (R)
    ///     - Left turn : (L)
    ///     - All light : (A)
    /// </summary>
    /// <param name="lightstate"> lightstate of the trafficlight</param>
    /// <param name="trafficLightType"> lightType of UGRP_traffic_light </param>
    /// <param name="isCSV"> Writing FileStream is csv or not. default is false </param>
    /// <param name="isRight"> RightLight is available. default is true </param>
    /// <returns></returns>
    public string LightReader(List<int> lightstate, int trafficLightType, bool isCSV = false, bool isRight = true)
    {   
        string retString = "";
        // car light
        if (trafficLightType / 100 == 1 || trafficLightType / 100 == 2)
        {
            if (lightstate[0] != 0)
            {
                if (lightstate[1] != 0)
                {
                    retString = "red(SR)+yellow(L)";
                }
                else if (lightstate[2] != 0)
                {
                    retString = "red(SR)+left(L)";
                }
                else
                {
                    retString = "red(A)";
                }
            }
            else if (lightstate[1] != 0)
            {
                retString = "yellow(A)";
            }
            else
            {
                if (!isRight) 
                { 
                    retString = "green(SR)"; 
                }
                else
                {
                    retString = "green(S)";
                }
                
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
            if (isRight)
            {
                if (lightstate[0] != 0)
                {
                    retString = "red(R)";
                }
                else if (lightstate[1] != 0)
                {
                    retString = "yellow(R)";
                }
                else
                {
                    retString = "right(R)";
                }
            }
            else
                retString = "None";
        }


        return isCSV ? retString : retString.PadRight(padnum, ' ');
    }
}
