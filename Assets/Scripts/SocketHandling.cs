using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketHandling : MonoBehaviour
{
    [SerializeField] private List<XRSocketInteractor> _socketsInObject;
    [SerializeField] private int _requiredAssemblyCount = 12;
    
    public int assemblyCount;
    public bool isAssemblySuccess;

    private void Awake()
    {
        assemblyCount =0;
        isAssemblySuccess = false;
    }

    // Start is called before the first frame update
    private void Start()
    {
        foreach (var socket in _socketsInObject)
        {
            socket.selectEntered.AddListener(SocketAttached);
            socket.selectExited.AddListener(SocketDettached);
        }
        
    }

    private void Update()
    {
        if (assemblyCount == _requiredAssemblyCount)
        {
            isAssemblySuccess = true;
            /*Debug.Log("Great Job, you are now a VR Certified Engine Mechanic");   */
        }
    }

    private void SocketAttached(SelectEnterEventArgs arg0)
    {
        IgnoreCollision(arg0.interactable, true);
        assemblyCount++;
    }

    private void SocketDettached(SelectExitEventArgs arg0)
    {
        IgnoreCollision(arg0.interactable, false);
        assemblyCount--;
    }


    private void IgnoreCollision(XRBaseInteractable interactable, bool ignore)
    {
        var myColliders = GetComponentsInChildren<Collider>();
        foreach (var myCollider in myColliders)
        {
            foreach (var interactableCollider in interactable.colliders)
            {
                Physics.IgnoreCollision(myCollider, interactableCollider, ignore);
            }
        }
    }
}
