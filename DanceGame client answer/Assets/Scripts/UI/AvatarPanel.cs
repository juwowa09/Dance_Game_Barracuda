using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarPanel : MonoBehaviour
{
    public ServerUDP udp;
    public GameObject activeAvatar;
    public GameObject activeUserAvatar;
    public GameObject initPanel;

    [Header("Anwser")]
    public GameObject maleBot;
    public GameObject femaleBot;
    public GameObject unityChan;
    public GameObject U;
    public GameObject shortCut;
    public GameObject white;
    public GameObject halloween;
    public GameObject ghost;
    public GameObject snow;
    public GameObject tween;
    
    [Header("User")]
    public GameObject userMaleBot;
    public GameObject userFemaleBot;
    public GameObject userUnityChan;
    public GameObject userU;
    public GameObject userShortCut;
    public GameObject userWhite;
    public GameObject userHalloween;
    public GameObject userGhost;
    public GameObject userSnow;
    public GameObject userTween;
    public void SetMaleBot()
    {
        activeAvatar.SetActive(false);
        activeUserAvatar.SetActive(false);
        
        Avatar av = userMaleBot.GetComponent<Avatar>();
        udp.avatar = av;
        
        maleBot.SetActive(true);
        userMaleBot.SetActive(true);
        activeAvatar = maleBot;
        activeUserAvatar = userMaleBot;
        
        GameManager.gameManager.avatar = av;
        GameManager.gameManager.ava = maleBot.transform;
        GameManager.gameManager.ansAnimator = maleBot.GetComponent<Animator>();
        GameManager.gameManager.posAnimator = userMaleBot.GetComponent<Animator>();
        GameManager.gameManager.Replace();
    }
    public void SetFemaleBot()
    {
        activeAvatar.SetActive(false);
        activeUserAvatar.SetActive(false);
        
        Avatar av = userFemaleBot.GetComponent<Avatar>();
        udp.avatar = av;
        
        femaleBot.SetActive(true);
        userFemaleBot.SetActive(true);
        activeAvatar = femaleBot;
        activeUserAvatar = userFemaleBot;
        
        GameManager.gameManager.avatar = av;
        GameManager.gameManager.ava = femaleBot.transform;
        GameManager.gameManager.ansAnimator = femaleBot.GetComponent<Animator>();
        GameManager.gameManager.posAnimator = userFemaleBot.GetComponent<Animator>();
        GameManager.gameManager.Replace();
    }
    public void SetUnityChan()
    {
        activeAvatar.SetActive(false);
        activeUserAvatar.SetActive(false);
        
        Avatar av = userUnityChan.GetComponent<Avatar>();
        udp.avatar = av;
        
        unityChan.SetActive(true);
        userUnityChan.SetActive(true);
        activeAvatar = unityChan;
        activeUserAvatar = userUnityChan;
        
        GameManager.gameManager.avatar = av;
        GameManager.gameManager.ava = unityChan.transform;
        GameManager.gameManager.ansAnimator = unityChan.GetComponent<Animator>();
        GameManager.gameManager.posAnimator = userUnityChan.GetComponent<Animator>();
        GameManager.gameManager.Replace();
    }
    public void SetU()
    {
        activeAvatar.SetActive(false);
        activeUserAvatar.SetActive(false);
        
        Avatar av = userU.GetComponent<Avatar>();
        udp.avatar = av;
        
        U.SetActive(true);
        userU.SetActive(true);
        activeAvatar = U;
        activeUserAvatar = userU;
        
        GameManager.gameManager.avatar = av;
        GameManager.gameManager.ava = U.transform;
        GameManager.gameManager.ansAnimator = U.GetComponent<Animator>();
        GameManager.gameManager.posAnimator = userU.GetComponent<Animator>();
        GameManager.gameManager.Replace();
    }
    public void SetShortCut()
    {
        activeAvatar.SetActive(false);
        activeUserAvatar.SetActive(false);
        
        Avatar av = userShortCut.GetComponent<Avatar>();
        udp.avatar = av;
        
        shortCut.SetActive(true);
        userShortCut.SetActive(true);
        activeAvatar = shortCut;
        activeUserAvatar = userShortCut;
        
        GameManager.gameManager.avatar = av;
        GameManager.gameManager.ava = shortCut.transform;
        GameManager.gameManager.ansAnimator = shortCut.GetComponent<Animator>();
        GameManager.gameManager.posAnimator = userShortCut.GetComponent<Animator>();
        GameManager.gameManager.Replace();
    }public void SetWhite()
    {
        activeAvatar.SetActive(false);
        activeUserAvatar.SetActive(false);
        
        Avatar av = userWhite.GetComponent<Avatar>();
        udp.avatar = av;
        
        white.SetActive(true);
        userWhite.SetActive(true);
        activeAvatar = white;
        activeUserAvatar = userWhite;
        
        GameManager.gameManager.avatar = av;
        GameManager.gameManager.ava = white.transform;
        GameManager.gameManager.ansAnimator = white.GetComponent<Animator>();
        GameManager.gameManager.posAnimator = userWhite.GetComponent<Animator>();
        GameManager.gameManager.Replace();
    }public void SetHalloween()
    {
        activeAvatar.SetActive(false);
        activeUserAvatar.SetActive(false);
        
        Avatar av = userHalloween.GetComponent<Avatar>();
        udp.avatar = av;
        
        halloween.SetActive(true);
        userHalloween.SetActive(true);
        activeAvatar = halloween;
        activeUserAvatar = userHalloween;
        
        GameManager.gameManager.avatar = av;
        GameManager.gameManager.ava = halloween.transform;
        GameManager.gameManager.ansAnimator = halloween.GetComponent<Animator>();
        GameManager.gameManager.posAnimator = userHalloween.GetComponent<Animator>();
        GameManager.gameManager.Replace();
    }public void SetGhost()
    {
        activeAvatar.SetActive(false);
        activeUserAvatar.SetActive(false);
        
        Avatar av = userGhost.GetComponent<Avatar>();
        udp.avatar = av;
        
        ghost.SetActive(true);
        userGhost.SetActive(true);
        activeAvatar = ghost;
        activeUserAvatar = userGhost;
        
        GameManager.gameManager.avatar = av;
        GameManager.gameManager.ava = ghost.transform;
        GameManager.gameManager.ansAnimator = ghost.GetComponent<Animator>();
        GameManager.gameManager.posAnimator = userGhost.GetComponent<Animator>();
        GameManager.gameManager.Replace();
    }public void SetSnow()
    {
        activeAvatar.SetActive(false);
        activeUserAvatar.SetActive(false);
        
        Avatar av = userSnow.GetComponent<Avatar>();
        udp.avatar = av;
        
        snow.SetActive(true);
        userSnow.SetActive(true);
        activeAvatar = snow;
        activeUserAvatar = userSnow;
        
        GameManager.gameManager.avatar = av;
        GameManager.gameManager.ava = snow.transform;
        GameManager.gameManager.ansAnimator = snow.GetComponent<Animator>();
        GameManager.gameManager.posAnimator = userSnow.GetComponent<Animator>();
        GameManager.gameManager.Replace();
    }public void SetTween()
    {
        activeAvatar.SetActive(false);
        activeUserAvatar.SetActive(false);
        
        Avatar av = userTween.GetComponent<Avatar>();
        udp.avatar = av;
        
        tween.SetActive(true);
        userTween.SetActive(true);
        activeAvatar = tween;
        activeUserAvatar = userTween;
        
        GameManager.gameManager.avatar = av;
        GameManager.gameManager.ava = tween.transform;
        GameManager.gameManager.ansAnimator = tween.GetComponent<Animator>();
        GameManager.gameManager.posAnimator = userTween.GetComponent<Animator>();
        GameManager.gameManager.Replace();
    }
    
    
    
    public void Back()
    {
        initPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
