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
    [SerializeField] protected Animator animator;
    [SerializeField] protected GameObject songPanel;
    [SerializeField] protected Transform ava;
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
        animator.Play(song);
        Quaternion temp = ava.rotation;
        Vector3 tempLoc = ava.localPosition;
        if (song.Equals("super shy"))
        {
            ava.Rotate(0,-80,0);
        }else if (song.Equals("likejennie"))
        {
            ava.Rotate(0,-50,0);
        }
        else if (song.Equals("haidilao"))
        {
            ava.Rotate(0, -10, 0);
        }

        GameManager.gameManager.restart(audio.clip.length, temp, tempLoc, selectedSong);
        songPanel.SetActive(false);
        // 게임매니저 스타트
    }
}
