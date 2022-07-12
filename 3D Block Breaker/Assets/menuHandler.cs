using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class menuHandler : MonoBehaviour
{
    public AudioSource ad;
    public GameObject infoTab;
    public TextMeshProUGUI level;
    public TextMeshProUGUI score;

    private void Awake()
    {
        level.text = PlayerPrefs.GetInt("level").ToString();
        score.text = PlayerPrefs.GetInt("highScore").ToString();
    }

    public void onPlay() 
    {
        SceneManager.LoadScene(1);
        ad.Play();
    }
    public void onInfo() 
    {
        ad.Play();
        infoTab.SetActive(true);
    }
    public void onInfoClose() 
    {
        ad.Play();
        infoTab.SetActive(false);
    }

    public void admin() 
    {
        PlayerPrefs.DeleteAll();
    }
}
