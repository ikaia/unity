using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using RootMotion.FinalIK;

public class ToggleIKOnTrigger : MonoBehaviour
{
    public FullBodyBipedIK ik;             // Reference to the Final IK component
    public Transform leftHandTarget;       // The target transform for the left hand IK
    public XRController controller;        // Reference to the XR controller (Oculus right controller)

    private bool isTriggerPressed = false; // To check if the trigger is pressed

    // Store the existing weights from the IK solver
    private float originalPositionWeight;
    private float originalRotationWeight;

    void Start()
    {
        // Save the current weights of the left hand effector
        originalPositionWeight = ik.solver.leftHandEffector.positionWeight;
        originalRotationWeight = ik.solver.leftHandEffector.rotationWeight;

        // Disable IK at the start (set weights to 0)
        DisableIK();
    }

    void Update()
    {
        // Check if the trigger is pressed
        CheckTriggerInput();
    }

    // Check for trigger input from the controller
    private void CheckTriggerInput()
    {
        if (controller.inputDevice.IsPressed(InputHelpers.Button.Trigger, out bool isPressed))
        {
            if (isPressed && !isTriggerPressed)
            {
                // Trigger just pressed
                isTriggerPressed = true;
                EnableIK();  // Enable IK when trigger is pressed
            }
            else if (!isPressed && isTriggerPressed)
            {
                // Trigger just released
                isTriggerPressed = false;
                DisableIK();  // Disable IK when trigger is released
            }
        }
    }

    // Enable the IK (connect the hand to the target)
    private void EnableIK()
    {
        ik.solver.leftHandEffector.target = leftHandTarget; // Set the IK target
        ik.solver.leftHandEffector.positionWeight = originalPositionWeight; // Use the original position weight
        ik.solver.leftHandEffector.rotationWeight = originalRotationWeight; // Use the original rotation weight
     
    }

    // Disable the IK (disconnect the hand from the target)
    private void DisableIK()
    {
    
        ik.solver.leftHandEffector.target = null;           // Remove the IK target
        ik.solver.leftHandEffector.positionWeight = 0f;     // Set position weight to 0 to disable IK
        ik.solver.leftHandEffector.rotationWeight = 0f;     // Set rotation weight to 0 to disable IK

   
    }
}
