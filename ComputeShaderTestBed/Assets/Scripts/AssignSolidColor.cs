using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ComputeShaderTestBed
{
    public class AssignSolidColor : MonoBehaviour
    {
        public ComputeShader _ComputeShader;
        public int _Resolution = 256;
        public string _KernelName;

        private RenderTexture _RendTex;
        private MeshRenderer _MeshRend;
        private int _KernelHandle;

        void Start()
        {
            _RendTex = new RenderTexture(_Resolution, _Resolution, 0);
            _RendTex.enableRandomWrite = true;
            _RendTex.Create();

            _MeshRend = this.GetComponent<MeshRenderer>();
            _MeshRend.enabled = true;

            InitShader();
        }

        void Update()
        {
            DispatchShader(8, 8);
        }

        void InitShader()
        {
            _KernelHandle = _ComputeShader.FindKernel(_KernelName);
            _ComputeShader.SetTexture(_KernelHandle, "Result", _RendTex);
            _ComputeShader.SetInt("Resolution", _Resolution);
            _MeshRend.material.SetTexture("_MainTex", _RendTex);
        }

        void DispatchShader(int x, int y)
        {
            _ComputeShader.Dispatch(_KernelHandle, _Resolution / x, _Resolution / y, 1);
        }
    }
}

