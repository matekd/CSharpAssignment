namespace Business.Interfaces;

public interface IFileService<T>
{
    public bool SaveListToFile(List<T> list);
    public List<T> LoadListFromFile();
}
