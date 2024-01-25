using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageScroll : MonoBehaviour
{
    [SerializeField] private float scrollXSpeed;
    [SerializeField] private float scrollYSpeed;
    private Image img;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        img.material.mainTextureOffset = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        img.material.mainTextureOffset += new Vector2(Time.deltaTime * (-scrollXSpeed / 10), Time.deltaTime * (-scrollYSpeed / 10));
    }
}
