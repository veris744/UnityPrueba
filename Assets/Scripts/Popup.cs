using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public string text;
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPopup()
    {
        if (button.allowpopup)
        {
            gameObject.SetActive(true);
            if (RecordingSystem.Instance.isRecording)
                RecordingSystem.Instance.RecordAction();
        }
    }

    public void ClosePopup()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            if (RecordingSystem.Instance.isRecording)
               RecordingSystem.Instance.RecordAction();
        }
    }
}
