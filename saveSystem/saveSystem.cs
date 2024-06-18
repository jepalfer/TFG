using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
/// <summary>
/// saveSystem es una clase que se usa para serializar datos y as� poder guardar y cargar partida sin que haya incoherencias.
/// </summary>
public static class saveSystem
{
    /// <summary>
    /// M�todo para guardar la informaci�n de los atributos del jugador.
    /// </summary>
    public static void saveAttributes()
    {
        //Creamos el formateador binario
        BinaryFormatter formatter = new BinaryFormatter();

        //Obtenemos la ruta
        string path = createPath("attributesData");

        //Abrimos el archivo
        FileStream stream = new FileStream(path, FileMode.Create);
        
        //Serializamos el flujo de datos
        attributesData data = new attributesData();
        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// M�todo para cargar la informaci�n de los atributos del jugador.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="attributesData"/> que contiene los datos de los atributos.</returns>
    public static attributesData loadAttributes()
    {
        //Obtenemos la ruta
        string path = createPath("attributesData");
        if (File.Exists(path))
        {
            ///Creamos el formateador binario
            BinaryFormatter formatter = new BinaryFormatter();

            //Abrimos el archivo
            FileStream stream = new FileStream(path, FileMode.Open);

            //Deserializamos el flujo de datos
            attributesData data = formatter.Deserialize(stream) as attributesData;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
    /// <summary>
    /// M�todo para guardar la informaci�n de las estad�sticas del jugador.
    /// </summary>
    public static void saveStats()
    {
        //Creamos el formateador binario
        BinaryFormatter formatter = new BinaryFormatter();
        //Obtenemos la ruta
        string path = createPath("statsData");

        //Abrimos el archivo
        FileStream stream = new FileStream(path, FileMode.Create);

        //Serializamos el flujo de datos
        statsData data = new statsData();
        formatter.Serialize(stream, data);
        stream.Close();
    }
    /// <summary>
    /// M�todo para cargar la informaci�n de las estad�sticas del jugador.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="statsData"/> que contiene los datos de las estad�sticas.</returns>
    public static statsData loadStats()
    {
        //Obtenemos la ruta
        
        string path = createPath("statsData");
        if (File.Exists(path))
        {        
            //Creamos el formateador binario
            BinaryFormatter formatter = new BinaryFormatter();

            //Abrimos el archivo
            FileStream stream = new FileStream(path, FileMode.Open);

            //Deserializamos el flujo de datos
            statsData data = formatter.Deserialize(stream) as statsData;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// M�todo para guardar la informaci�n referente a la posici�n del jugador.
    /// </summary>
    public static void savePlayer()
    {
        //Creamos el formateador binario
        BinaryFormatter formatter = new BinaryFormatter();

        //Obtenemos la ruta
        string path = createPath("playerData");

        //Abrimos el archivo
        FileStream stream = new FileStream(path, FileMode.Create);

        //Serializamos el flujo de datos
        playerData data = new playerData();
        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// M�todo para cargar la informaci�n referente a la posici�n del jugador.
    /// </summary>
    /// <returns>Un objeto tipo <see cref="playerData"/> que contiene los datos de posici�n del jugador.</returns>
    public static playerData loadPlayer()
    {
        //Obtenemos la ruta
        string path = createPath("playerData");
        if (File.Exists(path))
        {
            //Creamos el formateador binario
            BinaryFormatter formatter = new BinaryFormatter();

            //Abrimos el archivo
            FileStream stream = new FileStream(path, FileMode.Open);

            //Deserializamos el flujo de datos
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

    /// <summary>
    /// M�todo para guardar la cantidad de almas del jugador.
    /// </summary>
    public static void saveSouls()
    {
        //Creamos el formateador binario
        BinaryFormatter formatter = new BinaryFormatter();

        //Obtenemos la ruta
        string path = createPath("soulsData");

        //Abrimos el archivo
        FileStream stream = new FileStream(path, FileMode.Create);

        //Serializamos el flujo de datos
        soulsData data = new soulsData();
        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// M�todo para cargar la cantidad de almas del jugador.
    /// </summary>
    /// <returns>Un objeto tipo <see cref="soulsData"/> que contiene el n�mero de almas del jugador.</returns>
    public static soulsData loadSouls()
    {
        //Obtenemos la ruta
        string path = createPath("soulsData");
        if (File.Exists(path))
        {
            //Creamos el formateador binario
            BinaryFormatter formatter = new BinaryFormatter();

            //Abrimos el archivo
            FileStream stream = new FileStream(path, FileMode.Open);

            //Deserializamos el flujo de datos
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

    /// <summary>
    /// M�todo para guardar los perfiles.
    /// </summary>
    public static void saveProfiles()
    {
        //Creamos el formateador binario
        BinaryFormatter formatter = new BinaryFormatter();

        //Obtenemos la ruta
        string path = createProfilesPath();

        //Abrimos el archivo
        FileStream stream = new FileStream(path, FileMode.Create);

        //Serializamos el flujo de datos
        profileData data = new profileData();
        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// M�todo para cargar los perfiles.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="profileData"/> que contiene la informaci�n de los perfiles.</returns>
    public static profileData loadProfiles()
    {
        //Obtenemos la ruta
        string path = createProfilesPath();
        if (File.Exists(path))
        {
            //Creamos el formateador binario
            BinaryFormatter formatter = new BinaryFormatter();

            //Abrimos el archivo
            FileStream stream = new FileStream(path, FileMode.Open);

            //Deserializamos el flujo de datos
            profileData data = formatter.Deserialize(stream) as profileData;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
    /// <summary>
    /// M�todo para guardar el �ltimo perfil cargado.
    /// </summary>
    /// <param name="profilePath">Ruta del perfil.</param>
    /// <param name="name">Nombre del perfil.</param>
    public static void savePath(string profilePath, string name)
    {
        //Creamos el formateador binario
        BinaryFormatter formatter = new BinaryFormatter();

        //Obtenemos la ruta
        string path = createRoutePath();

        //Abrimos el archivo
        FileStream stream = new FileStream(path, FileMode.Create);

        //Serializamos el flujo de datos
        lastPath data = new lastPath(profilePath, name);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// M�todo para cargar �l �ltimo perfil cargado.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="lastPath"/> que contiene la informaci�n del �ltimo perfil.</returns>
    public static lastPath loadPath()
    {
        //Obtenemos la ruta
        string path = createRoutePath();
        if (File.Exists(path))
        {
            //Creamos el formateador binario
            BinaryFormatter formatter = new BinaryFormatter();

            //Abrimos el archivo
            FileStream stream = new FileStream(path, FileMode.Open);

            //Deserializamos el flujo de datos
            lastPath data = formatter.Deserialize(stream) as lastPath;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// M�todo que se encarga de guardar los datos del inventario.
    /// </summary>
    public static void saveInventory()
    {
        //Creamos el formateador binario
        BinaryFormatter formatter = new BinaryFormatter();

        //Obtenemos la ruta
        string path = createPath("inventoryData");

        //Abrimos el archivo
        FileStream stream = new FileStream(path, FileMode.Create);

        //Serializamos el flujo de datos
        inventoryData data = new inventoryData(config.getInventory().GetComponent<inventoryManager>().getInventory(), config.getInventory().GetComponent<inventoryManager>().getBackUp(), config.getInventory().GetComponent<inventoryManager>().getMaximumRefillable());
        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// M�todo que se encarga de cargar los datos del inventario.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="inventoryData"/> que contiene los datos guardados.</returns>
    public static inventoryData loadInventory()
    {
        //Obtenemos la ruta
        string path = createPath("inventoryData");
        if (File.Exists(path))
        {        
            //Creamos el formateador binario
            BinaryFormatter formatter = new BinaryFormatter();

            //Abrimos el archivo
            FileStream stream = new FileStream(path, FileMode.Open);

            //Deserializamos el flujo de datos
            inventoryData data = formatter.Deserialize(stream) as inventoryData;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// M�todo para guardar la informaci�n de los enemigos.
    /// </summary>
    public static void saveEnemyData(List<sceneEnemiesState> enemyData)
    {
        //Creamos el formateador binario
        BinaryFormatter formatter = new BinaryFormatter();

        //Obtenemos la ruta
        string path = createPath("enemiesData");

        //Abrimos el archivo
        FileStream stream = new FileStream(path, FileMode.Create);

        //Serializamos el flujo de datos
        enemyStateData data = new enemyStateData(enemyData);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// M�todo para cargar la informaci�n de los enemigos.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="enemyStateData"/> que contiene los datos de los enemigos.</returns>
    public static enemyStateData loadEnemyData()
    {
        //Obtenemos la ruta
        string path = createPath("enemiesData");
        if (File.Exists(path))
        {
            //Creamos el formateador binario
            BinaryFormatter formatter = new BinaryFormatter();

            //Abrimos el archivo
            FileStream stream = new FileStream(path, FileMode.Open);

            //Deserializamos el flujo de datos
            enemyStateData data = formatter.Deserialize(stream) as enemyStateData;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// M�todo para guardar la informaci�n de los objetos looteables.
    /// </summary>
    public static void saveLootableObjectsData(List<sceneLootableItem> objectsData)
    {
        //Creamos el formateador binario
        BinaryFormatter formatter = new BinaryFormatter();

        //Obtenemos la ruta
        string path = createPath("lootableData");

        //Abrimos el archivo
        FileStream stream = new FileStream(path, FileMode.Create);

        //Serializamos el flujo de datos
        lootableItemData data = new lootableItemData(objectsData);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// M�todo para cargar la informaci�n de los objetos looteables.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="lootableItemData"/> que contiene los objetos looteables.</returns>
    public static lootableItemData loadLootableObjectsData()
    {
        //Obtenemos la ruta
        string path = createPath("lootableData");
        if (File.Exists(path))
        {
            //Creamos el formateador binario
            BinaryFormatter formatter = new BinaryFormatter();

            //Abrimos el archivo
            FileStream stream = new FileStream(path, FileMode.Open);

            //Deserializamos el flujo de datos
            lootableItemData data = formatter.Deserialize(stream) as lootableItemData;
            stream.Close();
            return data;
        }
        
        else
        {
            return null;
        }
    }

    /// <summary>
    /// M�todo para guardar la informaci�n de las armas.
    /// </summary>
    public static void saveWeaponsState(int primary, int secundary, List<int> levels)
    {
        //Creamos el formateador binario
        BinaryFormatter formatter = new BinaryFormatter();

        //Obtenemos la ruta
        string path = createPath("weaponsData");

        //Abrimos el archivo
        FileStream stream = new FileStream(path, FileMode.Create);

        //Serializamos el flujo de datos
        equippedWeaponsData data = new equippedWeaponsData(primary, secundary, levels);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// M�todo para cargar la informaci�n de las armas.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="equippedWeaponsData"/> que contiene los datos de las armas.</returns>
    public static equippedWeaponsData loadWeaponsState()
    {
        //Obtenemos la ruta
        string path = createPath("weaponsData");
        if (File.Exists(path))
        {
            //Creamos el formateador binario
            BinaryFormatter formatter = new BinaryFormatter();

            //Abrimos el archivo
            FileStream stream = new FileStream(path, FileMode.Open);

            //Deserializamos el flujo de datos
            equippedWeaponsData data = formatter.Deserialize(stream) as equippedWeaponsData;
            stream.Close();
            return data;
        }

        else
        {
            return null;
        }
    }

    /// <summary>
    /// M�todo para guardar la informaci�n de las habilidades desbloqueadas.
    /// </summary>
    public static void saveSkillsState(List<sceneSkillsState> skills)
    {
        //Creamos el formateador binario
        BinaryFormatter formatter = new BinaryFormatter();

        //Obtenemos la ruta
        string path = createPath("unlockedSkillsData");

        //Abrimos el archivo
        FileStream stream = new FileStream(path, FileMode.Create);

        //Serializamos el flujo de datos
        unlockedSkillsData data = new unlockedSkillsData(skills);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// M�todo para cargar la informaci�n de las habilidades desbloqueadas.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="unlockedSkillsData"/> que contiene los datos de las habilidades desbloqueadas.</returns>
    public static unlockedSkillsData loadSkillsState()
    {
        //Obtenemos la ruta
        string path = createPath("unlockedSkillsData");
        if (File.Exists(path))
        {
            //Creamos el formateador binario
            BinaryFormatter formatter = new BinaryFormatter();

            //Abrimos el archivo
            FileStream stream = new FileStream(path, FileMode.Open);

            //Deserializamos el flujo de datos
            unlockedSkillsData data = formatter.Deserialize(stream) as unlockedSkillsData;
            stream.Close();
            return data;
        }

        else
        {
            return null;
        }
    }

    /// <summary>
    /// M�todo para guardar la informaci�n de las habilidades equipadas.
    /// </summary>
    /// <param name="IDs">Array con los IDs de las habilidades equipadas.</param>
    /// <param name="types">Array con los tipos de las habilidades equipadas.</param>
    public static void saveEquippedSkillsState(int [] IDs, skillTypeEnum [] types)
    {
        //Creamos el formateador binario
        BinaryFormatter formatter = new BinaryFormatter();

        //Obtenemos la ruta
        string path = createPath("equippedSkillsData");

        //Abrimos el archivo
        FileStream stream = new FileStream(path, FileMode.Create);

        //Serializamos el flujo de datos
        equippedSkillsData data = new equippedSkillsData(IDs, types);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// M�todo para cargar la informaci�n de las habilidades equipadas.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="equippedSkillsData"/> que contiene los datos de las habilidades equipadas.</returns>
    public static equippedSkillsData loadEquippedSkillsState()
    {
        //Obtenemos la ruta
        string path = createPath("equippedSkillsData");
        if (File.Exists(path))
        {
            //Creamos el formateador binario
            BinaryFormatter formatter = new BinaryFormatter();

            //Abrimos el archivo
            FileStream stream = new FileStream(path, FileMode.Open);

            //Deserializamos el flujo de datos
            equippedSkillsData data = formatter.Deserialize(stream) as equippedSkillsData;
            stream.Close();
            return data;
        }

        else
        {
            return null;
        }
    }

    /// <summary>
    /// M�todo para guardar la informaci�n de los obst�culos.
    /// </summary>
    public static void saveObstaclesData(List<sceneObstaclesData> dataToStore)
    {
        //Creamos el formateador binario
        BinaryFormatter formatter = new BinaryFormatter();

        //Obtenemos la ruta
        string path = createPath("obstaclesData");

        //Abrimos el archivo
        FileStream stream = new FileStream(path, FileMode.Create);

        //Serializamos el flujo de datos
        obstaclesData data = new obstaclesData(dataToStore);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// M�todo para cargar la informaci�n de los obst�culos.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="obstaclesData"/> que contiene los datos de los obst�culos.</returns>
    public static obstaclesData loadObstaclesData()
    {
        //Obtenemos la ruta
        string path = createPath("obstaclesData");
        if (File.Exists(path))
        {
            //Creamos el formateador binario
            BinaryFormatter formatter = new BinaryFormatter();

            //Abrimos el archivo
            FileStream stream = new FileStream(path, FileMode.Open);

            //Deserializamos el flujo de datos
            obstaclesData data = formatter.Deserialize(stream) as obstaclesData;
            stream.Close();
            return data;
        }

        else
        {
            return null;
        }
    }

    /// <summary>
    /// M�todo para guardar la informaci�n de los objetos equipados.
    /// </summary>
    public static void saveEquippedObjectsData(List<newEquippedObjectData> dataToStore, int id)
    {
        //Creamos el formateador binario
        BinaryFormatter formatter = new BinaryFormatter();

        //Obtenemos la ruta
        string path = createPath("objectsData");

        //Abrimos el archivo
        FileStream stream = new FileStream(path, FileMode.Create);

        //Serializamos el flujo de datos
        equippedObjectData data = new equippedObjectData(dataToStore, id);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// M�todo para cargar la informaci�n de los objetos equipados.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="equippedObjectData"/> que contiene los datos de los objetos equipados.</returns>
    public static equippedObjectData loadEquippedObjectsData()
    {
        //Obtenemos la ruta
        string path = createPath("objectsData");
        if (File.Exists(path))
        {
            //Creamos el formateador binario
            BinaryFormatter formatter = new BinaryFormatter();

            //Abrimos el archivo
            FileStream stream = new FileStream(path, FileMode.Open);

            //Deserializamos el flujo de datos
            equippedObjectData data = formatter.Deserialize(stream) as equippedObjectData;
            stream.Close();
            return data;
        }

        else
        {
            return null;
        }
    }

    /// <summary>
    /// M�todo para guardar la informaci�n de las tiendas.
    /// </summary>
    public static void saveShopData(List<sceneShopData> dataToStore)
    {
        //Creamos el formateador binario
        BinaryFormatter formatter = new BinaryFormatter();

        //Obtenemos la ruta
        string path = createPath("shopData");

        //Abrimos el archivo
        FileStream stream = new FileStream(path, FileMode.Create);

        //Serializamos el flujo de datos
        shopData data = new shopData(dataToStore);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// M�todo para cargar la informaci�n de las tiendas.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="shopData"/> que contiene los datos de las tiendas.</returns>
    public static shopData loadShopData()
    {
        //Obtenemos la ruta
        string path = createPath("shopData");
        if (File.Exists(path))
        {
            //Creamos el formateador binario
            BinaryFormatter formatter = new BinaryFormatter();

            //Abrimos el archivo
            FileStream stream = new FileStream(path, FileMode.Open);

            //Deserializamos el flujo de datos
            shopData data = formatter.Deserialize(stream) as shopData;
            stream.Close();
            return data;
        }

        else
        {
            return null;
        }
    }

    /// <summary>
    /// M�todo para guardar la informaci�n de la �ltima hoguera en la que se ha descansado.
    /// </summary>
    public static void saveLastBonfireData(bool rest)
    {
        //Creamos el formateador binario
        BinaryFormatter formatter = new BinaryFormatter();

        //Obtenemos la ruta
        string path = createPath("lastBonfireData");

        //Abrimos el archivo
        FileStream stream = new FileStream(path, FileMode.Create);

        //Serializamos el flujo de datos
        float[] playerPosition = new float[3];
        playerPosition[0] = config.getPlayer().transform.position.x;
        playerPosition[1] = config.getPlayer().transform.position.y;
        playerPosition[2] = config.getPlayer().transform.position.z;
        lastBonfireData data = new lastBonfireData(playerPosition, SceneManager.GetActiveScene().buildIndex, rest);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// M�todo para cargar la informaci�n de la �ltima hoguera en la que se ha descansado.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="lastBonfireData"/> que contiene los datos de la �ltima hoguera en la que se ha descansado.</returns>
    public static lastBonfireData loadLastBonfireData()
    {
        //Obtenemos la ruta
        string path = createPath("lastBonfireData");
        if (File.Exists(path))
        {
            //Creamos el formateador binario
            BinaryFormatter formatter = new BinaryFormatter();

            //Abrimos el archivo
            FileStream stream = new FileStream(path, FileMode.Open);

            //Deserializamos el flujo de datos
            lastBonfireData data = formatter.Deserialize(stream) as lastBonfireData;
            stream.Close();
            return data;
        }

        else
        {
            return null;
        }
    }

    /// <summary>
    /// M�todo para guardar la informaci�n de las almas perdidas.
    /// </summary>
    public static void saveSoulContainerData(bool died)
    {
        //Creamos el formateador binario
        BinaryFormatter formatter = new BinaryFormatter();

        //Obtenemos la ruta
        string path = createPath("soulContainerData");

        //Abrimos el archivo
        FileStream stream = new FileStream(path, FileMode.Create);

        //Serializamos el flujo de datos
        float[] position = new float[3];
        position[0] = config.getPlayer().transform.position.x;
        position[1] = config.getPlayer().transform.position.y - (config.getPlayer().transform.localScale.y * config.getPlayer().GetComponent<BoxCollider2D>().size.y / 2);
        position[2] = config.getPlayer().transform.position.z;
        soulContainerData data = new soulContainerData(config.getPlayer().GetComponent<combatController>().getSouls(), position, SceneManager.GetActiveScene().buildIndex, died);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// M�todo para cargar la informaci�n de las almas perdidas.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="soulContainerData"/> que contiene la informaci�n de las almas perdidas.</returns>
    public static soulContainerData loadSoulContainerData()
    {
        //Obtenemos la ruta
        string path = createPath("soulContainerData");
        if (File.Exists(path))
        {
            //Creamos el formateador binario
            BinaryFormatter formatter = new BinaryFormatter();

            //Abrimos el archivo
            FileStream stream = new FileStream(path, FileMode.Open);

            //Deserializamos el flujo de datos
            soulContainerData data = formatter.Deserialize(stream) as soulContainerData;
            stream.Close();
            return data;
        }

        else
        {
            return null;
        }
    }
    /// <summary>
    /// M�todo para guardar la informaci�n de la �ltima escena visitada.
    /// </summary>
    public static void saveLastScene()
    {
        //Creamos el formateador binario
        BinaryFormatter formatter = new BinaryFormatter();

        //Obtenemos la ruta
        string path = createPath("lastSceneData");

        //Abrimos el archivo
        FileStream stream = new FileStream(path, FileMode.Create);

        lastSceneData data = new lastSceneData(SceneManager.GetActiveScene().buildIndex);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// M�todo para cargar la informaci�n de la �ltima escena visitada.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="lastSceneData"/> que contiene los datos de la �ltima escena visitada.</returns>
    public static lastSceneData loadLastSceneData()
    {
        //Obtenemos la ruta
        string path = createPath("lastSceneData");
        if (File.Exists(path))
        {
            //Creamos el formateador binario
            BinaryFormatter formatter = new BinaryFormatter();

            //Abrimos el archivo
            FileStream stream = new FileStream(path, FileMode.Open);

            //Deserializamos el flujo de datos
            lastSceneData data = formatter.Deserialize(stream) as lastSceneData;
            stream.Close();
            return data;
        }

        else
        {
            return null;
        }
    }


    /// <summary>
    /// M�todo para serializar la configuraci�n de audio.
    /// </summary>
    /// <param name="master">Volumen master.</param>
    /// <param name="ost">Volumen de las OST.</param>
    /// <param name="sfx">Volumen de los SFX.</param>
    public static void saveAudioSettings(float master, float ost, float sfx)
    {
        //Creamos el formateador binario
        BinaryFormatter formatter = new BinaryFormatter();

        //Obtenemos la ruta
        string path = createAudioPath();
        if (File.Exists(path))
        {
            //Abrimos el archivo
            FileStream stream = new FileStream(path, FileMode.Open);

            //Creamos un objeto con la configuraci�n buscada
            audioSettingsData data = new audioSettingsData(master, ost, sfx);

            //Serializamos
            formatter.Serialize(stream, data);
            stream.Close();
        }
        else
        {
            //Abrimos el archivo
            FileStream stream = new FileStream(path, FileMode.Create);
            
            //Creamos los valores por defecto
            audioSettingsData data = new audioSettingsData();

            //Serializamos
            formatter.Serialize(stream, data);
            stream.Close();
        }
    }

    /// <summary>
    /// M�todo para cargar la informaci�n de la configuraci�n de audio.
    /// </summary>
    /// <returns>Un objeto de tipo <see cref="audioSettingsData"/> que contiene los datos de la configuraci�n de audio.</returns>
    public static audioSettingsData loadAudioSettings()
    {
        //Obtenemos la ruta
        string path = createAudioPath();
        if (File.Exists(path))
        {
            //Creamos el formateador binario
            BinaryFormatter formatter = new BinaryFormatter();

            //Abrimos el archivo
            FileStream stream = new FileStream(path, FileMode.Open);

            //Deserializamos el flujo de datos
            audioSettingsData data = formatter.Deserialize(stream) as audioSettingsData;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// M�todo que devuelve la ruta al archivo que contiene la �ltima ruta.
    /// </summary>
    /// <returns>Un string que contiene la ruta al archivo con la �ltima ruta.</returns>
    public static string createRoutePath()
    {
        return Application.persistentDataPath + "/route.txt";
    }

    /// <summary>
    /// M�todo que devuelve la ruta al archivo que contiene la configuraci�n de pantalla, com�n a todos los perfiles.
    /// </summary>
    /// <returns>Un string que contiene la ruta al archivo con la configuraci�n de pantalla.</returns>
    public static string createScreenPath()
    {
        return Application.persistentDataPath + "/screenOptions.txt";
    }

    /// <summary>
    /// M�todo que devuelve la ruta al archivo que contiene los nombres de los perfiles.
    /// </summary>
    /// <returns>Un string que contiene la ruta al archivo con  todos los nombres de los perfiles.</returns>
    public static string createProfilesPath()
    {
        return Application.persistentDataPath + "/userNames.txt";
    }

    /// <summary>
    /// M�todo que devuelve la ruta al archivo que contiene la configuraci�n de audio, com�n a todos los perfiles.
    /// </summary>
    /// <returns>Un string que contiene la ruta al archivo con la configuraci�n de audio.</returns>
    public static string createAudioPath()
    {
        return Application.persistentDataPath + "/audioSettings.txt";
    }

    /// <summary>
    /// M�todo que devuelve la ruta a un archivo concreto de un perfil.
    /// </summary>
    /// <param name="text">El nombre del archivo.</param>
    /// <returns>Un string que contiene la ruta a un archivo en concreto del perfil actual.</returns>
    public static string createPath(string text)
    {
        return profileSystem.getCurrentPath() + "/" + text + ".txt";
    }
}
