using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketHandling : MonoBehaviour
{
    private XRSocketInteractor _socket;
    // Start is called before the first frame update
    void Start()
    {
        _socket = this.gameObject.GetComponent<XRSocketInteractor>();
        _socket.selectEntered.AddListener(DisableCollision);
        _socket.selectExited.AddListener(EnableCollision);

    }
    
    private void DisableCollision(SelectEnterEventArgs arg0)
    {
        arg0.interactable.gameObject.GetComponent<Collider>().enabled = false;
    }

    private void EnableCollision(SelectExitEventArgs arg0)
    {
        arg0.interactable.gameObject.GetComponent<Collider>().enabled = true;;
    }
    
    
    /*
     
      var rb = arg0.interactable.gameObject.GetComponent<Rigidbody>();
        ToggleRBKinematic(rb, true);
    private void ToggleRBKinematic(Rigidbody arg0, bool toggle)
    {
        arg0.isKinematic = toggle;
        var rbArray = this.gameObject.GetComponentsInParent<Rigidbody>();
        foreach (var rb in rbArray)
        {
            rb.isKinematic = toggle;
        }
    }
    
        var colliders = this.gameObject.GetComponentsInParent<Collider>();
        foreach (var col in colliders)
        {
            if (col.gameObject.TryGetComponent<XRGrabInteractable>(out var grabInteractable))
            {
                Physics.IgnoreCollision(arg0.interactable.gameObject.GetComponent<Collider>(), col, false);
            }   
        }
    */



    
}
