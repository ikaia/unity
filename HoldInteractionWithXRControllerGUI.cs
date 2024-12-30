/*using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using RootMotion.FinalIK;

namespace RootMotion.Demos {

    /// <summary>
    /// Controls interaction with an object based on VR controller grip press using XR Controller. 
	    This happens whenever you press right controller Grip button. 
    /// </summary>
    public class HoldInteractionWithXRController : MonoBehaviour {

        [Tooltip("The object to interact with")]
        public InteractionObject interactionObject;
        [Tooltip("The effectors to interact with")]
        public FullBodyBipedEffector[] effectors;

        public XRController xrController; // Reference to XR Controller

        private InteractionSystem interactionSystem;
        private bool isInteracting = false;

        void Awake() {
            interactionSystem = GetComponent<InteractionSystem>();
        }

        void Update() {
            // Check if the grip button is pressed
            if (xrController.inputDevice.IsPressed(InputHelpers.Button.Grip, out bool isPressed) && isPressed) {
                if (!isInteracting) {
                    StartInteraction();
                }
            } else if (isInteracting) {
                StopInteraction();
            }
        }

        private void StartInteraction() {
            isInteracting = true;

            foreach (FullBodyBipedEffector effector in effectors) {
                interactionSystem.StartInteraction(effector, interactionObject, true);
            }
        }

        private void StopInteraction() {
            isInteracting = false;

            foreach (FullBodyBipedEffector effector in effectors) {
                interactionSystem.StopInteraction(effector);
            }
        }
    }
}*/

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using UnityEngine.XR.Interaction.Toolkit;

namespace RootMotion.Demos { //this work to stop intreacting once but doesnt override handshake animation

        /// <summary>
    /// Controls interaction with an object based on VR controller grip press using XR Controller. 
	///  HOWEVER, this happens ONCE only. 
    /// </summary>
    public class HoldInteractionWithXRController : MonoBehaviour {

        [Tooltip("The object to interact with")]
        public InteractionObject interactionObject;
        [Tooltip("The effectors to interact with")]
        public FullBodyBipedEffector[] effectors;
		[Tooltip("The Animator component of the avatar")]
        public Animator avatarAnimator;

        public XRController xrController; // Reference to XR Controller
		

        private InteractionSystem interactionSystem;
        private bool isInteracting = false; // Flag to ensure interaction happens only once

        void Awake() {
            interactionSystem = GetComponent<InteractionSystem>();
        }

        void Update() {
            // Check if the grip button is pressed and interaction has not occurred yet
            if (!isInteracting && xrController.inputDevice.IsPressed(InputHelpers.Button.Grip, out bool isPressed) && isPressed) {
                StartInteraction();
            }
		 else if (isInteracting) {
                StopInteraction();
            }
        }

        private void StartInteraction() {
             isInteracting = true;

        // Pause the animator to override default animations
        if (avatarAnimator != null)
        {
            avatarAnimator.enabled = false;
        }

            foreach (FullBodyBipedEffector effector in effectors) {
                interactionSystem.StartInteraction(effector, interactionObject, true);
            }
        }

          private void StopInteraction() {
               isInteracting = false;

        // Resume the animator to restore default animations
        if (avatarAnimator != null)
        {
            avatarAnimator.enabled = true;
        }

            foreach (FullBodyBipedEffector effector in effectors) {
                interactionSystem.StopInteraction(effector);
            }
        }
}

}*/
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using UnityEngine.XR.Interaction.Toolkit;
namespace RootMotion.Demos {
public class HoldInteractionWithXRController : MonoBehaviour //This override animation but doesnt stop 1, THIS WORK
{
    [Tooltip("The object to interact with")]
    public InteractionObject interactionObject;

    [Tooltip("The effectors to interact with")]
    public FullBodyBipedEffector[] effectors;

    [Tooltip("Reference to the XR Controller for interaction")]
    public XRController xrController;

    [Tooltip("The Animator component of the avatar")]
    public Animator avatarAnimator;

    private InteractionSystem interactionSystem;
    private bool isInteracting = false; // Flag to track interaction state

