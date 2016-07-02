using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HexCell : MonoBehaviour
{
    public HexCoordinates coordinates;
    public Color color;
    public bool IsSelected { get { return IsSelectedCell(); } }
    public bool IsHovered { get { return IsHoveredCell(); } }
    private HexGrid grid;

    void Awake()
    {
        grid = GetComponentInParent<HexGrid>();
    }
    void Start()
    {
        if(grid == null)
        {
            grid = GetComponentInParent<HexGrid>();
        }
    }

    private bool IsHoveredCell()
    {
        return grid.HoveredCell != null && grid.HoveredCell == this;
    }
    private bool IsSelectedCell()
    {
        return grid.SelectedCell != null && grid.SelectedCell == this;
    }
}

