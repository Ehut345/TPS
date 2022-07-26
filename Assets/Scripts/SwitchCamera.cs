using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [Header("Camera to Assign")]
    [SerializeField] public GameObject AimCam;
    [SerializeField] public GameObject AimCanvas;
    [SerializeField] public GameObject ThirdPersonCam;
    [SerializeField] public GameObject ThirdPersonCanvas;
    [Header("Camera Animator")]
    [SerializeField] public Animator anim;



    void Update()
    {
        if (Input.GetButton("Fire2") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetBool("Idel", false);
            anim.SetBool("IdelAim", true);
            anim.SetBool("AimWalk", true);
            anim.SetBool("Walk", true);

            ThirdPersonCam.SetActive(false);
            ThirdPersonCanvas.SetActive(false);
            AimCam.SetActive(true);
            AimCanvas.SetActive(true);
        }
        else if (Input.GetButton("Fire2"))
        {
            anim.SetBool("Idel", false);
            anim.SetBool("IdelAim", true);
            anim.SetBool("AimWalk", false);
            anim.SetBool("Walk", false);

            ThirdPersonCam.SetActive(false);
            ThirdPersonCanvas.SetActive(false);
            AimCam.SetActive(true);
            AimCanvas.SetActive(true);
        }
        else
        {
            anim.SetBool("Idel", true);
            anim.SetBool("IdelAim", false);
            anim.SetBool("AimWalk", false);

            ThirdPersonCam.SetActive(true);
            ThirdPersonCanvas.SetActive(true);
            AimCam.SetActive(false);
            AimCanvas.SetActive(false);
        }
    }
}
