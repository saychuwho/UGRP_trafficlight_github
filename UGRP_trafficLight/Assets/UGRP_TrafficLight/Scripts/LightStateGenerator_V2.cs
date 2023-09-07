using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * LightStateGeneratorV2
 * ���� �������� ������
 * - ���뼺 ���ٴ� 110�� 200���� ��ȣ���� Random State Start�� �����ϴµ� ������ ��
 * - ������ ���� �̸� �ݿ���
 */
public class LightStatesGeneratorV2
{
    public List<int[]> lightStates = new List<int[]>();
    
    public LightStatesGeneratorV2(int trafficLightType)
    {
        switch (trafficLightType)
        {
            case 110: // ���� �� ��ȸ�� ���� ��ȣ
                
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 1, 0 });
                lightStates.Add(new int[] { 1, 1, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });
                lightStates.Add(new int[] { 3, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });
                break;
            case 111: // ���� �� ��ȸ�� ������ �ƴ� ��ȣ
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });
                lightStates.Add(new int[] { 3, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 1, 0 });
                lightStates.Add(new int[] { 1, 1, 0, 0 });
                break;

            case 112: // ���� �� ��ȸ�� ���� ��ȣ : ���� ���ý�ȣ�� ���� ����
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                lightStates.Add(new int[] { 1, 0, 1, 0 });
                lightStates.Add(new int[] { 1, 1, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });
                break;


            // ���⼭���ʹ� ���� �� �״�� �ҷ���
            case 210: // ���� ���ý�ȣ : 112 ���� ��ȣ�� ���� ���

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });
                lightStates.Add(new int[] { 3, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                break;
            case 211: // 210�� �ݴ��� ��ȣ

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });
                lightStates.Add(new int[] { 3, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });

                lightStates.Add(new int[] { 0, 0, 1, 1 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });

                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 2, 0, 0, 0 });
                break;
            // ���� ���ý�ȣ ��� ��Ÿ��� �� �ִ� ���
            // ��� ��ȣ�� �ѹ��� ���ߴ� ��찡 ���� ��
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
            
            // ���� ���ý�ȣ ��� ��Ÿ��� �� �ִ� ���
            // ��� ��ȣ�� �ѹ��� ���ߴ� ��찡 ���� ��
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

            case 310: // ������ ��ȣ��
            case 320: // 320�� �밢�� ��ȣ��
                lightStates.Add(new int[] { 1, 0 });
                lightStates.Add(new int[] { 0, 1 });
                break;

            case 410: // ��ȸ�� ��ȣ��
                lightStates.Add(new int[] { 1, 0, 0 });
                lightStates.Add(new int[] { 0, 1, 0 });
                lightStates.Add(new int[] { 0, 0, 1 });
                break;

            case 510: // ��ȣ ��ȸ�� : ��� ��ȣ�� ���ÿ� �ȸ���
                lightStates.Add(new int[] { 1, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0 });
                break;
            case 511: // ��ȣ ��ȸ�� : ��� ��ȣ�� ���ÿ� �ȸ���
                lightStates.Add(new int[] { 0, 0, 1 });
                lightStates.Add(new int[] { 0, 1, 0 });
                lightStates.Add(new int[] { 1, 0, 0 });
                break;

            case 520: // ��ȣ ��ȸ�� : ��� ��ȣ�� ���ÿ� ����
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
