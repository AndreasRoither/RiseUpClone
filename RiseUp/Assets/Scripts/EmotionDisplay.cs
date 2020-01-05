using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EmotionDisplay : MonoBehaviour
{
    #region Fields

    // close for objects that are close
    // love for saving the player
    // happy emotion randomly
    // lost emotions on loosing
    [Space] [Header("Emotions")] public List<Sprite> closeEmotions = new List<Sprite>();
    public List<Sprite> loveEmotions = new List<Sprite>();
    public List<Sprite> happyEmotions = new List<Sprite>();
    public List<Sprite> lostEmotions = new List<Sprite>();

    public float delayBetweenEmotions = 0f;
    
    [Space] [Header("Spawn bounds")] public float xLeftBound = 1f;
    public float xRightBound = 1f;
    public float yUpperBound = 1f;
    public float yLowerBound = 1f;

    private Vector3 topLeftCorner = Vector3.zero;
    private Vector3 topRightCorner = Vector3.zero;
    private Vector3 bottomLeftCorner = Vector3.zero;
    private Vector3 bottomRightCorner = Vector3.zero;

    private SpriteRenderer spriteRenderer;

    #endregion

    #region Lifecycle

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        var position = transform.position;
        topLeftCorner = new Vector3(position.x - xLeftBound, position.y + yUpperBound, 0);
        topRightCorner = new Vector3(position.x + xRightBound, position.y + yUpperBound, 0);
        bottomLeftCorner = new Vector3(position.x - xLeftBound, position.y - yLowerBound, 0);
        bottomRightCorner = new Vector3(position.x + xRightBound, position.y - yLowerBound, 0);
    }

    #endregion

    #region Methods

    #endregion

    #region Gizmos

    private void OnDrawGizmos()
    {
        var position = transform.position;
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