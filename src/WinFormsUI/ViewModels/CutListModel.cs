using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.WinFormsUI.ViewModels;

public class CutListModel {

    public string Company { get; set; } = string.Empty;
    public string Vendor { get; set; } = string.Empty;
    public string OrderNum { get; set; } = string.Empty;
    public string JobName { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
    public string OrderDate { get; set; } = string.Empty;
    public int BoxCount { get; set; } = default;
    public string OrderNotch { get; set; } = string.Empty;
    public string OrderClips { get; set; } = string.Empty;
    public string OrderPostFinish { get; set; } = string.Empty;
    public string OrderMountingHoles { get; set; } = string.Empty;
    public IEnumerable<CutListItem> Items { get; set; } = Enumerable.Empty<CutListItem>();

    public class CutListItem {
        public string CabNum { get; set; } = string.Empty;
        public string PartName { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public int Qty { get; set; } = default;
        public double Width { get; set; } = default;
        public double Length { get; set; } = default;
        public string Material { get; set; } = string.Empty;
        public int LineNum { get; set; } = default;
        public string ItemSize { get; set; } = string.Empty;
    }
}