using System;
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

    // close for objects that are close
    // love for saving the player
    // happy emotion randomly
    // lost emotions on loosing
    [Space] [Header("Emotions")] 
    public List<Sprite> closeEmotions = new List<Sprite>();
    public List<Sprite> happyEmotions = new List<Sprite>();
    public List<Sprite> lostEmotions = new List<Sprite>();
    public List<Sprite> loveEmotions = new List<Sprite>();
    public bool lostEmotionOverride = true;
    
    [Space][Header("Infinite Emotions")]
    public bool infiniteEmotion = false;
    public Emotion infiniteEmotionType;
    public float repeatTime = 3f;
    
    [Space] [Header("Emotion time")] 
    public float minEmotionTime = 3f;
    public float maxEmotionTime = 5f;
    public float delayBetweenEmotions;
    
    [Space] [Header("Spawn bounds")] public float xLeftBound = 1f;
    public float xRightBound = 1f;
    public float yLowerBound = 1f;
    public float yUpperBound = 1f;

    private readonly Random random = new Random();
    private Vector3 topLeftCorner = Vector3.zero;
    private Vector3 topRightCorner = Vector3.zero;
    private Vector3 bottomLeftCorner = Vector3.zero;
    private Vector3 bottomRightCorner = Vector3.zero;
    private Vector3 initialLocalPosition;
    private Coroutine currentRoutine;
    private Sprite currentSprite;
    private SpriteRenderer spriteRenderer;
    private float displayTime;

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

        currentSprite = happyEmotions[random.Next(0, happyEmotions.Count)];
        DisplayEmotion();
        
        if (infiniteEmotion)
        {
            StartCoroutine(SearchForTargetRepeat());
        }
    }

    public void DisplayEmotionIfPossible(Emotion emotion)
    {
        if (!(lostEmotionOverride && emotion == Emotion.Lost))
            if (Time.time < displayTime + delayBetweenEmotions)
                return;
        if (currentRoutine != null) StopCoroutine(currentRoutine);
        
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
        
        DisplayEmotion();
    }

    private void DisplayEmotion()
    {
        var position = initialLocalPosition;
        var x = (float) random.NextDouble() * (position.x + xRightBound - (position.x - xLeftBound)) +
                (position.x - xLeftBound);
        var y = (float) random.NextDouble() * (position.y + yUpperBound - (position.y - yLowerBound)) +
                (position.y - yLowerBound);
        spriteRenderer.transform.localPosition = new Vector3(x, y, 0) * gameObject.transform.localScale.x;

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
    
    private IEnumerator SearchForTargetRepeat() {
        while(infiniteEmotion) {
            DisplayEmotionIfPossible(infiniteEmotionType);
            yield return new WaitForSeconds(repeatTime);
        }
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