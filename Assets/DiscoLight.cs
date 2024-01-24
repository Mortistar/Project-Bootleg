using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoLight : MonoBehaviour
{
    [SerializeField] private float colorThreshold;
    private Light pointLight;
    private List<Color> rainbowList;
    private int colorIndex = 0;
    private float colorTimer;
    void Awake()
    {
        pointLight = GetComponent<Light>();
        rainbowList = new List<Color>()
        {
            Color.red,
            Color.magenta,
            Color.blue,
            Color.cyan,
            Color.green,
            Color.yellow,
        };
        pointLight.color = rainbowList[0];
    }

    // Update is called once per frame
    void Update()
    {
        colorTimer += Time.deltaTime;
        if (colorTimer > colorThreshold)
        {
            colorTimer = 0;
            colorIndex += 1;
            if (colorIndex > rainbowList.Count - 1)
            {
                colorIndex = 0;
            }
            pointLight.color = rainbowList[colorIndex];
        }
    }
}
