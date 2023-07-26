using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTrailer : MonoBehaviour
{
    /// <summary>
    /// Префаб прицепа
    /// </summary>
	[SerializeField, Tooltip("Prefab trailer / Префаб прицепа")]
    private GameObject trailerPrefab;

    /// <summary>
    /// Координата относительно грузовика, в которой появится прицеп
    /// </summary>
	[SerializeField, Tooltip("Coordinates relative to the truck in which the trailer appears / Координата относительно грузовика, в которой появится прицеп")]
    private Vector3 trailerInitPosition;

    /// <summary>
    /// Прицеп
    /// </summary>
    public GameObject trailer;

    public WheelCollider[] wc;

    void Start()
    {
        Init();

        trailer.transform.position = new Vector3(trailer.transform.position.x, trailer.transform.position.y + 0.3f, trailer.transform.position.z);
    }
    public void OnDestroy()
    {
        if(Application.isPlaying)
        Destroy(trailer);
        else
        {
            DestroyImmediate(trailer);
        }
    }
    /// <summary>
    /// Инициализация прицепа
    /// </summary>
    public void Init()
    {
        if(trailerPrefab == null) return;
        
        if (trailer == null)
        {
            //trailer = Instantiate(trailerPrefab, transform.TransformPoint(trailerInitPosition), transform.rotation);
            trailer = Instantiate(trailerPrefab, transform.TransformPoint(trailerInitPosition), transform.localRotation);
        }
        // установка ConfigurableJoint
        ConfigurableJoint cjTrailer = trailer.GetComponent<ConfigurableJoint>();
        cjTrailer.connectedBody = GetComponent<Rigidbody>();

        SetIgnoreCollisions();
        trailer.transform.rotation = transform.localRotation;

        ParentOfTrailer parOfTrailer = trailer.GetComponent<ParentOfTrailer>();

        parOfTrailer.PAR = gameObject;
        parOfTrailer.InitTag();
    }

    /// <summary>
    /// Установка игноров для физики. Коллайдер прицепа не должен взаимодействовать с 1) колайдером авто, 2) с коллайдерами колёс этого авто
    /// </summary>
    private void SetIgnoreCollisions()
    {
        Collider[] trailerCollider = trailer.GetComponentsInChildren<Collider>();

        Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();

        for (int i = 0; i < colliders.Length; i++)
        {
            for (int a = 0; a < trailerCollider.Length; a++)
            {
                Physics.IgnoreCollision(trailerCollider[a], colliders[i]);
            }
        }
    }

    void OnDrawGizmos()
    {
        // Рисуем точку в которую будет установлен трейлер
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.TransformPoint(trailerInitPosition), 0.05f);
    }

}