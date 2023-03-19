using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using DG.Tweening;

namespace Client
{
    sealed class InitAirBalloon : IEcsInitSystem
    {
        readonly EcsCustomInject<GameState> _gameState = default;

        readonly EcsWorldInject _world = default;

        readonly EcsPoolInject<AirBalloon> _airBalloonPool = default;
        readonly EcsPoolInject<UnderPlayerControlle> _underPlayerControllePool = default;
        readonly EcsPoolInject<Collidable> _collidablePool = default;
        readonly EcsPoolInject<View> _viewPool = default;

        private int _airBalloonEntity;

        public void Init (IEcsSystems systems)
        {
            _airBalloonEntity = _world.Value.NewEntity();

            InitAirBalloonComponent();

            InitUnderPlayerControlle();

            InitCollidable();

            InitView();

            DoFirstAnimation();
        }

        private void InitAirBalloonComponent()
        {
            ref var airBalloon = ref _airBalloonPool.Value.Add(_airBalloonEntity);
            airBalloon.AirBallonMB = GameObject.Instantiate(_gameState.Value.AirBallonConfig.AirBallon);
        }

        private void InitUnderPlayerControlle()
        {
            ref var underPlayerControlle = ref _underPlayerControllePool.Value.Add(_airBalloonEntity);
            underPlayerControlle.CurrentLine = LineType.Middle;
            underPlayerControlle.TargetLine = underPlayerControlle.CurrentLine;

            underPlayerControlle.MaxTimeToMove = _gameState.Value.AirBallonConfig.LineSwipingTime;
            underPlayerControlle.CurrentTimeToMove = 0;
        }

        private void InitCollidable()
        {
            ref var collidable = ref _collidablePool.Value.Add(_airBalloonEntity);
            ref var airBalloon = ref _airBalloonPool.Value.Get(_airBalloonEntity);
            
            collidable.CollidingCenter = airBalloon.AirBallonMB.CollidingCenter;
            collidable.CollidingRadius = airBalloon.AirBallonMB.CollidingRadius;
        }

        private void InitView()
        {
            ref var view = ref _viewPool.Value.Add(_airBalloonEntity);
            ref var airBalloon = ref _airBalloonPool.Value.Get(_airBalloonEntity);

            view.GameObject = airBalloon.AirBallonMB.gameObject;
            view.Transform = airBalloon.AirBallonMB.transform;
            view.ViewSprite = airBalloon.AirBallonMB.ViewSprite;
        }

        private void DoFirstAnimation()
        {
            ref var view = ref _viewPool.Value.Get(_airBalloonEntity);

            var sequence = DOTween.Sequence();

            float timeToTransition = 1f;

            var rightRotation = new Vector3(0, 0, _gameState.Value.AirBallonConfig.WobbleStrength);
            var leftRotation = - rightRotation;

            sequence.Append(view.ViewSprite.transform.DORotate(rightRotation, timeToTransition));
            sequence.AppendCallback(DoBaseAnimation);
        }

        private void DoBaseAnimation()
        {
            ref var view = ref _viewPool.Value.Get(_airBalloonEntity);

            var sequence = DOTween.Sequence();

            float timeToTransition = 1f;

            var rightRotation = new Vector3(0, 0, _gameState.Value.AirBallonConfig.WobbleStrength);
            var leftRotation = -rightRotation;

            sequence.Append(view.ViewSprite.transform.DORotate(leftRotation, timeToTransition));
            sequence.Append(view.ViewSprite.transform.DORotate(rightRotation, timeToTransition));
            sequence.SetLoops(-1);
        }
    }
}