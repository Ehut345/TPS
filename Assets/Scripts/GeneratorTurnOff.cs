using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorTurnOff : MonoBehaviour
{
    [Header("Generator Lights and button")]
    public GameObject greenLight;
    public GameObject redLight;
    public bool button;

    [Header("Generator Sound Effects and Radius")]
    private float radius = 2f;
    public Player player;
    public Animator anim;

    [Header("Sounds and UI")]
    [SerializeField] private AudioClip objectivesCompleteSound;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        button = false;
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            button = true;
            anim.enabled = false;
            greenLight.SetActive(false);
            redLight.SetActive(true);
            audioSource.Stop();
            ObjectivesComplete.occurance.GetObjectivesDone(true, true, true, false);
            audioSource.PlayOneShot(objectivesCompleteSound);
        }
        else if (button == false)
        {
            greenLight.SetActive(true);
            redLight.SetActive(false);
        }
    }
}
