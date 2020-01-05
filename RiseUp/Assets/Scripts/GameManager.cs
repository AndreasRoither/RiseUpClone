using System;
using System.Collections;
using Controller;
using GameInput;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Fields

    public GameUiManager uiManager;
    public RisingUpController risingUpController;
    public float startDelay = 3f;
    public Rigidbody2D player;

    private Coroutine currentCoroutine = null;
    private Vector3 newPosition = Vector3.zero;

    #endregion

    #region Lifecycle

    private void Awake()
    {
        InputManager.ContinuousInputEvent.AddListener(OnInput);
    }

    private void Start()
    {
        currentCoroutine = StartCoroutine(StartDelay());
        risingUpController.hitEvent.AddListener(OnRiseUpHit);
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

    #endregion

    #region Methods

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
    }

    private IEnumerator StartDelay()
    {
        for (float i = startDelay; i > 0; --i)
        {
            uiManager.SetMidText("Starting in\n" + i);
            yield return new WaitForSeconds(1);
        }

        uiManager.SetMidText("");
        risingUpController.StartRise(0);
    }

    #endregion
}