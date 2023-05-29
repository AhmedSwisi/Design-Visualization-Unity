
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Draggable : XRSimpleInteractable
{
    public XRInteractionManager currentInteractor;
    public InteractionLayerMask mask;
    private void Start()
    {
        GetComponent<XRInteractionManager>();
    }
    public void test()
    {
        if (currentInteractor.GetComponent<ActionBasedController>().activateAction.action.ReadValue<float>() > 0.5f)
        {
            Debug.Log("trigger pressed");
        }
    }
}

