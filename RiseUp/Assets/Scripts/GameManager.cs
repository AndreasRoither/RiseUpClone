using System;
using System.Collections;
using Controller;
using GameInput;
using UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameUiManager uiManager;
    public RisingUpController risingUpController;
    public float startDelay = 3f;
    public GameObject player;

    private Coroutine currentCoroutine = null;

    private void Awake()
    {
        InputManager.ContinuousInputEvent.AddListener(OnInput);
    }

    private void Start()
    {
        currentCoroutine = StartCoroutine(StartDelay());
        risingUpController.hitEvent.AddListener(OnRiseUpHit);
    }

    private void OnInput(Vector3 position)
    {
        player.transform.position = position;
    }

    private void OnRiseUpHit()
    {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        uiManager.SetMidText("Nice try! Your highscore:" + (int)risingUpController.GetHeight());
    }

    private void Update()
    {
        uiManager.SetScore((int) risingUpController.GetHeight());
    }

    private IEnumerator StartDelay()
    {
        for (float i = startDelay; i >= 0; --i)
        {
            uiManager.SetMidText("Starting in\n" + i);
            yield return new WaitForSeconds(1);
        }

        uiManager.SetMidText("");
        risingUpController.StartRise(0);
    }
}