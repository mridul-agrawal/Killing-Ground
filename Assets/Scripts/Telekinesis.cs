using KillingGround.Services;
using KillingGround.Audio;
using UnityEngine;
using TMPro;

public class Telekinesis : MonoBehaviour
{
    // References:
    private GameObject heldObject;
    [SerializeField] private Transform holdPosition;
    private Rigidbody boxRigidBody;

    // Variables:
    private float manipulationDistance = 40f;
    private bool holdsObject;
    private float attractionSpeed = 5f;
    public float throwForce = 0f;
    private float minThrowForce = 2f;
    private float maxThrowForce = 100f;
    private Vector3 rotateVector;


    private void Update()
    {
        HandleMouseInput();
        HandleKeyboardInput();
        HandleHeldObject();
        UIService.Instance.UpdateForceUI(throwForce);
    }

    // Reacts to Mouse Input for Telekinesis
    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!holdsObject)
            {
                Raycast();
                throwForce = minThrowForce;
                CalculateRotationVector();
            }
            else
            {
                ThrowObject();
            }
        }
        else if (Input.GetMouseButton(1))
        {
            throwForce += 0.5f;
        }
    }

    // Reacts to Keyboard Input for Telekinesis
    private void HandleKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ReleaseObject();
        }
    }

    // Moves & Rotates the held object accordingly.
    private void HandleHeldObject()
    {
        if (holdsObject)
        {
            MoveObjectTowardsPlayer();
            RotateBox();
        }
    }

    // Used to rotate an held object.
    private void RotateBox()
    {
        heldObject.transform.Rotate(rotateVector);
    }

    // This method is used to raycast to find a throwable object in environment which can be used as a weapon.
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
                SoundManager.Instance.PlaySoundEffects(SoundType.PullSound);
            }
        }
    }

    // This method is used to add rotational value to an already held gameobject.
    private void CalculateRotationVector()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);

        rotateVector = new Vector3(x, y, z);
    }

    // This method adds force to the held gamobject. 
    public void ThrowObject()
    {
        throwForce = Mathf.Clamp(throwForce, minThrowForce, maxThrowForce);
        Vector3 throwVector = new Vector3(-0.15f, 0.15f, 0f);
        boxRigidBody.AddForce((Camera.main.transform.forward + throwVector) * throwForce, ForceMode.Impulse);
        ReleaseObject();
        SoundManager.Instance.PlaySoundEffects(SoundType.PushSound);
    }

    // Used for moving held object towards its company.
    public void MoveObjectTowardsPlayer()
    {
        heldObject.transform.position = Vector3.Lerp(heldObject.transform.position, holdPosition.transform.position, attractionSpeed * Time.deltaTime);
    }

    // Releases the object.
    public void ReleaseObject()
    {
        boxRigidBody.constraints = RigidbodyConstraints.None;
        heldObject.transform.parent = null;
        holdsObject = false;
        heldObject = null;
        SoundManager.Instance.StopSoundEffect();

    }
}
