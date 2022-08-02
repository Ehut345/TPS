using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyNetwork
{
    public class KeyGateRegulator : MonoBehaviour
    {
        [Header("Animations")]
        private Animator gateAnim;
        private bool openGate = false;
        [SerializeField] private string openAnimationName = "GateOpen";
        [SerializeField] private string closeAnimationName = "GateClosed";

        [Header("Time and UI")]
        [SerializeField] private int timeToShowUi = 1;
        [SerializeField] private GameObject showGateLockedUI = null;
        [SerializeField] private KeyList keyList = null;
        [SerializeField] private int waitTimer = 1;
        [SerializeField] private bool pauseInteraction = false;

        private void Awake()
        {
            gateAnim = gameObject.GetComponent<Animator>();
        }
        public void StartAnimantion()
        {
            if (keyList.hasKey)
            {
                OpenGate();
            }
            else
            {
                StartCoroutine(ShowGateLocked());
            }
        }
        private IEnumerator StopGateInterConnection()
        {
            pauseInteraction = true;
            yield return new WaitForSeconds(waitTimer);
            pauseInteraction = false;
        }
        void OpenGate()
        {
            if (!openGate && !pauseInteraction)
            {
                gateAnim.Play(openAnimationName, 0, 0.0f);
                openGate = true;
                ObjectivesComplete.occurance.GetObjectivesDone(true, false, false, false);
                StartCoroutine(StopGateInterConnection());
            }
            else if (openGate && !pauseInteraction)
            {
                gateAnim.Play(closeAnimationName, 0, 0.0f);
                openGate = false;
                StartCoroutine(StopGateInterConnection());
            }
        }
        IEnumerator ShowGateLocked()
        {
            showGateLockedUI.SetActive(true);
            yield return new WaitForSeconds(timeToShowUi);
            showGateLockedUI.SetActive(false);
        }
    }
}
