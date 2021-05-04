using MY.Scripts.UnityComponents;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

namespace MY.Scripts
{
    [CreateAssetMenu(menuName = "My Setting/Configuration", fileName = "Configuration", order = 0)]
    internal class Configuration : ScriptableObject
    {
        [Range(0.1f, 2)] public float CubeMergeDuration;
        [Range(0, 4)] public float CubeMergeJumpPower;
        public float CubeMoveDuration;
        [Space] public CubeView CubeView;
        public CellView CellView;
        public ParticleSystem BombParticle;
        public GameObject ClickAnimationPrefab;

        [Header("Bot Settings")] public float MaxDelayAddingBotScore = 2f;
        [Range(0f, 1f)] public float BotDifficulty = 0.4f;

        [Space] [Header("Timer value in seconds")]
        public float GameTime;
        [Range(0, 0.2f)] public float CubesMoveTimeStep = 0.05f;
        public float CubesMoveIntervalTimeMin = 1;
        [Header("At the wrong select cubes")] public float CameraShakeDuration;
        public float CameraShakeStrength;
        public int CameraShakeVibrato;
        public float CameraShakeRandomness;
        public bool CameraShakeFadeOut;
        public long DeviceVibrateInMilliseconds;
        [Space] public int LevelWidth = 6;
        public int LevelHeight = 6;
        [Space] public int StartCashCount;
        public float RatioOfCache;
        public float CooldownBombBoost;
        public float CooldownAutoMergeBoost;
        public int AdCashCoefficient = 3;
        [Range(0,1)] public float WidthOrHeight = 0;

        public DictionaryOfIntAndSprite IconsByRank;
        public DictionaryOfIntAndInt ScorsByRank;
        public DictionaryOfIntAndFloat StartMoveSpeedByRank;
    }

    [System.Serializable]
    public class DictionaryOfIntAndSprite : SerializableDictionaryBase<int, Sprite>
    {
    }
    
 [System.Serializable]
    public class DictionaryOfIntAndFloat : SerializableDictionaryBase<int, float>
    {
    }

    [System.Serializable]
    public class DictionaryOfIntAndInt : SerializableDictionaryBase<int, int>
    {
    }
}