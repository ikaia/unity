using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using RootMotion.FinalIK;

namespace RootMotion.Demos {

    /// <summary>
    /// Controls interaction with an object based on VR controller grip press using XR Controller.
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
}

