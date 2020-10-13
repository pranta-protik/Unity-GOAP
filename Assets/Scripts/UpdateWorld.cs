using System.Collections;
using System.Collections.Generic;
using GOAP;
using UnityEngine;
using UnityEngine.UI;

public class UpdateWorld : MonoBehaviour
{
    public Text states;
    
    void LateUpdate()
    {
        Dictionary<string, int> worldStates = GWorld.Instance.GetWorld().GetStates();
        states.text = "";
        foreach (KeyValuePair<string,int> worldState in worldStates)
        {
            states.text += worldState.Key + ": " + worldState.Value + "\n";
        }
    }
}
