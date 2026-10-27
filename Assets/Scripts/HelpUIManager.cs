
using UnityEngine;

public class HelpUIManager : MonoBehaviour
{
    public GameObject helpPanel;

    public void OpenHelp()
    {
        helpPanel.SetActive(true);
    }

    public void CloseHelp()
    {
        helpPanel.SetActive(false);
    }
}
