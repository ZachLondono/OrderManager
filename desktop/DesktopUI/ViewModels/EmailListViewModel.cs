using DesktopUI.Common;
using DesktopUI.Views;
using OrderManager.ApplicationCore.Emails;
using OrderManager.Domain.Emails;
using ReactiveUI;
using System;
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

        DeleteEmailCommand = ReactiveCommand.CreateFromTask<EmailTemplateSummary, Unit>(OnDeleteEmail);
        EditEmailCommand = ReactiveCommand.CreateFromTask<EmailTemplateSummary, Unit>(OnEditEmail);
        CreateEmailCommand = ReactiveCommand.CreateFromTask(OnCreateEmail);
        
        ShowDialog = new Interaction<DialogWindowContent, Unit>();
        ShowFileDialogAndReturnPath = new Interaction<Unit, string?>();
    }

    public Interaction<DialogWindowContent, Unit> ShowDialog { get; }
    public Interaction<Unit, string?> ShowFileDialogAndReturnPath { get; }

    public ReactiveCommand<EmailTemplateSummary, Unit> DeleteEmailCommand { get; set; }
    public ReactiveCommand<EmailTemplateSummary, Unit> EditEmailCommand { get; set; }
    public ReactiveCommand<Unit, Unit> CreateEmailCommand { get; set; }

    public async Task LoadData() {
        Emails.Clear();
        var labels = await _query();
        foreach (var label in labels) {
            Emails.Add(label);
        }
    }

    private async Task<Unit> OnDeleteEmail(EmailTemplateSummary email) {
        await _repo.Remove(email.Id);
        Emails.Remove(email);
        return Unit.Default;
    }

    private async Task<Unit> OnEditEmail(EmailTemplateSummary email) {

        var query = App.GetRequiredService<EmailQuery.GetEmailDetailsById>();
        var details = await query(email.Id);

        var editorvm = App.GetRequiredService<EmailTemplateEditorViewModel>();
        editorvm.SetData(details);

        await ShowDialog.Handle(new("Email Editor", 800, 450, new EmailTemplateEditorView {
            DataContext = editorvm
        }));

        var index = Emails.IndexOf(email);
        Emails.RemoveAt(index);
        email.Name = details.Name;
        Emails.Insert(index, email);

        return Unit.Default;

    }

    private async Task OnCreateEmail() {
        var context = await _repo.Add("New Email Template");
        var query = App.GetRequiredService<EmailQuery.GetEmailDetailsById>();
        var details = await query(context.Id);

        var editorvm = App.GetRequiredService<EmailTemplateEditorViewModel>();
        editorvm.SetData(details);

        await ShowDialog.Handle(new("Email Editor", 800, 450, new EmailTemplateEditorView {
            DataContext = editorvm
        }));

        Emails.Add(new() {
            Id = details.Id,
            Name = details.Name,
        });
    }

}
