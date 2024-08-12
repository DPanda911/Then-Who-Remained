using UnityEngine;
using Ink.Runtime;
using System.Collections.Generic;
using System.IO;

public class DialogueVariables
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }

    public Story globalVariableStory;
    private const string saveVariablesKey = "INK_VARIABLES";

    public DialogueVariables(TextAsset loadGlobalsJSON)
    {
        
        globalVariableStory = new Story(loadGlobalsJSON.text);
        if (PlayerPrefs.HasKey(saveVariablesKey))
        {
            string jsonState = PlayerPrefs.GetString(saveVariablesKey);
            globalVariableStory.state.LoadJson(jsonState);
        }

        variables =  new Dictionary<string, Ink.Runtime.Object>();
        foreach(string name in globalVariableStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariableStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            Debug.Log("Initilized global dialogue variable:" + name + " = " + value);
        }



    }


    public void StartListening(Story story)
    {
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += variableChanged;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= variableChanged;
    }
    private void variableChanged(string name, Ink.Runtime.Object value)
    {
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);
        }

    }

    private void VariablesToStory(Story story)
    {
        foreach(KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }

    //Call this method to save variables!
    public void saveVariables()
    {
        if(globalVariableStory != null)
        {
            //Load the current state of all our variables to the globals story
            VariablesToStory(globalVariableStory);
            PlayerPrefs.SetString(saveVariablesKey, globalVariableStory.state.ToJson());
        }
    }

    
}
