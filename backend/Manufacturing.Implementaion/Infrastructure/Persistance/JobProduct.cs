namespace Manufacturing.Implementation.Infrastructure.Persistance;

internal class JobProduct {

    public int Id { get; set; }

    public int JobId { get; set; }

    public int ProductId { get; set; }

    public int QtyOrdered { get; set; }

}