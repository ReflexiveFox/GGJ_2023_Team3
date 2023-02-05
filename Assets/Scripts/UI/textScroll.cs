using System.Collections;
using UnityEngine;
using TMPro;

public class TextScroll : MonoBehaviour
{
    [SerializeField] private float speed = 50.0f;
    [SerializeField] private float textPosBegin = -530.0f;
    [SerializeField] private float boundaryTextEnd = 1230.0f;

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
