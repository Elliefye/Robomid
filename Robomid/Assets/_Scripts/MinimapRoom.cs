﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapRoom : MonoBehaviour
{
    private bool m_active = false;
    public Vector2Int coordinate;
    [SerializeField]
    private Sprite[] sprites;
    private bool m_discovered = false;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public bool Discovered
    {
        get
        {
            return m_discovered;
        }
        set
        {
            m_discovered = value;
            if (value == true)
            {
                spriteRenderer.color = Color.grey;
                spriteRenderer.sprite = sprites[0];
            }
            else
            {
                spriteRenderer.color = new Color(0, 0, 0, 0);
                spriteRenderer.sprite = sprites[0];
            }
        }
    }

    public bool Active
    {
        get
        {
            return m_active;
        }
        set
        {
            m_active = value;

            if (value == true)
            {
                spriteRenderer.sprite = sprites[1];
                spriteRenderer.color = Color.white;
            }
            else
            {
                spriteRenderer.sprite = sprites[0];
                spriteRenderer.color = Color.grey;
            }
        }
    }
}
