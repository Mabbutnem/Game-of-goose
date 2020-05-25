using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   #region Variables
   //singleton
   private UIManager instance;

   //Public
   public static Button RollButton;

   //Private
   private GameObject victoryMenu;
   private GameObject pauseMenu;
   #endregion

   #region Unity Callbacks
   private void Awake()
   {
      //SINGLETON
      if (instance == null) instance = this;
      else GameObjectUtils.DebugErrorStop("Multiple instances of singleton.");
      //SINGLETON

      RollButton = GameObjectUtils.Find("Roll Button").GetComponent<Button>();
      victoryMenu = GameObjectUtils.Find("Victory Menu");
      victoryMenu.SetActive(false);
      pauseMenu = GameObjectUtils.Find("Pause Menu");
      pauseMenu.SetActive(false);
   }

   private void Start()
   {
      GameManager.OnEndGame += ShowVictoryMenu;
   }

   private void Update()
   {
      if(Input.GetKeyDown(KeyCode.Escape))
      {
         SetPauseMenuActive(true);
      }
   }

   private void OnDestroy()
   {
      GameManager.OnEndGame -= ShowVictoryMenu;
   }
   #endregion

   #region Methods
   private void ShowVictoryMenu()
   {
      victoryMenu.SetActive(true);
   }

   public void SetPauseMenuActive(bool active)
   {
      pauseMenu.SetActive(active);
   }

   public void RestartGame()
   {
      SceneManager.LoadScene("StartScene");
   }

   public void QuitGame()
   {
      Application.Quit();
   }
   #endregion
}
