using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class CardSaveLoad
{
    const string CARD_FILE_PREFIX = "SavedCard";

    #region JSON Save (UNUSED)

    //This approach works almost fine but serializing Sprite is problematic.
    //Instance ID is not consistent and I don't think you can easily get a path of
    //an asset file using Resources folder or AssetBundles.
    //Maybe serialize the texture or make a custom ID solution, or ScriptableObject wrapper for Sprite?
    //For now let's use index based saving.

    //Save CardData to JSON file a with unique name.
    public static void SaveCardDataToFileWithJSON(CardData cardData)
    {
        string path = $"{Application.persistentDataPath}/{CARD_FILE_PREFIX}_{System.DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss_ffff")}.json";
        File.WriteAllText(path, cardData.ToJSON(), System.Text.Encoding.Unicode);
    }
    public static CardData LoadCardDataFromPathWithJSON(string path)
    {
        return new CardData(File.ReadAllText(path, System.Text.Encoding.Unicode));
    }

    //Returns directories where they match the pattern and the files exist.
    public static string[] GetLoadableCardJSONFilePaths()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath, $"{CARD_FILE_PREFIX}_*.json");

        List<string> loadableFilesPaths = new List<string>();

        for (int i = 0; i < files.Length; i++)
        {
            if (!File.Exists(files[i])) continue;
            loadableFilesPaths.Add(files[i]);
        }

        return loadableFilesPaths.ToArray();
    }


    #endregion

    #region Index Save

    //Save CardData as CardGenerationData indexes
    //Limitation: providing a modified CardGenerationDataAsset when loading will result in incorrect loading.
    //To correctly load CardGenerationDataAsset has to be identical to when the file was saved.
    //TODO: Fix this or use a different approach.
    public static void SaveCardDataToFileWithIndexes(CardData cardData, CardGenerationDataAsset cardGenerationData)
    {
        string path = $"{Application.persistentDataPath}/{CARD_FILE_PREFIX}_{System.DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss_ffff")}.card";

        FileStream stream = new FileStream(path, FileMode.Create);

        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, cardData.GetMatchingIndexes(cardGenerationData));

        stream.Close();
    }

    public static CardData LoadCardDataFromPathWithIndexes(string path, CardGenerationDataAsset cardGenerationData)
    {
        if (!File.Exists(path)) return null;
        FileStream stream = new FileStream(path, FileMode.Open);

        BinaryFormatter formatter = new BinaryFormatter();
        CardData.CardIndexes indexes = formatter.Deserialize(stream) as CardData.CardIndexes;

        stream.Close();

        return new CardData(indexes, cardGenerationData);
    }

    public static string[] GetLoadableCardIndexesFilePaths()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath, $"{CARD_FILE_PREFIX}_*.card");

        List<string> loadableFilesPaths = new List<string>();

        for (int i = 0; i < files.Length; i++)
        {
            if (!File.Exists(files[i])) continue;
            loadableFilesPaths.Add(files[i]);
        }

        return loadableFilesPaths.ToArray();
    }

    public static void DeleteAllCardIndexFiles()
    {
        string[] paths = GetLoadableCardIndexesFilePaths();

        for (int i = 0; i < paths.Length; i++) File.Delete(paths[i]);
    }

    #endregion
}