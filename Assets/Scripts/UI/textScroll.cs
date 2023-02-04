using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textScroll : MonoBehaviour
{
    float speed = 50.0f;
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
        myGorectTransform = gameObject.GetComponent<RectTransform>();
      
    }

    public void IniziaScroll()
    {
        StartCoroutine(AutoScrollText());
    }

    IEnumerator AutoScrollText ()
    {
        while (myGorectTransform.localPosition.y <boundaryTextEnd)
        {
            myGorectTransform.Translate(Vector3.up * speed*Time.deltaTime);
            if (myGorectTransform.localPosition.y>boundaryTextEnd)
            {
                if (isLooping)
                {
                    myGorectTransform.localPosition = Vector3.up * textPosBegin;
                }
                else
                {
                    break;
                }
            }
            yield return null;
            
        }
    }
}
