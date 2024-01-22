using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class saveSystem
{
    public static void saveAttributes()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = createAttributesPath();

        FileStream stream = new FileStream(path, FileMode.Create);

        attributesData data = new attributesData();
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static attributesData loadAttributes()
    {
        string path = createAttributesPath();
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            attributesData data = formatter.Deserialize(stream) as attributesData;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
    public static void saveStats()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = createStatsPath();

        FileStream stream = new FileStream(path, FileMode.Create);

        statsData data = new statsData();
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static statsData loadStats()
    {
        string path = createStatsPath();
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            statsData data = formatter.Deserialize(stream) as statsData;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
    public static void savePlayer()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = createPlayerPath();

        FileStream stream = new FileStream(path, FileMode.Create);

        playerData data = new playerData();
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static playerData loadPlayer()
    {
        string path = createPlayerPath();
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            playerData data = formatter.Deserialize(stream) as playerData;
            stream.Close();
            return data;
        }
        else
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            stream.Close();
            return null;
        }
    }

    public static void saveSouls()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = createSoulsPath();

        FileStream stream = new FileStream(path, FileMode.Create);

        soulsData data = new soulsData();
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static soulsData loadSouls()
    {
        string path = createSoulsPath();
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            soulsData data = formatter.Deserialize(stream) as soulsData;
            stream.Close();
            return data;
        }
        else
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            stream.Close();
            return null;
        }
    }


    public static void saveProfiles()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = createProfilesPath();

        FileStream stream = new FileStream(path, FileMode.Create);

        profileData data = new profileData();
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static profileData loadProfiles()
    {
        string path = createProfilesPath();
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            profileData data = formatter.Deserialize(stream) as profileData;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
    public static void savePath(string routePath, string name)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = createRoutePath();

        FileStream stream = new FileStream(path, FileMode.Create);

        lastPath data = new lastPath(routePath, name);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static lastPath loadPath()
    {
        string path = createRoutePath();
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            lastPath data = formatter.Deserialize(stream) as lastPath;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
    public static void saveInventory()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = createInventoryPath();

        FileStream stream = new FileStream(path, FileMode.Create);

        inventoryData data = new inventoryData(config.getInventory().GetComponent<inventoryManager>().getInventory(), config.getInventory().GetComponent<inventoryManager>().getBackUp());
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static inventoryData loadInventory()
    {
        string path = createInventoryPath();
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            inventoryData data = formatter.Deserialize(stream) as inventoryData;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }

    public static void saveEnemyData(List<sceneEnemiesState> enemyData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = createEnemyDataPath();

        FileStream stream = new FileStream(path, FileMode.Create);

        enemyStateData data = new enemyStateData(enemyData);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static enemyStateData loadEnemyData()
    {
        string path = createEnemyDataPath();
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            enemyStateData data = formatter.Deserialize(stream) as enemyStateData;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
    public static void saveLootableObjectsData(List<sceneLootableItem> objectsData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = createLootableObjectsPath();

        FileStream stream = new FileStream(path, FileMode.Create);

        lootableItemData data = new lootableItemData(objectsData);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static lootableItemData loadLootableObjectsData()
    {
        string path = createLootableObjectsPath();
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            lootableItemData data = formatter.Deserialize(stream) as lootableItemData;
            stream.Close();
            return data;
        }
        
        else
        {
            return null;
        }
    }
    public static void saveWeaponsState(int primary, int secundary, List<int> levels)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = createWeaponsPath();

        FileStream stream = new FileStream(path, FileMode.Create);

        equippedWeaponsData data = new equippedWeaponsData(primary, secundary, levels);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static equippedWeaponsData loadWeaponsState()
    {
        string path = createWeaponsPath();
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            equippedWeaponsData data = formatter.Deserialize(stream) as equippedWeaponsData;
            stream.Close();
            return data;
        }

        else
        {
            return null;
        }
    }

    public static void saveSkillsState(List<sceneSkillsState> skills)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = createSkillsPath();

        FileStream stream = new FileStream(path, FileMode.Create);

        unlockedSkillsData data = new unlockedSkillsData(skills);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static unlockedSkillsData loadSkillsState()
    {
        string path = createSkillsPath();
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            unlockedSkillsData data = formatter.Deserialize(stream) as unlockedSkillsData;
            stream.Close();
            return data;
        }

        else
        {
            return null;
        }
    }

    public static void saveEquippedSkillsState(int [] IDs, skillType [] types)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = createEquippedSkillsPath();

        FileStream stream = new FileStream(path, FileMode.Create);

        equippedSkillData data = new equippedSkillData(IDs, types);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static equippedSkillData loadEquippedSkillsState()
    {
        string path = createEquippedSkillsPath();
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            equippedSkillData data = formatter.Deserialize(stream) as equippedSkillData;
            stream.Close();
            return data;
        }

        else
        {
            return null;
        }
    }

    public static void saveObstaclesData(List<sceneObstaclesData> dataToStore)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = createObstaclesPath();

        FileStream stream = new FileStream(path, FileMode.Create);

        obstaclesData data = new obstaclesData(dataToStore);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static obstaclesData loadObstaclesData()
    {
        string path = createObstaclesPath();
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            obstaclesData data = formatter.Deserialize(stream) as obstaclesData;
            stream.Close();
            return data;
        }

        else
        {
            return null;
        }
    }

    public static string createRoutePath()
    {
        return Application.persistentDataPath + "/route.txt";
    }

    public static string createWeaponsPath()
    {
        return profileSystem.getCurrentPath() + "/weapons.txt";
    }

    public static string createInventoryPath()
    {
        return profileSystem.getCurrentPath() + "/inventory.txt";
    }
    public static string createEnemyDataPath()
    {
        return profileSystem.getCurrentPath() + "/enemyState.txt";
    }

    public static string createProfilesPath()
    {
        return Application.persistentDataPath + "/userNames.txt";
    }
    public static string createAttributesPath()
    {
        return profileSystem.getCurrentPath() + "/attributesData.txt";
    }

    public static string createSkillsPath()
    {
        return profileSystem.getCurrentPath() + "/skillsData.txt";
    }

    public static string createStatsPath()
    {
        return profileSystem.getCurrentPath() + "/statsData.txt";
    }
    public static string createPlayerPath()
    {
        return profileSystem.getCurrentPath() + "/playerData.txt";
    }
    public static string createLootableObjectsPath()
    {
        return profileSystem.getCurrentPath() + "/lootable.txt";
    }
    public static string createSoulsPath()
    {
        return profileSystem.getCurrentPath() + "/souls.txt";
    }
    public static string createEquippedSkillsPath()
    {
        return profileSystem.getCurrentPath() + "/equippedSkills.txt";
    }
    public static string createObstaclesPath()
    {
        return profileSystem.getCurrentPath() + "/obstacles.txt";
    }
}
