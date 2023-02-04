using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textScroll : MonoBehaviour
{
    float speed = 100.0f;
    float textPosBegin= -530.0f;
    float boundaryTextEnd=1090.0f;

    RectTransform myGorectTransform;
    [SerializeField]
    TextMeshProUGUI mainText;

    [SerializeField]
    bool isLooping = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
