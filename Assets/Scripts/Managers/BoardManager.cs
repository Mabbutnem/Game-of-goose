using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
   #region Variables
   //Singleton
   private static BoardManager instance;

   //Readonly
   public static readonly int NB_CELLS = 64;
   private static readonly Dictionary<CellEnum, int[]> CELL_INITIALIZER = new Dictionary<CellEnum, int[]>()
   {
      {CellEnum.GOOZE,  new int[] { 5, 9, 14, 18, 23, 27, 32, 36, 41, 45, 50, 54, 59} },
      {CellEnum.BRIDGE, new int[] { 6} },
      {CellEnum.HOTEL,  new int[] { 19} },
      {CellEnum.WELL,   new int[] { 31} },
      {CellEnum.MAZE,   new int[] { 42} },
      {CellEnum.PRISON, new int[] { 52} },
      {CellEnum.DEATH,  new int[] { 58} },
      {CellEnum.END,    new int[] { 63} },
   };

   //Public
   public static ACell[] Cells { get; private set; } = new ACell[NB_CELLS];
   #endregion

   #region Unity Callbacks
   private void Awake()
   {
      //SINGLETON
      if (instance == null) instance = this;
      else GameObjectUtils.DebugErrorStop("Multiple instances of singleton.");
      //SINGLETON

      Transform waypointsTransform = GameObjectUtils.Find("Waypoints").transform;
      if(Cells.Length != waypointsTransform.childCount)
      {
         GameObjectUtils.DebugErrorStop("Not found " + Cells.Length + " waypoints on board. Found " + waypointsTransform.childCount + " instead.");
      }

      for (int i = 0; i < Cells.Length; i++)
      {
         Cells[i] = GenerateCell(IdentifyCell(i), i, waypointsTransform.GetChild(i));
      }

      //Subsribe to move manager
      MoveManager.OnMoved += RefreshBoardData;
   }

   private void OnDestroy()
   {
      //Unsubsribe to move manager
      MoveManager.OnMoved -= RefreshBoardData;
   }
   #endregion

   #region Methods
   public void RefreshBoardData(AGoose goose, ACell destCell)
   {
      //Refresh goose and board data
      Cells[goose.CurrentCellIndex].Occupant = null;
      goose.CurrentCellIndex = destCell.Index;
      destCell.Occupant = goose;
   }
   #endregion

   #region Init Cells
   private CellEnum IdentifyCell(int index)
   {
      foreach(CellEnum cellEnum in CELL_INITIALIZER.Keys)
      {
         if(CELL_INITIALIZER[cellEnum].Contains(index))
         {
            return cellEnum;
         }
      }
      return CellEnum.DEFAULT;
   }

   private ACell GenerateCell(CellEnum cellEnum, int index, Transform waypoint)
   {
      switch (cellEnum)
      {
         case CellEnum.GOOZE:
            return new GooseCell(index, waypoint);
         case CellEnum.BRIDGE:
            return new BridgeCell(index, waypoint);
         case CellEnum.HOTEL:
            return new HotelCell(index, waypoint);
         case CellEnum.WELL:
            return new WellCell(index, waypoint);
         case CellEnum.MAZE:
            return new MazeCell(index, waypoint);
         case CellEnum.PRISON:
            return new PrisonCell(index, waypoint);
         case CellEnum.DEATH:
            return new DeathCell(index, waypoint);
         case CellEnum.END:
            return new EndCell(index, waypoint);
         case CellEnum.DEFAULT:
            return new DefaultCell(index, waypoint);
         default:
            return new DefaultCell(index, waypoint);
      }
   }
   #endregion
}
