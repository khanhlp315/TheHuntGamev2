using System;
using System.Linq;
using TheHuntGame.EventSystem.Events;
using TheHuntGame.Utilities;
using UnityEngine;

namespace TheHuntGame.GameMachine
{
    public class GameMachine: MonoSingleton<GameMachine, GameMachineConfig>
    {
        [SerializeField]
        private SerialController _serialControllerPrefab;
        private SerialController _serialController;

        private GameMachineButton[] _gameButtons;

        public override void Initialize()
        {
            _gameButtons = _config.Buttons.ToArray();
            _serialController = Instantiate(_serialControllerPrefab, this.transform);
#if UNITY_EDITOR
            _serialController.portName = $"COM{_config.Port}";
#elif UNITY_STANDALONE_WIN
        _serialController.portName = $"COM{_config.Port}";
#elif UNITY_STANDALONE_LINUX
        _serialController.portName = $"/dev/ttyUSB{_config.Port}";
#endif
            OnInitializeDone?.Invoke();
        }

        private void Update()
        {
            foreach (var button in _gameButtons)
            {
                button.IsButtonPressed = button.IsButtonPressedLastFrame;
            }
            
#if !UNITY_EDITOR || USE_SERIAL_IN_EDITOR
            string message = _serialController.ReadSerialMessage();
            while (message != null)
            {
                if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
                    Debug.Log("Connection established");
                else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
                    Debug.Log("Connection attempt failed or disconnection detected");
                message = message.Trim();
                Debug.Log(message);

                if (message == _config.CoinInsertCommand)
                {
                    EventSystem.EventSystem.Instance.Emit<CoinInsertEvent>();
                }

                if (message == _config.CoinOutCommand)
                {
                    EventSystem.EventSystem.Instance.Emit<CoinOutEvent>();
                }

                foreach (var button in _gameButtons)
                {
                    if (message == button.PressCommand)
                    {
                        button.IsButtonPressed = true;
                    }

                    if (message == button.ReleaseCommand)
                    {
                        button.IsButtonPressed = false;
                    }
                }
                
                message = _serialController.ReadSerialMessage();
            }
#endif
        }
        
        

        private void LateUpdate()
        {
            foreach (var button in _gameButtons)
            {
                button.IsButtonPressedLastFrame = button.IsButtonPressed;
            }
        }

        private GameMachineButton GetButtonByCode(GameMachineButtonCode buttonCode)
        {
            return _gameButtons.FirstOrDefault(b => b.GameMachineButtonCode == buttonCode);
        }

        public bool GetButton(GameMachineButtonCode buttonCode)
        {
            var gameButton = GetButtonByCode(buttonCode);
            return gameButton != null && gameButton.IsButtonPressed;
        }
        
        public bool GetButtonUp(GameMachineButtonCode buttonCode)
        {
            var gameButton = GetButtonByCode(buttonCode);
            return gameButton != null && (gameButton.IsButtonPressedLastFrame && (!gameButton.IsButtonPressed));
        }
        
        public bool GetButtonDown(GameMachineButtonCode buttonCode)
        {
            var gameButton = GetButtonByCode(buttonCode);
            return gameButton != null && ((!gameButton.IsButtonPressedLastFrame) && gameButton.IsButtonPressed);
        }
    }
}