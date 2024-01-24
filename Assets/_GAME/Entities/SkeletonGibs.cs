using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonGibs : MonoBehaviour
{
    [SerializeField] private Transform explosionCenter;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Rigidbody rig in GetComponentsInChildren<Rigidbody>())
        {
            rig.velocity = (rig.transform.position - explosionCenter.position) * 10;
        }
        StartCoroutine(IClean());
    }
    private IEnumerator IClean()
    {
        yield return new WaitForSeconds(5f);
        foreach(Rigidbody rig in GetComponentsInChildren<Rigidbody>())
        {
            Destroy(rig);
            Destroy(rig.GetComponent<Collider>());
        }
    }
}
