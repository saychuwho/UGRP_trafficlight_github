using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SemaphoreMovementSide : MonoBehaviour
{
    private List<Passersby> passersbies = new List<Passersby>();
    private bool arrowMoveState;
    private bool forwardMoveState;
    private bool peopleMoveState;

    public int PassersbiesOnCrosswalk => passersbies.Count;
    public bool ArrowMoveState => arrowMoveState;
    public bool ForwardMoveState => forwardMoveState;
    public bool PeopleMoveState => peopleMoveState;
    
    public bool flicker { get; set; }

    [SerializeField] private ViewCarSemaphore[] carSemaphores;
    [SerializeField] private ViewPeopleSemaphore[] peopleSemaphores;

    private void Awake()
    {
        foreach (var semaphore in carSemaphores)
        {
            semaphore.OnCarGreenChanged += ChangeForwardMoveState;
            semaphore.OnArrowChanged += ChangeArrowMoveState;
        }
        
        foreach (var semaphore in peopleSemaphores)
        {
            semaphore.OnPeopleGreenChanged += ChangePeopleMoveState;
        }
    }

    private void ChangeForwardMoveState(bool state)
    {
        forwardMoveState = state;
    }
    
    private void ChangeArrowMoveState(bool state)
    {
        arrowMoveState = state;
    }
    
    private void ChangePeopleMoveState(bool state)
    {
        peopleMoveState = state;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("People"))
        {
            var passersby = other.GetComponentInParent<Passersby>();

            if (passersby != null)
            {
                passersbies.Add(passersby);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("People"))
        {
            if(other.transform.GetComponentInParent<Passersby>())
            {
                Passersby people = other.GetComponentInParent<Passersby>();
                people.INSIDE = true;

                if(!peopleMoveState)
                {
                    people.RED = true;
                }
                else
                {
                    people.RED = false;
                }
            }
        }

        if (other.CompareTag("Car"))
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
        if (other.CompareTag("Car"))
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

        if (other.CompareTag("People"))
        {
            var passersby = other.GetComponentInParent<Passersby>();
            
            if(passersby != null)
            {
                StartCoroutine(StopInside(passersby));
                passersbies.Remove(passersby);
            }
        }
    }

    public bool IsPassersbiesMoving()
    {
        for (var i = 0; i < passersbies.Count; i++)
        {
            if(passersbies[i].CurMoveSpeed < 0.1f) continue;

            return true;
        }

        return false;
    }

    IEnumerator StopInside(Passersby passersby)
    {
        yield return new WaitForSeconds(1.0f);

        passersby.INSIDE = false;
        passersby.RED = false;
        passersby.ANIMATION_STATE = passersby.LastState;
    }
}