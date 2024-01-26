using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpin : MonoBehaviour
{
    [SerializeField] private bool hasSpin = true;
    [SerializeField] private float spinSpeed = 1;
    [SerializeField] private bool hasBob = true;
    [SerializeField] private float bobSpeed = 1;
    [SerializeField] private float bobMag = 1;

    private float bob;
    private Vector3 startPos;

    void Awake()
    {
        startPos = transform.position;
    }
    void Update()
    {
        if (hasBob){Bob();}
        if (hasSpin){Spin();}
    }
    private void Spin()
    {
        transform.Rotate(Vector3.up, (spinSpeed * 0.2f), Space.World);
    }
    private void Bob()
    {
        bob += Time.deltaTime;
        transform.position = new Vector3(startPos.x, startPos.y + (Mathf.Sin(bob * (bobSpeed * 2f)) * (bobMag * 0.2f)), startPos.z);
    }
}
