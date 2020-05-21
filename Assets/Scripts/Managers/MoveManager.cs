using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
   #region Variables
   //Singleton
   private static MoveManager instance;

   //Readonly
   private static readonly float SPEED = 0.1f;
   private static readonly float FAST_SPEED = 0.25f;
   private static readonly float DONE_RADIUS = 0.05f;
   private static readonly float FAST_DONE_RADIUS = 0.2f;
   private static readonly float WAIT_BEFORE_MOVE = 0.5f;

   //Private
   private static bool mustMove = false;
   private static Transform currentTransform;
   private static Vector3 dir;
   private static Vector3 partialDestPosition;
   private static bool donePartialMove = false;
   private static float speed = SPEED;
   private static float doneRadius = DONE_RADIUS;

   //Events
   public static event Action<AGoose, ACell> OnMoved = (goose, destCell) => { };
   #endregion

   #region Unity Callbacks
   private void Awake()
   {
      //SINGLETON
      if (instance == null) instance = this;
      else GameObjectUtils.DebugErrorStop("Multiple instances of singleton.");
      //SINGLETON
   }

   private void FixedUpdate()
   {
      if(mustMove && !donePartialMove)
      {
         currentTransform.Translate(speed * dir);
         if((currentTransform.position - partialDestPosition).magnitude <= doneRadius)
         {
            donePartialMove = true;
            currentTransform.position = partialDestPosition;
         }
      }
   }
   #endregion

   #region Methods
   public static void Move(AGoose goose, int nbCells, bool fast = false)
   {
      speed = fast ? FAST_SPEED : SPEED;
      doneRadius = fast ? FAST_DONE_RADIUS : DONE_RADIUS;
      instance.StartCoroutine(instance.MoveRoutine(goose, nbCells));
   }

   public static void MoveAt(AGoose goose, int cellIndex, bool fast = false)
   {
      int nbCells = cellIndex - goose.CurrentCellIndex;
      Move(goose, nbCells, fast);
   }

   public static void MoveAfter(AGoose goose, int nbCells, bool fast = false)
   {
      instance.StartCoroutine(instance.MoveAfterRoutine(goose, nbCells, fast));
   }

   public static void MoveAtAfter(AGoose goose, int cellIndex, bool fast = false)
   {
      instance.StartCoroutine(instance.MoveAtAfterRoutine(goose, cellIndex, fast));
   }
   #endregion

   #region Routines
   public IEnumerator MoveRoutine(AGoose goose, int nbCells)
   {
      //Init variable used for all the move
      currentTransform = goose.Transform;
      int it = (int)Mathf.Sign(nbCells);
      ACell currentCell = BoardManager.Cells[goose.CurrentCellIndex];
      //ACell destCell = BoardManager.Cells[goose.CurrentCellIndex + nbCells];

      //Put after mustMove to prevent wrong first time direction
      partialDestPosition = BoardManager.Cells[currentCell.Index + it].Waypoint.position;
      dir = (partialDestPosition - currentCell.Waypoint.position).normalized;

      mustMove = true;
      while (mustMove)
      {
         //Verify if the destination is after the last cell
         int partialDestIndex = currentCell.Index + it;
         if(partialDestIndex >= BoardManager.NB_CELLS)
         {
            it = -it; nbCells = -nbCells; //Move to the other direction
            partialDestIndex += 2 * it; //Compensation for the previous add
         }

         //Refresh partialDestPosition and dir
         ACell partialDestCell = BoardManager.Cells[partialDestIndex];
         partialDestPosition = partialDestCell.Waypoint.position;
         dir = (partialDestPosition - currentCell.Waypoint.position).normalized;

         //Wait until partial move is done
         yield return new WaitUntil(() => donePartialMove);

         nbCells -= it; //Remove nb cell to move by 1
         donePartialMove = false; //partial move is not done anymore

         //Move done verification
         currentCell = partialDestCell;
         if (nbCells == 0) mustMove = false;
      }

      //Is there an other goose at destination ?
      if (currentCell.Occupied())
         Move(currentCell.Occupant, -1);
      else
         currentCell.TryEndTurn(goose);

      OnMoved(goose, currentCell);
      currentCell.OnMoved(goose);
   }

   private IEnumerator MoveAfterRoutine(AGoose goose, int cellIndex, bool fast)
   {
      yield return new WaitForSeconds(WAIT_BEFORE_MOVE);
      Move(goose, cellIndex, fast);
   }

   private IEnumerator MoveAtAfterRoutine(AGoose goose, int cellIndex, bool fast)
   {
      yield return new WaitForSeconds(WAIT_BEFORE_MOVE);
      MoveAt(goose, cellIndex, fast);
   }
   #endregion
}
