using System;
using UnityEngine;

namespace TheHuntGame.GameMachine
{
    public enum GameMachineButtonCode
    {
        LEFT,
        CENTER,
        RIGHT
    }

    [Serializable]
    public class GameMachineButton
    {
        public GameMachineButtonCode GameMachineButtonCode;
        public string PressCommand;
        public string ReleaseCommand;
        [HideInInspector]
        public bool IsButtonPressed;
        [HideInInspector]
        public bool IsButtonPressedLastFrame;
    } 
}