using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
/// <summary>
/// saveSystem es una clase que se usa para serializar datos y así poder guardar y cargar partida sin que haya incoherencias.
/// </summary>
public static class saveSystem
{
    public static void saveAttributes()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = createPath("attributesData");

        FileStream stream = new FileStream(path, FileMode.Create);

        attributesData data = new attributesData();
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static attributesData loadAttributes()
    {
        string path = createPath("attributesData");
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
        string path = createPath("statsData");

        FileStream stream = new FileStream(path, FileMode.Create);

        statsData data = new statsData();
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static statsData loadStats()
    {
        string path = createPath("statsData");
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
        string path = createPath("playerData");

        FileStream stream = new FileStream(path, FileMode.Create);

        playerData data = new playerData();
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static playerData loadPlayer()
    {
        string path = createPath("playerData");
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
        string path = createPath("soulsData");

        FileStream stream = new FileStream(path, FileMode.Create);

        soulsData data = new soulsData();
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static soulsData loadSouls()
    {
        string path = createPath("soulsData");
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
        string path = createPath("inventoryData");

        FileStream stream = new FileStream(path, FileMode.Create);

        inventoryData data = new inventoryData(config.getInventory().GetComponent<inventoryManager>().getInventory(), config.getInventory().GetComponent<inventoryManager>().getBackUp(), config.getInventory().GetComponent<inventoryManager>().getMaximumRefillable());
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static inventoryData loadInventory()
    {
        string path = createPath("inventoryData");
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
        string path = createPath("enemiesData");

        FileStream stream = new FileStream(path, FileMode.Create);

        enemyStateData data = new enemyStateData(enemyData);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static enemyStateData loadEnemyData()
    {
        string path = createPath("enemiesData");
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
        string path = createPath("lootableData");

        FileStream stream = new FileStream(path, FileMode.Create);

        lootableItemData data = new lootableItemData(objectsData);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static lootableItemData loadLootableObjectsData()
    {
        string path = createPath("lootableData");
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
        string path = createPath("weaponsData");

        FileStream stream = new FileStream(path, FileMode.Create);

        equippedWeaponsData data = new equippedWeaponsData(primary, secundary, levels);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static equippedWeaponsData loadWeaponsState()
    {
        string path = createPath("weaponsData");
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
        string path = createPath("unlockedSkillsData");

        FileStream stream = new FileStream(path, FileMode.Create);

        unlockedSkillsData data = new unlockedSkillsData(skills);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static unlockedSkillsData loadSkillsState()
    {
        string path = createPath("unlockedSkillsData");
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

    public static void saveEquippedSkillsState(int [] IDs, skillTypeEnum [] types)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = createPath("equippedSkillsData");

        FileStream stream = new FileStream(path, FileMode.Create);

        equippedSkillData data = new equippedSkillData(IDs, types);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static equippedSkillData loadEquippedSkillsState()
    {
        string path = createPath("equippedSkillsData");
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
        string path = createPath("obstaclesData");

        FileStream stream = new FileStream(path, FileMode.Create);

        obstaclesData data = new obstaclesData(dataToStore);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static obstaclesData loadObstaclesData()
    {
        string path = createPath("obstaclesData");
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
    public static void saveEquippedObjectsData(List<newEquippedObjectData> dataToStore, int id)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = createPath("objectsData");

        FileStream stream = new FileStream(path, FileMode.Create);

        equippedObjectData data = new equippedObjectData(dataToStore, id);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static equippedObjectData loadEquippedObjectsData()
    {
        string path = createPath("objectsData");
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            equippedObjectData data = formatter.Deserialize(stream) as equippedObjectData;
            stream.Close();
            return data;
        }

        else
        {
            return null;
        }
    }

    public static void saveShopData(List<sceneShopData> dataToStore)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = createPath("shopData");

        FileStream stream = new FileStream(path, FileMode.Create);

        shopData data = new shopData(dataToStore);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static shopData loadShopData()
    {
        string path = createPath("shopData");
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            shopData data = formatter.Deserialize(stream) as shopData;
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

    public static string createProfilesPath()
    {
        return Application.persistentDataPath + "/userNames.txt";
    }

    public static string createPath(string text)
    {
        return profileSystem.getCurrentPath() + "/" + text + ".txt";
    }
    /*
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
    }*/
}
