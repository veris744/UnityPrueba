using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public string tooltipText;
    public float tooltipTime = 0.5f;
    GameObject subpanel;
    GameObject text;


    // Start is called before the first frame update
    void Start()
    {
        subpanel = transform.GetChild(0).gameObject;
        text = subpanel.transform.GetChild(0).gameObject;
        text.GetComponent<TextMeshProUGUI>().SetText(tooltipText);
        subpanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowTooltip()
    {
        //StartCoroutine(CountdownToTooltip());
        //transform.position = Input.mousePosition - transform.localScale / 2;
        //subpanel.SetActive(true);
    }

    public void HideTooltip()
    {
        StopCoroutine(CountdownToTooltip());
        subpanel.SetActive(false);
    }

    public IEnumerator CountdownToTooltip()
    {
        float currCountdownValue = tooltipTime;
        while (currCountdownValue > 0)
        {
            yield return new WaitForSeconds(0.1f);
            currCountdownValue -= 0.1f;
        }

        transform.position = Input.mousePosition - transform.localScale / 2;
        subpanel.SetActive(true);
    }
}
