using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public GameObject endMenu;

    public TextMeshProUGUI levelText;

    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void ShowEndMenu()
    {
        endMenu.SetActive(true);
        levelText.text = GameManager.instance.floorNumber.ToString();

        gameObject.GetComponent<MouseCursor>().ShowCursor();
        gameObject.GetComponent<MouseCursor>().enabled = false;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

}
