namespace Business.Interfaces;

public interface IFileService<T> : IFileWriter<T>, IFileReader<T>
{
    
}
