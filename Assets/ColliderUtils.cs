using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderUtils : MonoBehaviour {

    private  Vector3[] raycastDirections;
    private Vector3 raycastDirections1;
    public GameObject Target;
    public float maxDistance = 100;

    // Use this for initialization
    void Start () {
        raycastDirections = new Vector3[5];
        raycastDirections[0] = new Vector3(0, 1, 0);
        raycastDirections[1] = new Vector3(0, -1, -0);
        raycastDirections[2] = new Vector3(0, 0, 1);
        raycastDirections[3] = new Vector3(-1.41f, 0, -0.5f);
        raycastDirections[4] = new Vector3(1.41f, 0, -0.5f);

        raycastDirections1 = new Vector3(0, 1, 0);
    }

    public bool IsContained(Vector3 targetPoint, Collider collider)
    {
        // A quick check - if the bounds doesn't contain targetPoint, then it definitely can't be contained in the collider
        if (!collider.bounds.Contains(targetPoint))
        {
            Debug.Log("Вне формы");
            return false;
        }

        // The "100f * direction" is a magic number so that we 
        // start far enough from the point.
        foreach (Vector3 direction in raycastDirections)
        {
            Debug.DrawLine(targetPoint, targetPoint + direction, Color.red);
        }

        foreach (Vector3 direction in raycastDirections)
        {
            Ray ray = new Ray(targetPoint, targetPoint + direction);
            RaycastHit dummyHit = new RaycastHit();
            // dummyHit because collider.Raycast requires a RaycastHit
            if (!collider.Raycast(ray, out dummyHit, maxDistance))
            {
                Debug.Log("Один луч не касается");
                return false;
            }
        }
        Debug.Log("В форме");
        return true;
    }

    public bool IsContained2(Vector3 targetPoint, Collider collider)
    {
        // A quick check - if the bounds doesn't contain targetPoint, then it definitely can't be contained in the collider
        if (!collider.bounds.Contains(targetPoint))
        {
            Debug.Log("Вне формы");
            //return false;
        }

        // The "100f * direction" is a magic number so that we 
        // start far enough from the point.
        //Debug.DrawLine(targetPoint, targetPoint + raycastDirections1, Color.red);

        Ray ray = new Ray(targetPoint, raycastDirections1);
        RaycastHit dummyHit = new RaycastHit();
        // dummyHit because collider.Raycast requires a RaycastHit
        if (!collider.Raycast(ray, out dummyHit, maxDistance))
        {

            //Debug.Log("(!collider.Raycast(ray, out dummyHit, maxDistance))" + dummyHit.collider.name + " distance=" + dummyHit.distance);
            //return false;
        }
        if (collider.Raycast(ray, out dummyHit, maxDistance))
        {
            Debug.Log("collider.Raycast(ray, out dummyHit, maxDistance)" + dummyHit.collider.name + " distance=" + dummyHit.distance);
            Debug.DrawLine(targetPoint, dummyHit.point, Color.red);
            //return false;
        }
        //Debug.Log("Конец");
        return true;
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawLine(contact.point, contact.point + contact.normal, Color.black);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Collider Coll = GetComponent<Collider>();
        Vector3 targetPoint = Target.transform.position;
        IsContained2(targetPoint, Coll);
    }


}
