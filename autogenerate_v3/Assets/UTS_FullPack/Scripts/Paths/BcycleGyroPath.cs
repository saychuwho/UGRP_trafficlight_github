using UnityEngine;
using System.Collections.Generic;

public class BcycleGyroPath : WalkPath
{
    [Tooltip("Direction of movement / Направление движения. Левостороннее, правостороннее, итд.")] [SerializeField] private EnumDir direction;

    [Tooltip("Bicyclist Speed/ Скорость велосипедиста")] public float moveSpeed = 8.0f;
    [Tooltip("Acceleration / Ускорение")] public float increaseSpeed = 2.0f;
    [Tooltip("Braking / Торможение")] public float decreaseSpeed = 5.0f;
    [Range(0.1f, 5.0f)] [Tooltip("Offset from the line along the X axis / Смещение от линии по оси X")] public float randXPos = 0.1f;
    [Range(0.1f, 5.0f)] [Tooltip("Offset from the line along the Z axis / Смещение от линии по оси Z")] public float randZPos = 0.1f;
    [Tooltip("Скорость поворота")] public float speedRotation = 5.0f;

    [HideInInspector] [SerializeField] [Tooltip("Ignore pedestrian colliders? / Игнорировать коллайдеры пешеходов?")] private bool _ignorePeople = false;
    [HideInInspector] [SerializeField] [Tooltip("Customize your animation speed / Настроить свою скорость анимации?")] private bool _overrideDefaultAnimationMultiplier = true;
    [HideInInspector] [SerializeField] [Tooltip("Animation speed / Скорость анимации")] private float _customAnimationMultiplier = 2.0f;
    [Tooltip("Distance to next point / Расстояние до следующей точки")] public float nextPointThreshold = 3;

    private void Start()
    {
        if(_ignorePeople)
        {
            Physics.IgnoreLayerCollision(12, 8, true);  
        }                 
    }

    public override void DrawCurved(bool withDraw, EnumDir direct = EnumDir.Forward)
    {
        base.DrawCurved(withDraw, direction);
    }

    public override void CreateSpawnPoints()
    {
        SpawnPoints = new SpawnPoint[points.GetLength(0)];

        for (int i = 0; i < points.GetLength(0); i++)
        {
            var startPoint = _forward[i] ? points[i, 0] : points[i, points.GetLength(1) - 1];
            var nextPoint = _forward[i] ? points[i, 2] : points[i, points.GetLength(1) - 3];

            SpawnPoints[i] = SpawnPoint.PeopleCreate(
                string.Format("SpawnPoint (Path {0})", i + 1),
                startPoint,
                nextPoint,
                lineSpacing,
                i,
                _forward[i],
                this,
                3f,
                1f
            );
        }
    }

    public override void SpawnOnePeople(int w, bool forward)
    {
        List<GameObject> pfb = new List<GameObject>(walkingPrefabs);

        for (int i = pfb.Count - 1; i >= 0; i--)
        {
            if (pfb[i] == null)
            {
                pfb.RemoveAt(i);
            }
        }

        walkingPrefabs = pfb.ToArray();
        int prefabNum = Random.Range(0, walkingPrefabs.Length);
        var people = gameObject;

        if (!forward)
        {
            people = Instantiate(walkingPrefabs[prefabNum], points[w, pointLength[0] - 2], Quaternion.identity) as GameObject;
        }
        else
        {
            people = Instantiate(walkingPrefabs[prefabNum], points[w, 1], Quaternion.identity) as GameObject;
        }

        var movePath = people.AddComponent<MovePath>();
        var controller = people.AddComponent<BcycleGyroController>();

        InitializeBcycleGyro(ref controller);

        movePath.randXFinish = Random.Range(-randXPos, randXPos);
        movePath.randZFinish = Random.Range(-randZPos, randZPos);

        people.transform.parent = par.transform;
        movePath.walkPath = this;

        if (!forward)
        {
            movePath.InitStartPosition(w, pointLength[0] - 3, loopPath, forward);
            people.transform.LookAt(points[w, pointLength[0] - 3]);
        }
        else
        {
            movePath.InitStartPosition(w, 1, loopPath, forward);
            people.transform.LookAt(points[w, 2]);
        }

        movePath._walkPointThreshold = nextPointThreshold;
    }

