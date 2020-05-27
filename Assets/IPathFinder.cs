
public interface IPathFinder
{
    INode[] GetPath();
    INode[] CalculatePath(INode start, INode end);
    
}
