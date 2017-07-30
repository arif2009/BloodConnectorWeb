namespace BloodConnector.WebAPI.Interface
{
    public interface IObjectWithState
    {
        ObjectState ObjectState { get; set; }
    }

    public enum ObjectState
    {
        Added,
        Modified,
        Deleted,
        Unchanged
    }
}
