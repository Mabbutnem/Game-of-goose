using UnityEngine;

public static class GameObjectUtils
{
   public static GameObject Find(string name)
   {
      GameObject foundObject = GameObject.Find(name);

      if (foundObject == null)
      {
         DebugErrorStop(name + " was not found.");
      }

      return foundObject;
   }

   public static void DebugErrorStop(string message)
   {
      Debug.LogError(message);
      Debug.Break();
   }
}
