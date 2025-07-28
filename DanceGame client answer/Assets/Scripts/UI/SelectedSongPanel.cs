using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedSongPanel : MonoBehaviour
{
    private SongAsset selectedSong;
    [SerializeField]protected TextMeshProUGUI title;
    [SerializeField]protected TextMeshProUGUI Artist;
    [SerializeField]protected TextMeshProUGUI Time;
    [SerializeField] protected TextMeshProUGUI HighScore;
    [SerializeField] protected Image TitleImg;
    [SerializeField] protected AudioSource audio;
    [SerializeField] protected Button btn;
    [SerializeField] protected GameObject songPanel;
    // [SerializeField] protected Transform ava;
    // Start is called before the first frame update
    void Start()
    {
        btn.onClick.AddListener(PlaySong);
    }

    public void DisplaySong(SongAsset song)
    {
        selectedSong = song;
        audio.clip = song.audioClip;
        audio.Play();
        title.text = song.songTitle;
        Artist.text = song.artist;
        HighScore.text = song.highScore.ToString("F2");
        Time.text = (int)song.audioClip.length/60 + ":" + (((int)song.audioClip.length % 60 < 10) ? ("0" + (int)song.audioClip.length % 60) : (int)song.audioClip.length % 60);
        TitleImg.sprite = song.coverImage;
    }

    void PlaySong()
    {
        String song = title.text.ToLower();
        audio.Play();
        GameManager.gameManager.restart(audio.clip.length, selectedSong);
        songPanel.SetActive(false);
        // 게임매니저 스타트
    }
}
