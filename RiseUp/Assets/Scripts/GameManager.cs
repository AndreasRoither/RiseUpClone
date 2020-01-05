﻿using System.Collections;
using Controller;
using GameInput;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Coroutine currentCoroutine;
    public EmotionDisplay emotionDisplay;
    private Vector3 newPosition = Vector3.zero;
    public Rigidbody2D player;
    public RisingUpController risingUpController;
    public float startDelay = 3f;

    public GameUiManager uiManager;


    private void Awake()
    {
        InputManager.ContinuousInputEvent.AddListener(OnInput);
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
    }

    private void FixedUpdate()
    {
        //player.MovePosition(newPosition);
        player.position = newPosition;
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Reload()
    {
        SceneManager.LoadScene(1);
    }

    private void OnInput(Vector3 position)
    {
        //player.transform.position = position;
        //player.MovePosition(position);
        newPosition = position;
    }

    private void OnInputEnd()
    {
    }

    private void OnRiseUpHit()
    {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        uiManager.SetMidText("Nice try! Your highscore:" + (int) risingUpController.GetHeight());
        uiManager.ToggleRetryUi(true);
        emotionDisplay.DisplayRandomEmotion(EmotionDisplay.Emotion.Lost);
    }

    private void OnRiseUpCloseByHit()
    {
        emotionDisplay.DisplayRandomEmotion(EmotionDisplay.Emotion.Close);
    }

    private IEnumerator StartDelay()
    {
        for (var i = startDelay; i > 0; --i)
        {
            uiManager.SetMidText("Starting in\n" + i);
            yield return new WaitForSeconds(1);
        }

        uiManager.SetMidText("");
        risingUpController.StartRise(0);
    }
}