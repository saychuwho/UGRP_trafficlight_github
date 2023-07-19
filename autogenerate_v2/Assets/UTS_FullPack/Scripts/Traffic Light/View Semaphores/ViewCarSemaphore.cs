using System;
using UnityEngine;

public class ViewCarSemaphore : TLGraphicsControl
{
    private Material yellowMat;
    private Material arrowMat;
    
    [Header("Car Semaphore Settings")]
    [SerializeField] private Texture2D yellowG;
    [SerializeField] private Texture2D arrowG;
    [SerializeField] protected MeshRenderer mshY;
    [SerializeField] private MeshRenderer mshA;

    public event Action<bool> OnCarGreenChanged;
    public event Action<bool> OnArrowChanged;
    
    protected override void Awake()
    {
        base.Awake();
        gameObject.tag = "red";
        yellowMat = mshY.materials[1];
        arrowMat = mshA.materials[1];
    }

    public override void ChangeGreen(bool greenState)
    {
        base.ChangeGreen(greenState);
       if(greenState)  gameObject.tag = "green";
        OnCarGreenChanged?.Invoke(greenState);
    }

    public void ChangeYellow(bool yellowState)
    {

        if(yellowState)
        {
            gameObject.tag = "yellow";
            yellowMat.EnableKeyword("_EMISSION");
            yellowMat.SetTexture("_EmissionMap", yellowG);
        }
        else
        {
            yellowMat.DisableKeyword("_EMISSION");
        }
    }
    
    public void ChangeArrow(bool arrowState)
    {
        if(arrowState)
        {
            gameObject.tag = "rightarrow";
            arrowMat.EnableKeyword("_EMISSION");
            arrowMat.SetTexture("_EmissionMap", arrowG);
        }
        else
        {
            gameObject.tag = "red";
            arrowMat.DisableKeyword("_EMISSION");
        }
        
        OnArrowChanged?.Invoke(arrowState);
    }

    public void ChangeArrowEmission(bool state)
    {
        if (state)
        {
            arrowMat.EnableKeyword("_EMISSION");
        }
        else
        {
            arrowMat.DisableKeyword("_EMISSION");
        }
    }
}