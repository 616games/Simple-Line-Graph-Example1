using System;
using UnityEngine;

public class GraphFunctionLibrary : MonoBehaviour
{
    #region --Custom Methods--
    
    /// <summary>
    /// Returns the appropriate method based on the provided GraphType.
    /// </summary>
    public static GraphFunction GetGraphFunction(GraphType _graphType)
    {
        switch (_graphType)
        {
            case GraphType.Line:
                return Line;

            case GraphType.Squared:
                return Squared;

            case GraphType.Cubed:
                return Cubed;

            case GraphType.SquareRoot:
                return SquareRoot;

            case GraphType.Sine:
                return Sine;

            case GraphType.Cosine:
                return Cosine;
        }

        return null;
    }
    
    /// <summary>
    /// Simulates the graph of the sine function.
    /// </summary>
    private static Vector3 Sine(float _xInput, float _coefficient = 1f, float _yIntercept = 0f)
    {
        return new Vector3(_xInput, _coefficient * Mathf.Sin(_xInput) + _yIntercept, 0);
    }
    
    /// <summary>
    /// Simulates the graph of the cosine function.
    /// </summary>
    private static Vector3 Cosine(float _xInput, float _coefficient = 1f, float _yIntercept = 0f)
    {
        return new Vector3(_xInput, _coefficient * Mathf.Cos(_xInput) + _yIntercept, 0);
    }
    
    /// <summary>
    /// Simulates the graph of a line.
    /// </summary>
    private static Vector3 Line(float _xInput, float _coefficient = 1f, float _yIntercept = 0f)
    {
        float _yInput = _coefficient * _xInput + _yIntercept;
        return new Vector3(_xInput, _yInput, 0);
    }

    /// <summary>
    /// Simulates the graph of a parabola.
    /// </summary>
    private static Vector3 Squared(float _xInput, float _coefficient = 1f, float _yIntercept = 0f)
    {
        float _yInput = _coefficient * (_xInput * _xInput) + _yIntercept;
        return new Vector3(_xInput, _yInput, 0);
    }

    /// <summary>
    /// Simulates the graph of a cubic function.
    /// </summary>
    private static Vector3 Cubed(float _xInput, float _coefficient = 1f, float _yIntercept = 0f)
    {
        float _yInput = _coefficient * (_xInput * _xInput * _xInput) + _yIntercept;
        return new Vector3(_xInput, _yInput, 0);
    }

    /// <summary>
    /// Simulates the graph of a square root function.
    /// </summary>
    private static Vector3 SquareRoot(float _xInput, float _coefficient = 1f, float _yIntercept = 0f)
    {
        float _yInput = _coefficient *  Mathf.Sqrt(_xInput) + _yIntercept;
        return new Vector3(_xInput, _yInput, 0);
    }
    
    #endregion
    
}
