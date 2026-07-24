using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Enums;
using UnityEngine;

namespace _Project.Scripts.Utilities {
    public class KeyedPool<TKey, TInstance> : MonoBehaviour
        where TInstance : Component, IPoolKeyed<TKey> {

        [Serializable]
        public class Config {
            public TKey type;
            public TInstance prefab;
            public int poolSize = 16;
            public PoolFullPolicy poolPolicy = PoolFullPolicy.Ring;
        }

        [SerializeField] private List<Config> configs = new();

        private readonly Dictionary<TKey, Config> _typeToConfig = new();
        private readonly Dictionary<TKey, Queue<TInstance>> _available = new();

        // LinkedList lets us remove returned instances while preserving
        // oldest-to-newest ordering for the Ring policy.
        private readonly Dictionary<TKey, LinkedList<TInstance>> _activeOldest = new();

        protected IEnumerable<TInstance> ActiveInstances {
            get {
                foreach (var activeList in _activeOldest.Values) {
                    foreach (var instance in activeList) {
                        if (instance && instance.InUse)
                            yield return instance;
                    }
                }
            }
        }

        private void Awake() {
            _typeToConfig.Clear();
            _available.Clear();
            _activeOldest.Clear();

            foreach (var config in configs) {
                if (config == null || !config.prefab)
                    continue;

                _typeToConfig[config.type] = config;

                _available[config.type] =
                    new Queue<TInstance>(Mathf.Max(0, config.poolSize));

                _activeOldest[config.type] =
                    new LinkedList<TInstance>();

                Prewarm(config);
            }
        }

        public TInstance Rent(TKey type) {
            if (!_typeToConfig.TryGetValue(type, out var config)) {
                Debug.LogError($"{name}: No pool configuration for {type}");
                return null;
            }

            EnsureCollections(type);

            TInstance instance;

            if (_available[type].Count > 0) {
                instance = _available[type].Dequeue();
                _activeOldest[type].AddLast(instance);
            }
            else if (config.poolPolicy == PoolFullPolicy.Ring) {
                instance = RecycleOldest(type);
            }
            else {
                instance = ProcessFull(config);

                if (instance)
                    _activeOldest[type].AddLast(instance);
            }

            if (!instance)
                return null;

            instance.MarkInUse();
            OnRented(instance);

            return instance;
        }

        private TInstance ProcessFull(Config config) {
            switch (config.poolPolicy) {
                case PoolFullPolicy.Wait:
                    return null;

                case PoolFullPolicy.Ring:
                    return RecycleOldest(config.type);

                case PoolFullPolicy.Grow:
                    return CreateInstance(config);

                default:
                    return null;
            }
        }

        private TInstance RecycleOldest(TKey type) {
            var activeList = _activeOldest[type];

            while (activeList.First != null) {
                var instance = activeList.First.Value;
                activeList.RemoveFirst();

                if (!instance)
                    continue;

                if (!instance.InUse)
                    continue;

                instance.ForceRecycle();

                // It is now the newest active instance.
                activeList.AddLast(instance);

                return instance;
            }

            return null;
        }

        private TInstance CreateInstance(Config config) {
            var instance = Instantiate(config.prefab, transform);

            instance.gameObject.SetActive(false);
            instance.Bind(ReturnToPool, config.type);
            instance.MarkFree();

            return instance;
        }

        private void Prewarm(Config config) {
            if (config.poolSize <= 0)
                return;

            for (var i = 0; i < config.poolSize; i++) {
                var instance = CreateInstance(config);
                _available[config.type].Enqueue(instance);
            }
        }

        private void ReturnToPool(Component component, TKey type) {
            if (component is not TInstance instance)
                return;

            if (!instance.InUse)
                return;

            EnsureCollections(type);

            instance.MarkFree();

            // Prevent the same object from remaining in the active ordering
            // after being returned.
            _activeOldest[type].Remove(instance);

            OnReturned(instance);

            instance.transform.SetParent(transform, worldPositionStays: false);
            instance.gameObject.SetActive(false);

            _available[type].Enqueue(instance);
        }

        private void EnsureCollections(TKey type) {
            if (!_available.ContainsKey(type))
                _available[type] = new Queue<TInstance>();

            if (!_activeOldest.ContainsKey(type))
                _activeOldest[type] = new LinkedList<TInstance>();
        }

        protected virtual void OnRented(TInstance instance) {
        }

        protected virtual void OnReturned(TInstance instance) {
        }
    }
}