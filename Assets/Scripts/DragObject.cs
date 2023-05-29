using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class DragObject : XRGrabInteractable
{
    [SerializeField] XRRayInteractor ray;

    private Vector3 mOffset;

    private ControllerPosition controllerPosition = null;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        controllerPosition = ray.GetComponent<ControllerPosition>();

        mOffset = gameObject.transform.position - controllerPosition.rayHitPosition;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        controllerPosition = null;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (isSelected)
        {
            if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
            {
                UpdateObjectPosition();
            }
        }
    }

    private void UpdateObjectPosition()
    {
        Vector3 position = controllerPosition ? controllerPosition.Position : Vector3.zero;

        transform.position = controllerPosition.rayHitPosition + mOffset;

    }
}
