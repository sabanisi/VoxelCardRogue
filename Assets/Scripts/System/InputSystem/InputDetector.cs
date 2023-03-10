using System.Collections.Generic;
using UnityEngine;

namespace System.InputSystem
{
    public class InputDetector:MonoBehaviour
    {
        private readonly List<Key> _checkKeys = new List<Key>();
        
        //TODO:仮実装
        private void Start()
        {
            Initialize();
        }
        
        //初期化処理
        public void Initialize()
        {
            IInput playerInput = new PlayerInput();
            _checkKeys.Add(new Key("W",KeyState.Down,playerInput));
            _checkKeys.Add(new Key("A",KeyState.Down,playerInput));
            _checkKeys.Add(new Key("S",KeyState.Down,playerInput));
            _checkKeys.Add(new Key("D",KeyState.Down,playerInput));
        }

        public void Update()
        {
            foreach (var key in _checkKeys)
            {
                if (KeyCheck(key))
                {
                    key.MyInput.Input(key.Name);
                }
            }
        }

        //指定されたキーのGetButtonDown、GetButton、GetButtonUpのいずれかを調べる
        private bool KeyCheck(Key key)
        {
            switch (key.State)
            {
                case KeyState.Down:
                    return Input.GetButtonDown(key.Name);
                case KeyState.Press:
                    return Input.GetButton(key.Name);
                case KeyState.Up:
                    return Input.GetButtonUp(key.Name);
                default:
                    return false;
            }
        }
        
        private class Key
        {
            public string Name { get; }
            public KeyState State { get; }
            public IInput MyInput{get;}
            public Key(string name, KeyState state,IInput myInput)
            {
                Name = name;
                State = state;
                MyInput = myInput;
            }
        }

        private enum KeyState
        {
            Down,Up,Press
        }
    }
}