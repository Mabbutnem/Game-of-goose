using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchManager : MonoBehaviour
{
   public void StartGameWithNbPlayer(int nbPlayer)
   {
      GameManager.NbPlayerGoose = nbPlayer;
      SceneManager.LoadScene("GameScene");
   }
}
