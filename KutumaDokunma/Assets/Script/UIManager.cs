using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;

    [SerializeField] private TMP_InputField userName;
    [SerializeField] private TextMeshProUGUI userNameText;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("UserName"))
        {
            OpenPanel(0);
        }
        else
        {
            OpenPanel(1);
            userNameText.text = " " + PlayerPrefs.GetString("UserName");
        }
    }

    private void ExitAllPanels()
    {
        foreach (var panel in panels) panel.gameObject.SetActive(false);
    }

    private void OpenPanel(int id)
    {
        ExitAllPanels();
        for (int i = 0; i < panels.Length; i++)
        {
            if (i == id)
            {
                panels[i].SetActive(true);
            }
        }
    }

    public void SaveUserName()
    {
        PlayerPrefs.SetString("UserName", userName.text);
        userNameText.text = " " +PlayerPrefs.GetString("UserName");
        OpenPanel(1);
    }
}
