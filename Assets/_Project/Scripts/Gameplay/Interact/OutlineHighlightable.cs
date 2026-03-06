using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public class OutlineHighlightable : MonoBehaviour, IHighlightable {
        [SerializeField] private float highlightValue = 1.05f;
        [SerializeField] private float unhighlightValue = 1f;
        [SerializeField] private GameObject meshToHighlight;
        [SerializeField] [Min(0)] private int outlineMaterialIndex = 1;
        [SerializeField] private string outlineMaterialVarReferenceName = "_outline_scale";
        private Material outlineMaterial;


        private void Start() {
            outlineMaterial = meshToHighlight.GetComponent<MeshRenderer>().materials[outlineMaterialIndex];
            if (outlineMaterial) { 
                outlineMaterial.SetFloat(outlineMaterialVarReferenceName, unhighlightValue);
            }
        }

        public void SetHighlight(bool isOn) {
            if (isOn) {
                outlineMaterial.SetFloat(outlineMaterialVarReferenceName, highlightValue);
            }
            else {
                outlineMaterial.SetFloat(outlineMaterialVarReferenceName, unhighlightValue);
            }
        }
    }
}