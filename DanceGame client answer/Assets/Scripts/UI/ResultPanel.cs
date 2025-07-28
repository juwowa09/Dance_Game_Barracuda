using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI title;
    [SerializeField] protected TextMeshProUGUI Artist;
    [SerializeField] protected TextMeshProUGUI highScore;
    [SerializeField] protected TextMeshProUGUI curScore;
    [SerializeField] protected Image TitleImg;
    [SerializeField] protected Button btn;
    [SerializeField] protected GameObject songPanel;
    [SerializeField] protected AudioSource audio;
    
    void Start()
    {
        btn.onClick.AddListener(Next);
    }
    public void display(SongAsset Song, float score)
    {
        title.text = Song.songTitle;
        Artist.text = Song.artist;
        highScore.text = Song.highScore.ToString("F2");
        curScore.text = score.ToString("F2");
        TitleImg.sprite = Song.coverImage;
    }

    void Next()
    {
        songPanel.SetActive(true);
        audio.Play();
        this.gameObject.SetActive(false);
    }
}
