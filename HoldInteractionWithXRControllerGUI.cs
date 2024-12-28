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

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using RootMotion.FinalIK;

namespace RootMotion.Demos {

        /// <summary>
    /// Controls interaction with an object based on VR controller grip press using XR Controller. 
	///  HOWEVER, this happens ONCE only. 
    /// </summary>
    public class HoldInteractionWithXRController : MonoBehaviour {

        [Tooltip("The object to interact with")]
        public InteractionObject interactionObject;
        [Tooltip("The effectors to interact with")]
        public FullBodyBipedEffector[] effectors;

        public XRController xrController; // Reference to XR Controller

        private InteractionSystem interactionSystem;
        private bool hasInteracted = false; // Flag to ensure interaction happens only once

        void Awake() {
            interactionSystem = GetComponent<InteractionSystem>();
        }

        void Update() {
            // Check if the grip button is pressed and interaction has not occurred yet
            if (!hasInteracted && xrController.inputDevice.IsPressed(InputHelpers.Button.Grip, out bool isPressed) && isPressed) {
                StartInteraction();
            }
        }

        private void StartInteraction() {
            hasInteracted = true;

            foreach (FullBodyBipedEffector effector in effectors) {
                interactionSystem.StartInteraction(effector, interactionObject, true);
            }
        }

        // Optional: Reset method to allow interaction again if needed
        public void ResetInteraction() {
            hasInteracted = false;

            foreach (FullBodyBipedEffector effector in effectors) {
                interactionSystem.StopInteraction(effector);
            }
        }
    }
}


