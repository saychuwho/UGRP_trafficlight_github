using UnityEngine;

public class BcycleGyroController : MonoBehaviour
{
    public VehiclesAllow allow { get; set; }
    private Animator anim;
    private Rigidbody rigBody;
    private BoxCollider bc;
    private MovePath movePath;
    private Vector3 fwdVector;
    private float curMoveSpeed;
    private float startSpeed;

    [Tooltip("Speed bicyclist / Скорость велосипедиста")] public float moveSpeed;
    [Tooltip("Acceleration / Ускорение")] public float increaseSpeed;
    [Tooltip("Braking / Торможение")] public float decreaseSpeed;
    [Tooltip("Swing speed / Скорость поворота")] public float speedRotation;
    [HideInInspector] public bool insideSemaphore;
    [HideInInspector] public bool tempStop;

    [SerializeField] [Tooltip("Set your animation speed / Выставить свою скорость анимации?")] private bool _overrideDefaultAnimationMultiplier;
    [SerializeField] [Tooltip("Animation speed / Скорость анимации")] private float _customAnimationMultiplier = 1f;

    public float CustomAnimationMultiplier
    {
        get { return _customAnimationMultiplier; }
        set { _customAnimationMultiplier = value; }
    }

    public float SPEED_ROTATION
    {
        get{return speedRotation;}
        set{speedRotation = value;}
    }

    public bool OverrideDefaultAnimationMultiplier
    {
        get { return _overrideDefaultAnimationMultiplier; }
        set { _overrideDefaultAnimationMultiplier = value; }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigBody = GetComponent<Rigidbody>();
        bc = GetComponentInChildren<BoxCollider>();
        movePath = GetComponent<MovePath>();
    }

    private void Start()
    {
        startSpeed = moveSpeed;

        BoxCollider[] box = GetComponentsInChildren<BoxCollider>();
        bc = box[0];
    }

    private void Update()
    {
        GetPath();
        Move();
        PushRay();

        fwdVector = new Vector3(transform.position.x + transform.forward.x, transform.position.y + 0.5f, transform.position.z + transform.forward.z * bc.size.z);

        if (anim != null)
        {
            if (_overrideDefaultAnimationMultiplier)
            {
                anim.speed = curMoveSpeed * _customAnimationMultiplier;
            }
            else
            {
                anim.speed = curMoveSpeed * 1.2f;
            }
        }
    }

    private void Move()
    {
        if (tempStop)
        {
            curMoveSpeed = Mathf.Lerp(curMoveSpeed, 0.0f, Time.deltaTime * decreaseSpeed);

            if(curMoveSpeed < 0.15f)
            {
                curMoveSpeed = 0.0f;
            }
        }
        else
        {
            curMoveSpeed = Mathf.Lerp(curMoveSpeed, moveSpeed, Time.deltaTime * increaseSpeed);
        }

        if (rigBody.velocity.magnitude > curMoveSpeed)
        {
            rigBody.velocity = rigBody.velocity.normalized * curMoveSpeed;
        }
    }

