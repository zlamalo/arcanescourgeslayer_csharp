
/// <summary>
/// Interface for control nodes that contains dropable data of type T
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IDropDataControl<T>
{
    /// <summary>
    /// Removes the picked data from the this control
    /// </summary>
    /// <param name="data"></param>
    public void RemovePickedData(T data);
}