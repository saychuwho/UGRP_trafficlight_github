using System.Collections;
using UnityEngine;

public class StandardSemaphoreSystem : OneWaySemaphoreSystem
{
    protected int semState;
    protected bool cachedWay;
    
    [SerializeField] protected ViewCarSemaphore[] secondWayCarLights;
    [SerializeField] protected float arrowTime;
    [SerializeField] protected bool blockFirstWay;
    
    protected override void Awake()
    {
        base.Awake();

        foreach (var semaphore in secondWayCarLights)
        {
            semaphore.ChangeRed(true);
        }

        cachedWay = blockFirstWay;
    }

    protected override void SetFlow()
    {
        semState = (semState + 1) % 6;

        curCarLights = blockFirstWay ? firstWayCarLights : secondWayCarLights;

        
        if (semState < 3)
        {
            flickType = FlickType.CarGreen;
            StartCoroutine(YellowOn());
        }
        else if(semState >= 3 && semState < 5)
        {
            flickType = FlickType.Arrow;
            StartCoroutine(Arrow());
        }
        else
        {
            flickType = FlickType.PeopleGreen;
            StartCoroutine(PeopleLights());
        }

        Debug.Log(flickType);

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
                case FlickType.Arrow:
                    foreach (var semaphore in curCarLights)
                    {
                        semaphore.ChangeArrowEmission(true);
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
                case FlickType.Arrow:
                    foreach (var semaphore in curCarLights)
                    {
                        semaphore.ChangeArrowEmission(false);
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
            case FlickType.Arrow:
                foreach (var semaphore in curCarLights)
                {
                    semaphore.ChangeArrow(false);
                }
            
                StartCoroutine(Red());
                break;
            case FlickType.PeopleGreen:
                foreach (var semaphore in allPeopleLights)
                {
                    semaphore.ChangeGreen(false);
                    semaphore.ChangeRed(true);
                }

                semState = 0;
                
                StartCoroutine(Red());
                break;
        }
    }
    
    private IEnumerator Arrow()
    {
        foreach (var sem in curCarLights)
        {
            sem.ChangeArrow(true);
        }
        
        yield return new WaitForSeconds(arrowTime);
        
        StartFlick();
    }

    protected override void ChangeSemaphoreState()
    {
        blockFirstWay = semState < 1 ? cachedWay : !blockFirstWay;

        SetFlow();
    }
}