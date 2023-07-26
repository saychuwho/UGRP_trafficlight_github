using System.Collections;
using UnityEngine;

public class TogetherArrowSemaphoreSystem : StandardSemaphoreSystem
{
    protected override void SetFlow()
    {
        semState = (semState + 1) % 4;
        
        curCarLights = blockFirstWay ? firstWayCarLights : secondWayCarLights;
        
        if (semState < 3)
        {
            flickType = FlickType.CarGreen;
            StartCoroutine(YellowOn());
        }
        else
        {
            flickType = FlickType.PeopleGreen;
            StartCoroutine(PeopleLights());
        }
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
                    semaphore.ChangeArrow(false);
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
                
                semState = 0;
            
                StartCoroutine(Red());
                break;
        }
    }
    
    protected override IEnumerator Green()
    {
        foreach (var semaphore in curCarLights)
        {
            semaphore.ChangeRed(false);
            semaphore.ChangeYellow(false);
            semaphore.ChangeGreen(true);
            semaphore.ChangeArrow(true);
        }
        
        yield return new WaitForSeconds(greenTime);
        
        StartFlick();
    }
}