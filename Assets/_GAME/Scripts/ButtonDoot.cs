using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;

public class ButtonDoot : MonoBehaviour, ISelectHandler
{
    [SerializeField] private EventReference navRef;
    public void OnSelect(BaseEventData eventData)
    {
        RuntimeManager.PlayOneShot(navRef);
    }
}
