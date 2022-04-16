using OrderManager.Domain.Labels;

namespace OrderManager.ApplicationCore.Labels;

public static class LabelQuery {

    public delegate Task<LabelFieldMap?> GetLabelById(int Id);

}
