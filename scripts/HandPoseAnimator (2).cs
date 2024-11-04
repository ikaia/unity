using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;



public class HandPoseAnimator : MonoBehaviour
{
    public List<XRController> controllers;
    public Animator handAnimator;
    public string whichHand = "";

    // Update is called once per frame
    void Update()
    {
        foreach (XRController xRController in controllers)
        {

            if (xRController.inputDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
            {               
                handAnimator.SetFloat("Grip_" + whichHand, gripValue);

            }
            else
            {             
                handAnimator.SetFloat("Grip_" + whichHand, 0);
            }

            if (xRController.inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f)
            {
               
                handAnimator.SetFloat("Trigger_" + whichHand, triggerValue);
            }
            else
            {             
                handAnimator.SetFloat("Trigger_" + whichHand, 0);
            }

        }
    }

    

}
