using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.InputSystem;

public class MoveInteractible : XRSimpleInteractable
{
    [SerializeField]
    //public InputActionManager inputActionManager;
    public Transform initialTransform;
    public XRRayInteractor rayInteractor;
    public XRRayInteractor leftRayInteractor;
    public ControllerPosition controllerPosition = null;
    public MaterialManager materialManager;
    //private InputDevice rightHand;
    public float speed = 4f;
    public bool isActivated;
    public Vector3 mOffset;
    public Vector3 zmove;
    public Vector3 rayPosition;
    public Vector3 normal;
    public InputData inputData;
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        controllerPosition = rayInteractor.GetComponent<ControllerPosition>();
        mOffset = gameObject.transform.position - controllerPosition.rayHitPosition;
     //   materialManager
    }
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        
        base.ProcessInteractable(updatePhase);
        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
            {
                Move();
            }
        }
    }
    public void Move()
    {
        Vector3 position = controllerPosition ? controllerPosition.Position : Vector3.zero;
        
        
        transform.position = controllerPosition.rayHitPosition + mOffset;
        bool triggervalue=false;

        //if (inputData.rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggervalue) && triggervalue && isSelected)

        //{
       //     Debug.Log("Triggers");
            
        //}
        
        
    }
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        controllerPosition = null;
    }
    private void Start()
    {
        inputData = GetComponent<InputData>();
    }



}
