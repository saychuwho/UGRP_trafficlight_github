using System;
using UnityEngine;

public class TLGraphicsControl : MonoBehaviour
{
    private Material greenMat;
    private Material redMat;

    [Header("Base Settings")]
    [SerializeField] private Texture2D greenG;
    [SerializeField] private Texture2D redG;   
    [SerializeField] private MeshRenderer mshG;
    [SerializeField] private MeshRenderer mshR;

    protected virtual void Awake()
    {
        greenMat = mshG.materials[1];
        redMat = mshR.materials[1];
    }
    
    public virtual void ChangeGreen(bool greenState)
    {
        if(greenState)
        {
            greenMat.EnableKeyword("_EMISSION");
            greenMat.SetTexture("_EmissionMap", greenG);
        }
        else
        {
            greenMat.DisableKeyword("_EMISSION");
        }
    }

    public void ChangeRed(bool redState)
    {
        if(redState)
        {
            if (redMat == null)
            {                
                redMat = mshR.materials[1];
            }
            
            redMat.EnableKeyword("_EMISSION");
            redMat.SetTexture("_EmissionMap", redG);
        }
        else
        {
            redMat.DisableKeyword("_EMISSION");
        }
    }

    public void ChangeGreenEmission(bool enable)
    {
        if (enable)
        {
            greenMat.EnableKeyword("_EMISSION");
        }
        else
        {
            greenMat.DisableKeyword("_EMISSION");
        }
    }
}