using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketHandling : MonoBehaviour
{
    [SerializeField] private List<XRSocketInteractor> _socketsInObject;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var socket in _socketsInObject)
        {
            socket.selectEntered.AddListener(SocketAttached);
            socket.selectExited.AddListener(SocketDettached);
        }
        
    }
    
    private void SocketAttached(SelectEnterEventArgs arg0)
    {
        IgnoreCollision(arg0.interactable, true);
    }

    private void SocketDettached(SelectExitEventArgs arg0)
    {
        IgnoreCollision(arg0.interactable, false);
    }


    private void IgnoreCollision(XRBaseInteractable interactable, bool ignore)
    {
        var myColliders = GetComponentsInChildren<Collider>();
        foreach (var myCollider in myColliders)
        {
            Debug.Log(myCollider.name);
            foreach (var interactableCollider in interactable.colliders)
            {
                Physics.IgnoreCollision(myCollider, interactableCollider, ignore);
            }
        }
    }
}
