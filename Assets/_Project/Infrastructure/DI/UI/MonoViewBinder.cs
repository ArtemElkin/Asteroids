# if UNITY_EDITOR
using UnityEditor;
#endif

using System;
using Plugins.MVVM;
using Plugins.MVVM.Binders;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace _Project.Infrastructure.DI.UI
{
    public sealed class MonoViewBinder : MonoBehaviour
    {
        [SerializeField] private Object view;

        [Space(8)]
# if UNITY_EDITOR
        [SerializeField] private MonoScript viewModelType;
# endif
        [SerializeField] private string viewModelTypeName;
        
        [Inject]
        private DiContainer diContainer;

        private IBinder _binder;

        private void Awake()
        {
            _binder = this.CreateBinder();
        }

        private void OnEnable()
        {
            _binder.Bind();
        }

        private void OnDisable()
        {
            _binder.Unbind();
        }

        private IBinder CreateBinder()
        {
            object model = this.diContainer.Resolve(Type.GetType(viewModelTypeName));

            return BinderFactory.CreateComposite(this.view, model);
        }
# if UNITY_EDITOR
        private void OnValidate()
        {
            if (viewModelType != null)
                viewModelTypeName = viewModelType.GetClass().AssemblyQualifiedName;
        }
# endif
    }
}