using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveFileDataWriter
{
    public string saveDataDirectoryPath = "";
    public string saveFileName = "";

    // Before we Create a New Save file, We must check to see if one of this Character Slot already Exist (Max 10 Character Slots)
    public bool CheckToSeeIfFileExists()
    {
        if(File.Exists(Path.Combine(saveDataDirectoryPath, saveFileName)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    // Use to Detele Character save files
    public void DeleteSaveFile()
    {
        File.Delete(Path.Combine(saveDataDirectoryPath, saveFileName));
    }

    
    // Use to Create a save file upon Starting a New Game
    public void CreateNewCharacterSaveFile(CharacterSaveData characterData)
    {
        // Make a path to Save the file (A location on the machine)
        string savePath = Path.Combine(saveDataDirectoryPath, saveFileName);

        try
        {
            // Create the Directory the file will be written to, if it does not already Exist
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            Debug.Log("Creating Save File, At Save Path: " + savePath);

            // Serialize the C# Game Data Object Into Json
            string dataToStore = JsonUtility.ToJson(characterData, true);

            // Write the file to our System
            using (FileStream stream = new FileStream(savePath, FileMode.Create))
            {
                using (StreamWriter fileWriter = new StreamWriter(stream))
                {
                    fileWriter.Write(dataToStore);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error whilst trying to save character data, Game not saved" + savePath + "\n" + ex);
        }
    }

    // Use to Load a save file upon Loading a Previous Game
    public CharacterSaveData LoadSaveFile()
    {
        CharacterSaveData characterData = null;

        // Make a path to Load the file (A location on the machine)
        string loadPath = Path.Combine(saveDataDirectoryPath, saveFileName);

        if(File.Exists(loadPath))
        {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(loadPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // Deserialize the data from Json back to Unity C#
                characterData = JsonUtility.FromJson<CharacterSaveData>(dataToLoad);
            }
            catch (Exception ex)
            {

            }
        }
        
        return characterData;
    }
}
