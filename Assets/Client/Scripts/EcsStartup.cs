using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    public sealed class EcsStartup : MonoBehaviour
    {
        private EcsWorld _world;
        private GameState _gameState;
        private EcsSystems _initSystems, _inRunSystems, _restartSystems;

        #region Configs
        [field: Space]
        [field: SerializeField] public GameSettings GameSettings { private set; get; }
        [field: SerializeField] public LevelConfig LevelConfig { private set; get; }
        [field: SerializeField] public ObstaclesConfig ObstaclesConfig { private set; get; }
        [field: SerializeField] public AirBallonConfig AirBallonConfig { private set; get; }
        #endregion

        [field: Space]
        [field: SerializeField] public AllPools AllPools { private set; get; }

        void Start()
        {
            _world = new EcsWorld();

            _gameState = GameState.InitializeNew(this);

            _initSystems = new EcsSystems(_world);
            _inRunSystems = new EcsSystems(_world);
            _restartSystems = new EcsSystems(_world);

            _initSystems
                .Add(new InitSwipe())
                .Add(new InitAirBalloon())
                .Add(new InitCamera())
                .Add(new InitPools())
                .Add(new InitInRunCanvas())

                .Add(new DefineScreenBounds())

                .Add(new StartGameSystem())
                ;

            _inRunSystems
                .Add(new SwipeSystem())
                .Add(new SwipedEventSystem())

                .Add(new ObstaclesSpawnTimer())
                .Add(new ObstaclesSpawnEventSystem())

                .Add(new ScrollingSystem())
                .Add(new MovingBetweenLines())
                .Add(new HeightCounting())

                .Add(new AirBalloonCollidingSystem())
                .Add(new PlayExplosionEventSystem())

                .Add(new MoveTransitionBackgroundEventSystem())
                .Add(new ChangeBackgroundColorEventSystem())

                .Add(new ActualLevelPartChanger())

                .Add(new ObstaclesDespawn())
                ;

            _restartSystems
                .Add(new RestartTimerSystem())
                ;
#if UNITY_EDITOR
            _inRunSystems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                ;
#endif

            InjectAllSystems(_initSystems, _inRunSystems, _restartSystems);
            InitAllSystems(_initSystems, _inRunSystems, _restartSystems);
        }

        void Update()
        {
            _initSystems?.Run();

            if (_gameState.IsPlayingGame)
            {
                _inRunSystems?.Run();
            }
            else
            {
                _restartSystems?.Run();
            }
        }

        void OnDestroy()
        {
            OnDestroyAllSystems(_initSystems, _inRunSystems, _restartSystems);

            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }

        private void InjectAllSystems(params EcsSystems[] systems)
        {
            foreach (var system in systems)
            {
                system.Inject(_gameState);
            }
        }

        private void InitAllSystems(params EcsSystems[] systems)
        {
            foreach (var system in systems)
            {
                system.Init();
            }
        }

        private void OnDestroyAllSystems(params EcsSystems[] systems)
        {
            for (int i = 0; i < systems.Length; i++)
            {
                if (systems[i] != null)
                {
                    systems[i].Destroy();
                    systems[i] = null;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;

            var leftLine = new Vector2(-GameSettings.LineOffset, 0);
            DrawRunLine(leftLine);

            var middleLine = new Vector2(0f, 0);
            DrawRunLine(middleLine);

            var rightLine = new Vector2(GameSettings.LineOffset, 0);
            DrawRunLine(rightLine);
        }

        private void DrawRunLine(Vector2 from)
        {
            Gizmos.DrawLine(from, from + (Vector2.up * 100));
        }
    }
}
