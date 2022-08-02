using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCount : MonoBehaviour
{
    [SerializeField] public Text ammoText;
    [SerializeField] public Text magText;

    public static AmmoCount occurrence;

    private void Awake()
    {
        occurrence = this;
        ammoText.text = "Ammo : " + 20;
        magText.text = "Magazines : " + 15;
    }
    public void UpdateAmmoText(int presentAmmo)
    {
        ammoText.text = "Ammo : " + presentAmmo;
    }
    public void UpdateMagText(int presentMags)
    {
       magText.text = "Magazines : " + presentMags;
    }
}
