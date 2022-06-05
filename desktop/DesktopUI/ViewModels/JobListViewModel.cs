using DesktopUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopUI.ViewModels;

public class JobListViewModel : ViewModelBase {

    public ObservableCollection<Job> Jobs { get; } = new();

    public JobListViewModel() {

        Jobs.Add(
            new() {
                Number = "OT000",
                Name = "Kitchen ABC",
                Vendor = "OT",
                Customer = "ABC's Cabinets",
                Qty = 5,
                ScheduledDate = DateTime.Today,
                ProductClass = "Drawer Boxes",
                WorkCell = "Drawer Boxes"
            });

        Jobs.Add(
            new() {
                Number = "OT001",
                Name = "Laundry DEF",
                Vendor = "OT",
                Customer = "ABC's Cabinets",
                Qty = 15,
                ScheduledDate = DateTime.Now,
                ProductClass = "Drawer Boxes",
                WorkCell = "Drawer Boxes"
            });

        Jobs.Add(
            new() {
                Number = "OT002",
                Name = "Closet DEF",
                Vendor = "Hafele",
                Customer = "LOL Construction",
                Qty = 6,
                ScheduledDate = DateTime.Now,
                ProductClass = "Drawer Boxes",
                WorkCell = "Drawer Boxes"
            });

    }

}
