using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * LightStateGeneratorV2
 * 이전 버전과의 차이점
 * - 범용성 보다는 110과 200번대 신호에서 Random State Start를 구현하는데 중점을 둠
 * - 생성자 또한 이를 반영함
 */
public class LightStatesGeneratorV2
{
    public List<int[]> lightStates = new List<int[]>();
    
    public LightStatesGeneratorV2(int trafficLightType)
    {
        switch (trafficLightType)
        {
            case 110: // 직진 후 좌회전 기준 신호
                
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 1, 0 });
                lightStates.Add(new int[] { 1, 1, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });
                lightStates.Add(new int[] { 3, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });
                break;
            case 111: // 직진 후 좌회전 기준이 아닌 신호
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });
                lightStates.Add(new int[] { 3, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 1, 0 });
                lightStates.Add(new int[] { 1, 1, 0, 0 });
                break;

            case 112: // 직진 후 좌회전 기준 신호 : 직좌 동시신호와 같이 사용시
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 1, 0 });
                lightStates.Add(new int[] { 1, 1, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });
                break;


            // 여기서부터는 이전 거 그대로 불러옴
            case 210: // 직좌 동시신호 : 112 기준 신호와 같이 사용

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });
                lightStates.Add(new int[] { 3, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                break;
            case 211: // 210의 반대편 신호

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });
                lightStates.Add(new int[] { 3, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });
                break;
            // 직좌 동시신호 모든 사거리에 다 있는 경우
            // 모든 신호가 한번에 멈추는 경우가 있을 때
            case 220:

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
            case 221:

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
            case 222:

                lightStates.Add(new int[] { 1, 0, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                break;
            case 223:
                
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
            
            // 직좌 동시신호 모든 사거리에 다 있는 경우
            // 모든 신호가 한번에 멈추는 경우가 없을 때
            case 230:
                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });
                break;
            case 231:
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });
                break;
            case 232:
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });
                break;

            case 233:
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                break;

            case 310: // 보행자 신호등
            case 320: // 320은 대각선 신호등
                lightStates.Add(new int[] { 1, 0 });
                lightStates.Add(new int[] { 0, 1 });
                break;

            case 410: // 우회전 신호등
                lightStates.Add(new int[] { 1, 0, 0 });
                lightStates.Add(new int[] { 0, 1, 0 });
                lightStates.Add(new int[] { 0, 0, 1 });
                break;

            case 510: // 비보호 좌회전 : 모든 신호가 동시에 안멈춤
                lightStates.Add(new int[] { 1, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0 });
                break;
            case 511: // 비보호 좌회전 : 모든 신호가 동시에 안멈춤
                lightStates.Add(new int[] { 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0 });
                lightStates.Add(new int[] { 1, 0, 0 });
                break;

            case 520: // 비보호 좌회전 : 모든 신호가 동시에 멈춤
                lightStates.Add(new int[] { 1, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0 });
                break;
            case 521:
                lightStates.Add(new int[] { 1, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0 });
                lightStates.Add(new int[] { 1, 0, 0 });
                break;
        }
    }
}

public class LightStateGenerator_V2 : MonoBehaviour
{
    // dummy
}
