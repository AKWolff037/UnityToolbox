using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
public class HexGrid : MonoBehaviour 
{
    public HexCell spawnThis;
    
    public int x = 5;
    public int y = 5;

    public float radius = 0.5f;
    public bool useAsInnerCircleRadius = true;

    public HexCell SelectedHex;
    public HexCell HoveredHex;

    private float offsetX, offsetY;

    void Start()
    {
        float unitLength = (useAsInnerCircleRadius) ? (radius / (Mathf.Sqrt(3) / 2)) : radius;
        offsetX = unitLength * Mathf.Sqrt(3);
        offsetY = unitLength * 0.5f;

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Vector2 hexPos = HexOffset(i, j);
                Vector3 pos = new Vector3(hexPos.x, hexPos.y, 0);
                var newHex = Instantiate(spawnThis, pos, Quaternion.identity) as HexCell;
                newHex.name = spawnThis.name + "(" + i + ", " + j + ")";
                newHex.transform.SetParent(this.transform, false);
                
            }
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (SelectedHex != null)
            {
                SelectedHex.IsSelected = false;
                Debug.Log("Unselected " + SelectedHex.name);                
            }
            if(SelectedHex == HoveredHex)
            {
                SelectedHex = null;
            }
            else if (HoveredHex != null)
            {            
                HoveredHex.IsSelected = true;
                SelectedHex = HoveredHex;
                Debug.Log("Selected " + SelectedHex.name);
            }            
        }
    }
    Vector2 HexOffset(int x, int y)
    {
        Vector2 position = Vector2.zero;

        if(y % 2 == 0)
        {
            position.x = x * offsetX;
            position.y = y * offsetY;
        }
        else
        {
            //position.x = x * offsetX;
            position.x = (x + 0.5f) * offsetX;
            position.y = y * offsetY;
            //position.y = (y - radius) * offsetY;
        }

        return position;        
    }

}

