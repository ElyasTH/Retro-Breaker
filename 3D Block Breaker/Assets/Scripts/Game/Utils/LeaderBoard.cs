using System.Collections;
using LootLocker.Requests;
using UnityEngine;
using TMPro;

public class LeaderBoard : MonoBehaviour
{
    [Header("LiveLeaderBoard")]
    [SerializeField] GameHandler game;
    [SerializeField] private bool liveLeaderBoard = false;
    [SerializeField] private float updateCoolDown = 5;
    
    [Header("Menu")]
    [SerializeField] private int showCount = 10;

    int stageLeaderBoardID = 4713;
    float timer = 0;
    bool submitIsDone = false, inTop = false, settingIsDone = false, liveSessionStarted = false;

    private LootLockerLeaderboardMember[] members;
    [Header("Text Fields")]
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI scoreLeaderBoard;
    public TextMeshProUGUI player_ID_Text;

    string player_Name, player_ID, player_Score;
    int player_Rank, startingScore, state = 0;

    private void Awake()
    {
        timer = updateCoolDown + 1;
        startingScore = PlayerPrefs.GetInt("highScore");

        if (liveLeaderBoard && LootLockerSDKManager.CheckInitialized()) 
        {
            liveSessionStarted = true;
            StartCoroutine(submitScoreRoutine());
        }
        else if ((liveLeaderBoard && !LootLockerSDKManager.CheckInitialized()) || startingScore == 0)
        {
            playerName.text = "";
            scoreLeaderBoard.text = "";
        }
    }

    [System.Obsolete]
    private void Update()
    {
        if (liveSessionStarted)
        {
            if (timer > updateCoolDown)
            {
                if (PlayerPrefs.GetInt("highScore") < game.getScore())
                {
                    PlayerPrefs.SetInt("highScore", game.getScore());
                    StartCoroutine(submitScoreRoutine());
                    timer = 0;
                }
            }
            else
                timer += Time.time;
        }
        else if (LootLockerSDKManager.CheckInitialized() && state == 0) 
        {
            setPlayerStatus();
            player_ID_Text.text = "ID: " + player_ID.ToString();
            state = 1;
        }
        
    }

    [System.Obsolete]
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

                if (liveLeaderBoard)
                {
                    Debug.Log("this was called2");
                    getPlayerSurroundings();
                }
            }
            else
            {
                Debug.Log("Submit Error");
                done = true;
            }
        });
        yield return new WaitWhile(() => done = false);
    }

    [System.Obsolete]
    public IEnumerator fetchHighScores()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreList(stageLeaderBoardID, 2000, 0, (responce) =>
        {
            if (responce.success)
            {
                string tempPlayerNames = "Names\n";
                string tempPlayerScores = "Scores\n";

                members = responce.items;

                for (int i = 0; i < showCount; i++)
                {
                    tempPlayerNames += members[i].rank + ". ";
                    if (members[i].player.name != "")
                    {
                        if (members[i].member_id == PlayerPrefs.GetString("PlayerID")) 
                        {
                            inTop = true;
                            tempPlayerNames += "<b><color=yellow>" + members[i].player.name + "</color></b>";
                        }else
                            tempPlayerNames += members[i].player.name;
                    }
                    else
                    {
                        if (members[i].member_id == PlayerPrefs.GetString("PlayerID"))
                        {
                            inTop = true;
                            tempPlayerNames += "<b><color=yellow>" + members[i].player.id + "</color></b>";
                        }else
                        tempPlayerNames += members[i].player.id;
                    }


                    if (members[i].member_id == PlayerPrefs.GetString("PlayerID"))
                    {
                        inTop = true;
                        tempPlayerScores += "<b><color=yellow>" + members[i].score + "</color></b>" + "\n";
                    }
                    else
                        tempPlayerScores += members[i].score + "\n";

                    tempPlayerNames += "\n";
                }
                done = true;
                if (!inTop) 
                {
                    tempPlayerNames += player_Rank + ". ";
                    if (player_Name != "") 
                    {
                        tempPlayerNames += "<b><color=yellow>" + player_Name + "</color></b>";
                    }else
                        tempPlayerNames += "<b><color=yellow>" + player_ID + "</color></b>";

                    tempPlayerScores += "<b><color=yellow>" + player_Score + "</color></b>" + "\n";

                }
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

    public bool getSubmitionStatus() 
    {
        return submitIsDone;
    }

    public bool getSettingStatus() 
    {
        return settingIsDone;
    }

    public LootLockerLeaderboardMember[] getMembers() 
    {
        return members;
    }

    [System.Obsolete]
    public void setPlayerStatus() 
    {
        LootLockerSDKManager.GetMemberRank(stageLeaderBoardID, PlayerPrefs.GetString("PlayerID"), (thisPlayer) =>
        {
            if (thisPlayer.statusCode == 200)
            {
                player_Rank = thisPlayer.rank;
                print(player_Rank);
                player_Name = thisPlayer.player.name;
                player_ID = thisPlayer.player.id.ToString();
                player_Score = thisPlayer.score.ToString();
                settingIsDone = true;
            }
        });
    }

    [System.Obsolete]
    public void getPlayerSurroundings() 
    {
        LootLockerSDKManager.GetMemberRank(stageLeaderBoardID, PlayerPrefs.GetString("PlayerID"), (response1) =>
        {
            if (response1.statusCode == 200)
            {
                int rank = response1.rank;
                int count = 5;
                int after = rank < 2 ? 0 : rank - 2;

                LootLockerSDKManager.GetScoreList(stageLeaderBoardID, count, after, (response2) =>
                {
                    if (response1.statusCode == 200)
                    {
                        Debug.Log("this was called3");
                        string tempPlayerNames = "Names\n";
                        string tempPlayerScores = "Scores\n";

                        members = response2.items;
                        for (int i = 0; i < members.Length; i++)
                        {
                            tempPlayerNames += members[i].rank + ". ";
                            if (members[i].player.name != "")
                            {
                                if (members[i].member_id == PlayerPrefs.GetString("PlayerID"))
                                {
                                    inTop = true;
                                    tempPlayerNames += "<b><color=yellow>" + members[i].player.name + "</color></b>";
                                }
                                else
                                    tempPlayerNames += members[i].player.name;
                            }
                            else
                            {
                                if (members[i].member_id == PlayerPrefs.GetString("PlayerID"))
                                {
                                    inTop = true;
                                    tempPlayerNames += "<b><color=yellow>" + members[i].player.id + "</color></b>";
                                }
                                else
                                    tempPlayerNames += members[i].player.id;
                            }


                            if (members[i].member_id == PlayerPrefs.GetString("PlayerID"))
                            {
                                inTop = true;
                                tempPlayerScores += "<b><color=yellow>" + members[i].score + "</color></b>" + "\n";
                            }
                            else
                                tempPlayerScores += members[i].score + "\n";

                            tempPlayerNames += "\n";
                        }
                        playerName.text = tempPlayerNames;
                        scoreLeaderBoard.text = tempPlayerScores;
                    }
                    else
                    {
                        Debug.Log("failed: " + response1.Error);
                    }
                });
            }
            else
            {
                Debug.Log("failed: " + response1.Error);
            }
        });
    }

    public void updateBoard() 
    {
        if (getSubmitionStatus())
        {
            StartCoroutine(fetchHighScores());
        }
    }
}
