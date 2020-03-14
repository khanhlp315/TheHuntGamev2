using System;
using System.Collections;
using System.Collections.Generic;
using TheHuntGame.Utilities;
using UnityEngine;

namespace TheHuntGame.Scenes
{
    public class LoadingScene: MonoBehaviour
    {
        private int _systemsLoaded = 0;
        private readonly List<ISystem> _systems = new List<ISystem>();

        private void Awake()
        {
            _systems.Add(Runner.Runner.Instance);
            _systems.Add(EventSystem.EventSystem.Instance);
            _systems.Add(Network.Network.Instance);
            _systems.Add(GameMachine.GameMachine.Instance);
        }

        private void Start()
        {
            StartCoroutine(InitSystems());
            StartCoroutine(WaitToGoToHomeScene());
        }

        private IEnumerator WaitToGoToHomeScene()
        {
            yield return new WaitForSeconds(3.0f);
            while (_systemsLoaded < _systems.Count)
            {
                yield return null;
            }


            yield return null;
        }
        
        private IEnumerator InitSystems()
        {
            for(int i = 0; i < _systems.Count; ++i)
            {
                var system = _systems[i];
                system.OnInitializeDone += () => { _systemsLoaded++; };
                system.Initialize();
                while (_systemsLoaded <= i)
                {
                    yield return null;
                }
            }
        }

    }
}