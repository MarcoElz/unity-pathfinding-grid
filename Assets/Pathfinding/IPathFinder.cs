
namespace Ignita.Pathfinding
{
    /// <summary>
    /// Pathfinding Algorithm
    /// </summary>
    public interface IPathFinder
    {
        INode[] GetPath(); //Get last calculated path
        INode[] CalculatePath(INode start, INode end); //The main pathfinding algorithm
    }
}