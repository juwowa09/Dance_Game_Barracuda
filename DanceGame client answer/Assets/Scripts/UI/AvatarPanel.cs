using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarPanel : MonoBehaviour
{
    public ServerUDP udp;
    public GameObject femaleBot;
    public GameObject unityChan;
    public GameObject U;
    public GameObject activeAvatar;
    public GameObject initPanel;

    public void SetBot()
    {
        activeAvatar.SetActive(false);
        Avatar av = femaleBot.GetComponent<Avatar>();
        udp.avatar = av;
        femaleBot.SetActive(true);
        activeAvatar = femaleBot;
        GameManager.gameManager.avatar = av;
    }
    public void SetUnityChan()
    {
        activeAvatar.SetActive(false);
        Avatar av = unityChan.GetComponent<Avatar>();
        udp.avatar = av;
        unityChan.SetActive(true);
        activeAvatar = unityChan;
        GameManager.gameManager.avatar = av;
    }
    public void SetU()
    {
        activeAvatar.SetActive(false);
        Avatar av = U.GetComponent<Avatar>();
        udp.avatar = av;
        U.SetActive(true);
        activeAvatar = U;
        GameManager.gameManager.avatar = av;
    }
    public void Back()
    {
        initPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
