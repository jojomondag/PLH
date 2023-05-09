using System.IO;
using System.Text.Json;

public class ProjectInfo
{
    public string Name { get; set; }
    public string Path { get; set; }
}

public static class SynExFolder
{
    private static readonly string _folderName = "SynEx";
    private static readonly string _jsonFileName = "projectInfo.json";
    private static readonly string _folderPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        _folderName);
    private static readonly string _jsonFilePath = Path.Combine(_folderPath, _jsonFileName);

    public static void CreateSynExFolder()
    {
        if (!Directory.Exists(_folderPath))
        {
            Directory.CreateDirectory(_folderPath);
        }
    }

    public static void SaveProjectInfo(ProjectInfo projectInfo)
    {
        CreateSynExFolder();

        // Serialize the ProjectInfo object to JSON
        string jsonString = JsonSerializer.Serialize(projectInfo);

        // Save the JSON to a file
        File.WriteAllText(_jsonFilePath, jsonString);
    }

    public static ProjectInfo GetProjectInfo()
    {
        CreateSynExFolder();

        if (File.Exists(_jsonFilePath))
        {
            // Load the JSON from the file
            string jsonString = File.ReadAllText(_jsonFilePath);

            // Deserialize the JSON to a ProjectInfo object
            return JsonSerializer.Deserialize<ProjectInfo>(jsonString);
        }

        return null;
    }
}
