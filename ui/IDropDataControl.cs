
/// <summary>
/// Interface for control nodes that contains draggable data of type T
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IDragDataControl<T>
{
    /// <summary>
    /// Must be called when data from this control is dropped onto another control
    /// </summary>
    /// <param name="draggedData">Data that was dragged and now dropped</param>
    /// <param name="targetData">Dragged data is placed here</param>
    public void OnDataDropped(T draggedData, T targetData);
}