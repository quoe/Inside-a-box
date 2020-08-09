using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ColliderFromSphere : MonoBehaviour {

    private  Vector3[] raycastDirections;
    public GameObject Target;
    public int MaxIterations = 10000;
    Collider TargetСollider;
    Vector3 colliderBoundsMin;
    Vector3 colliderBoundsMax;
    Vector3 Pos;
    private bool isInside;
    private bool isSaved = false;

    // Use this for initialization
    void Start () {
        NewPos();
    }

    public bool InCollider(GameObject TargetObject) {
        Collider TargetColl = TargetObject.GetComponent<Collider>();
        Vector3 TargetPos_ = TargetObject.transform.position;
        Vector3 SelfPos = transform.position;
        Ray ray = new Ray(SelfPos, (TargetPos_ - SelfPos));
        RaycastHit Hit = new RaycastHit();
        if (TargetСollider.Raycast(ray, out Hit, 100))
        {
            Debug.DrawLine(SelfPos, Hit.point, Color.red);
            return false;
        }
        else { return true; }
    }

    private void NewPos()
    {
        TargetСollider = Target.GetComponent<Collider>();
        colliderBoundsMin = TargetСollider.bounds.min;
        colliderBoundsMax = TargetСollider.bounds.max;
        Pos.x = Random.Range(colliderBoundsMin.x, colliderBoundsMax.x);
        Pos.y = Random.Range(colliderBoundsMin.y, colliderBoundsMax.y);
        Pos.z = Random.Range(colliderBoundsMin.z, colliderBoundsMax.z);
        transform.position = Pos;
    }

    public bool IsBoundsContained(Vector3 targetPoint, Collider collider)
    {
        if (collider.bounds.Contains(targetPoint)) { return true; }
        return false;
    }

    public void File_AppendText(string FilePath, string AppendString)
    {
        using (StreamWriter sw = File.AppendText(FilePath))
        {
            sw.WriteLine(AppendString);
        }
    }

    void Update()
    {
        isInside = InCollider(Target);
        if (!isInside) NewPos();
        if (isInside && !isSaved)
        {
            isSaved = true;
            float x = transform.position.x;
            float y = transform.position.y;
            float z = transform.position.z;
            float x_rot = Target.transform.rotation.x;
            float y_rot = Target.transform.rotation.y;
            float z_rot = Target.transform.rotation.z;
            string ParamStr = Target.name + "\t" + x.ToString() + "\t" + y.ToString() + "\t" + z.ToString() + "\t" + x_rot.ToString() + "\t" + y_rot.ToString() + "\t" + z_rot.ToString();
            File_AppendText("Result_Name_xyzSelfPos_xyzTargetRotation.txt", ParamStr);
        }
    }
}


