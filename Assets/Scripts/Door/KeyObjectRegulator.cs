using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyNetwork
{
    public class KeyObjectRegulator : MonoBehaviour
    {
        [SerializeField] public bool key = false;
        [SerializeField] public bool gate = false;
        [SerializeField] public KeyList keyList = null;

        private KeyGateRegulator gateObject;
        private void Start()
        {
            if (gate)
            {
                gateObject = GetComponent<KeyGateRegulator>();
            }
        }
        public void FoundObject()
        {
            if(key)
            {
                keyList.hasKey = true;
                gameObject.SetActive(false);
            }
            else if(gate)
            {
                gateObject.StartAnimantion();
            }
        }
    }
}
