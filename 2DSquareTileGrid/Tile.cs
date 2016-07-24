using UnityEngine;
using System.Collections;
using System;

[Serializable]
[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour {

    [SerializeField]
    private Coordinate coord;

    public Coordinate Coords
    {
        get { return coord; }
    }

    protected TileGrid grid;

    public void InitializeTile(Sprite sprite, Coordinate coordinate, TileGrid grid)
    {
        var spriteRender = GetComponent<SpriteRenderer>();
        spriteRender.sprite = sprite;
        this.transform.SetParent(grid.transform, false);
        var x_offset = (sprite == null ? 0 : sprite.bounds.size.x);
        var y_offset = (sprite == null ? 0 : sprite.bounds.size.y);
        this.transform.localPosition = new Vector2(coordinate.X * x_offset, coordinate.Y * y_offset);
        coord = coordinate;

        this.name = "Tile " + coord.ToString();
    }	
}