using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[RequireComponent(typeof(SpriteRenderer))]
public class EmotionDisplay : MonoBehaviour
{
    public enum Emotion
    {
        Close,
        Love,
        Happy,
        Lost
    }

    private readonly Random random = new Random();
    private Vector3 bottomLeftCorner = Vector3.zero;
    private Vector3 bottomRightCorner = Vector3.zero;


    // close for objects that are close
    // love for saving the player
    // happy emotion randomly
    // lost emotions on loosing
    [Space] [Header("Emotions")] public List<Sprite> closeEmotions = new List<Sprite>();

    private Coroutine currentRoutine;
    private Sprite currentSprite;
    public float delayBetweenEmotions;
    private float displayTime;
    public List<Sprite> happyEmotions = new List<Sprite>();
    private Vector3 initialLocalPosition;
    public bool lostEmotionOverride = true;
    public List<Sprite> lostEmotions = new List<Sprite>();
    public List<Sprite> loveEmotions = new List<Sprite>();
    public float maxEmotionTime = 5f;

    [Space] [Header("Emotion time")] public float minEmotionTime = 3f;
    private SpriteRenderer spriteRenderer;

    private Vector3 topLeftCorner = Vector3.zero;
    private Vector3 topRightCorner = Vector3.zero;

    [Space] [Header("Spawn bounds")] public float xLeftBound = 1f;
    public float xRightBound = 1f;
    public float yLowerBound = 1f;
    public float yUpperBound = 1f;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) Debug.LogError($"{nameof(EmotionDisplay)} {nameof(spriteRenderer)} is missing.");
    }

    private void Start()
    {
        var position = transform.position;
        initialLocalPosition = position;

        topLeftCorner = new Vector3(position.x - xLeftBound, position.y + yUpperBound, 0);
        topRightCorner = new Vector3(position.x + xRightBound, position.y + yUpperBound, 0);
        bottomLeftCorner = new Vector3(position.x - xLeftBound, position.y - yLowerBound, 0);
        bottomRightCorner = new Vector3(position.x + xRightBound, position.y - yLowerBound, 0);

        DisplayRandomEmotion(Emotion.Happy);
    }


    public void DisplayRandomEmotion(Emotion emotion)
    {
        if (!(lostEmotionOverride && emotion == Emotion.Close))
            if (currentSprite != null && Time.time < displayTime + delayBetweenEmotions)
                return;
        if (currentRoutine != null) StopCoroutine(currentRoutine);

        var position = initialLocalPosition;

        switch (emotion)
        {
            case Emotion.Close:
                currentSprite = closeEmotions[random.Next(0, closeEmotions.Count)];
                break;
            case Emotion.Love:
                currentSprite = loveEmotions[random.Next(0, loveEmotions.Count)];
                break;
            case Emotion.Happy:
                currentSprite = happyEmotions[random.Next(0, happyEmotions.Count)];
                break;
            case Emotion.Lost:
                currentSprite = lostEmotions[random.Next(0, lostEmotions.Count)];
                break;
            default:
                currentSprite = null;
                break;
        }

        var x = (float) random.NextDouble() * (position.x + xRightBound - (position.x - xLeftBound)) +
                (position.x - xLeftBound);
        var y = (float) random.NextDouble() * (position.y + yUpperBound - (position.y - yLowerBound)) +
                (position.y - yLowerBound);
        spriteRenderer.transform.localPosition = new Vector3(x, y, 0);

        if (currentSprite == null) return;

        spriteRenderer.sprite = currentSprite;
        displayTime = Time.time;
        currentRoutine = StartCoroutine(RemoveSpriteAfterDelay());
    }

    private IEnumerator RemoveSpriteAfterDelay()
    {
        var vanishTime = random.NextDouble() * (maxEmotionTime - minEmotionTime) + minEmotionTime;
        yield return new WaitForSeconds((float) vanishTime);
        spriteRenderer.sprite = null;
        currentSprite = null;
    }

    #region Gizmos

    private void OnDrawGizmos()
    {
        // in case parent is null; get correct display of the gizmos since this transform is changed over time
        var position = transform.parent == null ? transform.position : transform.parent.position;

        topLeftCorner = new Vector3(position.x - xLeftBound, position.y + yUpperBound, 0);
        topRightCorner = new Vector3(position.x + xRightBound, position.y + yUpperBound, 0);
        bottomLeftCorner = new Vector3(position.x - xLeftBound, position.y - yLowerBound, 0);
        bottomRightCorner = new Vector3(position.x + xRightBound, position.y - yLowerBound, 0);
        Gizmos.color = Color.blue;
        // Top
        Gizmos.DrawLine(topLeftCorner, topRightCorner);
        // Right
        Gizmos.DrawLine(topRightCorner, bottomRightCorner);
        // Bottom
        Gizmos.DrawLine(bottomRightCorner, bottomLeftCorner);
        // Left
        Gizmos.DrawLine(bottomLeftCorner, topLeftCorner);
    }

    #endregion
}