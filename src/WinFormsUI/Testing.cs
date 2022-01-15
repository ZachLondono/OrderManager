using Microsoft.Extensions.DependencyInjection;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Features.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderManager.WinFormsUI;
public partial class Testing : Form {
    
    public Testing() {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e) {

        ReportController? reportController = Program.ServiceProvider?.GetService<ReportController>();

        if (reportController is null) return;

        Report report = new() {
            Name = "Invoice",
            Template = ""
        };

        reportController.GenerateReport(report, order, "");

    }
}
