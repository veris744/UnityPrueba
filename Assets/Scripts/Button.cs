using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button : MonoBehaviour, IPointerClickHandler
{
    bool isDragging = false;
    public int color = 0;
    public bool allowpopup = true;

    // Start is called before the first frame update
    void Start()
    {
        SetColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (isDragging)
        {
            Vector3 desp = Input.mousePosition - transform.position;

            foreach (GameObject go in UIManager.Instance.allbuttons)
            {
                go.transform.position = go.transform.position + desp;
            }
            if (RecordingSystem.Instance.isRecording)
                RecordingSystem.Instance.RecordAction();
        }
    }

    public void BeginDrag()
    {
        isDragging= true;
        allowpopup= false;
    }

    public void EndDrag()
    {
        isDragging= false;
        StartCoroutine(CountdownToPopupAllow());
    }


    public void SetColor()
    {
        if (color == 0)
        {
            GetComponent<Image>().color = Color.red;
        }
        else if (color == 1) 
        {
            GetComponent<Image>().color = Color.green;
        }
        else
        {
            GetComponent<Image>().color = Color.blue;
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            color++;
            if (color > 2) color = 0;
            SetColor();

            if (RecordingSystem.Instance.isRecording)
                RecordingSystem.Instance.RecordAction();
        }
    }

    public IEnumerator CountdownToPopupAllow()
    {
        float currCountdownValue = 0.2f;
        while (currCountdownValue > 0)
        {
            yield return new WaitForSeconds(0.1f);
            currCountdownValue -= 0.1f;
        }

        allowpopup= true;
    }
}