    public override void SpawnPeople()
    {
        List<GameObject> pfb = new List<GameObject>(walkingPrefabs);

        for (int i = pfb.Count - 1; i >= 0; i--)
        {
            if (pfb[i] == null)
            {
                pfb.RemoveAt(i);
            }
        }

        walkingPrefabs = pfb.ToArray();

        if (points == null) DrawCurved(false);

        if (par == null)
        {
            par = new GameObject();
            par.transform.parent = gameObject.transform;
            par.name = "walkingObjects";
        }

        int pathPointCount;

        if (!loopPath)
        {
            pathPointCount = pointLength[0] - 2;
        }
        else
        {
            pathPointCount = pointLength[0] - 1;
        }

        if (pathPointCount < 2) return;

        var pCount = loopPath ? pointLength[0] - 1 : pointLength[0] - 2;

        for (int wayIndex = 0; wayIndex < numberOfWays; wayIndex++)
        {
            _distances = new float[pCount];

            float pathLength = 0f;

            for (int i = 1; i < pCount; i++)
            {
                Vector3 vector;
                if (loopPath && i == pCount - 1)
                {
                    vector = points[wayIndex, 1] - points[wayIndex, pCount];
                }
                else
                {
                    vector = points[wayIndex, i + 1] - points[wayIndex, i];
                }

                pathLength += vector.magnitude;
                _distances[i] = pathLength;
            }

            bool forward = false;

            switch (direction.ToString())
            {
                case "Forward":
                    forward = true;
                    break;
                case "Backward":
                    forward = false;
                    break;
                case "HugLeft":
                    forward = (wayIndex + 2) % 2 == 0;
                    break;
                case "HugRight":
                    forward = (wayIndex + 2) % 2 != 0;
                    break;
                case "WeaveLeft":
                    forward = wayIndex != 1 && wayIndex != 2 && (wayIndex - 1) % 4 != 0 && (wayIndex - 2) % 4 != 0;
                    break;
                case "WeaveRight":
                    forward = wayIndex == 1 || wayIndex == 2 || (wayIndex - 1) % 4 == 0 || (wayIndex - 2) % 4 == 0;
                    break;
            }

            int peopleCount = Mathf.FloorToInt((Density * pathLength) / _minimalObjectLength);
            float segmentLen = _minimalObjectLength + (pathLength - (peopleCount * _minimalObjectLength)) / peopleCount;

            int[] pickList = CommonUtils.GetRandomPrefabIndexes(peopleCount, ref walkingPrefabs);

            Vector3[] pointArray = new Vector3[_distances.Length];

            for (int i = 1; i < _distances.Length; i++)
            {
                pointArray[i - 1] = points[wayIndex, i];
            }

            pointArray[_distances.Length - 1] = loopPath ? points[wayIndex, 1] : points[wayIndex, _distances.Length];

            for (int peopleIndex = 0; peopleIndex < peopleCount; peopleIndex++)
            {
                var people = gameObject;
                var randomShift = Random.Range(-segmentLen / 3f, segmentLen / 3f) + (wayIndex * segmentLen);
                var finalRandomDistance = (peopleIndex + 1) * segmentLen + randomShift;

                Vector3 routePosition = GetRoutePosition(pointArray, finalRandomDistance, pCount, loopPath);

                float XPos = Random.Range(-randXPos, randXPos);
                float ZPos = Random.Range(-randZPos, randZPos);

                routePosition = new Vector3(routePosition.x + XPos, routePosition.y, routePosition.z + ZPos);
                Vector3 or;

                RaycastHit[] rrr = Physics.RaycastAll(or = new Vector3(routePosition.x, routePosition.y + 10000, routePosition.z), Vector3.down, Mathf.Infinity);

                bool isSemaphore = false;

                for (int i = 0; i < rrr.Length; i++)
                {
                    if (rrr[i].collider.GetComponent<SemaphoreSystem>() != null || rrr[i].collider.GetComponent<SemaphoreMovementSide>() != null)
                    {
                        isSemaphore = true;
                    }
                }

                if (isSemaphore) continue;

                float dist = 0;
                int bestCandidate = 0;

                rrr = Physics.RaycastAll(or = new Vector3(routePosition.x, routePosition.y + highToSpawn, routePosition.z), Vector3.down, Mathf.Infinity);

                for (int i = 0; i < rrr.Length; i++)
                {
                    if (dist < Vector3.Distance(rrr[0].point, or))
                    {
                        bestCandidate = i;
                        dist = Vector3.Distance(rrr[0].point, or);
                    }
                }

                if (rrr.Length > 0)
                {
                    routePosition.y = rrr[bestCandidate].point.y;
                }

                people = Instantiate(walkingPrefabs[pickList[peopleIndex]], routePosition, Quaternion.identity) as GameObject;

                var movePath = people.AddComponent<MovePath>();
                var controller = people.AddComponent<BcycleGyroController>();

                InitializeBcycleGyro(ref controller);

                movePath.randXFinish = XPos;
                movePath.randZFinish = ZPos;

                people.transform.parent = par.transform;
                movePath.walkPath = this;
                movePath._walkPointThreshold = nextPointThreshold;

                movePath.InitStartPosition(wayIndex,
                    GetRoutePoint((peopleIndex + 1) * segmentLen + randomShift, wayIndex, pCount, forward, loopPath), loopPath, forward);

                movePath.SetLookPosition();
            }
        }
    }

    private void InitializeBcycleGyro(ref BcycleGyroController _bcycleGyro)
    {
        float randMoveSpeed = moveSpeed + Random.Range(moveSpeed * -0.15f, moveSpeed * 0.15f);

        _bcycleGyro.moveSpeed = randMoveSpeed;
        _bcycleGyro.increaseSpeed = increaseSpeed;
        _bcycleGyro.decreaseSpeed = decreaseSpeed;
        _bcycleGyro.speedRotation = speedRotation;

        _bcycleGyro.OverrideDefaultAnimationMultiplier = _overrideDefaultAnimationMultiplier;
        _bcycleGyro.CustomAnimationMultiplier = _customAnimationMultiplier;
    }
}