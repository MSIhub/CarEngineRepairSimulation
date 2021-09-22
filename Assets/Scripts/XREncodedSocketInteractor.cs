using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XREncodedSocketInteractor : XRSocketInteractor
{
    public int socketCode = 0;

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        return base.CanSelect(interactable) && ValidateSocket(interactable);
    }

 


    public override bool CanHover(XRBaseInteractable interactable)
    {
        return base.CanHover(interactable) && ValidateSocket(interactable);
    }

    private bool ValidateSocket(XRBaseInteractable interactable)
    {
        var isRightSocket = false;

        if (interactable.gameObject.TryGetComponent<SocketEncoder>(out var code))
        {
            isRightSocket = (code.socketCode == this.socketCode);
            /*var rb = interactable.gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = true;*/
        }

        return isRightSocket;
    }

}