    private void GetPath()
    {
        Vector3 randFinishPos = new Vector3(movePath.finishPos.x + movePath.randXFinish, movePath.finishPos.y, movePath.finishPos.z + movePath.randZFinish);

        Vector3 targetPos = new Vector3(randFinishPos.x, rigBody.transform.position.y, randFinishPos.z);

        var richPointDistance = Vector3.Distance(Vector3.ProjectOnPlane(rigBody.transform.position, Vector3.up), Vector3.ProjectOnPlane(randFinishPos, Vector3.up));

        if (richPointDistance < 2.0f && ((movePath.loop) || (!movePath.loop && movePath.targetPoint > 0 && movePath.targetPoint < movePath.targetPointsTotal)))
        {
            if (movePath.forward)
            {
                if (movePath.targetPoint < movePath.targetPointsTotal)
                {
                    targetPos = movePath.walkPath.getNextPoint(movePath.w, movePath.targetPoint + 1);
                }
                else
                {
                    targetPos = movePath.walkPath.getNextPoint(movePath.w, 0);
                }

                targetPos.y = rigBody.transform.position.y;
            }
            else
            {
                if (movePath.targetPoint > 0)
                {
                    targetPos = movePath.walkPath.getNextPoint(movePath.w, movePath.targetPoint - 1);
                }
                else
                {
                    targetPos = movePath.walkPath.getNextPoint(movePath.w, movePath.targetPointsTotal);
                }

                targetPos.y = rigBody.transform.position.y;
            }
        }

        Vector3 targetVector = targetPos - rigBody.transform.position;

        if (targetVector != Vector3.zero)
        {
            Quaternion look = Quaternion.identity;
            look = Quaternion.Lerp(rigBody.transform.rotation, Quaternion.LookRotation(targetVector),
                Time.deltaTime * speedRotation);

            look.x = rigBody.transform.rotation.x;
            look.z = rigBody.transform.rotation.z;

            rigBody.transform.rotation = look;
        }

        if (richPointDistance > movePath._walkPointThreshold)
        {
            if (Time.deltaTime > 0)
            {
                Vector3 velocity = movePath.finishPos - rigBody.transform.position;

                velocity.y = rigBody.velocity.y;
                rigBody.velocity = new Vector3(velocity.normalized.x * curMoveSpeed, velocity.y, velocity.normalized.z * curMoveSpeed);
            }
        }
        else if (richPointDistance <= movePath._walkPointThreshold && movePath.forward)
        {
            if (movePath.targetPoint != movePath.targetPointsTotal)
            {
                movePath.targetPoint++;

                movePath.finishPos = movePath.walkPath.getNextPoint(movePath.w, movePath.targetPoint);
            }
            else if (movePath.targetPoint == movePath.targetPointsTotal)
            {
                if (movePath.loop)
                {
                    movePath.finishPos = movePath.walkPath.getStartPoint(movePath.w);

                    movePath.targetPoint = 0;
                }
                else
                {
                    movePath.walkPath.SpawnPoints[movePath.w].AddToSpawnQuery(new MovePathParams { });
                    Destroy(gameObject);
                }
            }

        }
        else if (richPointDistance <= movePath._walkPointThreshold && !movePath.forward)
        {
            if (movePath.targetPoint > 0)
            {
                movePath.targetPoint--;

                movePath.finishPos = movePath.walkPath.getNextPoint(movePath.w, movePath.targetPoint);
            }
            else if (movePath.targetPoint == 0)
            {
                if (movePath.loop)
                {
                    movePath.finishPos = movePath.walkPath.getNextPoint(movePath.w, movePath.targetPointsTotal);

                    movePath.targetPoint = movePath.targetPointsTotal;
                }
                else
                {
                    movePath.walkPath.SpawnPoints[movePath.w].AddToSpawnQuery(new MovePathParams { });
                    Destroy(gameObject);
                }
            }
        }
    }

    private void PushRay()
    {
        RaycastHit hit;
        Ray fwdRay = new Ray(fwdVector, transform.forward * 10);

        if (Physics.Raycast(fwdRay, out hit, 20))
        {
            float distance = Vector3.Distance(fwdVector, hit.point);

            if(hit.transform.CompareTag("Car"))
            {                                    
                GameObject car = (hit.transform.GetComponentInChildren<ParentOfTrailer>()) ? hit.transform.GetComponent<ParentOfTrailer>().PAR : hit.transform.gameObject;

                if(car != null)
                { 
                    ReasonsStoppingCars.CarInView(car, rigBody, distance, startSpeed, ref moveSpeed, ref tempStop);
                }
            }
            else if(hit.transform.CompareTag("Bcycle"))
            {
                ReasonsStoppingCars.BcycleGyroInView(hit.transform.GetComponentInChildren<BcycleGyroController>(), rigBody, distance, startSpeed, ref moveSpeed, ref tempStop);
            }
            else if (hit.transform.CompareTag("PeopleSemaphore"))
            {
                ReasonsStoppingCars.SemaphoreInView(hit.transform.GetComponent<SemaphoreMovementSide>(), allow, distance, startSpeed, insideSemaphore, ref moveSpeed, ref tempStop);
            }
            else if (hit.transform.CompareTag("Player"))
            {
                ReasonsStoppingCars.PlayerInView(hit.transform, distance, startSpeed, ref moveSpeed, ref tempStop);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        if (bc != null)
        {
            Gizmos.DrawRay(new Vector3(transform.position.x + transform.forward.x, transform.position.y + 0.5f, transform.position.z + transform.forward.z * bc.size.z), transform.forward * 20);
        }
    }
}