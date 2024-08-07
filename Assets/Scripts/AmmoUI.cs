using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    private TextMeshProUGUI ammoText;
    private TextMeshProUGUI magText;

    public static AmmoUI instance;

    private void Awake()
    {
        ammoText = transform.Find("AmmoText").GetComponent<TextMeshProUGUI>();
        magText = transform.Find("MagText").GetComponent<TextMeshProUGUI>();
        instance = this; 
    }

    public void UpdateAmmoText(int ammo)
    {
        ammoText.text = "Ammo: " + ammo.ToString();
    }
    public void UpdateMagText(int mag)
    {
        magText.text = "Magzine: " + mag.ToString();
    }
}
