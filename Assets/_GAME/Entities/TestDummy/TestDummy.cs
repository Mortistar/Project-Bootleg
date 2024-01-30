using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDummy : MonoBehaviour, IKickable, ISweepable
{
    [SerializeField] private int kickMult;
    
    private Rigidbody rb;
    private bool isKicked;
    private bool isSwept;
    private float kickTimer = 0;
    private Vector3 startPos;
    
    void Update()
    {
        if (isKicked)
        {
            if (rb.velocity.magnitude == 0 || rb.velocity.magnitude > 50)
            {
                kickTimer -= Time.deltaTime;
                if (kickTimer <= 0)
                {
                    Reset();
                }
            }
        }
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    public void Kick(float damage, Vector3 direction)
    {
        rb.velocity = direction * kickMult;

        isKicked = true;
        kickTimer = 1;
    }
    public void Sweep(Vector3 direction)
    {
        rb.angularVelocity = direction * kickMult;
        isKicked = true;
        kickTimer = 1;
    }
    private void Reset()
    {
        rb.velocity = Vector3.zero;
        transform.position = startPos;
        transform.rotation = Quaternion.identity;
        isKicked = false;
    }
}
