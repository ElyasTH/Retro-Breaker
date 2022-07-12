using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuHandler : MonoBehaviour
{
    public AudioSource ad;
    public GameObject infoTab;


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
}
