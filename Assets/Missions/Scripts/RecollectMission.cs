using UnityEngine;

public class RecollectMission : Mission
{
    private Products product;
    private Transform location;

    [SerializeField] private float missionTime;

    [HideInInspector] public static float currentTime;

    private bool missionFinished;

    [Header("Canvas Settings")]
    [SerializeField] private Transform failedCanvas;
    [SerializeField] private Transform passedCanvas;


    private void Awake()
    {
        currentTime = missionTime;
        missionFinished = false;
    }

    private void Update()
    {
        checkFinishConditions();
    }

    protected override void checkFinishConditions()
    {
        CheckEnemyCount();

        CheckMissionTime();
    }

    private void CheckEnemyCount()
    {
        
    }

    private void CheckMissionTime()
    {
        if (!missionFinished)
        {
            if (currentTime <= 0)
            {
                OnFailed();
                currentTime = 0;
                missionFinished = true;
            }
            else
            {
                currentTime -= Time.deltaTime;
            }
        }
    }

    protected override void OnPassed()
    {
        passedCanvas.gameObject.SetActive(true);
        Time.timeScale = 0.5f;
    }

    protected override void OnFailed()
    {
        failedCanvas.gameObject.SetActive(true);
        Time.timeScale = 0.5f;
    }
}