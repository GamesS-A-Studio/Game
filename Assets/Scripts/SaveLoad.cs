using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour
{
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.gmInstance;
    }

    // Update is called once per frame
  
    public void buttonSave()
    {
        ChamadaSvae();
    }
    public void buttonLoad()
    {
        LoadGame();
    }
    public void ChamadaSvae()
    {
        if(gm == null)
        {
            gm = GameManager.gmInstance;
            return;
        }
        else
        {
            SaveInfo save = new SaveInfo();

            save.chefe1 = (gm.Boss1 == true) ? 1 : 0;

            save.quantGrana = gm.dinheiro;
            save.quantCarne = gm.carne;

            save.vidaAtual = gm.vida.lifeAtual;

            Transform pos = gm.movimentPlayer.transform;
            save._posX = pos.transform.position.x;
            save._posY = pos.transform.position.y;
            save._posZ = pos.transform.position.z;

            save._rotX = pos.transform.rotation.x;
            save._rotY = pos.transform.rotation.y;
            save._rotZ = pos.transform.rotation.z;

            SaveGame(save);
        }
        
    }
    public void SaveGame(SaveInfo save)
    {

        string saveFileName = "savegame" + ".save";
        string path = Path.Combine(Application.persistentDataPath, saveFileName);
        BinaryFormatter bi = new BinaryFormatter();
        FileStream file;

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        file = File.Create(path);
        bi.Serialize(file, save);
        file.Close();
    }
    public void LoadGame()
    {
        if(gm == null)
        {
            gm = GameManager.gmInstance;
            return;
        }else
        {
            Time.timeScale = 1.0f;
            gm.menuGameOver.SetActive(false);
            BinaryFormatter bi = new BinaryFormatter();
            string saveFileName = "savegame" + ".save";
            string path = Path.Combine(Application.persistentDataPath, saveFileName);

            FileStream file;

            if (File.Exists(path))
            {
                file = File.Open(path, FileMode.Open);
                SaveInfo load = (SaveInfo)bi.Deserialize(file);
                file.Close();

                gm.Boss1 = (load.chefe1 == 1) ? true : false;

                gm.vida.lifeAtual = load.vidaAtual;

                gm.dinheiro = load.quantGrana;
                gm.carne = load.quantCarne;

                Transform pos = gm.movimentPlayer.transform;

                pos.transform.position = new Vector3(load._posX, load._posY, load._posZ);
                pos.transform.rotation = Quaternion.Euler(load._rotX, load._rotY, load._rotZ);
            }
        }
      
    }
}
