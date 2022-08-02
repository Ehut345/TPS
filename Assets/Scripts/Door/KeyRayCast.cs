using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KeyNetwork
{
    public class KeyRayCast : MonoBehaviour
    {
        [Header("RayCast Radius and Layers")]
        [SerializeField] private int rayRadius = 6;
        [SerializeField] private LayerMask layerMaskCollective;
        [SerializeField] private string banLayerName = null;

        private KeyObjectRegulator raycastedObject;
        [SerializeField] private KeyCode openGateButton = KeyCode.F;
        [SerializeField] private Image crossHair = null;

        private bool checkCrossHair;
        private bool oneTime;

        private string collectiveTag = "CollectiveObject";
        private void Update()
        {
            RaycastHit hitInfo;
            Vector3 forwarDirection = transform.TransformDirection(Vector3.forward);
            int mask = 1 << LayerMask.NameToLayer(banLayerName) | layerMaskCollective.value;

            if (Physics.Raycast(transform.position, forwarDirection, out hitInfo, rayRadius, mask))
            {
                if (hitInfo.collider.CompareTag(collectiveTag))
                {
                    if (!oneTime)
                    {
                        raycastedObject = hitInfo.collider.gameObject.GetComponent<KeyObjectRegulator>();
                        ChangeCrossHair(true);
                    }
                    checkCrossHair = true;
                    oneTime = true;
                    if (Input.GetKeyDown(openGateButton))
                    {
                        raycastedObject.FoundObject();
                    }
                }
            }
            else
            {
                if (checkCrossHair)
                {
                    ChangeCrossHair(false);
                    oneTime = false;
                }
            }
        }
        void ChangeCrossHair(bool changeCH)
        {
            if (changeCH && !oneTime)
            {
                crossHair.color = Color.blue;
            }
            else
            {
                crossHair.color = Color.white;
                checkCrossHair = false;
            }
        }
    }
}
