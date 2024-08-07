using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private PlayerData playerData;
    public PlayerData PlayerData => playerData;


    private void Awake()
    {
        playerData = Saver.Read<PlayerData>(GameConstant.PLAYERDATA);
        if (playerData == null)
        {
            playerData = new PlayerData();
            playerData.GodId = 0;
            playerData.ItemList = new List<Item>();
        }
    }

    public void LoadScene(string name)
    {
        StartCoroutine(OnLoadSceneAsync(name));
    }

    IEnumerator OnLoadSceneAsync(string name)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }


    #region Helper

    /*
    public void DoLoadSpriteResourceAsync(Image img, string resourcePath)
    {
        StartCoroutine(LoadSpriteResourceAsync(img, resourcePath));
    }

    private IEnumerator LoadSpriteResourceAsync(Image img, string resourcePath)
    {
        ResourceRequest request = Resources.LoadAsync<Sprite>(resourcePath);

        yield return request;

        Sprite loadedResource = (Sprite)request.asset;

        if (loadedResource != null)
        {
            img.sprite = loadedResource;
        }
        else
        {
            Debug.LogError("Failed to load resource.");
        }
    }

    */
    public void Save()
    {
        if (playerData != null)
        {
            Saver.Write(playerData, GameConstant.PLAYERDATA);
        }

    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            Save();
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Save();
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }
    #endregion
}
