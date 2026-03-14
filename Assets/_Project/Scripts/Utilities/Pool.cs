using System.Collections.Generic;
using _Project.Scripts.Gameplay.Enums;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public class Pool<TInstance> : MonoBehaviour where TInstance : Component, IPoolable {
        [SerializeField] private TInstance prefab;
        [SerializeField] private int poolSize = 16;
        [SerializeField] private PoolFullPolicy fullPolicy = PoolFullPolicy.Ring;
        
        private readonly Queue<TInstance> _available = new();
        private readonly Queue<TInstance> _activeOldest = new();
        private void Awake() {
            Prewarm(poolSize);
        }

        private void Prewarm(int size) {
            for (int i = 0; i < size; i++) {
                var inst = Instantiate(prefab, transform);
                inst.gameObject.SetActive(false);
                inst.Bind(ReturnToPool);
                inst.MarkFree();
                _available.Enqueue(inst);
            }
        }
        public TInstance Rent() {
            if (_available.Count > 0) {
                var inst = _available.Dequeue();
                inst.MarkInUse();
                _activeOldest.Enqueue(inst);
                return inst;
            }
            return ProcessFull();
        }
                
        private TInstance ProcessFull()
        {
            switch (fullPolicy)
            {
                case PoolFullPolicy.Wait:
                    break;
                case PoolFullPolicy.Ring:
                    while (_activeOldest.Count > 0)
                    {
                        var candidate = _activeOldest.Dequeue();

                        if (!candidate || !candidate.InUse)
                            continue;

                        candidate.ForceRecycle();
                        candidate.MarkInUse();
                        _activeOldest.Enqueue(candidate);

                        return candidate;
                    }
                    break;
                case PoolFullPolicy.Grow:
                    var inst = Instantiate(prefab, transform);
                    inst.Bind(ReturnToPool);
                    inst.MarkInUse();
                    _activeOldest.Enqueue(inst);
                    return inst;
            }

            return null;
        }

        private void ReturnToPool(Component obj) {
            var inst = obj as TInstance;
            if (!inst) return;
            inst.MarkFree();
            inst.transform.SetParent(transform, false);
            inst.gameObject.SetActive(false);
            _available.Enqueue(inst);
        }
    }
}