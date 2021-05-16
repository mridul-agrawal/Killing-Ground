using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telekinesis : MonoBehaviour
{
    private float manipulationDistance = 40f;
    private GameObject heldObject;
    public Transform holdPosition;
    private Rigidbody boxRigidBody;
    private bool holdsObject;
    private float attractionSpeed = 5f;
    public float throwForce = 5f;
    private float minThrowForce = 2f;
    private float maxThrowForce = 100f;
    private Vector3 rotateVector;

    public void Raycast()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, manipulationDistance))
        {
            if(hit.collider.CompareTag("Box"))
            {
                heldObject = hit.collider.gameObject;
                heldObject.transform.SetParent(holdPosition);

                boxRigidBody = heldObject.GetComponent<Rigidbody>();
                boxRigidBody.constraints = RigidbodyConstraints.FreezeAll;
                holdsObject = true;
            }
        }
    }

    public void MoveObjectToPosition()
    {
        heldObject.transform.position = Vector3.Lerp(heldObject.transform.position, holdPosition.transform.position, attractionSpeed * Time.deltaTime);
    }

    public float CheckDistance()
    {
        return Vector3.Distance(heldObject.transform.position, holdPosition.transform.position);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(!holdsObject)
            {
                Raycast();
                throwForce = minThrowForce;
                CalculateRotationVector();
            } else
            {
                ThrowObject();
            }
        }

        if(Input.GetMouseButton(1))
        {
            throwForce += 0.2f;
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            ReleaseObject();
        }

        if(holdsObject)
        {
            MoveObjectToPosition();
            RotateBox();

     /*     if (CheckDistance() >= 1f)
            {
                MoveObjectToPosition();
            }
     */ }
    }

    public void ReleaseObject()
    {
        boxRigidBody.constraints = RigidbodyConstraints.None;
        heldObject.transform.parent = null;
        holdsObject = false;
        heldObject = null;

    }

    public void ThrowObject()
    {
        throwForce = Mathf.Clamp(throwForce, minThrowForce, maxThrowForce);
        boxRigidBody.AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);
        ReleaseObject();
    }

    private void CalculateRotationVector()
    {
        float x = Random.Range(-1f,1f);
        float y = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);

        rotateVector = new Vector3(x,y,z);
    }

    private void RotateBox()
    {
        heldObject.transform.Rotate(rotateVector);
    }

}
