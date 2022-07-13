using DesktopUI.Models;
using OrderManager.Domain.Jobs;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DesktopUI.ViewModels;

public class JobListViewModel : ViewModelBase {

    private JobQuery? _activeQuery;
    public string? _activeSearch;

    public ObservableCollection<JobListItem> Jobs { get; } = new();

    private int _page = 1;
    public int Page {
        get => _page;
        set {
            this.RaiseAndSetIfChanged(ref _page, value);
            UpdateJobList();
        }
    }

    public JobListViewModel() {
        UpdateJobList();        
    }

    public void ApplySearch(string search) {
        Page = 1;
        _activeSearch = search;
        UpdateJobList();
    }

    public void ApplyFilter(JobQuery query) {
        Page = 1;
        _activeQuery = query;
        UpdateJobList();
    }

    public void UpdateJobList() {
        // var jobs = jobAPI.GetJobs(_page, _activeFilter, _activeSearch);

        Debug.WriteLine("Updating Order List");

        Jobs.Clear();

        for (int i = (Page - 1) * 10; i < Page * 10; i++) {

            Jobs.Add(
                new() {
                    Number = $"OT{i:000}",
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

}
