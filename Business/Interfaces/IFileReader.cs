
namespace Business.Interfaces
{
    public interface IFileReader<T>
    {
        List<T> LoadListFromFile();
    }
}