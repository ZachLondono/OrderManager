using OrderManager.ApplicationCore.Emails;
using OrderManager.Domain.Emails;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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

    public ObservableCollection<EmailAddress> EmailTo { get; init; } = new();
    public ObservableCollection<EmailAddress> EmailCc { get; } = new();
    public ObservableCollection<EmailAddress> EmailBcc { get; } = new();

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
    
    private readonly IEmailTemplateRepository _repo;

    public EmailTemplateEditorViewModel(IEmailTemplateRepository repo) {
        _repo = repo;

        var canSave = this.WhenAny(x => x.CanSave, x => x.Value);

        SaveChangesCommand = ReactiveCommand.CreateFromTask(Save, canExecute: canSave);
        
        AddTo = ReactiveCommand.Create(AddToEmail);
        AddCc = ReactiveCommand.Create(AddCcEmail);
        AddBcc = ReactiveCommand.Create(AddBccEmail);

    }

    public void SetData(EmailTemplateDetails email) {
        _email = email;
        EmailTo.Clear();
        foreach (var to in _email.To) EmailTo.Add(new(to));
        foreach (var cc in _email.Cc) EmailCc.Add(new(cc));
        foreach (var bcc in _email.Bcc) EmailBcc.Add(new(bcc));
    }

    public ICommand SaveChangesCommand { get; }
    public ICommand AddTo { get; }
    public ICommand AddCc { get; }
    public ICommand AddBcc { get; }

    public async Task Save() {

        if (_email is null) return;

        try { 
            var context = await _repo.GetById(_email.Id);

            if (_nameChanged) context.SetName(_email.Name);
            if (_subjectChanged) context.SetSubject(_email.Subject);
            if (_bodyChanged) context.SetBody(_email.Body);
            if (_senderChanged) context.SetSender(_email.Sender);
            if (_passwordChanged) context.SetPassword(_email.Password);


            foreach (var address in EmailTo) { 
                if (address.HasChanged) {
                    if (!string.IsNullOrEmpty(address.PreviousValue))
                        context.RemoveTo(address.PreviousValue);
                    context.AddTo(address.Value);
                    address.Reset();
                }
            }

            foreach (var address in EmailCc) {
                if (address.HasChanged) {
                    if (!string.IsNullOrEmpty(address.PreviousValue))
                        context.RemoveCc(address.PreviousValue);
                    context.AddCc(address.Value);
                    address.Reset();
                }
            }

            foreach (var address in EmailBcc) {
                if (address.HasChanged) {
                    if (!string.IsNullOrEmpty(address.PreviousValue))
                        context.RemoveBcc(address.PreviousValue);
                    context.AddBcc(address.Value);
                    address.Reset();
                }
            }

            await _repo.Save(context);

            _nameChanged = false;
            _senderChanged = false;
            _passwordChanged = false;
            _bodyChanged = false;
            _subjectChanged = false;
            CanSave = false;

            MessageText = "Saved";

        } catch (Exception e) {
            Debug.WriteLine(e);
            MessageText = "Failed to Save";
        }

    }

    public void AddToEmail() {
        EmailTo.Add(new(""));
    }
    
    public void AddCcEmail() {
        EmailCc.Add(new(""));
    }
    
    public void AddBccEmail() {
        EmailBcc.Add(new(""));
    }

    public class EmailAddress {

        public bool HasChanged { get; private set; }

        public string? PreviousValue {
            get;
            private set;
        } = string.Empty;

        private string _value;
        public string Value {
            get => _value;
            set {
                _value = value;
                HasChanged = true;
            }
        }

        public EmailAddress(string initialValue) {
            _value = initialValue;
            PreviousValue = initialValue;
        }

        public void Reset() {
            HasChanged = false;
            PreviousValue = Value;
        }

    }

}
