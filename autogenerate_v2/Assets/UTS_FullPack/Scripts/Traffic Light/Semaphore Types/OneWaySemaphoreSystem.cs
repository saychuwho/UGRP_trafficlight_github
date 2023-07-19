using System.Collections;
using UnityEngine;

public class OneWaySemaphoreSystem : SemaphoreSystem
{
    protected ViewCarSemaphore[] curCarLights;
    private bool peopleSemaphoreState;
    
    [SerializeField] protected ViewCarSemaphore[] firstWayCarLights;
    
    protected virtual void Awake()
    {
        foreach (var semaphore in firstWayCarLights)
        {
            semaphore.ChangeRed(true);
        }
        
        foreach (var semaphore in allPeopleLights)
        {
            semaphore.ChangeRed(true);
        }
        
        curCarLights = firstWayCarLights;
    }

    protected void Start()
    {
        SetFlow();
    }

    private void Update()
    {
        if(!greenFlicking) return;
        
        if(currentFlickCount >= maxFlickCount)
        {
            EndFlick();
            return;
        }

        currentFlickRate -= Time.deltaTime;

        if (!(currentFlickRate < 0)) return;

        Flick();
    }

    protected override void SetFlow()
    {
        if (peopleSemaphoreState)
        {
            flickType = FlickType.PeopleGreen;
            StartCoroutine(PeopleLights());
        }
        else
        {
            flickType = FlickType.CarGreen;
            StartCoroutine(YellowOn());
        }
    }

    protected override void StartFlick()
    {
        currentFlickCount = 0;
        currentFlickRate = flickRate;
        greenFlicking = true;
    }

    protected override void Flick()
    {
        currentFlickRate = flickRate;
        currentFlickerState = !currentFlickerState;
        
        if (currentFlickerState)
        {
            switch (flickType)
            {
                case FlickType.CarGreen:
                    foreach (var semaphore in curCarLights)
                    {
                        semaphore.ChangeGreenEmission(true);
                    }
                    break;
                case FlickType.PeopleGreen:
                    foreach (var semaphore in allPeopleLights)
                    {
                        semaphore.ChangeGreenEmission(true);
                    }
                    break;
            }
        }
        else
        {
            switch (flickType)
            {
                case FlickType.CarGreen:
                    foreach (var semaphore in curCarLights)
                    {
                        semaphore.ChangeGreenEmission(false);
                    }
                    break;
                case FlickType.PeopleGreen:
                    foreach (var semaphore in allPeopleLights)
                    {
                        semaphore.ChangeGreenEmission(false);
                    }
                    break;
            }

            currentFlickCount++;
        }
    }

    protected override void EndFlick()
    {
        greenFlicking = false;

        switch (flickType)
        {
            case FlickType.CarGreen:
                foreach (var semaphore in curCarLights)
                {
                    semaphore.ChangeGreen(false);
                    semaphore.ChangeYellow(true);
                }
            
                StartCoroutine(YellowOff());
                break;
            case FlickType.PeopleGreen:
                foreach (var semaphore in allPeopleLights)
                {
                    semaphore.ChangeGreen(false);
                    semaphore.ChangeRed(true);
                }
            
                StartCoroutine(Red());
                break;
        }
    }

    protected override IEnumerator YellowOn()
    {
        foreach (var semaphore in curCarLights)
        {
            semaphore.ChangeYellow(true);
        }
        
        yield return new WaitForSeconds(yellowTime);
        
        StartCoroutine(Green());
    }

    protected override IEnumerator Green()
    {
        foreach (var semaphore in curCarLights)
        {
            semaphore.ChangeRed(false);
            semaphore.ChangeYellow(false);
            semaphore.ChangeGreen(true);
        }
        
        yield return new WaitForSeconds(greenTime);
        
        StartFlick();
    }
    
    public override IEnumerator YellowOff()
    {
        foreach (var semaphore in curCarLights)
        {
            semaphore.ChangeYellow(true);
        }

        yield return new WaitForSeconds(yellowTime);

        foreach (var semaphore in curCarLights)
        {
            semaphore.ChangeYellow(false);
        }

        StartCoroutine(Red());
    }
    
    public override IEnumerator Red()
    {
        foreach (var semaphore in curCarLights)
        {
            semaphore.ChangeRed(true);
        }

        yield return new WaitForSeconds(redTime);

        ChangeSemaphoreState();
    }
    
    protected override IEnumerator PeopleLights()
    {
        foreach (var semaphore in allPeopleLights)
        {
            semaphore.ChangeRed(false);
            semaphore.ChangeGreen(true);
        }

        yield return new WaitForSeconds(peopleTime);
        
        StartFlick();
    }
    
    protected override void ChangeSemaphoreState()
    {
        peopleSemaphoreState = !peopleSemaphoreState;
        
        SetFlow();
    }
}