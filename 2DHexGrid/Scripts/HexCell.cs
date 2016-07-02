using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
[RequireComponent(typeof(HexSprite))]
public class HexCell : MonoBehaviour
{
    public Color defaultColor;
    public Color hoverColor;
    public Color selectedColor;

    public bool IsSelected;
    public bool IsHover { get; private set; }

    private SpriteRenderer spriteRenderer;
    private HexGrid grid;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        grid = GetComponentInParent<HexGrid>();
    }
    void OnMouseEnter()
    {
        IsHover = true;
        grid.HoveredHex = this;
    }
    void OnMouseExit()
    {
        IsHover = false;
        grid.HoveredHex = null;
    }
    void Update()
    {
        Color theColor = IsSelected ? selectedColor : IsHover ? hoverColor : defaultColor;
        SetColor(theColor);
    }

    void SetColor(Color theColor)
    {
        if(spriteRenderer.color != theColor)
        {
            spriteRenderer.color = theColor;
        }        
    }
}

