using System;
using Plugins.MVVM.Binders;
using TMPro;
using UniRx;

namespace _Project.Features.UI.Common.Binders
{
    public class TextBinder : IBinder, IObserver<string>
    {
        private readonly TMP_Text _view;
        private readonly IReadOnlyReactiveProperty<string> _property;
        private IDisposable _handle;


        public TextBinder(TMP_Text view, IReadOnlyReactiveProperty<string> property)
        {
            _view = view;
            _property = property;
        }
        
        public void Bind()
        {
            OnNext(_property.Value);
            _handle = _property.Subscribe(this);
        }

        public void Unbind()
        {
            _handle?.Dispose();
            _handle = null;
        }

        public void OnNext(string value)
        {
            _view.text = value;
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }
    }
}