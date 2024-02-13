using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public Vector2 pos;
    public bool enabled;
    public Color color;
}


public class Recording
{
    public List<float> times = new List<float>();
    public List<State> InitialStates = new List<State>();
    public List<List<State>> ActionsStates = new List< List<State>>();

    public string name = "";
    public float currentTime = 0;
    
}
