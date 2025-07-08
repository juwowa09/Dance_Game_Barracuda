using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SongPanel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected GameObject m_SongButton;
    [SerializeField] protected ScrollRect m_ScrollRect;
    [SerializeField] protected RectTransform m_ScrollViewContent;
    [SerializeField] protected SelectedSongPanel m_SelectedSongPanel;
    private SongAsset[] allSongs;
    private Sprite[] SongImg;
    void Start()
    {
        allSongs = Resources.LoadAll<SongAsset>("Music/Audio");
        // Debug.Log(allSongs.Length);
        foreach(var song in allSongs)
        {
            song.highScore = LoadUserScore(song).highScore;
            
            Debug.Log("Good Load Score");
            GameObject songBt = Instantiate(m_SongButton, m_ScrollViewContent);
            TextMeshProUGUI btText = songBt.GetComponentInChildren<TextMeshProUGUI>();
            
            btText.text = song.songTitle;
            // Debug.Log(btText.text);
            Button btn = songBt.GetComponent<Button>();
            
            btn.onClick.AddListener(() =>
            {
                m_SelectedSongPanel.DisplaySong(song);
            });
        }
        m_SelectedSongPanel.DisplaySong(allSongs[0]);
    }
    private SongAsset LoadUserScore(SongAsset song)
    {
        string path = Application.persistentDataPath + "/SongScores/" + song.songTitle + ".json";
        
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, song);  // 덮어쓰기!
            return song;
        }
        else
        {
            return song;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
