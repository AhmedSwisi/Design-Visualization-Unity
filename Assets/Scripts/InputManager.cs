using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class InputManager : MonoBehaviour
{
    private InputDevice controller;
    [SerializeField]
    private XRNode xrNode = XRNode.LeftHand;
    private List<InputDevice> devices = new List<InputDevice>();
    void GetDevice()
    {
        InputDevices.GetDeviceAtXRNode(xrNode);
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
