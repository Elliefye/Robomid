using System.Collections;
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
            if(value == true)
            {
                m_discovered = true;
                spriteRenderer.color = Color.grey;
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
            if(value == true)
            {
                m_active = true;
                spriteRenderer.sprite = sprites[1];
                spriteRenderer.color = Color.white;
            }
            else
            {
                m_active = false;
                spriteRenderer.sprite = sprites[0];
                spriteRenderer.color = Color.grey;
            }
        }
    }
}
