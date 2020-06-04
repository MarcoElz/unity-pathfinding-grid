
using UnityEngine;

namespace Ignita.Pathfinding
{
    /// <summary>
    /// Common heuristic methods for pathfinding
    /// </summary>
    public static class Heuristic
    {
        #region EuclidianDistance
        /// <summary>
        /// Calculates the squared Euclidian Distance from a to b. 
        /// It saves the square root operation.
        /// </summary>
        public static float SquaredEuclidianDistance(INode a, INode b)
        {
            return SquaredEuclidianDistance(a.Position, b.Position);
        }

        /// <summary>
        /// Calculates the squared Euclidian Distance from a to b. 
        /// It saves the square root operation.
        /// </summary>
        public static float SquaredEuclidianDistance(Vector3 a, Vector3 b)
        {
            return (a - b).sqrMagnitude;
        }

        /// <summary>
        /// Calculates the Euclidian Distance from a to b. 
        /// </summary>
        public static float EuclidianDistance(INode a, INode b)
        {
            return EuclidianDistance(a.Position, b.Position);
        }

        /// <summary>
        /// Calculates the Euclidian Distance from a to b. 
        /// </summary>
        public static float EuclidianDistance(Vector3 a, Vector3 b)
        {
            return (a - b).magnitude;
        }

        #endregion

        #region ManhattanDistance
        /// <summary>
        /// Calculates the Manhattan Distance from a to b. 
        /// </summary>
        public static float ManhattanDistance(INode a, INode b)
        {
            return ManhattanDistance(a.Position, b.Position);
        }

        /// <summary>
        /// Calculates the Manhattan Distance from a to b. 
        /// </summary>
        public static float ManhattanDistance(Vector3 a, Vector3 b)
        {
            return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z);
        }

        /// <summary>
        /// Calculates the Manhattan Distance from a to b. 
        /// </summary>
        public static float ManhattanDistance(Vector2 a, Vector2 b)
        {
            return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
        }

        #endregion
    }
}