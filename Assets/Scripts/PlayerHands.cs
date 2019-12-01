using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.XR;
public class PlayerHands : MonoBehaviour
{
    [SerializeField] Transform joyStick;
    [SerializeField] Transform xButton;
    [SerializeField] Transform yButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        JoyStickMovement();
        ButtonInput();
    }

    private void JoyStickMovement()
    {
        float leftX_Input = Input.GetAxis("LHorizontal");
        float leftY_Input = Input.GetAxis("LVertical");

        if (leftX_Input <= -0.05f || leftX_Input >= 0.05f || leftY_Input <= -0.05f || leftY_Input >= 0.05f)
        {
            float x_angle = scale(1f, -1f, -40f, 40f, leftX_Input); // X needs to be inverted
            float y_angle = scale(-1f, 1f, -40f, 40f, leftY_Input);

            joyStick.rotation = Quaternion.AngleAxis(x_angle, Vector3.forward) * Quaternion.AngleAxis(y_angle, Vector3.right);
                
        }
        else
        {
            joyStick.rotation = Quaternion.AngleAxis(0f, Vector3.forward);
        }
    }

    private void ButtonInput()
    {

        if (Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            Debug.Log("PRESSED");
            xButton.localPosition = new Vector3(0f, -1f, 0f);
        }

        // Release
        if (Input.GetKeyUp(KeyCode.JoystickButton4))
        {
            xButton.localPosition = new Vector3(0f, 0f, 0f);
        }
        


    }

    private bool CheckButton(float input)
    {
        return input <= -0.1f ? true : false;
    }



    public float scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {

        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }
}
