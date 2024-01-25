using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHint : MonoBehaviour
{
    [SerializeField] private UIManager.HintType hintType;
    [SerializeField] private string hintText;
    private BoxCollider col;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.instance.GiveHint(hintType, hintText);
            Destroy(gameObject);
        }
    }
}
