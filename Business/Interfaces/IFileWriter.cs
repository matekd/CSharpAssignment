
namespace Business.Interfaces
{
    public interface IFileWriter<T>
    {
        bool SaveListToFile(List<T> list);
    }
}