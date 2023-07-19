using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class NPCStats : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rigbody;
    private Passersby passersby;
    private PeopleController peopleController;
    private bool hit;

    [SerializeField] private bool destroy;
    [SerializeField] private float timeToDestroy;
    [SerializeField] private float boundsMass;
    [SerializeField] private List<Rigidbody> ragdollElements;
    [SerializeField] private Collider[] col;

    private void Awake()
    {
        rigbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        passersby = GetComponent<Passersby>();
        peopleController = GetComponent<PeopleController>();

        ragdollElements.AddRange(GetComponentsInChildren<Rigidbody>());

        rigbody.mass = boundsMass;

        for (var i = 0; i < ragdollElements.Count; i++)
        {
            ragdollElements[i].mass = boundsMass;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car") && !hit)
        {
            EnablePhysics();
        }
    }

    public void EnablePhysics()
    {
        hit = true;

        for (int i = 0; i < ragdollElements.Count; i++)
        {
            ragdollElements[i].isKinematic = false;
        }

        foreach (var collider in col)
        {
            Destroy(collider);
        }

        Destroy(rigbody);
        anim.enabled = false;

        if (passersby != null)
            passersby.enabled = false;

        if (peopleController != null)
            peopleController.enabled = false;

        if(destroy)
        {
            StartCoroutine(DestroyPeople());
        }
    }

    private IEnumerator DestroyPeople()
    {
        yield return new WaitForSeconds(timeToDestroy);

        if (passersby != null)
        {
            passersby.movePath.walkPath.SpawnPoints[passersby.movePath.w].AddToSpawnQuery(new MovePathParams());
        }

        Destroy(gameObject);
    }
}