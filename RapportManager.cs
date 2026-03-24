using UnityEngine;

public class RapportManager : MonoBehaviour
{
    // This is used for Rapport UI and end of year stuff (?)


    public float GetFarmerRapport()
    {
        return PlayerPrefs.GetFloat("rapportFarmer");
    }

    public float GetBakerRapport()
    {
        return PlayerPrefs.GetFloat("rapportBaker");
    }

    public float GetSalesmanRapport()
    {
        return PlayerPrefs.GetFloat("rapportSalesman");
    }

    public float GetSadFatherRapport()
    {
        return PlayerPrefs.GetFloat("rapportSadFather");
    }

    public float GetMinerRapport()
    {
        return PlayerPrefs.GetFloat("rapportMiner");
    }

    public float GetMoonGirlRapport()
    {
        return PlayerPrefs.GetFloat("rapportMoonGirl");
    }

    public float GetGoldenWizardRapport()
    {
        return PlayerPrefs.GetFloat("rapportGoldenWizard");
    }

}
