using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    [Tooltip("result panel.")]
    [SerializeField] protected ResultPanel resultPanel;
    [SerializeField] protected SongPanel songPanel;
    [SerializeField] protected GameObject initPanel;
    [SerializeField] protected GameObject avatarPanel;

    [SerializeField] public Transform ava;
    [SerializeField] protected Animator ansAnimator;
    [SerializeField] protected Animator posAnimator;
    [SerializeField] public Avatar avatar;
    private HumanPoseHandler ansHandler;
    private HumanPoseHandler posHandler;
    private int perfect = 0;
    private int great = 0;
    private int good = 0;
    private int bad = 0;

    private Coroutine scoreCoroutine;
    
    [Tooltip("Play Button")] [SerializeField]
    protected Button m_PlayButton;
    
    int[] limbMuscles = new int[]
    {
        // 팔
        55, 56, 57, 58, 59, 60, 61, 62,
        // 다리
        1, 2, 3, 4, 5, 6, 7, 8, 9, 10
    };
    
    // Start is called before the first frame update
    void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(gameObject);
            return;
        }
        gameManager = this;
        DontDestroyOnLoad(gameManager);
    }

    void Start()
    {
        ansHandler = new HumanPoseHandler(ansAnimator.avatar, ansAnimator.transform);
        posHandler = new HumanPoseHandler(posAnimator.avatar, posAnimator.transform);
        songPanel.gameObject.SetActive(false);
        resultPanel.gameObject.SetActive(false);
        avatarPanel.gameObject.SetActive(false);
        initPanel.SetActive(true);
        
        // if (ansAnimator.avatar.isHuman)
        // {
        //     Debug.Log("이 Animator는 Humanoid 타입입니다.");
        // }
        // else
        // {
        //     Debug.Log("이 Animator는 Humanoid가 아닙니다.");
        // }
    }

    public void restart(float sec, SongAsset song)
    {
        string title = song.songTitle.ToLower();
        ansAnimator.Play(title);
        Quaternion temp = ava.rotation;
        Vector3 tempLoc = ava.localPosition;
        // if (song.songTitle.ToLower().Equals("super shy"))
        // {
        //     Debug.Log("ss");
        //     ava.Rotate(0,-80,0);
        // }
        // // else if (song.songTitle.ToLower().Equals("likejennie"))
        // // {
        // //     Debug.Log("ll");
        // //     ava.Rotate(0,-50,0);
        // // }
        // else if (song.songTitle.ToLower().Equals("haidilao"))
        // {
        //     Debug.Log("hh");
        //     ava.Rotate(0, -10, 0);
        // }
        if (title.Equals("jump"))
        {
            ava.Rotate(0,-30,0);
        }
        StartCoroutine(WaitForSong(sec, temp, tempLoc, song));
    }

    IEnumerator WaitForSong(float sec, Quaternion quat, Vector3 loc, SongAsset song)
    {
        perfect = 0;
        great = 0;
        good = 0; 
        bad = 0;
        // 점수판정 실행
        float reactTimer = 0f;
        float timer = 0f;
        float interval = 5.0f;
        perfect = 0;
        int t = 0;

        while (timer < sec + 2.0f)
        {
            if (reactTimer >= interval)
            {
                StartCoroutine(Judgement());
                reactTimer = 0;
                t++;
            }
            reactTimer += Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }

        // 결과 출력
        // Debug.Log("노래끝");
        float pointPerUnit = 100f / t;
        float weightedScore =
            (perfect * 1.0f + great * 0.75f + good * 0.5f + bad * 0.25f) * pointPerUnit;
        // Debug.Log(weightedScore);
        if (song.highScore < weightedScore)
            song.highScore = weightedScore;
        ava.rotation = quat;
        ava.localPosition = loc;
        resultPanel.gameObject.SetActive(true);
        resultPanel.display(song, weightedScore);
    }

    IEnumerator Judgement()
    {
        HumanPose pose1 = new HumanPose();
        ansHandler.GetHumanPose(ref pose1);
        yield return new WaitForSeconds(0.3f);
        HumanPose pose2 = new HumanPose();
        posHandler.GetHumanPose(ref pose2);
        
        float similarity = CompareLimbPose(pose1, pose2); // 유사도 (작을수록 유사)
        // Debug.Log("simliarity: " + similarity);
        if (similarity < 0.925f)
        {
            avatar.reactScore(0);
            bad++;
        }
        else if (similarity < 0.95f)
        {
            avatar.reactScore(1);
            good++;
        }
        else if (similarity < 0.975f)
        {
            avatar.reactScore(2);
            great++;
        }
        else
        {
            avatar.reactScore(3);
            perfect++;
        }
    }
    
    float CompareLimbPose(HumanPose a, HumanPose b)
    {
        float dot = 0f;
        float normA = 0f;
        float normB = 0f;

        foreach (int i in limbMuscles)
        {
            float va = a.muscles[i];
            float vb = b.muscles[i];
            // Debug.Log(a.muscles[i]+" "+b.muscles[i]);

            dot += va * vb;
            normA += va * va;
            normB += vb * vb;
        }

        float denominator = Mathf.Sqrt(normA) * Mathf.Sqrt(normB);
        if (denominator == 0f)
            return 0f;

        return Mathf.Clamp01((dot / denominator + 1f) / 2f); // → 0~1로 정규화
    }

}
