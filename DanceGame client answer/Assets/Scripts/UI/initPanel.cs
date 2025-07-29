using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initPanel : MonoBehaviour
{
    public GameObject songPanel;
    public GameObject avatarPanel;
    public GameObject Cam;
    // Start is called before the first frame update
    public void SongPanel()
    {
        songPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void AvatarPanel()
    {
        avatarPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnOff()
    {
        if (Cam.activeSelf)
        {
            Cam.gameObject.SetActive(false);
        }
        else
        {
            Cam.gameObject.SetActive(true);
        }
    }
}
