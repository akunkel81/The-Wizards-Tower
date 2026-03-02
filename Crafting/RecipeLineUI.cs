using UnityEngine;
using TMPro;

public class RecipeLineUI : MonoBehaviour
{
    public TMP_Text requirementText;
    public TMP_Text haveText;

    public void Set(string ingredientName, int need, int have)
    {
        if (requirementText != null)
            requirementText.text = ingredientName + " x" + need;

        if (haveText != null)
        {
            haveText.text = have.ToString();
            haveText.color = (have >= need) ? Color.green : Color.red;
        }

        gameObject.SetActive(true);
    }

    public void SetEmpty()
    {
        if (requirementText != null) requirementText.text = "";
        if (haveText != null) haveText.text = "";
        gameObject.SetActive(false);
    }
}