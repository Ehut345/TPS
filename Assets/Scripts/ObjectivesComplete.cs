using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivesComplete : MonoBehaviour
{
    [Header("Objectives to complete")]
    public Text objectives1;
    public Text objectives2;
    public Text objectives3;
    public Text objectives4;

    public static ObjectivesComplete occurance;
    private void Awake()
    {
        occurance = this;
    }
    public void GetObjectivesDone(bool obj1, bool obj2, bool obj3, bool obj4)
    {
        if (obj1 == true)
        {
            objectives1.text = "1. Key picked up";
            objectives1.color = Color.green;
        }
        else
        {
            objectives1.text = "1. Find keys to open the gate";
            objectives1.color = Color.white;
        }
        if (obj2 == true)
        {
            objectives2.text = "2. Computer is offline";
            objectives2.color = Color.green;
        }
        else
        {
            objectives2.text = "2. Shutdown the computer";
            objectives2.color = Color.white;
        }
        if (obj3 == true)
        {
            objectives3.text = "3. Both of the Generators are shutdown";
            objectives3.color = Color.green;
        }
        else
        {
            objectives3.text = "3. Shutdown both of the Generators";
            objectives3.color = Color.white;
        }
        if (obj4 == true)
        {
            objectives4.text = "4. Mission Completed";
            objectives4.color = Color.green;
        }
        else
        {
            objectives4.text = "4. Escape from the facility";
            objectives4.color = Color.white;
        }
    }
}
