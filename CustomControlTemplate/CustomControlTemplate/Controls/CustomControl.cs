
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CustomControlTemplate.Controls
{
    public class CustomControl : ContentView
    {
        public static BindableProperty IsProcessingRequestProperty = BindableProperty.Create(propertyName: nameof(IsProcessingRequest), returnType: typeof(bool), declaringType: typeof(CustomControl), defaultValue: false);
        public static BindableProperty CallbackCommandProperty = BindableProperty.Create(nameof(CallbackCommand), typeof(ICommand), declaringType: typeof(CustomControl),  defaultValue: null);
        public static BindableProperty IsButtonPressedProperty = BindableProperty.Create(nameof(IsButtonPressed), typeof(bool), typeof(CustomControl), defaultValue: false);

        public bool IsProcessingRequest { get => (bool)GetValue(IsProcessingRequestProperty); set => SetValue(IsProcessingRequestProperty, value); }
        public ICommand CallbackCommand { get => (ICommand)GetValue(CallbackCommandProperty); set => SetValue(CallbackCommandProperty, value); }
        public bool IsButtonPressed { get => (bool)GetValue(IsButtonPressedProperty); set => SetValue(IsButtonPressedProperty, value); }

        private ICommand _command;
        public ICommand Command {
            get {
                return _command ?? (_command = new Command(async () =>
                {
                    // Make sure that we dont do anything if the button is still processing the request
                    if (IsProcessingRequest) {
                        return;
                    }

                    // Set the button pressed state
                    IsButtonPressed = true;
                    // Execute the button command
                    CallbackCommand?.Execute(null);
                    await Task.Delay(1000); // wait for a second
                    IsButtonPressed = false; // then set the button state back to normal
                }));
            } 
        }

    }
}