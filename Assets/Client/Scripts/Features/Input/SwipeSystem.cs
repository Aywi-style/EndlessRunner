using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class SwipeSystem : IEcsRunSystem
    {
        readonly EcsCustomInject<GameState> _gameState;

        readonly EcsWorldInject _world;

        readonly EcsFilterInject<Inc<InputComponent>> _inputFilter = default;

        readonly EcsPoolInject<SwipedEvent> _swipedEventPool = default;

        readonly EcsPoolInject<InputComponent> _inputPool = default;

        private Touch _touch;

        private bool _isTouched;

        public void Run (IEcsSystems systems)
        {
            if (Input.touchCount <= 0)
            {
                return;
            }

            foreach (var inputEntity in _inputFilter.Value)
            {
                ref var inputComponent = ref _inputPool.Value.Get(inputEntity);

                _touch = Input.GetTouch(0);

                if (TouchIsBegan())
                {
                    _isTouched = true;
                    inputComponent.FirstTapPosition = _touch.position;
                }
                else if (TouchIsOver())
                {
                    ClearTouchInfo(ref inputComponent);
                }

                inputComponent.Direction = Vector2.zero;

                if (_isTouched)
                {
                    inputComponent.Direction = Input.GetTouch(0).position - inputComponent.FirstTapPosition;
                }

                if (SwipeIsShort(ref inputComponent))
                {
                    continue;
                }

                if (IsVerticalSwipe(ref inputComponent))
                {
                    continue;
                }

                var swipeDirection = inputComponent.Direction.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;

                _swipedEventPool.Value.Add(_world.Value.NewEntity()).Invoke(swipeDirection);

                ClearTouchInfo(ref inputComponent);
            }
        }

        private bool TouchIsBegan()
        {
            return _touch.phase == TouchPhase.Began;
        }

        private bool TouchIsOver()
        {
            return _touch.phase == TouchPhase.Canceled || _touch.phase == TouchPhase.Ended;
        }

        private bool SwipeIsShort(ref InputComponent inputComponent)
        {
            return inputComponent.Direction.magnitude < _gameState.Value.GameSettings.SwipeSensitivity;
        }

        private bool IsVerticalSwipe(ref InputComponent inputComponent)
        {
            return Mathf.Abs(inputComponent.Direction.x) < Mathf.Abs(inputComponent.Direction.y);
        }

        private void ClearTouchInfo(ref InputComponent inputComponent)
        {
            _isTouched = false;
            inputComponent.FirstTapPosition = Vector2.zero;
            inputComponent.Direction = Vector2.zero;
        }
    }
}