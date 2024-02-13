using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject[] allbuttons;

    public static UIManager Instance
    {
        get;
        private set;
    }

    public void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        allbuttons = GameObject.FindGameObjectsWithTag("Button");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
