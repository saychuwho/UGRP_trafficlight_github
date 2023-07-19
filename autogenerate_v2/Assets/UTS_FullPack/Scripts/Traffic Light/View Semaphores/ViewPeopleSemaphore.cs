using System;

public class ViewPeopleSemaphore : TLGraphicsControl
{
    public event Action<bool> OnPeopleGreenChanged;

    private void Start()
    {
        gameObject.tag = "red";
    }
    public override void ChangeGreen(bool greenState)
    {
        string tag = greenState ? "green" : "red";
        gameObject.tag = tag;
                
        base.ChangeGreen(greenState);
        OnPeopleGreenChanged?.Invoke(greenState);
    }
  
}