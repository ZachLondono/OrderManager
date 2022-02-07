using Microsoft.Extensions.DependencyInjection;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Features.Priorities;
using System.Diagnostics;
using System.Linq;

namespace OrderManager.WinFormsUI;

public partial class PriorityLevelsForm : Form {

    private readonly PriorityController? _controller;

    public PriorityLevelsForm() {
        if (Program.ServiceProvider is null) throw new ArgumentNullException(nameof(Program.ServiceProvider)); ; 
        _controller = Program.ServiceProvider.GetService<PriorityController>();
        if (_controller is null) throw new ArgumentNullException(nameof(_controller));

        InitializeComponent();


        PriorityLevelsBox.AllowDrop = true;
        PriorityLevelsBox.MouseDown += PriorityLevelsBox_MouseDown;
        PriorityLevelsBox.DragOver += PriorityLevelsBox_DragOver;
        PriorityLevelsBox.DragDrop += PriorityLevelsBox_DragDrop;

        this.FormClosed += DialogClosing;

        var task = _controller.GetAllPriorities();
        task.ContinueWith(t => {
            
            // Priorities need to be sorted in ascending order to match their level values
            var priorities = t.Result.OrderBy(p => p.Level);

            // Add the priorities to the listbox
            foreach (var priority in priorities)
                PriorityLevelsBox.Items.Add(new PriorityView(priority));
        });

    }

    private async void CreatePriorityBtn_Click(object sender, EventArgs e) {

        string newName = NewPriorityName.Text;

        if (_controller is null) return;

        // Create new priority
        Priority newPriority = await _controller.CreatePriority(newName, 0);

        // Add to the list of priorites
        PriorityLevelsBox.Items.Insert(0, newPriority);

    }

    private async Task UpdatePriorities() {
        // TODO: switch to a batch update command to that there aren't so many sql commands

        if (_controller is null) return;

        var items = PriorityLevelsBox.Items;

        int index = 0;
        foreach (var item in items) {
            Priority priority = ((PriorityView)item).Priority;
            if (priority is null) continue;

            priority.Level = index;
            try {
                await _controller.UpdatePriority(priority);
            } catch (Exception e) {
                Debug.WriteLine("Could not update priority " + e);
            }

            index++;
        }

    }
    private async void DialogClosing(object? sender, EventArgs? e) {
        await UpdatePriorities();
    }

    private void PriorityLevelsBox_MouseDown(object? sender, MouseEventArgs? e) {
        if (PriorityLevelsBox.SelectedItem == null) return;
        PriorityLevelsBox.DoDragDrop(PriorityLevelsBox.SelectedItem, DragDropEffects.Move);
    }

    private void PriorityLevelsBox_DragOver(object? sender, DragEventArgs? e) {
        if (e is null || e.Data is null) return;
        if (!e.Data.GetDataPresent(typeof(PriorityView))) return;
        e.Effect = DragDropEffects.Move;
    }

    private void PriorityLevelsBox_DragDrop(object? sender, DragEventArgs? e) {
        if (e is null || e.Data is null) return;
        // Get the point at which the item is being dropped
        Point point = PriorityLevelsBox.PointToClient(new Point(e.X, e.Y));

        // Get the index of the point
        int index = PriorityLevelsBox.IndexFromPoint(point);
        if (index < 0) index = PriorityLevelsBox.Items.Count - 1;

        object data = e.Data.GetData(typeof(PriorityView));
        
        // Replace the dropped data into that index
        PriorityLevelsBox.Items.Remove(data);
        PriorityLevelsBox.Items.Insert(index, data);
    }

    public record PriorityView(Priority Priority) {
        public override string ToString() => Priority.PriorityName;
    }

}
