using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{

[SerializeField] private Animator Pivot_EmptyGameObject = null;

[SerializeField] private bool openTrigger = false;
[SerializeField] private bool closeTrigger = false;



private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        Debug.Log("Player entered trigger");

        if (openTrigger)
        {
            Debug.Log("Playing DoorOpen animation");
            Pivot_EmptyGameObject.Play("DoorOpen", 0, 0.0f);
            // gameObject.SetActive(false);
        }
        else if (closeTrigger)
        {
            Debug.Log("Playing DoorClose animation");
            Pivot_EmptyGameObject.Play("DoorClose", 0, 0.0f);
            // gameObject.SetActive(false);
        }
		openTrigger = !openTrigger;
		closeTrigger = !closeTrigger;
    }
}
}

