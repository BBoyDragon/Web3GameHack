using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class FinishController:ICleanup
{
    private FinishData _data;
    private FinishView _winView;
    private FinishView _loseView;

    public FinishController(FinishData finishData)
    {
        _data = finishData;
    }

    public void Win()
    {
        _winView = GameObject.Instantiate<FinishView>(_data.WinView);
        _winView.Init();
        _winView.OnRestart += Restart;
        _winView.StartCoroutine(IncrementScore());       
    }
    public void Lose()
    {
        _loseView = GameObject.Instantiate<FinishView>(_data.LoseView);
        _loseView.Init();
        _loseView.OnRestart += Restart;
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Cleanup()
    {
        if (_winView != null)
        {
            _winView.OnRestart -= Restart;
        }
        if (_loseView != null)
        {
            _loseView.OnRestart -= Restart;
        }
    }

    private static readonly string incrementScoreUrl = "https://ismaxis.ru/api/leaderboard/scores";
    private IEnumerator IncrementScore()
    {
        string json = JsonUtility.ToJson(new IncrementScoreBody(PlayerPrefs.GetString("Sub"), 0, PlayerPrefs.GetString("User")));
        var www = new UnityWebRequest(incrementScoreUrl, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();
    }

    [System.Serializable]
    class IncrementScoreBody
    {
        public string sub;
        public long score;
        public string username;

        public IncrementScoreBody(string sub, long score, string username)
        {
            this.sub = sub;
            this.score = score;
            this.username = username;
        }
    }
}
