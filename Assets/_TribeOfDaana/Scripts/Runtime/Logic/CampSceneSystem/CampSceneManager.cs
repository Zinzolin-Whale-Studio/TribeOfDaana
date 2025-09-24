using System;
using _TribeOfDaana.Scripts.Runtime.Core.Manager;
using _TribeOfDaana.Scripts.Runtime.Logic.Levels;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Logic.CampSceneSystem
{
    public class CampSceneManager : SceneSystemManager<CampSceneManager>
    {
        [SerializeField] private LevelInfoDatabase _levelInfoDatabase;
        //For test
        [SerializeField] private SpriteRenderer _campBackground;
        
        public override bool InitializeManager()
        {
            return true;
        }
        
        public override bool StartScene()
        {
            if (_levelInfoDatabase == null)
            {
                CampSceneDebug.LogError("Can't start level because Level Info Database isn't referenced.");
                return false;
            }

            try
            {
                _levelInfoDatabase.GetLevelToLoad();
            }
            catch (Exception exception)
            {
                CampSceneDebug.LogError("Error when trying to get the level info.");
                CampSceneDebug.LogError($"Error message : {exception.Message}");
            }


            return true;
        }
    }
}
