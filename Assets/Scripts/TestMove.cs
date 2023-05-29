using UnityEngine;
using System.Collections;
using UnityEngine.XR;
using System.Collections.Generic;

public class TestMove : MonoBehaviour
{
    public LineRenderer laserLineRenderer;
    public float laserWidth = 0.1f;
    public float laserMaxLength = 20f;

    private InputDevice _rightHandController;


    private bool _haveLastRaycastHit = false;
    private RaycastHit _lastRaycastHit;
    private bool _lastIsTiggerPressed;


    void Start()
    {
        Vector3[] initLaserPositions = new Vector3[2] { Vector3.zero, Vector3.zero };
        laserLineRenderer.SetPositions(initLaserPositions);

        laserLineRenderer.startWidth = laserWidth;
        laserLineRenderer.endWidth = laserWidth;
        laserLineRenderer.enabled = true;

        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevices(inputDevices);

        foreach (var device in inputDevices)
        {
            if ((device.characteristics & InputDeviceCharacteristics.Right) == InputDeviceCharacteristics.Right &&
                (device.characteristics & InputDeviceCharacteristics.HeldInHand) == InputDeviceCharacteristics.HeldInHand)
            {
                _rightHandController = device;
                break;
            }
        }

        _haveLastRaycastHit = false;
        _lastIsTiggerPressed = false;

    }


    void FixedUpdate()
    {
        RaycastHit raycastHit;
        bool isTriggerPressed;

        // setup ray cast and see if we hit something
        Ray ray = new Ray(transform.position, transform.forward);
        Vector3 endPosition = transform.position + (laserMaxLength * transform.forward);

        var didHitSomething = Physics.Raycast(ray, out raycastHit, laserMaxLength);
        if (didHitSomething)
            endPosition = raycastHit.point;

        // set laser to not go through hit object
        laserLineRenderer.SetPosition(0, transform.position);
        laserLineRenderer.SetPosition(1, endPosition);

        // see if the trigger was pressed
        _rightHandController.TryGetFeatureValue(CommonUsages.triggerButton, out isTriggerPressed);

        if (didHitSomething && isTriggerPressed)
        {
            if (_haveLastRaycastHit)
            {
                // get the delta move from last raycast hit on this content window
                var deltaPosition = (raycastHit.point - _lastRaycastHit.point);
                raycastHit.transform.position += deltaPosition;
            }

            _haveLastRaycastHit = true;
            _lastRaycastHit = raycastHit;
        }
        else
        {
            _haveLastRaycastHit = false;
        }
    }
}
