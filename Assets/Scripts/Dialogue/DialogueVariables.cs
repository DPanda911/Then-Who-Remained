using UnityEngine;
using Ink.Runtime;
using System.Collections.Generic;
using System.IO;

public class DialogueVariables
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }

    public DialogueVariables(string globalIsFilePath)
    {
        string inkFileContents = File.ReadAllText(globalIsFilePath);
        Ink.Compiler compiler = new Ink.Compiler(inkFileContents);
        Story globalVariableStory = compiler.Compile();

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
}
