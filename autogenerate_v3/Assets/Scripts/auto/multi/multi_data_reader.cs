using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class multi_data_reader : MonoBehaviour
{
    public string FilePath;
    private List<List<string>> scenario_data;
    public List<string> ego_position;
    public List<string> car_action;
    public List<string> ped1_position;
    public List<string> ped2_position;
    public List<string> ped_signal1_condition;
    public List<string> ped_signal2_condition;
    public List<string> ped_signal1_light;
    public List<string> ped_signal2_light;
    public List<string> car_signal1_light;
    public List<string> car_signal2_light;
    public List<string> right_signal_light;
    public int line_num;
    public int environment_offset;
    //first_file_read
    List<List<string>> ReadFile(string FilePath)
    {
        FileInfo fileInfo = new FileInfo(FilePath);
        List<List<string>> data = new List<List<string>>(); ;
        if (fileInfo.Exists)
        {
            StreamReader streamReader = new StreamReader(FilePath);
            string file_contents = streamReader.ReadToEnd();
            streamReader.Close();
            string[] lines = file_contents.Split('\n');
            line_num = lines.Length;
            for (int i = 0; i < line_num; i++)
            {
                string[] splited_line = lines[i].Split(' ');
                List<string> lineData = new List<string>(splited_line);
                data.Add(lineData);
            }
        }
        return data;

    }
    // Start is called before the first frame update
    void Awake()
    {
        scenario_data = ReadFile(FilePath);
       
        for(int i = 0; i < scenario_data.Count; i++) {
            // 0 : ego position ->   1 or 2
            ego_position.Add(scenario_data[i][0]);

            //1 : car action - 0: 직진, 1 : 우회전, 2: 좌회전, 3: U턴
            if (scenario_data[i][1] == "0")
            {
                car_action.Add("straight");
            }
            else if (scenario_data[i][1] == "1")
            {
                car_action.Add("right_turn");
            }
            else if (scenario_data[i][1] == "2")
            {
                car_action.Add("left_turn");
            }
            else if (scenario_data[i][1] == "3")
            {
                car_action.Add("U_turn");
            }

            //2 : ped postion - 0 : 없음, 1 : 보행 중
            if (scenario_data[i][2] == "0")
            {
                ped1_position.Add("none");
            }
            else if (scenario_data[i][2] == "1")
            {
                ped1_position.Add("exist");
            }

            //3 : ped1 postion - 0 : 없음, 1 : 보행 중
            if (scenario_data[i][3] == "0")
            {
                ped2_position.Add("none");
            }
            else if (scenario_data[i][3] == "1")
            {
                ped2_position.Add("exist");
            }

            //4 : ped signal1 postion - 0 : 없음, 1 : 있음
            if (scenario_data[i][4] == "0")
            {
                ped_signal1_condition.Add("none");
            }
            else if (scenario_data[i][4] == "1")
            {
                ped_signal1_condition.Add("exist");
            }

            //5 : ped signal2 postion - 0 : 없음, 1 : 있음
            if (scenario_data[i][5] == "0")
            {
                ped_signal2_condition.Add("none");
            }
            else if (scenario_data[i][5] == "1")
            {
                ped_signal2_condition.Add("exist");
            }


            //6 : ped signal 1 light - 0 : red, 1 : green
            if (scenario_data[i][6] == "0")
            {
                ped_signal1_light.Add("red");
            }
            else if (scenario_data[i][6] == "1")
            {
                ped_signal1_light.Add("green");
            }

            //7 : ped signal 2 light - 0 : red, 1 : green
            if (scenario_data[i][7] == "0")
            {
                ped_signal2_light.Add("red");
            }
            else if (scenario_data[i][7] == "1")
            {
                ped_signal2_light.Add("green");
            }

            //8 : car signal1 light
            //0 : red, 1 : yellow, 2 : green, 3:  red + left 4 : red + yellow(직진 후 좌회전의 경우)
            if (scenario_data[i][8] == "0")
            {
                car_signal1_light.Add("red");
            }
            else if (scenario_data[i][8] == "1")
            {
                car_signal1_light.Add("yellow");
            }
            else if (scenario_data[i][8] == "2")
            {
                car_signal1_light.Add("green");
            }
            else if (scenario_data[i][8] == "3")
            {
                car_signal1_light.Add("red+left");
            }
            else if (scenario_data[i][8] == "4")
            {
                car_signal1_light.Add("red+yellow");
            }
           

            //9 : car signal2 light
            //0 : red, 1 : yellow, 2 : green, 3:  red + left 4 : red + yellow
            if (scenario_data[i][9] == "0")
            {
                car_signal2_light.Add("red");
            }
            else if (scenario_data[i][9] == "1")
            {
                car_signal2_light.Add("yellow");
            }
            else if (scenario_data[i][8] == "2")
            {
                car_signal2_light.Add("green");
            }
            else if (scenario_data[i][8] == "3")
            {
                car_signal2_light.Add("red+left");
            }
            else if (scenario_data[i][8] == "4")
            {
                car_signal2_light.Add("red+yellow");
            }


            //10 : right signal light
            //0 : red, 1 : yellow, 2 : green ,3 : none
            if (scenario_data[i][10] == "0")
            {
                right_signal_light.Add("red");
            }
            else if (scenario_data[i][10] == "1")
            {
                right_signal_light.Add("yellow");
            }
            else if (scenario_data[i][10] == "2")
            {
                right_signal_light.Add("green");
            }
            else if (scenario_data[i][10] == "3")
            {
                right_signal_light.Add("none");
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
