﻿namespace ScottPlot;

/// <summary>
/// Represents a straight line in coordinate space
/// </summary>
public readonly struct CoordinateLine
{
    public readonly double X1;
    public readonly double X2;
    public readonly double Y1;
    public readonly double Y2;
    public double XSpan => X2 - X1;
    public double YSpan => Y2 - Y1;
    public double Slope => (X1 == X2) ? double.NaN : YSpan / XSpan;
    public double YIntercept => Y1 - Slope * X1;

    public Coordinates Start => new(X1, Y1);
    public Coordinates End => new(X2, Y2);

    public CoordinateLine(double x1, double y1, double x2, double y2)
    {
        X1 = x1;
        Y1 = y1;
        X2 = x2;
        Y2 = y2;
    }

    public CoordinateLine(Coordinates pt1, Coordinates pt2)
    {
        X1 = pt1.X;
        Y1 = pt1.Y;
        X2 = pt2.X;
        Y2 = pt2.Y;
    }

    public override string ToString()
    {
        return $"CoordinateLine from ({X1}, {Y1}) to ({X2}, {Y2})";
    }

    /// <summary>
    /// Adjust the line to fit within the boundaries of the given rectangle.
    /// The slope and Y intercept will not be changed.
    /// </summary>
    public CoordinateLine ExtendTo(CoordinateRect rect)
    {
        double dBottomX = Y1 - rect.Bottom;
        double xAtBottom = X1 - dBottomX * Slope;
        Coordinates bottom = new(xAtBottom, rect.Bottom);

        double dTopX = rect.Top - Y1;
        double xAtTop = X1 + dTopX * Slope;
        Coordinates top = new(xAtTop, rect.Top);

        return new CoordinateLine(bottom, top);
    }

    /// <summary>
    /// Return the X position on the line at the given Y
    /// </summary>
    public Coordinates X(double y = 0)
    {
        double dX = Y1 - y;
        double x = X1 - dX * Slope;
        return new Coordinates(x, y);
    }

    /// <summary>
    /// Return the Y position on the line at the given X
    /// </summary>
    public Coordinates Y(double x = 0)
    {
        double y = Slope * x + YIntercept;
        return new Coordinates(x, y);
    }
}
