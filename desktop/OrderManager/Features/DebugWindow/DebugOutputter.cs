using Avalonia.Controls;
using Avalonia.Threading;
using System.IO;
using System.Text;

namespace OrderManager.Features.DebugWindow;

internal class DebugOutputter : TextWriter {
    
    public override Encoding Encoding => System.Text.Encoding.UTF8;

    private readonly int MAX_LEN = 1000;

    private readonly TextBox textBox;

    public DebugOutputter(TextBox output) {
        textBox = output;
    }

    public override void Write(char value) {
        base.Write(value);

        // Update to ui must be done in the UI thread
        Dispatcher.UIThread.Post(delegate {
            textBox.Text += value.ToString();

            if (textBox.Text.Length > MAX_LEN) {
                textBox.Text.Substring(textBox.Text.Length - MAX_LEN);
            }
        });
    }

}
