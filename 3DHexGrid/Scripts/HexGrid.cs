using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
public class HexGrid : MonoBehaviour 
{
    public int width = 6;
    public int height = 6;
    public HexCell cellPrefab;
    public Text cellLabelPrefab;

    public Color defaultColor;
    public Color hoverColor;
    public Color selectedColor;

    public HexCell SelectedCell;
    public HexCell HoveredCell;

    Canvas gridCanvas;
    HexCell[] cells;
    HexMesh hexMesh;

    void Awake()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();
        cells = new HexCell[width * height];
        for(int z = 0, i = 0; z < height; z++)
        {
            for(int x = 0; x < width; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }

    void Start()
    {
        hexMesh.Triangulate(cells);
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool wasHit = Physics.Raycast(inputRay, out hit);
        bool mousePressed = Input.GetMouseButtonDown(0);
        if (wasHit)
        {            
            if (mousePressed)
            {
                SelectCell(hit.point);
            }
            else
            {
                HoverCell(hit.point);
            }
        }
        else if (mousePressed)
        {
            ToggleSelectedCell(null, selectedColor, defaultColor);
        }
        else
        {
            ToggleHoveredCell(null, hoverColor, defaultColor);
        }

           
    }
    void HoverCell(Vector3 position)
    {
        var coords = GetCoordinates(position);
        var hoveredCell = GetCellByCoordinates(coords);
        ToggleHoveredCell(hoveredCell, hoverColor, defaultColor);
    }
    void SelectCell(Vector3 position)
    {
        var coords = GetCoordinates(position);
        var selectedCell = GetCellByCoordinates(coords);
        ToggleSelectedCell(selectedCell, selectedColor, defaultColor);
    }
    private void ToggleHoveredCell(HexCell input, Color newColor, Color oldColor)
    {
        if (input != null && input.IsSelected) { return; }
        if(HoveredCell != null)
        {
            if(HoveredCell != input)
            {
                HoveredCell.color = oldColor;
            }
        }
        if(input != null)
        {            
            input.color = newColor;
        }
        HoveredCell = input;        
        Refresh();
    }
    private void ToggleSelectedCell(HexCell input, Color newColor, Color oldColor)
    {
        if (input != SelectedCell)
        {
            if (SelectedCell != null)
            {
                SelectedCell.color = oldColor;
            }
            SelectedCell = input;
            if(input != null)
            {
                input.color = newColor;
            }            
        }
        else
        {
            input.color = oldColor;
            SelectedCell = null;
        }
        if(HoveredCell == SelectedCell)
        {
            HoveredCell = null;
        }
        Refresh();
    }

    private HexCell GetCellByCoordinates(HexCoordinates coords)
    {
        int index = coords.X + coords.Z * width + coords.Z / 2;
        return cells[index];
    }
    private HexCoordinates GetCoordinates(Vector3 position)
    {
        position = transform.InverseTransformPoint(position);
        return HexCoordinates.FromPosition(position);
    }

    void CreateCell(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f);

        HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
        cell.name = "HexCell" + cell.coordinates;
        
        cell.color = defaultColor;

        Text label = Instantiate<Text>(cellLabelPrefab);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
        label.text = cell.coordinates.ToStringOnSeparateLines();
        label.name = "HexCellLabel(" + x.ToString() + ", " + z.ToString() + ")";
    }

    public void Refresh()
    {
        hexMesh.Triangulate(cells);
    }
}

