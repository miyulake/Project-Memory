using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class JsonReadWriteSystem : MonoBehaviour
{
    private string saveFolder;
    [SerializeField] private string dataPath = "/DataFile.json";
    [SerializeField] private InventoryData inventory;
    [SerializeField] private LootList lootList;
    [SerializeField] private Transform player;

    private void Awake()
    {
        saveFolder = Application.dataPath + "/SaveData/";
        if (!Directory.Exists(saveFolder))
        {
            Directory.CreateDirectory(saveFolder);
        }
    }

    private void Start()
    {
        DeactivateItems();
    }

    private void Update()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SaveToJson()
    {
        GameData data = new GameData();

        // Save Player Position
        data.playerPos = player.position;

        // Save Loot List
        data.loot = lootList.lootedObjects;

        // Save Inventory Amounts
        data.soulAmount = inventory.soulAmount;
        data.orbAmount = inventory.orbAmount;
        data.currency = inventory.currency;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFolder + dataPath, json);
    }

    public void LoadFromJson()
    {
        if (File.Exists(saveFolder + dataPath))
        {
            string json = File.ReadAllText(saveFolder + dataPath);
            GameData data = JsonUtility.FromJson<GameData>(json);

            // Load Player Position
            player.position = data.playerPos;

            // Load Loot List
            lootList.lootedObjects = data.loot;

            // Load Inventory Amounts
            inventory.soulAmount = data.soulAmount;
            inventory.orbAmount = data.orbAmount;
            inventory.currency = data.currency;
        }
    }

    public void DeactivateItems()
    {
        // This is fine if you reload the scene when loading data
        // Or call it as a function
        // Otherwise items won't dissapear when loading
        foreach (var loot in lootList.lootedObjects)
        {
            loot.SetActive(false);
        }
    }
}
