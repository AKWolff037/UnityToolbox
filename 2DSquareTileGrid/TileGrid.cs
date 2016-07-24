using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class TileGrid : MonoBehaviour
{
    public Tile tilePrefab;

    public Sprite grassSprite;
    public Sprite leftRightRoadSprite;
    public Sprite updownRoadSprite;
    public Sprite corner_bottom_right_Sprite;
    public Sprite corner_right_bottom_Sprite;
    public Sprite corner_top_right_Sprite;
    public Sprite corner_right_top_Sprite;

    public int width = 5;
    public int height = 5;

    private Tile[,] grid;
    private Dictionary<Coordinate, Sprite> roadMap;
    private LinkedList<Coordinate> roads;

    public void Awake()
    {
        grid = new Tile[width,height];
        roadMap = CreateRoadMap();
        CreateGrid();
    }

    private void CreateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var coordinate = new Coordinate(x, y);
                var tileSprite = GetTileSprite(coordinate);
                var newTile = Instantiate(tilePrefab);
                newTile.InitializeTile(tileSprite, coordinate, this);
                grid[x,y] = newTile;
                
                if(y == (height - 1) && x == (width - 1))
                {
                    newTile.gameObject.AddComponent<Castle>();
                }
                else if (y == 0 && x == 0)
                {
                    newTile.gameObject.AddComponent<SpawnPoint>();
                }
            }        
        }
    }
    private Dictionary<Coordinate, Sprite> CreateRoadMap()
    {
        var startingPoint = new Coordinate(0, 0);
        var endingPoint = new Coordinate(width - 1, height - 1);

        var coordinateMap = new Dictionary<Coordinate, Sprite>();

        roads = new LinkedList<Coordinate>();
        roads.AddFirst(startingPoint);
        coordinateMap.Add(startingPoint, null);                

        var iterations = 0;
        var max_iterations = 5000;        

        while(!coordinateMap.ContainsKey(endingPoint) && iterations < max_iterations)
        {
            var lastCoordinate = roads.Last.Value;
            var validCoordinates = new List<Coordinate>();
            var rightCoordinate = Coordinate.Right(lastCoordinate);
            var upCoordinate = Coordinate.Up(lastCoordinate);
            var downCoordinate = Coordinate.Down(lastCoordinate);
            
            validCoordinates.Add(rightCoordinate);
            validCoordinates.Add(upCoordinate);
            validCoordinates.Add(downCoordinate);
            //Remove any invalid coordinates based on X/Y position
            validCoordinates.RemoveAll(c => c.X < 0 || c.Y < 0 || c.X > (width - 1) || c.Y > (height - 1));

            //Remove any existing coordinates as valid
            validCoordinates.RemoveAll(c => coordinateMap.ContainsKey(c));

            //If on the last column, remove down coordinate
            if(lastCoordinate.X == (width - 1))
            {
                validCoordinates.Remove(downCoordinate);
                //Add up coordinate if it doesn't exist for some reason
                if(!validCoordinates.Contains(upCoordinate)) { validCoordinates.Add(upCoordinate); }
            }

            if(validCoordinates.Count > 0)
            {
                var newCoordinate = validCoordinates[UnityEngine.Random.Range(0, validCoordinates.Count)];
                coordinateMap.Add(newCoordinate, null);
                roads.AddAfter(roads.Last, newCoordinate);
            }
            else
            {
                Debug.Log("No valid coordinates found for " + lastCoordinate);
            }
            iterations++;
        }
        Debug.Log("Finished grid in " + iterations + " iterations");
        MapCoordinatesToSprites(roads, ref coordinateMap);
        return coordinateMap;
    }

    private void MapCoordinatesToSprites(LinkedList<Coordinate> directions, ref Dictionary<Coordinate, Sprite> map)
    {
        var currentPoint = directions.First;
        while(currentPoint != null)
        {
            var priorPoint = currentPoint.Previous;
            var nextPoint = currentPoint.Next;

            var priorDirection = (priorPoint == null) ? CoordinateDirection.NONE : GetDirection(priorPoint.Value, currentPoint.Value);
            var nextDirection = (nextPoint == null) ? CoordinateDirection.NONE : GetDirection(currentPoint.Value, nextPoint.Value);

            var sprite = GetSprite(priorDirection, nextDirection);
            map[currentPoint.Value] = sprite;

            currentPoint = currentPoint.Next;
        }
    }

    private CoordinateDirection GetDirection(Coordinate start, Coordinate end)
    {
        if(end == Coordinate.Up(start))
        {
            return CoordinateDirection.UP;
        }
        else if (end == Coordinate.Down(start))
        {
            return CoordinateDirection.DOWN;
        }
        else if (end == Coordinate.Right(start))
        {
            return CoordinateDirection.RIGHT;
        }
        else
        {
            return CoordinateDirection.NONE;
        }
    }

    private Sprite GetSprite(CoordinateDirection priorDirection, CoordinateDirection nextDirection)
    {
        Sprite spr = null;
        if(priorDirection == CoordinateDirection.NONE)
        {
            if(nextDirection == CoordinateDirection.RIGHT)
            {
                spr = leftRightRoadSprite;
            }
            else
            {
                spr = updownRoadSprite;
            }
        }
        else if (nextDirection == CoordinateDirection.NONE)
        {
            if(priorDirection == CoordinateDirection.RIGHT)
            {
                spr = leftRightRoadSprite;
            }
            else
            {
                spr = updownRoadSprite;
            }
        }
        else
        {
            if(priorDirection == CoordinateDirection.RIGHT)
            {
                if(nextDirection == CoordinateDirection.UP)
                {
                    spr = corner_right_top_Sprite;
                }
                else if (nextDirection == CoordinateDirection.DOWN)
                {
                    spr = corner_right_bottom_Sprite;
                }
                else if (nextDirection == CoordinateDirection.RIGHT)
                {
                    spr = leftRightRoadSprite;
                }
            }
            else if (priorDirection == CoordinateDirection.UP)
            {
                if(nextDirection == CoordinateDirection.RIGHT)
                {
                    spr = corner_bottom_right_Sprite;
                }
                else if (nextDirection == CoordinateDirection.UP || nextDirection == CoordinateDirection.DOWN)
                {
                    spr = updownRoadSprite;
                }
            }
            else if (priorDirection == CoordinateDirection.DOWN)
            {
                if(nextDirection == CoordinateDirection.RIGHT)
                {
                    spr = corner_top_right_Sprite;
                }
                else if (nextDirection == CoordinateDirection.UP || nextDirection == CoordinateDirection.DOWN)
                {
                    spr = updownRoadSprite;
                }
            }
        }
        return spr;
    }

    private Sprite GetTileSprite(Coordinate coord)
    {
        if(roadMap.ContainsKey(coord))
        {
            return roadMap[coord];
        }
        return grassSprite;
    }
}

