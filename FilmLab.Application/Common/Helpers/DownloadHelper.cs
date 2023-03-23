namespace FilmLab.Application.Common.Helpers;

public static class DownloadHelper
{
    public static async Task<byte[]> DownloadFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return null;
        }

        using (var fileStream = new FileStream(filePath, FileMode.Open))
        {
            var fileData = new byte[fileStream.Length];
            await fileStream.ReadAsync(fileData, 0, (int)fileStream.Length);
            return fileData;
        }
    }

}