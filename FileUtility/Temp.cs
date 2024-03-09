namespace FileUtility;

public class Temp
{
    public DirectoryInfo GetTemporaryDirectoryInfo() =>
        new(Path.GetTempPath());

    public FileInfo GetTemporaryFileInfo() =>
        new(Path.GetTempFileName());

    public FileInfo GetTemporaryFileInfo(string nameOnly) =>
        new(Path.Combine(Path.GetTempPath(), nameOnly));
}