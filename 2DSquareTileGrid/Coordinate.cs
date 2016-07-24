using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
[Serializable]
public struct Coordinate
{
    [SerializeField]
    private int _x;
    [SerializeField]
    private int _y;

    public int X { get { return _x; } }
    public int Y { get { return _y; } }

    public Coordinate(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public bool Equals(Coordinate compare)
    {
        return compare.X == this.X && compare.Y == this.Y;
    }
    public override bool Equals(object obj)
    {
        try
        {
            var compare = (Coordinate)obj;
            return Equals(compare);
        }
        catch(Exception)
        {
            return false;
        }
    }

    public static Coordinate Left(Coordinate input)
    {
        return new Coordinate(input.X - 1, input.Y);
    }
    public static Coordinate Right(Coordinate input)
    {
        return new Coordinate(input.X + 1, input.Y);
    }
    public static Coordinate Up(Coordinate input)
    {
        return new Coordinate(input.X, input.Y + 1);
    }
    public static Coordinate Down(Coordinate input)
    {
        return new Coordinate(input.X, input.Y - 1);
    }

    public override string ToString()
    {
        return "(" + X.ToString() + ", " + Y.ToString() + ")";
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    public static bool operator ==(Coordinate compare, Coordinate compare2)
    {
        return compare.Equals(compare2);
    }
    public static bool operator !=(Coordinate compare, Coordinate compare2)
    {
        return !compare.Equals(compare2);
    }
}

public enum CoordinateDirection
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    NONE
}
