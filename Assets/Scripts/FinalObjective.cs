using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalObjective : MonoBehaviour
{
    [Header("Car button")]
    [SerializeField] private KeyCode vehicleButton = KeyCode.F;

    [Header("Generator Sound Effects and Radius")]
    private float radius = 3f;
    public Player player;

    private void Update()
    {
        if (Input.GetKeyDown(vehicleButton) && Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(0);
            ObjectivesComplete.occurance.GetObjectivesDone(true, true, true, true);
        }
    }
}
