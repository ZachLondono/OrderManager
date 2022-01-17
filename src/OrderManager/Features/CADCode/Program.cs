namespace OrderManager.ApplicationCore.Features.CADCode;

internal class Program {

    public string JobName { get; set; } = default!;

    public string ProgramName { get; set; } = default!;

    public int ProgramNum { get; set; } = default!;

    public string Material { get; set; } = default!;

    public IEnumerable<Tokens.Token> Tokens { get; set; } = default!;

}
