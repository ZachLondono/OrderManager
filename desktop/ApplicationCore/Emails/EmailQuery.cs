using OrderManager.Domain.Emails;

namespace OrderManager.ApplicationCore.Emails;

public class EmailQuery {

    public delegate Task<EmailTemplate?> GetEmailById(int id);

}
