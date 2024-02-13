using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class RecordingSystem : MonoBehaviour
{
    public GameObject RecordingButton;
    public GameObject inputField;
    public GameObject loadButton;
    public GameObject playButton;

    public List<Recording> recordings = new List<Recording>();
    public bool isRecording = false;

    public List<GameObject> gameObjects;
    public Recording CurrentRecording;

    public static RecordingSystem Instance
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
        gameObjects.AddRange(GameObject.FindGameObjectsWithTag("Button").ToList());
        gameObjects.AddRange(GameObject.FindGameObjectsWithTag("Popup").ToList());
        gameObjects.AddRange(GameObject.FindGameObjectsWithTag("Tooltip").ToList());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (isRecording)
        //{
        //    RecordAction();
        //}
    }

    public void RecordButtonPressed()
    {
        if (isRecording)
        {
            StopRecording();
        }
        else
        {
            StartRecording();
        }
    }

    public void StartRecording()
    {
        TMP_InputField input = inputField.GetComponent<TMP_InputField>();
        string name = input.text;

        if (name == "") return;

        isRecording = true;

        CurrentRecording = new Recording();
        CurrentRecording.name = name;

        foreach (Recording r in recordings)
        {
            if (name == r.name)
            {
                recordings.Remove(r);
                break;
            }
        }

        foreach (GameObject go in gameObjects)
        {
            State state = new State();
            state.pos = go.transform.position;
            state.enabled = go.activeSelf;
            //if (go.GetComponent<Image>() != null) {
            //    state.color = go.GetComponent<Image>().GetComponent<Color>();
            //}
            CurrentRecording.InitialStates.Add(state);
        }
        CurrentRecording.times.Add(0f);
        StartCoroutine(Timing());
    }

    public void StopRecording()
    {
        StopCoroutine(Timing());
        isRecording = false;
        SaveRecording();
    }

    public void LoadRecording()
    {

    }

    public void PlayRecording()
    {
        if (recordings.Count == 0) { return; }

        TMP_InputField input = inputField.GetComponent<TMP_InputField>();
        string name = input.text;

        Recording r = null;

        if (name == "")
        {
            r = recordings.Last();
        }
        else
        {
            foreach (Recording temp in recordings)
            {
                if (name == temp.name)
                {
                    r = temp;
                    break;
                }
            }
        }
        

        if (r == null) return;

        int i = 0;
        foreach (GameObject go in gameObjects)
        {
            go.transform.position = r.InitialStates[i].pos;
            go.SetActive(r.InitialStates[i].enabled);

            //if (go.GetComponent<Image>() != null)
            //{
            //    go.GetComponent<Image>().GetComponent<Color>().Equals(r.InitialStates[i].color);
            //}
            i++;
        }
        StartCoroutine(ReplayActions(r, 1));
    }

    IEnumerator ReplayActions(Recording r, int step)
    {
        yield return new WaitForSeconds(r.times[step] - r.times[step-1]);
        //Debug.Log(r.times[step] - r.times[step - 1]);
        if (step < r.ActionsStates.Count())
        {
            int i = 0;
            foreach (GameObject go in gameObjects)
            {
                go.transform.position = r.ActionsStates[step][i].pos;
                go.SetActive(r.ActionsStates[step][i].enabled);

                //if (go.GetComponent<Image>() != null)
                //{
                //    go.GetComponent<Image>().GetComponent<Color>().Equals(r.ActionsStates[step][i].color);
                //}
                i++;
            }
            StartCoroutine(ReplayActions(r, ++step));
        }
    }

    private void SaveRecording()
    {
        recordings.Add(CurrentRecording);
        CurrentRecording = null;
    }

    public void RecordAction()
    {
        if (CurrentRecording.times.Last() == CurrentRecording.currentTime) { return; }

        List<State> currentStates = new List<State>();
        foreach (GameObject go in gameObjects)
        {
            State state = new State();
            state.pos = go.transform.position;
            state.enabled = go.activeSelf;
            //state.color = go.GetComponent<Color>();
            currentStates.Add(state);
        }
        if (isRecording)
        {
            CurrentRecording.ActionsStates.Add(currentStates);
            CurrentRecording.times.Add(CurrentRecording.currentTime);
        }
    }

    IEnumerator Timing()
    {
        yield return new WaitForSeconds(0.1f);
        if (isRecording)
        {
            CurrentRecording.currentTime += 0.1f;
            StartCoroutine(Timing());
        }
    }
}