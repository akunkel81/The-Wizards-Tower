using UnityEngine;

public class PlayerPrefsTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Raport Farmer: " + PlayerPrefs.GetFloat("rapportFarmer"));

        float test = PlayerPrefs.GetFloat("rapportMoonGirl");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
