using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using FMODUnity;
 
public class ButtonFixer : EventTrigger 
{
    public override void OnPointerEnter(PointerEventData data) 
    {
        Selectable selectable = GetComponent<Selectable>();
        if (EventSystem.current.currentSelectedGameObject != gameObject && selectable.interactable) {
            // Somebody else is still selected?!?  Screw that.  Select us now.
            selectable.Select();
        }
    }
}