using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Passersby : MonoBehaviour
{
    private Rigidbody rigBody;
    private Animator animator;
    public MovePath movePath { get; set; }
    private Passersby nearPassersby;
    private SemaphoreMovementSide nearSemaphore;
    private Transform nearCar;
    private Transform nearPlayer;
    private PeopleAnimState lastState;
    private float curMoveSpeed;
    private float moveSpeed;
    private float startWalkSpeed;
    private float startRunSpeed;
    private bool redSemaphore;
    private bool insideSemaphore;
    private bool tempStop;
    private bool timeToWalk = true;

    [Tooltip("Layers of car, traffic light, pedestrians, player / Слои автомобиля, светофора, пешеходов, игрока")] public LayerMask targetMask;
    [HideInInspector] public LayerMask obstacleMask;
    [SerializeField] private PeopleAnimState animationState;
    [SerializeField] [Tooltip("Walking speed / Скорость ходьбы")] private float walkSpeed;
    [SerializeField] [Tooltip("Running speed / Скорость бега")] private float runSpeed;
    [SerializeField] [Tooltip("Swing speed / Скорость поворота")] private float speedRotation;
    [SerializeField] [Tooltip("Viewing Angle / Угол обзора")] private float viewAngle;
    [SerializeField] [Tooltip("Radius of visibility / Радиус видимости")] private float viewRadius;
    [SerializeField] [Tooltip("Distance to pedestrian / Расстояние до пешехода")] private float distToPeople;

    [SerializeField] [Tooltip("Set your animation speed / Установить свою скорость анимации?")] private bool _overrideDefaultAnimationMultiplier;
    [SerializeField] [Tooltip("Speed animation walking / Скорость анимации ходьбы")] private float _customWalkAnimationMultiplier = 1.0f;
    [SerializeField] [Tooltip("Running animation speed / Скорость анимации бега")] private float _customRunAnimationMultiplier = 1.0f;

    public float CurMoveSpeed => curMoveSpeed;
    
    public PeopleAnimState ANIMATION_STATE
    {
        get { return animationState; }
        set { animationState = value; }
    }
    public PeopleAnimState LastState
    {
        get{ return lastState;}
        set{lastState = value;}
    }
    public float WALK_SPEED
    {
        get { return walkSpeed; }
        set { walkSpeed = value; }
    }
    public float RUN_SPEED
    {
        get { return runSpeed; }
        set { runSpeed = value; }
    }
    public float SPEED_ROTATION
    {
        get { return speedRotation; }
        set { speedRotation = value; }
    }
    public float VIEW_ANGLE
    {
        get { return viewAngle; }
        set { viewAngle = value; }
    }
    public float VIEW_RADIUS
    {
        get { return viewRadius; }
        set { viewRadius = value; }
    }
    public bool INSIDE
    {
        get { return insideSemaphore; }
        set { insideSemaphore = value; }
    }
    public bool RED
    {
        get { return redSemaphore; }
        set { redSemaphore = value; }
    }
    public float DIST_TO_PEOPLE
    {
        get{return distToPeople;}
        set{distToPeople = value;}
    }
    public float CustomWalkAnimationMultiplier
    {
        get { return _customWalkAnimationMultiplier; }
        set { _customWalkAnimationMultiplier = value; }
    }
    public float CustomRunAnimationMultiplier
    {
        get {return _customRunAnimationMultiplier;}
        set {_customRunAnimationMultiplier = value;}
    }
    public bool OverrideDefaultAnimationMultiplier
    {
        get { return _overrideDefaultAnimationMultiplier; }
        set { _overrideDefaultAnimationMultiplier = value; }
    }

    private void Awake()
    {
        rigBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        movePath = GetComponent<MovePath>();
    }

    private void Start()
    {
        lastState = animationState;
        startWalkSpeed = walkSpeed;
        startRunSpeed = runSpeed;

        animator.CrossFade(animationState.ToString(), 0.1f, 0, Random.Range(0.0f, 1.0f));
    }

    private void Update()
    {
        ActionNearPassersby();
        ActionNearSemaphore();
        ActionNearCar();
        ActionNearPlayer();

        Move();

        if (insideSemaphore)
        {
            if (redSemaphore)
            {
                animationState = PeopleAnimState.run;
            }
        }

        if(tempStop)
        {
            if(curMoveSpeed < 0.3f)
            {
                animator.Play("idle1");
            }
        }
        else
        {
            animator.Play(animationState.ToString());
        }

        curMoveSpeed = Mathf.Lerp(curMoveSpeed, moveSpeed, Time.deltaTime * 4.5f);

        AISight();
    }

    private void FixedUpdate()
    {
        GetPath();
    }

    private void Move()
    {
        switch (animationState)
        {
            case PeopleAnimState.idle1:
                moveSpeed = 0.0f;

                if (curMoveSpeed < 0.15f)
                {
                    curMoveSpeed = 0.0f;
                }
                break;

            case PeopleAnimState.walk:
                moveSpeed = walkSpeed;

                if (_overrideDefaultAnimationMultiplier)
                {
                    animator.speed = curMoveSpeed * _customWalkAnimationMultiplier;
                }
                else
                {
                    animator.speed = curMoveSpeed * 1.2f;
                }
                break;

            case PeopleAnimState.run:
                moveSpeed = runSpeed;

                if (_overrideDefaultAnimationMultiplier)
                {
                    animator.speed = curMoveSpeed * _customRunAnimationMultiplier;
                }
                else
                {
                    animator.speed = curMoveSpeed / 3;
                }
                break;
        }

        tempStop = (animationState == PeopleAnimState.idle1) ? true : false;
    }

    private void AISight()
    {
        nearPassersby = null;
        nearSemaphore = null;
        nearCar = null;
        nearPlayer = null;

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        List<Passersby> passersby = new List<Passersby>();
        List<SemaphoreMovementSide> semaphores = new List<SemaphoreMovementSide>();
        List<Transform> cars = new List<Transform>();
        List<Transform> players = new List<Transform>();

        SortTargetsInView<Passersby>(passersby, targetsInViewRadius, "People");
        SortTargetsInView<SemaphoreMovementSide>(semaphores, targetsInViewRadius, "PeopleSemaphore");
        SortTargetsInView<Transform>(cars, targetsInViewRadius, "Car");
        SortTargetsInView<Transform>(players, targetsInViewRadius, "Player");

        if (nearPassersby == null && nearSemaphore == null)
        {
            if (nearCar == null && nearPlayer == null)
            {
                if(!insideSemaphore && !redSemaphore)
                    animationState = lastState;
            }
        }
    }

    private void GetPath()
    {
        Vector3 randFinishPos = new Vector3(movePath.finishPos.x + movePath.randXFinish, movePath.finishPos.y, movePath.finishPos.z + movePath.randZFinish);

        Vector3 targetPos = new Vector3(randFinishPos.x, rigBody.transform.position.y, randFinishPos.z);

        var richPointDistance = Vector3.Distance(Vector3.ProjectOnPlane(rigBody.transform.position, Vector3.up), Vector3.ProjectOnPlane(randFinishPos, Vector3.up));

        if (richPointDistance < 0.2f && animationState == PeopleAnimState.walk && ((movePath.loop) || (!movePath.loop && movePath.targetPoint > 0 && movePath.targetPoint < movePath.targetPointsTotal)))
        {
            if (movePath.forward)
            {
                if (movePath.targetPoint < movePath.targetPointsTotal)
                    targetPos = movePath.walkPath.getNextPoint(movePath.w, movePath.targetPoint + 1);
                else
                    targetPos = movePath.walkPath.getNextPoint(movePath.w, 0);
                targetPos.y = rigBody.transform.position.y;
            }
            else
            {
                if (movePath.targetPoint > 0)
                    targetPos = movePath.walkPath.getNextPoint(movePath.w, movePath.targetPoint - 1);
                else
                    targetPos = movePath.walkPath.getNextPoint(movePath.w, movePath.targetPointsTotal);
                targetPos.y = rigBody.transform.position.y;
            }
        }

        if (richPointDistance < 0.5f && animationState == PeopleAnimState.run && ((movePath.loop) || (!movePath.loop && movePath.targetPoint > 0 && movePath.targetPoint < movePath.targetPointsTotal)))
        {
            if (movePath.forward)
            {
                if (movePath.targetPoint < movePath.targetPointsTotal)
                    targetPos = movePath.walkPath.getNextPoint(movePath.w, movePath.targetPoint + 1);
                else
                    targetPos = movePath.walkPath.getNextPoint(movePath.w, 0);
                targetPos.y = rigBody.transform.position.y;
            }
            else
            {
                if (movePath.targetPoint > 0)
                    targetPos = movePath.walkPath.getNextPoint(movePath.w, movePath.targetPoint - 1);
                else
                    targetPos = movePath.walkPath.getNextPoint(movePath.w, movePath.targetPointsTotal);
                targetPos.y = rigBody.transform.position.y;
            }
        }

        Vector3 direction = targetPos - transform.position;

        if (direction != Vector3.zero)
        {
            Vector3 newDir = Vector3.zero;

            rigBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            newDir = Vector3.RotateTowards(transform.forward, direction, speedRotation * Time.deltaTime, 0.0f);

            transform.rotation = Quaternion.LookRotation(newDir);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), speedRotation * Time.deltaTime);
        }

        if (richPointDistance > movePath._walkPointThreshold)
        {
            if (Time.deltaTime > 0)
            {
                transform.position += transform.forward * curMoveSpeed * Time.deltaTime;
                //rigBody.MovePosition(transform.position + transform.forward * curMoveSpeed * Time.fixedDeltaTime);
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
                    if (movePath != null)
                    {
                        movePath.walkPath.SpawnPoints[movePath.w].AddToSpawnQuery(new MovePathParams { });
                    }

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

    private void SortTargetsInView<T>(IList<T> list, Collider[] targetsInViewRadius, string tag)
    {
        foreach(Collider c in targetsInViewRadius)
        {
            if(!c.transform.CompareTag(tag) || c.transform.position == transform.position) continue;

            if(typeof(T) == typeof(Passersby))
            {
                if(c.transform.parent.transform.parent.name != transform.parent.transform.parent.name || c.GetComponent<MovePath>().w != movePath.w || c.GetComponent<MovePath>().forward != movePath.forward) continue;
            }

            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 target = c.transform.position - transform.position;

            if (Vector3.Dot(forward, target) > 0)
            {
                list.Add(c.GetComponent<T>());
            }
        }

        if(typeof(T) == typeof(Passersby))
        {
            List<Passersby> passersby = list as List<Passersby>;
            NearTarget<Passersby>(passersby);
        }
        else if(typeof(T) == typeof(SemaphoreMovementSide))
        {
            List<SemaphoreMovementSide> semaphore = list as List<SemaphoreMovementSide>;
            NearTarget<SemaphoreMovementSide>(semaphore);
        }
        else if(typeof(T) == typeof(Transform))
        {
            if (tag == "Car")
            {
                List<Transform> cars = list as List<Transform>;
                NearTarget<Transform>(cars, "Car");
            }
            else if(tag == "Player")
            {
                List<Transform> players = list as List<Transform>;
                NearTarget<Transform>(players, "Player");
            }
        }
    }

    private void NearTarget<T>(IList<T> list, string tag = "")
    {
        foreach(T t in list)
        {
            Transform target = null;
            
            if(typeof(T) == typeof(Passersby))
            {
                Passersby p = t as Passersby;
                target = p.GetComponent<Transform>();
            }
            else if(typeof(T) == typeof(SemaphoreMovementSide))
            {
                SemaphoreMovementSide s = t as SemaphoreMovementSide;
                target = s.GetComponent<Transform>();
            }
            else if(typeof(T) == typeof(Transform))
            {
                if (tag == "Car")
                {
                    Transform car = t as Transform;
                    target = car.GetComponent<Transform>();
                }
                else if(tag == "Player")
                {
                    Transform player = t as Transform;
                    target = player.GetComponent<Transform>();
                }
            }

            Vector3 dirToTarget = ((new Vector3(target.position.x, target.position.y, target.position.z)) - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                float dstToNearTarget = 0.0f;

                if(typeof(T) == typeof(Passersby))
                {
                    if (nearPassersby != null)
                    {
                        dstToNearTarget = Vector3.Distance(transform.position, nearPassersby.transform.position);
                    }
                }
                else if(typeof(T) == typeof(SemaphoreMovementSide))
                {
                    if (nearSemaphore != null)
                    {
                        dstToNearTarget = Vector3.Distance(transform.position, nearSemaphore.transform.position);
                    }
                }
                else if(typeof(T) == typeof(Transform))
                {
                    if (tag == "Car")
                    {
                        if (nearCar != null)
                        {
                            dstToNearTarget = Vector3.Distance(transform.position, nearCar.transform.position);
                        }
                    }
                    else if(tag == "Player")
                    {
                        if (nearPlayer != null)
                        {
                            dstToNearTarget = Vector3.Distance(transform.position, nearPlayer.transform.position);
                        }
                    }
                }

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    if(typeof(T) == typeof(Passersby))
                    {
                        if (nearPassersby == null)
                        {
                            nearPassersby = t as Passersby;
                            continue;
                        }
                        else
                        {
                            if (dstToTarget < dstToNearTarget)
                            {
                                nearPassersby = t as Passersby;
                            }
                        }
                    }
                    else if(typeof(T) == typeof(SemaphoreMovementSide))
                    {
                        if (nearSemaphore == null)
                        {
                            nearSemaphore = t as SemaphoreMovementSide;
                            continue;
                        }
                        else
                        {
                            if (dstToTarget < dstToNearTarget)
                            {
                                nearSemaphore = t as SemaphoreMovementSide;
                            }
                        }
                    }
                    else if(typeof(T) == typeof(Transform))
                    {
                        if (tag == "Car")
                        {
                            if (nearCar == null)
                            {
                                nearCar = t as Transform;
                                continue;
                            }
                            else
                            {
                                if (dstToTarget < dstToNearTarget)
                                {
                                    nearCar = t as Transform;
                                }
                            }
                        }
                        else if(tag == "Player")
                        {
                            if (nearPlayer == null)
                            {
                                nearPlayer = t as Transform;
                                continue;
                            }
                            else
                            {
                                if (dstToTarget < dstToNearTarget)
                                {
                                    nearPlayer = t as Transform;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void ActionNearPassersby()
    {
        if (nearPassersby != null)
        {
            float distance = Vector3.Distance(transform.position, nearPassersby.transform.position);

            if (distance < distToPeople)
            {
                if (nearPassersby.ANIMATION_STATE == PeopleAnimState.idle1)
                {
                    animationState = PeopleAnimState.idle1;
                    timeToWalk = false;
                }
                else
                {
                    walkSpeed = nearPassersby.walkSpeed;
                    runSpeed = nearPassersby.runSpeed;
                }
            }

            if(animationState == PeopleAnimState.idle1 && nearPassersby.ANIMATION_STATE != PeopleAnimState.idle1)
            {
                if (!timeToWalk)
                {
                    StartCoroutine(StartMove());
                    timeToWalk = true;
                }
            }
        }
        else
        {
            walkSpeed = startWalkSpeed;
            runSpeed = startRunSpeed;
        }
    }

    private void ActionNearSemaphore()
    {
        if (nearSemaphore == null) return;

        float distance = Vector3.Distance(transform.position, nearSemaphore.transform.position);

        if(distance < 25.0f)
        {
            if(nearSemaphore.PeopleMoveState)
            {
                animationState = lastState;
            }
            else
            {
                if(!insideSemaphore)
                {
                    animationState = PeopleAnimState.idle1;
                }
            }
        }
    }

    private void ActionNearCar()
    {
        if (nearCar == null) return;

        float distance = Vector3.Distance(transform.position, nearCar.transform.position);

        if (distance < 10.0f)
        {
            animationState = PeopleAnimState.idle1;
        }
        else
        {
            animationState = lastState;
        }
    }

    private void ActionNearPlayer()
    {
        if (nearPlayer == null) return;

        float distance = Vector3.Distance(transform.position, nearPlayer.transform.position);

        if (distance < 10.0f)
        {
            animationState = PeopleAnimState.idle1;
        }
        else
        {
            animationState = lastState;
        }
    }

    IEnumerator StartMove()
    {
        yield return new WaitForSeconds(1.2f);
        animationState = lastState;
        tempStop = false;
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}