using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initPanel : MonoBehaviour
{
    public GameObject songPanel;
    public GameObject avatarPanel;
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
}
