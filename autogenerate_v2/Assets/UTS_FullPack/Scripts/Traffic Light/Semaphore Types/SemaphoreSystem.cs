using System.Collections;
using UnityEngine;

public abstract class SemaphoreSystem : MonoBehaviour
{
    public enum FlickType
    {
        CarGreen,
        Arrow,
        PeopleGreen
    }

    protected FlickType flickType;
    protected int currentFlickCount;
    protected float currentFlickRate;
    protected bool greenFlicking;
    protected bool currentFlickerState;
    
    [SerializeField] protected ViewPeopleSemaphore[] allPeopleLights;
    [SerializeField] protected float greenTime;
    [SerializeField] protected float yellowTime;
    [SerializeField] protected float redTime;
    [SerializeField] protected float peopleTime;
    [SerializeField] protected float flickRate;
    [SerializeField] protected int maxFlickCount;
    
    protected abstract void SetFlow();
    protected abstract void ChangeSemaphoreState();
    protected abstract void StartFlick();
    protected abstract void Flick();
    protected abstract void EndFlick();

    protected abstract IEnumerator YellowOn();
    protected abstract IEnumerator Green();
    public abstract IEnumerator YellowOff();
    public abstract IEnumerator Red();
    protected abstract IEnumerator PeopleLights();

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
}