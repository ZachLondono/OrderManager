using OrderManager.ApplicationCore.Emails;
using OrderManager.Domain.Emails;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopUI.ViewModels;

public class EmailTemplateEditorViewModel : ViewModelBase {

    private EmailTemplateDetails? _email = null;
    public EmailTemplateDetails? Email {
        get => _email;
        set => this.RaiseAndSetIfChanged(ref _email, value);
    }

    public string EmailName {
        get => _email?.Name ?? "";
        set {
            if (_email is null) return;
            _email.Name = value;
            _nameChanged = true;
            CanSave = true;
        }
    } 
    
    public string EmailSender {
        get => _email?.Sender ?? "";
        set {
            if (_email is null) return;
            _email.Sender = value;
            _senderChanged = true;
            CanSave = true;
        }
    }
    
    public string SenderPassword {
        get => _email?.Password ?? "";
        set {
            if (_email is null) return;
            _email.Password = value;
            _passwordChanged = true;
            CanSave = true;
        }
    }

    public string EmailBody {
        get => _email?.Body ?? "";
        set {
            if (_email is null) return;
            _email.Body = value;
            _bodyChanged = true;
            CanSave = true;
        }
    }
    
    public string EmailSubject {
        get => _email?.Subject ?? "";
        set {
            if (_email is null) return;
            _email.Subject = value;
            _subjectChanged = true;
            CanSave = true;
        }
    }

    private string _emailTo = "";
    public string EmailTo {
        get {
            if (_email is not null)
                _emailTo = string.Join(',', _email?.To ?? new());
            return _emailTo;
        }
        set {
            if (_email is null) return;
            _email.To = new(_emailTo.Split(','));
        }
    }
    
    public string EmailCc => _email?.Cc?.ToString() ?? "";
    
    public string EmailBcc => _email?.Bcc?.ToString() ?? "";

    private string _messageText = string.Empty;
    public string MessageText {
        get => _messageText;
        set => this.RaiseAndSetIfChanged(ref _messageText, value);
    }

    private bool _canSave = false;
    public bool CanSave {
        get => _canSave;
        set {
            this.RaiseAndSetIfChanged(ref _canSave, value);
            MessageText = string.Empty;
        }
    }

    private bool _nameChanged = false;
    private bool _senderChanged = false;
    private bool _passwordChanged = false;
    private bool _bodyChanged = false;
    private bool _subjectChanged = false;
    private bool _toChanged = false;
    private bool _ccChanged = false;
    private bool _bccChanged = false;

    private readonly IEmailTemplateRepository _repo;

    public EmailTemplateEditorViewModel(IEmailTemplateRepository repo) {
        _repo = repo;

        var canSave = this.WhenAny(x => x.CanSave, x => x.Value);

        SaveChangesCommand = ReactiveCommand.CreateFromTask(Save, canExecute: canSave);
    }

    public void SetData(EmailTemplateDetails email) {
        _email = email;
    }

    public ICommand SaveChangesCommand { get; }

    public async Task Save() {

        if (_email is null) return;

        try { 
            var context = await _repo.GetById(_email.Id);

            if (_nameChanged) context.SetName(_email.Name);
            if (_subjectChanged) context.SetSubject(_email.Subject);
            if (_bodyChanged) context.SetBody(_email.Body);

            await _repo.Save(context);

            _nameChanged = false;
            _senderChanged = false;
            _passwordChanged = false;
            _bodyChanged = false;
            _subjectChanged = false;
            _toChanged = false;
            _ccChanged = false;
            _bccChanged = false;
            CanSave = false;

            MessageText = "Saved";

        } catch (Exception e) {
            Debug.WriteLine(e);
            MessageText = "Failed to Save";
        }

    }
}
