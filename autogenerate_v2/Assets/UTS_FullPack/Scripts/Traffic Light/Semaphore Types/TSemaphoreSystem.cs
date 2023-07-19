using System.Collections;
using UnityEngine;

public class TSemaphoreSystem : MonoBehaviour
{
    private int semState;
    private int currentFlickCount;
    private float currentFlickRate;
    private bool greenFlicking;
    private bool currentFlickerState;
    private ViewCarSemaphore[] curCarLights;
    
    [SerializeField] private ViewCarSemaphore[] firstWayCarLights;
    [SerializeField] private ViewCarSemaphore[] secondWayCarLights;
    [SerializeField] private ViewPeopleSemaphore[] firstWayPeopleLights;
    [SerializeField] private ViewPeopleSemaphore[] secondWayPeopleLights;
    [SerializeField] protected float greenTime;
    [SerializeField] protected float yellowTime;
    [SerializeField] protected float redTime;
    [SerializeField] protected float peopleTime;
    [SerializeField] protected float flickRate;
    [SerializeField] protected float arrowTime;
    [SerializeField] protected int maxFlickCount;
    
    private void Awake()
    {
        foreach (var semaphore in firstWayCarLights)
        {
            semaphore.ChangeRed(true);
        }
        
        foreach (var semaphore in secondWayCarLights)
        {
            semaphore.ChangeRed(true);
        }
        
        foreach (var semaphore in firstWayPeopleLights)
        {
            semaphore.ChangeRed(true);
        }
        
        foreach (var semaphore in secondWayPeopleLights)
        {
            semaphore.ChangeRed(true);
        }
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
    
    private void SetFlow()
    {
        if (semState == 0)
        {
            curCarLights = firstWayCarLights;
            StartCoroutine(YellowOn());
        }
        else if (semState == 1)
        {
            StartCoroutine(Arrow());
        }
        else if(semState == 2)
        {
            curCarLights = secondWayCarLights;
            
            StartCoroutine(Arrow());
        }
        else if (semState == 3)
        {
            StartCoroutine(PeopleLights());
        }
    }

    private void ChangeSemaphoreState()
    {
        semState = (semState + 1) % 4;
        
        SetFlow();
    }

    private void StartFlick()
    {
        currentFlickCount = 0;
        currentFlickRate = flickRate;
        greenFlicking = true;
    }

    private void Flick()
    {
        currentFlickRate = flickRate;
        currentFlickerState = !currentFlickerState;
        
        if (currentFlickerState)
        {
            if (semState == 0)
            {
                foreach (var semaphore in curCarLights)
                {
                    semaphore.ChangeGreenEmission(true);
                }

                foreach (var semaphore in secondWayPeopleLights)
                {
                    semaphore.ChangeGreenEmission(true);
                }
            }
            else if (semState == 1)
            {
                foreach (var semaphore in curCarLights)
                {
                    semaphore.ChangeArrowEmission(true);
                }
            }
            else if (semState == 2)
            {
                foreach (var semaphore in curCarLights)
                {
                    semaphore.ChangeArrowEmission(true);
                }
            }
            else if(semState == 3)
            {
                foreach (var semaphore in firstWayPeopleLights)
                {
                    semaphore.ChangeGreen(true);
                }
                
                foreach (var semaphore in secondWayPeopleLights)
                {
                    semaphore.ChangeGreen(true);
                }
            }
        }
        else
        {
            if (semState == 0)
            {
                foreach (var semaphore in curCarLights)
                {
                    semaphore.ChangeGreenEmission(false);
                }

                foreach (var semaphore in secondWayPeopleLights)
                {
                    semaphore.ChangeGreenEmission(false);
                }
            }
            else if (semState == 1)
            {
                foreach (var semaphore in curCarLights)
                {
                    semaphore.ChangeArrowEmission(false);
                }
            }
            else if (semState == 2)
            {
                foreach (var semaphore in curCarLights)
                {
                    semaphore.ChangeArrowEmission(false);
                }
            }
            else if(semState == 3)
            {
                foreach (var semaphore in firstWayPeopleLights)
                {
                    semaphore.ChangeGreen(false);
                }
                
                foreach (var semaphore in secondWayPeopleLights)
                {
                    semaphore.ChangeGreen(false);
                }
            }

            currentFlickCount++;
        }
    }

    private void EndFlick()
    {
        greenFlicking = false;

        if (semState == 0)
        {
            foreach (var semaphore in curCarLights)
            {
                semaphore.ChangeGreen(false);
                semaphore.ChangeYellow(true);
            }

            foreach (var semaphore in secondWayPeopleLights)
            {
                semaphore.ChangeGreen(false);
                semaphore.ChangeRed(true);
            }
            
            StartCoroutine(YellowOff());
        }
        else if (semState == 1)
        {
            foreach (var semaphore in curCarLights)
            {
                semaphore.ChangeArrow(false);
            }
            
            StartCoroutine(Red());
        }
        else if (semState == 2)
        {
            foreach (var semaphore in curCarLights)
            {
                semaphore.ChangeArrow(false);
            }
            
            StartCoroutine(Red());
        }
        else if(semState == 3)
        {
            foreach (var semaphore in firstWayPeopleLights)
            {
                semaphore.ChangeGreen(false);
            }
                
            foreach (var semaphore in secondWayPeopleLights)
            {
                semaphore.ChangeGreen(false);
            }
            
            StartCoroutine(Red());
        }
    }

    private IEnumerator YellowOn()
    {
        foreach (var semaphore in curCarLights)
        {
            semaphore.ChangeYellow(true);
        }
        
        yield return new WaitForSeconds(yellowTime);
        
        StartCoroutine(Green());
    }

    private IEnumerator Green()
    {
        foreach (var semaphore in curCarLights)
        {
            semaphore.ChangeRed(false);
            semaphore.ChangeYellow(false);
            semaphore.ChangeGreen(true);
        }
        
        foreach (var semaphore in secondWayPeopleLights)
        {
            semaphore.ChangeRed(false);
            semaphore.ChangeGreen(true);
        }
        
        yield return new WaitForSeconds(greenTime);
        
        StartFlick();
    }

    private IEnumerator YellowOff()
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

    private IEnumerator Red()
    {
        foreach (var semaphore in curCarLights)
        {
            semaphore.ChangeRed(true);
        }

        yield return new WaitForSeconds(redTime);

        ChangeSemaphoreState();
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
    
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Car"))
        {
            if (other.transform.GetComponentInParent<CarAIController>())
            {
                CarAIController car = other.GetComponentInParent<CarAIController>();
                car.INSIDE = true;
            }
        }

        if (other.transform.CompareTag("Bcycle"))
        {
            if (other.transform.GetComponentInParent<BcycleGyroController>())
            {
                BcycleGyroController bcycle = other.GetComponentInParent<BcycleGyroController>();
                bcycle.insideSemaphore = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Car"))
        {
            if (other.transform.GetComponentInParent<CarAIController>())
            {
                CarAIController car = other.GetComponentInParent<CarAIController>();
                car.INSIDE = false;
            }
        }

        if (other.transform.CompareTag("Bcycle"))
        {
            if (other.transform.GetComponentInParent<BcycleGyroController>())
            {
                BcycleGyroController bcycle = other.GetComponentInParent<BcycleGyroController>();
                bcycle.insideSemaphore = false;
            }
        }
    }
    
    private IEnumerator PeopleLights()
    {
        foreach (var semaphore in firstWayPeopleLights)
        {
            semaphore.ChangeRed(false);
            semaphore.ChangeGreen(true);
        }
        
        foreach (var semaphore in secondWayPeopleLights)
        {
            semaphore.ChangeRed(false);
            semaphore.ChangeGreen(true);
        }

        yield return new WaitForSeconds(peopleTime);
        
        StartFlick();
    }
}