using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Linq;

public class Data_reader : MonoBehaviour
{
    public string FilePath;
    private List<string> scenario_data;
    public string ego_position;
    public string car_action;
    public string ped1_position;
    public string ped2_position;
    public int line_num;
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
        scenario_data = ReadFile(FilePath)[0];
        // 0 : ego position ->   1 or 2
        ego_position = scenario_data[0];

        //1 : car action - 0: 직진, 1 : 우회전, 2: 좌회전, 3: U턴
        if (scenario_data[1] == "0")
        {
            car_action = "straight";
        }
        else if (scenario_data[1] == "1")
        {
            car_action = "right_turn";
        }
        else if (scenario_data[1] == "2")
        {
            car_action = "left_turn";
        }
        else if (scenario_data[1] == "3")
        {
            car_action = "U_turn";
        }

        //2 : ped postion - 0 : 없음, 1 : 보행 중
        if (scenario_data[2] == "0")
        {
            ped1_position = "none";
        }
        else if (scenario_data[2] == "1")
        {
            ped1_position = "exist";
        }
        //3 : ped1 postion - 0 : 없음, 1 : 보행 중
        if (scenario_data[3] == "0")
        {
            ped2_position = "none";
        }
        else if (scenario_data[3] == "1")
        {
            ped2_position = "exist";
        }
        //ped_position = scenario_data[2];
    }

  
    // Update is called once per frame
    void Update()
    {
        
    }
}
