using Business.Interfaces;
using System.Diagnostics;
using System.Text.Json;

namespace Business.Services;

public class FileService<T> : IFileService<T>
{
    private readonly string _filePath;
    private readonly string _directoryPath;

    public FileService(string directoryPath = @"d:\vsProject\data", string fileName = "contacts.json")
    {
        _directoryPath = directoryPath;
        _filePath = Path.Combine(_directoryPath, fileName);
    }

    public List<T> LoadListFromFile()
    {
        try
        {
            if (!File.Exists(_filePath))
                return [];

            string json = File.ReadAllText(_filePath);
            List<T> list = JsonSerializer.Deserialize<List<T>>(json)!;

            return list ?? [];
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return [];
        }
    }

    public bool SaveListToFile(List<T> list)
    {
        try
        {
            if (!Directory.Exists(_directoryPath))
                Directory.CreateDirectory(_directoryPath);

            var json = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);

            return true;
        } 
        catch { 
            return false;
        }
    }
}
