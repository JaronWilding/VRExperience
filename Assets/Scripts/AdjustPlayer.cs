using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AdjustPlayer : MonoBehaviour
{

    [SerializeField] private Transform cameraFloorOffset = null;
    [SerializeField] private Transform vrRig = null;
    [SerializeField] private Camera cam = null;

    private float rightX_Input;
    private float ground;
    private float addedRotation;

    private Quaternion vrBodyRotation;

    private CharacterController charCon = null;
    private Vector3 startPos;



    void Start()
    {
        startPos = transform.position;
    }
    
    void Update()
    {
        rightX_Input = Input.GetAxis("RHorizontal");
        MoveCam();
        ControllerMoveCamera();
    }


    private void MoveCam()
    {
        Vector3 hmdWorld = cam.transform.position;
        float hmdRotation = cam.transform.rotation.eulerAngles.y;
        float hmdDown = cam.transform.rotation.eulerAngles.x;
        
        ground = transform.position.y;
        
        //Set the camera floor offset to follow properly.
        cameraFloorOffset.localPosition = new Vector3(-cam.transform.localPosition.x, cameraFloorOffset.localPosition.y, -cam.transform.localPosition.z);

        //Set current position to the camera pos.
        transform.position = new Vector3(hmdWorld.x, ground, hmdWorld.z);
        
        //Rotate main body in the same place as the camera.
        transform.rotation = Quaternion.Euler(new Vector3(0f, hmdRotation, 0f));
        //Rotate the camera's parent back so it doesn't spin around
        vrRig.transform.localRotation = Quaternion.Euler(new Vector3(0f, (hmdRotation + addedRotation) * -1f, 0f));

        //If head looks down towards own body (holsters)
        //if ((hmdDown < 110 && hmdDown > 40) == false)
        //{
        //If not looking down, set proper rotation to the same as the camera
        //Save camera's rotation into a global variable
        //    vrBodyRotation = Quaternion.Euler(new Vector3(0f, hmdRotation + addedRotation, 0f));
        //}

        //One line if statement, may be better \ faster?
        vrBodyRotation = (hmdDown < 110 && hmdDown > 40) ? vrBodyRotation : Quaternion.Euler(new Vector3(0f, hmdRotation, 0f));
        transform.position = startPos;
    }
    private void ControllerMoveCamera()
    {

        if (rightX_Input <= -0.2f || rightX_Input >= 0.2f)
        {
            addedRotation -= rightX_Input;
        }
    }
}
