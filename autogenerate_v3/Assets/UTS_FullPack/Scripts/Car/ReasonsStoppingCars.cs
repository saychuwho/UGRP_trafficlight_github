using UnityEngine;

public class ReasonsStoppingCars : MonoBehaviour
{
	public static void CarInView(GameObject go, Rigidbody rigbody, float distance, float startSpeed, ref float moveSpeed, ref bool tempStop, float distanceToCar = 15)
    {
        if (go == null) return;

        CarAIController car = go.GetComponentInParent<CarAIController>();

        if (distance >= distanceToCar)
        {
            if (car.TEMP_STOP)
            {
                moveSpeed = startSpeed * 0.5f;
            }
            else
            {
                moveSpeed = startSpeed;
            }

            tempStop = false;
        }
        else if (distance < distanceToCar)
        {
            if (car.GetComponentInParent<Rigidbody>().velocity.magnitude < rigbody.velocity.magnitude)
            {
                tempStop = true;
            }
            else
            {
                if (!car.TEMP_STOP)
                {
                    tempStop = false;
                }
            }
        }
    }

    public static void SemaphoreInView(SemaphoreMovementSide semaphore, VehiclesAllow allow, float distance, float startSpeed, bool insideSemaphore, ref float moveSpeed, ref bool tempStop, float distanceToSem = 10)
    {
        if (distance >= distanceToSem)
        {
            if (semaphore.ForwardMoveState && allow == VehiclesAllow.Forward || semaphore.ArrowMoveState && allow == VehiclesAllow.Turn)
            {
                if (semaphore.PassersbiesOnCrosswalk > 0)
                {
                    moveSpeed = startSpeed * 0.5f;
                }
                else
                {
                    if (semaphore.flicker)
                    {
                        if (!insideSemaphore)
                        {
                            moveSpeed = startSpeed * 0.5f;
                        }
                        else
                        {
                            moveSpeed = startSpeed;
                            tempStop = false;
                        }
                    }
                    else
                    {
                        moveSpeed = startSpeed;
                        tempStop = false;
                    }
                }
            }
            else
            {
                if (!insideSemaphore)
                {
                    moveSpeed = startSpeed * 0.5f;
                }
                else
                {
                    moveSpeed = startSpeed;
                    tempStop = false;
                }
            }
        }
        else
        {
            if (semaphore.ForwardMoveState && allow == VehiclesAllow.Forward || (semaphore.ArrowMoveState && allow == VehiclesAllow.Turn))
            {
                if (semaphore.PassersbiesOnCrosswalk > 0)
                {
                    tempStop = true;
                }
                else
                {
                    moveSpeed = startSpeed;
                    tempStop = false;
                }
            }
            else
            {
                if (!insideSemaphore)
                {
                    tempStop = true;
                }
                else
                {
                    if (semaphore.PassersbiesOnCrosswalk > 0)
                    {
                        tempStop = true;
                    }
                    else
                    {
                        tempStop = false;
                        moveSpeed = startSpeed;
                    }
                }
            }
        }
    }

    public static void PlayerInView(Transform player, float distance, float startSpeed, ref float moveSpeed, ref bool tempStop)
    {
        if (distance >= 8.0f)
        {
            moveSpeed = startSpeed * 0.5f;
        }
        else
        {
            tempStop = true;
        }
    }

    public static void BcycleGyroInView(BcycleGyroController controller, Rigidbody rigbody, float distance, float startSpeed, ref float moveSpeed, ref bool tempStop)
    {
        if (distance >= 9.0f)
        {
            if (controller.tempStop)
            {
                moveSpeed = startSpeed * 0.5f;
            }
            else
            {
                moveSpeed = startSpeed;
            }

            tempStop = false;
        }
        else if (distance < 9.0f)
        {
            if (controller.GetComponent<Rigidbody>().velocity.magnitude < rigbody.velocity.magnitude)
            {
                tempStop = true;
            }
            else
            {
                if (!controller.tempStop)
                {
                    tempStop = false;
                }
            }
        }
    }
}