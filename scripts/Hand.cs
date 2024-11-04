using System.Collections;
using UnityEngine;
using RootMotion.FinalIK;
using RootMotion.Demos;

public class Hand : MonoBehaviour
{
    public InteractionObject Right_hand; //CC_Base_R_Hand - Male
    public Vector3 holdOffset;

    private float holdWeight;
    private FullBodyBipedIK ik;

    IEnumerator PickUp()
    {
        ik = Right_hand.lastUsedInteractionSystem.GetComponent<FullBodyBipedIK>();

        while (holdWeight < 1f)
        {
            holdWeight += Time.deltaTime;
            yield return null;
        }
    }

    void LateUpdate()
    {
        if (ik == null) return;

        ik.solver.rightHandEffector.positionOffset += ik.transform.rotation * holdOffset * holdWeight;
    }
}
