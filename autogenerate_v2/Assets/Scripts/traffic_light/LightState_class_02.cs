using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 신호등의 상태들의 조합을 만들어내는 class
public class LightStatesGenerator{
    public List<int[]> lightStates = new List<int[]>();
    public int yellowNum = 0;
    
    // 신호등의 type은 세 자리수로 표현한다.
    // 백의 자리는 신호등이 양 옆 신호를 고려해 진행 신호를 켜면 1, 모든 신호를 고려해 진행 신호를 켜면 2이다.
    // 십의 자리는 그 신호의 고유한 type이다.
    // 일의 자리는 0일 경우 기준이 되는 신호, 1일 경우에는 반대편 신호다. >> 1의 자리가 동일한 경우는 Duration 또한 동일하다.
    public LightStatesGenerator(int trafficLightType){
        switch(trafficLightType){
            case 110: // 직진 후 좌회전
            case 111:
                yellowNum = 2;
                lightStates.Add(new int[]{1,0,0,0});
                lightStates.Add(new int[]{0,0,0,1});
                lightStates.Add(new int[]{0,1,0,0});
                lightStates.Add(new int[]{1,0,1,0});
                lightStates.Add(new int[]{1,1,0,0});
                break;
            case 120: // 좌회전 후 직진
            case 121:
                yellowNum = 2;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 1, 0 });
                lightStates.Add(new int[] { 1, 1, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });            
                break;
            case 130: // 직좌 후 직진                
                yellowNum = 2;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 1 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                break;
            case 131: // 직진 후 직좌 : 직좌 후 직진의 반대편 신호
                yellowNum = 1;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                break;
            case 140: // 좌회전 후 직진
                yellowNum = 2;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 1, 0 });
                lightStates.Add(new int[] { 1, 1, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                break;
            case 141: // 직진 후 직좌 : 좌회전 후 직진의 반대편 신호
                yellowNum = 1;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                break;
            case 150: // 직좌 후 직진
                yellowNum = 2;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 1 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                break;
            case 151: // 직진 후 좌회전 : 직좌 후 직진의 반대편 신호
                yellowNum = 2;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 1, 0 });
                lightStates.Add(new int[] { 1, 1, 0, 0 });
                break;
            case 160: // 좌회전 후 직좌 후 직진
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
            case 161: // 직진 후 직좌 : 좌회전 후 직좌 후 직진의 반대편 신호
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
            case 170: // 직좌 후 직진
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
            case 171: // 직진후 직좌 후 좌회전 : 직좌 후 직진의 반대편 신호
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
            case 180: // 직진 직좌 좌회전
                yellowNum = 2;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 1, 0 });
                lightStates.Add(new int[] { 1, 0, 1, 0 });
                lightStates.Add(new int[] { 1, 1, 0, 0 });
                break;
            case 181: // 직진 적신호 좌회전 : 직진 직좌 좌회전의 반대편 신호
                yellowNum = 2;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 1, 0 });
                lightStates.Add(new int[] { 1, 1, 0, 0 });
                break;
            case 190: // 좌회전 직좌 직진
                yellowNum = 2;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 1, 0 });
                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 1 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                break;
            case 191: // 좌회전 적신호 직진 : 좌회전 직좌 직진의 반대편 신호
                yellowNum = 2;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 1, 0 });
                lightStates.Add(new int[] { 1, 1, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                break;
            case 210: // 직좌 동시신호
                yellowNum = 2;
                lightStates.Add(new int[]{1,0,0,0});
                
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[]{0,0,1,1});
                lightStates.Add(new int[]{0,1,0,0});
                break;
            case 211: // 210의 반대편 신호
                yellowNum = 2;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                
                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });
                break;
            // 직좌 동시신호 모든 사거리에 다 있는 경우
            // 모든 신호가 한번에 멈추는 경우가 있을 때
            case 220: 
                yellowNum = 1;
                lightStates.Add(new int[] { 1, 0, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });
                
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                break;
            case 221:
                yellowNum = 1;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                break;
            case 222:
                yellowNum = 1;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });
                break;
            case 223:
                yellowNum = 1;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                
                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });
                break;
            // 직좌 동시신호 모든 사거리에 다 있는 경우
            // 모든 신호가 한번에 멈추는 경우가 없을 때
            case 230:
                yellowNum = 1;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                break;
            case 231:
                yellowNum = 1;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                break;
            case 232:
                yellowNum = 1;
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });
                break;
            case 233:
                yellowNum = 1;
                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });
                break;
            case 310: // 보행자 신호등
            case 320: // 320은 대각선 신호등
                lightStates.Add(new int[] { 1, 0 });
                lightStates.Add(new int[] { 0, 1 });
                break;
            case 410: // 우회전 신호등
                yellowNum = 1;
                lightStates.Add(new int[] { 1, 0, 0 });
                lightStates.Add(new int[] { 0, 1, 0 });
                lightStates.Add(new int[] { 0, 0, 1 });
                break;
            case 510: // 비보호 좌회전
            case 511:
                yellowNum = 1;
                lightStates.Add(new int[] { 1, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0 });
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
