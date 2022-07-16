using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using LootLocker.Requests;

public class menuHandler : MonoBehaviour
{
    public AudioSource ad;
    public AudioSource errorSound;
    public GameObject infoTab;
    public GameObject leaderBoardHolder;
    public TextMeshProUGUI level;
    public TextMeshProUGUI score;
    public TextMeshProUGUI scoreLeaderBoard;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI ErrorText;
    //LeaderBoard
    int stageLeaderBoardID = 4713;
    int counter = 0;
    bool loginIsDone = false;
    bool submitIsDone = false;
    public TMP_InputField playerNameInput;
    LootLockerLeaderboardMember[] members;

    private void Awake()
    {
        level.text = PlayerPrefs.GetInt("level").ToString();
        score.text = PlayerPrefs.GetInt("highScore").ToString();
    }
    private void Start()
    {
        StartCoroutine(loginRoutine());
    }

    private void Update()
    {
        if (counter == 0 && loginIsDone)
        {
            if (PlayerPrefs.GetInt("highScore") != 0)
                StartCoroutine(submitScoreRoutine());
            counter = 1;
        }
    }

    public void SetPlayerName()
    {
        bool IsValid = true;
        for (int i = 0; i < members.Length; i++)
        {
            if (playerNameInput.text == members[i].player.name)
            {
                IsValid = false;
                ErrorText.text = "This Name Is Taken";
            }
        }
        for (int i = 0; i < playerNameInput.text.Length; i++)
        {
            if (playerNameInput.text[i] >= '!' && playerNameInput.text[i] <= '+')
            {
                IsValid = false;
                ErrorText.text = "Invalid Characters";
            }
        }
        if (playerNameInput.text.Length > 13)
        {
            IsValid = false;
            ErrorText.text = "Name Is Too Long";
        }


        if (IsValid)
        {
            ad.Play();
            ErrorText.text = "";
            LootLockerSDKManager.SetPlayerName(playerNameInput.text, (responce) =>
            {
                if (responce.success)
                {
                    Debug.Log("Set Name Success");
                    StartCoroutine(fetchHighScores());
                }
                else
                {
                    Debug.Log("Name Error");
                }
            });
        }
        else 
        {
            errorSound.Play();
        }
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

    IEnumerator loginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((responce) =>
        {
            if (responce.success)
            {
                Debug.Log("Login Success");
                PlayerPrefs.SetString("PlayerID", responce.player_id.ToString());
                done = true;
                loginIsDone = true;
            }
        });
        yield return new WaitWhile(() => done = false);
    }

    //leaderBoard

    public IEnumerator submitScoreRoutine()
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, PlayerPrefs.GetInt("highScore"), stageLeaderBoardID, (responce) =>
         {
             if (responce.success)
             {
                 Debug.Log("Submit Score Success");
                 done = true;
                 submitIsDone = true;
             }
             else
             {
                 Debug.Log("Submit Error");
                 done = true;
             }
         });
        yield return new WaitWhile(() => done = false);
    }

    public IEnumerator fetchHighScores()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreList(stageLeaderBoardID, 7, 0, (responce) =>
        {
            if (responce.success)
            {
                string tempPlayerNames = "Names\n";
                string tempPlayerScores = "Scores\n";

                members = responce.items;

                for (int i = 0; i < members.Length; i++)
                {
                    tempPlayerNames += members[i].rank + ". ";
                    if (members[i].player.name != "")
                    {
                        tempPlayerNames += members[i].player.name;
                    }
                    else
                    {
                        tempPlayerNames += members[i].player.id;
                    }

                    tempPlayerScores += members[i].score + "\n";
                    tempPlayerNames += "\n";
                }
                done = true;
                playerName.text = tempPlayerNames;
                scoreLeaderBoard.text = tempPlayerScores;
            }
            else
            {
                Debug.Log("Failed Fetching LeaderBoard");
                done = true;
            }
        });
        yield return new WaitWhile(() => done = false);
    }

    public void openLeaderBoard()
    {
        ad.Play();
        leaderBoardHolder.SetActive(true);
        if (counter == 1 && submitIsDone)
        {
            StartCoroutine(fetchHighScores());
            counter = 2;
        }

    }
    public void closeLeaderBoard() 
    {
        ad.Play();
        leaderBoardHolder.SetActive(false);
    }
}
