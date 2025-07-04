using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    [SerializeField] protected Transform ava;
    [Tooltip("Song panel.")]
    [SerializeField] protected GameObject songPanel;
    [Tooltip("result panel.")]
    [SerializeField] protected GameObject resultPanel;

    [SerializeField] protected Animator ansAnimator;
    [SerializeField] protected Animator posAnimator;
    [SerializeField] protected Avatar avatar;
    private HumanPoseHandler ansHandler;
    private HumanPoseHandler posHandler;
    private int perfect = 0;
    private int great = 0;
    private int good = 0;
    private int bad = 0;
    
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
    }

    public void restart(float sec, Quaternion quat, Vector3 loc,SongAsset song)
    {
        StartCoroutine(WaitForSong(sec, quat, loc, song));
    }

    IEnumerator WaitForSong(float sec, Quaternion quat, Vector3 loc, SongAsset song)
    {
        perfect = 0;
        great = 0;
        good = 0; 
        bad = 0;
        // 점수판정 실행
        float timer = 0f;
        float interval = 5.0f;
        perfect = 0;
        int t = 0;

        while (timer < sec)
        {
            yield return new WaitForSeconds(interval);
            yield return StartCoroutine(Judgement());
            timer += (interval + 0.5f);
            t++;
        }

        // 결과 출력
        // Debug.Log("노래끝");
        float pointPerUnit = 100f / t;
        float weightedScore =
            (perfect * 1.0f + great * 0.75f + good * 0.5f + bad * 0.25f) * pointPerUnit;
        Debug.Log(weightedScore);
        if (song.highScore < weightedScore)
            song.highScore = weightedScore;
        ava.rotation = quat;
        ava.localPosition = loc;
        songPanel.SetActive(true);
    }

    IEnumerator Judgement()
    {
        HumanPose pose1 = new HumanPose();
        ansHandler.GetHumanPose(ref pose1);
        yield return new WaitForSeconds(0.5f);
        HumanPose pose2 = new HumanPose();
        posHandler.GetHumanPose(ref pose2);
        
        float similarity = CompareLimbPose(pose1, pose2); // 유사도 (작을수록 유사)
        Debug.Log("simliarity: " + similarity);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
