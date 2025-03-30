using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string _dataDirPath = "";
    private string _dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        _dataDirPath = dataDirPath;
        _dataFileName = dataFileName;
    }

    public SaveData Load()
    {
        string fullPath = Path.Combine(_dataDirPath, _dataFileName);
        SaveData loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<SaveData>(dataToLoad);

            }
            catch (Exception e)
            {
                Debug.LogError($"Error occured when trying to load data from file: {fullPath}\n{e}");
            }
        }

        return loadedData;
    }
    public void Save(SaveData gameData)
    {
        string fullPath = Path.Combine(_dataDirPath, _dataFileName);
        try
        {
            Directory.CreateDirectory(_dataDirPath);

            string dataToStore = JsonUtility.ToJson(gameData, true);
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
            PlayerPrefs.SetInt("DoesSaveExist", 1);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error occured when trying to save data to file: {fullPath}\n{e}");
        }
    }
}
