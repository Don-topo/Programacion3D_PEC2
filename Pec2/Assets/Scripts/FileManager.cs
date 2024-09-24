using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class FileManager
{
    private static readonly string gameInfopath = Application.persistentDataPath + "/GameInfo.txt";
    private static readonly string checkPointInfoPath = Application.persistentDataPath + "/CheckpointInfo.txt";

    public static GameInfo LoadGameConfig()
    {
        if (File.Exists(gameInfopath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(gameInfopath, FileMode.Open);
            GameInfo gameInfo = formatter.Deserialize(stream) as GameInfo;
            if (gameInfo == null) gameInfo = new GameInfo(100f, 100f, "Level1");
            stream.Close();
            return gameInfo;
        }
        else
        {
            return new GameInfo(100f, 100f, "Level1");
        }
    }

    public static void SaveGameConfig(GameInfo gameInfo)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(gameInfopath, FileMode.Create);
        formatter.Serialize(stream, gameInfo);
        stream.Close();
    }

    public static bool CheckIfExistSavedData()
    {
        return File.Exists(gameInfopath);
    }

    public static CheckPointInfo LoadCheckPoint()
    {
        if (CheckIfCheckpointExists()) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(checkPointInfoPath, FileMode.Open);
            CheckPointInfo checkPointInfo = formatter.Deserialize(stream) as CheckPointInfo;
            stream.Close();
            return checkPointInfo;
        }
        else
        {
            return null;
        }
    }

    public static void DeleteCheckPoint()
    {
        if (CheckIfCheckpointExists())
        {
            File.Delete(checkPointInfoPath);
        }
    }

    public static void SaveCheckpoint(CheckPointInfo checkPointInfo)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(checkPointInfoPath, FileMode.Create);
        formatter.Serialize(stream, checkPointInfo);
        stream.Close();
    }    

    public static bool CheckIfCheckpointExists()
    {
        return File.Exists(checkPointInfoPath);
    }
}
