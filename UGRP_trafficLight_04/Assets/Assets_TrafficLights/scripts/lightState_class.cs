using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightStates
{
    public List<int[]> lightStates = new List<int[]>();

    public LightStates(int lightStateType)
    {
        switch (lightStateType)
        {
            case 0:
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 0, 1, 0, 0 });
                lightStates.Add(new int[] { 0, 0, 1, 0 });
                break;
            case 1:
                lightStates.Add(new int[] { 0, 0, 0, 1 });
                lightStates.Add(new int[] { 1, 0, 0, 0 });
                lightStates.Add(new int[] { 0, 1, 0, 1 });
                lightStates.Add(new int[] { 1, 0, 1, 0 });
                break;
        }
    }
}


public class lightState_class : MonoBehaviour
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
