namespace _Project.Core.Input
{
    public static class InputTypeSelector
    {
        public static InputType Select(bool useMobileInputInEditor)
        {
#if UNITY_EDITOR
            return useMobileInputInEditor ? InputType.Mobile : InputType.Standalone;
#elif UNITY_ANDROID || UNITY_IOS
            return InputType.Mobile;
#else
            return InputType.Standalone;
#endif
        }
    }
}