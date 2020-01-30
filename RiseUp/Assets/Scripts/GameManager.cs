using System.Collections;
using Controller;
using GameInput;
using Level;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Utility;

public class GameManager : MonoBehaviour
{
    private const string PlayerScore = "player_highscore";
    
    public GameUiManager uiManager;
    public EmotionDisplay emotionDisplay;
    public RisingUpController risingUpController;
    public TargetJoint2D player;
    public LevelLoader levelLoader;
    public Animator jellyAnimator;
    public float startDelay = 3f;

    private Coroutine currentCoroutine;
    private Vector3 newPosition = new Vector3(0, -1 ,0);
    private string triggerName = "Stop";

    private void Awake()
    {
        InputManager.ContinuousInputEvent.AddListener(OnInput);
        InputManager.InputEndEvent.AddListener(OnInputEnd);
    }

    private void Start()
    {
        currentCoroutine = StartCoroutine(StartDelay());
        risingUpController.hitEvent.AddListener(OnRiseUpHit);
        risingUpController.closeByEvent.AddListener(OnRiseUpCloseByHit);
    }

    private void Update()
    {
        uiManager.SetScore((int) risingUpController.GetHeight());
        uiManager.SetLevel(DifficultyController.Instance.DifficultyLevel);
    }

    private void FixedUpdate()
    {
        player.target = newPosition;
    }

    public void LoadStartMenu()
    {
        levelLoader.LoadLevel(0);
    }

    public void Reload()
    {
        levelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnInput(Vector3 position)
    {
        newPosition = position;
        player.gameObject.SetActive(true);
    }

    private void OnInputEnd(Vector3 position)
    {
        player.gameObject.SetActive(false);
    }

    private void OnRiseUpHit()
    {
        int score = (int) risingUpController.GetHeight();
        int highScore = PlayerPrefs.GetInt(PlayerScore, score);
        
        if (highScore <= score)
        {
            PlayerPrefs.SetInt(PlayerScore, score);
            PlayerPrefs.Save();
            highScore = score;
        }
        
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        jellyAnimator.SetTrigger(triggerName);
        uiManager.SetMidText("Nice try!\nScore: " + score);
        uiManager.SetMidText2("Best: " + highScore);
        uiManager.ToggleRetryUi(true);
        emotionDisplay.DisplayEmotionIfPossible(EmotionDisplay.Emotion.Lost);
    }

    private void OnRiseUpCloseByHit()
    {
        emotionDisplay.DisplayEmotionIfPossible(EmotionDisplay.Emotion.Close);
    }

    private IEnumerator StartDelay()
    {
        for (var i = startDelay; i > 0; --i)
        {
            uiManager.SetMidText("Starting in\n" + i);
            yield return new WaitForSeconds(1);
        }

        uiManager.SetMidText("");
        uiManager.StartGradientChange();
        risingUpController.StartRise(0);
    }
}