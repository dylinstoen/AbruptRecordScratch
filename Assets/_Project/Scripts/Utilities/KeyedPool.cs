using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Enums;
using UnityEngine;

namespace _Project.Scripts.Utilities {
    public class KeyedPool<TKey, TInstance> : MonoBehaviour where TInstance : Component, IPoolKeyed<TKey> {
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
        private readonly Dictionary<TKey, Queue<TInstance>> _activeOldest = new();

        private void Awake() {
            _typeToConfig.Clear();
            _available.Clear();
            _activeOldest.Clear();
            foreach (var c in configs) {
                if (c == null || !c.prefab)
                    continue;
                _typeToConfig[c.type] = c;
                if (!_available.ContainsKey(c.type))
                    _available[c.type] = new Queue<TInstance>(Mathf.Max(0, c.poolSize));
                if (!_activeOldest.ContainsKey(c.type))
                    _activeOldest[c.type] = new Queue<TInstance>(Mathf.Max(0, c.poolSize));
                Prewarm(c, c.type, c.poolSize);
            }
        }

        public TInstance Rent(TKey type) {
            if (!_typeToConfig.TryGetValue(type, out var config)) {
                Debug.LogError($"{name}: No pool config for {type}");
                return null;
            }
            if (!_available.ContainsKey(type)) _available[type] = new Queue<TInstance>();
            if (!_activeOldest.ContainsKey(type)) _activeOldest[type] = new Queue<TInstance>();
            
            if (_available.TryGetValue(type, out var queue) && queue.Count > 0) {
                var inst = _available[type].Dequeue();
                inst.MarkInUse();
                _activeOldest[type].Enqueue(inst);
                return inst;
            }
            return ProcessFull(config, type);
        }

        private TInstance ProcessFull(Config config, TKey type) {
            switch (config.poolPolicy) {
                case PoolFullPolicy.Wait:
                    return null;
                case PoolFullPolicy.Ring:
                    while (_activeOldest[type].Count > 0) {
                        var candidate = _activeOldest[type].Dequeue();
                        if (!candidate) continue;
                        if (!candidate.InUse) continue;
                        candidate.ForceRecycle();
                        candidate.MarkInUse();
                        _activeOldest[type].Enqueue(candidate);
                        return candidate;
                    }
                    return null;
                case PoolFullPolicy.Grow:
                    var inst = Instantiate(config.prefab, transform);
                    inst.gameObject.SetActive(false);
                    inst.Bind(ReturnToPool, config.type);
                    inst.MarkInUse();
                    _activeOldest[type].Enqueue(inst);
                    return inst;
                default:
                    return  null;
            }
        }

        private void Prewarm(Config c, TKey type, int count) {
            if (count <= 0) return;
            for (var i = 0; i < count; i++) {
                if (!c.prefab)
                    continue;
                var inst = Instantiate(c.prefab, transform);
                inst.gameObject.SetActive(false);
                inst.Bind(ReturnToPool, c.type);
                inst.MarkFree();
                _available[type].Enqueue(inst); 
            }
        }

        private void ReturnToPool(Component component, TKey type) {
            var inst = component as TInstance;
            if (!inst) return;

            inst.MarkFree();
            inst.transform.SetParent(transform, worldPositionStays: false);
            inst.gameObject.SetActive(false);

            if (!_available.ContainsKey(type))
                _available[type] = new Queue<TInstance>();

            _available[type].Enqueue(inst);
        }
    }
}