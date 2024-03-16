using System;
using UnityEngine;
using System.IO;

public class SaveGameDataWriter
{
    public string saveDataDirectoryPath = "";
    public string dataSaveFileName = "";

    public CharacterSaveData LoadCharacterDataFormJson()
    {
        string savePath = Path.Combine(saveDataDirectoryPath, dataSaveFileName);

        CharacterSaveData loadedSaveData = null;

        if (File.Exists(savePath))
        {
            try
            {
                string saveDataToLoad = "";

                using (FileStream stream = new FileStream(savePath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        saveDataToLoad = reader.ReadToEnd();
                    }
                }

                loadedSaveData = JsonUtility.FromJson<CharacterSaveData>(saveDataToLoad);
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex.Message);
            }
        }
        else
        {
            Debug.Log("Save File Does Not Exist");
        }

        return loadedSaveData;
    }

    public void WriteCharacterDataToSaveFile(CharacterSaveData characterData)
    {
        string savePath = Path.Combine(saveDataDirectoryPath, dataSaveFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            //Debug.Log("SAVE PATH = " + savePath);
            
            // Serialize the C# game data object to json
            string dataToStore = JsonUtility.ToJson(characterData, true);

            using (FileStream stream = new FileStream(savePath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error While Trying To Save Data, Game Cound Not Be Saved" + ex);
        }
    }

    public void DeleteSaveFile()
    {
        File.Delete(Path.Combine(saveDataDirectoryPath, dataSaveFileName));
    }

    public bool CheckIfSaveFileExists()
    {
        if (File.Exists(Path.Combine(saveDataDirectoryPath, dataSaveFileName)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
