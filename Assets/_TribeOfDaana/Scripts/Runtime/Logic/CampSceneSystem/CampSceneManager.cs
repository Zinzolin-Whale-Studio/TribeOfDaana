using System;
using _TribeOfDaana.Scripts.Runtime.Core.Manager;
using _TribeOfDaana.Scripts.Runtime.Logic.Levels;
using UnityEngine;
using UnityEngine.Serialization;

namespace _TribeOfDaana.Scripts.Runtime.Logic.CampSceneSystem
{
    public class CampSceneManager : SceneSystemManager<CampSceneManager>
    {
        [SerializeField] private LevelInfoDatabase m_levelInfoDatabase;
        //For test
        [SerializeField] private SpriteRenderer m_campBackground;
        
        public override bool InitializeManager()
        {
            return true;
        }
        
        public override bool StartScene()
        {
            if (m_levelInfoDatabase == null)
            {
                CampSceneDebug.LogError("Can't start level because Level Info Database isn't referenced.");
                return false;
            }

            try
            {
                LevelInfo levelInfo = m_levelInfoDatabase.GetLevelToLoad();
                m_campBackground.sprite = levelInfo.CampBackgroundSprite;
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
