namespace SandBox.Core.ToDos.Get
{
    public interface IGetToDosHandler
    {
        Task<List<GetToDosResult>> Handle();
    }
}