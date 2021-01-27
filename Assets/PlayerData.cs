using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    public static int health;
    public static Vector3 position;
    public static string scene;
    public static List<string> inventoryItems;

    public void PlayerDataSave()
    {
        health = gameObject.GetComponent<BarsController>().health;
        position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        scene = SceneManager.GetActiveScene().name;
        inventoryItems = gameObject.GetComponent<PlayerInter>().inventory.GetComponent<InventoryDisplay>().copyItemList();
    }
    public int getHealth()
    {
        return health;
    }
    public List<string> getInventoryItems()
    {
        return inventoryItems;
    }
    public string getScene()
    {
        return scene;
    }
    public Vector3 getPosition()
    {
        return position;
    }
}
