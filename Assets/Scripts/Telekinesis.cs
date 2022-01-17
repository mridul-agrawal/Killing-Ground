using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Telekinesis : MonoBehaviour
{
    private float manipulationDistance = 40f;
    private GameObject heldObject;
    public Transform holdPosition;
    private Rigidbody boxRigidBody;
    private bool holdsObject;
    private float attractionSpeed = 5f;
    public float throwForce = 0f;
    private float minThrowForce = 2f;
    private float maxThrowForce = 100f;
    private Vector3 rotateVector;
    public TextMeshProUGUI forceText;
    public AudioClip PullSound;
    public AudioClip PushSound;
    public AudioClip HoldSound;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


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
                audioSource.PlayOneShot(PullSound);
                audioSource.Play();
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
            throwForce += 0.5f;
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
        forceText.text = throwForce.ToString();
    }

    public void ReleaseObject()
    {
        boxRigidBody.constraints = RigidbodyConstraints.None;
        heldObject.transform.parent = null;
        holdsObject = false;
        heldObject = null;
        audioSource.Stop();

    }

    public void ThrowObject()
    {
        throwForce = Mathf.Clamp(throwForce, minThrowForce, maxThrowForce);
        Vector3 throwVector = new Vector3(-0.15f,0.15f,0f);
        boxRigidBody.AddForce((Camera.main.transform.forward + throwVector) * throwForce, ForceMode.Impulse);
        ReleaseObject();
        audioSource.PlayOneShot(PushSound);
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
