using DesktopUI.Common;
using DesktopUI.Views;
using OrderManager.ApplicationCore.Emails;
using OrderManager.Domain.Emails;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace DesktopUI.ViewModels;

public class EmailListViewModel : ViewModelBase {

    public ObservableCollection<EmailTemplateSummary> Emails { get; set; } = new();

    private readonly IEmailTemplateRepository _repo;
    private readonly EmailQuery.GetEmailSummaries _query;

    public EmailListViewModel(IEmailTemplateRepository repo, EmailQuery.GetEmailSummaries query) {
        _repo = repo;
        _query = query;

        DeleteEmailCommand = ReactiveCommand.CreateFromTask<EmailTemplateSummary>(OnDeleteEmail);
        EditEmailCommand = ReactiveCommand.CreateFromTask<EmailTemplateSummary>(OnEditEmail);
        CreateEmailCommand = ReactiveCommand.CreateFromTask(OnCreateEmail);

        ShowDialog = new Interaction<ToolWindowContent, Unit>();
        ShowFileDialogAndReturnPath = new Interaction<Unit, string?>();
    }

    public Interaction<ToolWindowContent, Unit> ShowDialog { get; }
    public Interaction<Unit, string?> ShowFileDialogAndReturnPath { get; }

    public ReactiveCommand<EmailTemplateSummary,Unit> DeleteEmailCommand { get; set; }
    public ReactiveCommand<EmailTemplateSummary,Unit> EditEmailCommand { get; set; }
    public ReactiveCommand<Unit, Unit> CreateEmailCommand { get; set; }

    public async Task LoadData() {
        Emails.Clear();
        var labels = await _query();
        foreach (var label in labels) {
            Emails.Add(label);
        }
    }

    private async Task OnDeleteEmail(EmailTemplateSummary email) {
        await _repo.Remove(email.Id);
        Emails.Remove(email);
    }

    private async Task OnEditEmail(EmailTemplateSummary email) {

        var details = await OpenEmailEditor(email.Id);
        if (details is null) return;

        var index = Emails.IndexOf(email);
        Emails.RemoveAt(index);
        email.Name = details.Name;
        Emails.Insert(index, email);

    }

    private async Task OnCreateEmail() {
        var context = await _repo.Add("New Email Template");

        var details = await OpenEmailEditor(context.Id);
        if (details is null) return;

        Emails.Add(new() {
            Id = details.Id,
            Name = details.Name,
        });
    }

    private async Task<EmailTemplateDetails?> OpenEmailEditor(int emailId) {

        var query = App.GetRequiredService<EmailQuery.GetEmailDetailsById>();
        EmailTemplateDetails? details = null;

        var editorvm = App.GetRequiredService<EmailTemplateEditorViewModel>();

        await ShowDialog.Handle(new("Email Editor", 800, 450, new EmailTemplateEditorView {
            DataContext = editorvm
        }, async () => {

            details = await query(emailId);
            editorvm.SetData(details);

        }));

        return details;

    }

}
