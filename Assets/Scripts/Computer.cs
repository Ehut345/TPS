using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    [Header("Computer On/Off")]
    public bool lightsOn = true;
    public float radius = 2.5f;

    public Light lights;

    [Header("Computer Assign things")]
    public Player player;
    [SerializeField] private GameObject computerUI;
    [SerializeField] private int showcomputerUIFor = 5;
    private void Awake()
    {
        lights = GetComponent<Light>();
    }
    private void Update()
    {
        if(Vector3.Distance(transform.position,player.transform.position)<radius)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(ShowComputerUI());
                lightsOn = false;
                lights.intensity = 0;
                ObjectivesComplete.occurance.GetObjectivesDone(true, true, false, false);
            }
        }
    }
    IEnumerator ShowComputerUI()
    {
        computerUI.SetActive(true);
        yield return new WaitForSeconds(showcomputerUIFor);
        computerUI.SetActive(false);
    }
}