    void Awake()
    {
        // Get the InteractionSystem component attached to the avatar
        interactionSystem = GetComponent<InteractionSystem>();
    }

    void Update()
    {
        // Check if the grip button is pressed on the XR controller
        if (xrController.inputDevice.IsPressed(InputHelpers.Button.Grip, out bool isPressed))
        {
            if (isPressed && !isInteracting)
            {
                StartInteraction();
            }
            else if (!isPressed && isInteracting)
            {
                StopInteraction();
            }
	}
    }
    private void StartInteraction()
    {
        isInteracting = true;

        // Pause the animator to override default animations
        if (avatarAnimator != null)
        {
            avatarAnimator.enabled = false;
        }

        // Begin the interaction for each specified effector
         foreach (FullBodyBipedEffector effector in effectors) {
                interactionSystem.StartInteraction(effector, interactionObject, true);
            }
        }


    private void StopInteraction()
    {
        isInteracting = false;

        // Resume the animator to restore default animations
        if (avatarAnimator != null)
        {
            avatarAnimator.enabled = true;
        }

        // End the interaction for each specified effector
         foreach (FullBodyBipedEffector effector in effectors) {
                interactionSystem.StopInteraction(effector);
            }
        }
}


}*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using UnityEngine.XR.Interaction.Toolkit;

namespace RootMotion.Demos
{
    public class HoldInteractionWithXRController : MonoBehaviour
    {
        [Tooltip("The object to interact with")]
        public InteractionObject interactionObject;

        [Tooltip("The effectors to interact with")]
        public FullBodyBipedEffector[] effectors;

        [Tooltip("Reference to the XR Controller for interaction")]
        public XRController xrController;

        [Tooltip("The Animator component of the avatar")]
        public Animator avatarAnimator;

        private InteractionSystem interactionSystem;
        private bool hasInteracted = false; // Flag to ensure interaction happens only once

        void Awake()
        {
            // Get the InteractionSystem component attached to the avatar
            interactionSystem = GetComponent<InteractionSystem>();
        }

        void Update()
        {
            // Check if the grip button is pressed or released on the XR controller
            if (!hasInteracted && xrController.inputDevice.IsPressed(InputHelpers.Button.Grip, out bool isPressed))
            {
                if (isPressed)
                {
                    StartInteraction();
                }
            }
            else if (hasInteracted && xrController.inputDevice.IsPressed(InputHelpers.Button.Grip, out isPressed) && !isPressed)
            {
                StopInteraction();
            }
        }

        private void StartInteraction()
        {
            hasInteracted = true; // Prevent further interactions

            // Pause the animator to override default animations
            if (avatarAnimator != null)
            {
                avatarAnimator.enabled = false;
            }

            // Begin the interaction for each specified effector
            foreach (FullBodyBipedEffector effector in effectors)
            {
                interactionSystem.StartInteraction(effector, interactionObject, true);
            }

            Debug.Log("Interaction started.");
        }

        private void StopInteraction()
        {
            // End the interaction for each specified effector
            foreach (FullBodyBipedEffector effector in effectors)
            {
                interactionSystem.StopInteraction(effector);
            }

            // Resume the animator to restore default animations
            if (avatarAnimator != null)
            {
                avatarAnimator.enabled = true;
            }

            // Disable further collisions and detach the interaction object
            DisableInteractionObject();

            Debug.Log("Interaction stopped and object detached.");
        }

        private void DisableInteractionObject() //This is what was needed
        {
            // Disable collisions on the interaction object
            Collider objectCollider = interactionObject.GetComponent<Collider>();
            if (objectCollider != null)
            {
                objectCollider.enabled = false;
            }

            // Optionally, remove the interaction object if necessary
            Rigidbody objectRigidbody = interactionObject.GetComponent<Rigidbody>();
            if (objectRigidbody != null)
            {
                objectRigidbody.isKinematic = false; // Enable physics
                objectRigidbody.useGravity = true;  // Enable gravity
            }

            // Optionally, detach the interaction object from the scene or avatar
            interactionObject.transform.SetParent(null);
        }
    }
}

