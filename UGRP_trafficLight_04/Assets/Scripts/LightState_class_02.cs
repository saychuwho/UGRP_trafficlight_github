using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ȣ���� ���µ��� ������ ������ class
public class LightStatesGenerator{
    public List<int[]> lightStates = new List<int[]>();
    public int yellowNum = 0;
    
    // ��ȣ���� type�� �� �ڸ����� ǥ���Ѵ�.
    // ���� �ڸ��� ��ȣ���� �� �� ��ȣ�� ����� ���� ��ȣ�� �Ѹ� 1, ��� ��ȣ�� ����� ���� ��ȣ�� �Ѹ� 2�̴�.
    // ���� �ڸ��� �� ��ȣ�� ������ type�̴�.
    // ���� �ڸ��� 0�� ��� ������ �Ǵ� ��ȣ, 1�� ��쿡�� �ݴ��� ��ȣ��. >> 1�� �ڸ��� ������ ���� Duration ���� �����ϴ�.
    public LightStatesGenerator(int trafficLightType){
        switch(trafficLightType){
            case 110: // ���� �� ��ȸ��
            case 111:
                yellowNum = 2;
                lightStates.Add(new int[]{1,0,0,0});
                lightStates.Add(new int[]{0,0,0,1});
                lightStates.Add(new int[]{0,1,0,0});
                lightStates.Add(new int[]{1,0,1,0});
                lightStates.Add(new int[]{1,1,0,0});
                break;
            case 120: // ��ȸ�� �� ����
            case 121:
                yellowNum = 2;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 1, 0 });
                lightStates.Add(new int[] { 1, 1, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });            
                break;
            case 130: // ���� �� ����                
                yellowNum = 2;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 1 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                break;
            case 131: // ���� �� ���� : ���� �� ������ �ݴ��� ��ȣ
                yellowNum = 1;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                break;
            case 140: // ��ȸ�� �� ����
                yellowNum = 2;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 1, 0 });
                lightStates.Add(new int[] { 1, 1, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                break;
            case 141: // ���� �� ���� : ��ȸ�� �� ������ �ݴ��� ��ȣ
                yellowNum = 1;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                break;
            case 150: // ���� �� ����
                yellowNum = 2;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 1 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                break;
            case 151: // ���� �� ��ȸ�� : ���� �� ������ �ݴ��� ��ȣ
                yellowNum = 2;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 1, 0 });
                lightStates.Add(new int[] { 1, 1, 0, 0 });
                break;
            case 160: // ��ȸ�� �� ���� �� ����
                yellowNum = 2;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 1, 0 });
                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 1 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                break;
            case 161: // ���� �� ���� : ��ȸ�� �� ���� �� ������ �ݴ��� ��ȣ
                yellowNum = 1;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                break;
            case 170: // ���� �� ����
                yellowNum = 2;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 1 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                break;
            case 171: // ������ ���� �� ��ȸ�� : ���� �� ������ �ݴ��� ��ȣ
                yellowNum = 2;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 1, 0 });
                lightStates.Add(new int[] { 1, 0, 1, 0 });
                lightStates.Add(new int[] { 1, 1, 0, 0 });
                break;
            case 180: // ���� ���� ��ȸ��
                yellowNum = 2;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 1, 0 });
                lightStates.Add(new int[] { 1, 0, 1, 0 });
                lightStates.Add(new int[] { 1, 1, 0, 0 });
                break;
            case 181: // ���� ����ȣ ��ȸ�� : ���� ���� ��ȸ���� �ݴ��� ��ȣ
                yellowNum = 2;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 1, 0 });
                lightStates.Add(new int[] { 1, 1, 0, 0 });
                break;
            case 190: // ��ȸ�� ���� ����
                yellowNum = 2;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 1, 0 });
                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 1 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                break;
            case 191: // ��ȸ�� ����ȣ ���� : ��ȸ�� ���� ������ �ݴ��� ��ȣ
                yellowNum = 2;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 1, 0 });
                lightStates.Add(new int[] { 1, 1, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                break;
            case 210: // ���� ���ý�ȣ
                yellowNum = 1;
                lightStates.Add(new int[]{1,0,0,0});
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[]{0,0,1,1});
                lightStates.Add(new int[]{0,1,0,0});
                break;
            case 211: // 210�� �ݴ��� ��ȣ
                yellowNum = 1;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                break;
            case 220: // ���� ���ý�ȣ ��� ��Ÿ��� �� �ִ� ���
                yellowNum = 1;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                break;
            case 221:
                yellowNum = 1;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                break;
            case 222:
                yellowNum = 1;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                break;
            case 223:
                yellowNum = 1;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                break;
            case 310: // ������ ��ȣ��
                lightStates.Add(new int[] { 1, 0 });
                lightStates.Add(new int[] { 0, 1 });
                break;
            case 410: // ��ȸ�� ��ȣ��
                yellowNum = 1;
                lightStates.Add(new int[] { 1, 0, 0 });
                lightStates.Add(new int[] { 0, 1, 0 });
                lightStates.Add(new int[] { 0, 0, 1 });
                break;
        }
    }
}


public class LightState_class_02 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
