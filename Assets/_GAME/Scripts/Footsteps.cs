using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using TheFirstPerson;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float distance;
    [SerializeField] private EventReference footstepRef;
    [SerializeField] private bool isWorld;
    private FPSController control;
    private Vector3 oldPos;
    void Start()
    {
        oldPos = transform.position;
        if (!isWorld)
        {
            control = GetComponent<FPSController>();
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, oldPos)  >= distance) //If greater than radius
        {
            if (!isWorld && control.grounded)
            {
                oldPos = transform.position;
                PlayFootstep();
            }
            else if (isWorld)
            {
                oldPos = transform.position;
                PlayFootstep();
            }
            
        }
    }
    private void PlayFootstep()
    {
        if (isWorld)
        {
            RuntimeManager.PlayOneShotAttached(footstepRef, gameObject);
        }else
        {
            RuntimeManager.PlayOneShot(footstepRef);
        }
    }
}
