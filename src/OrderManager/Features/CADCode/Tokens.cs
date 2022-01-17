namespace OrderManager.ApplicationCore.Features.CADCode;

internal class Tokens {

    public abstract record Token(string Tool, int Sequence) {
        public abstract string[] Components();
    }

    public record Rectangle(double X, double Y, double StartZ, double EndZ, double Width, double Height, string Tool, int Sequence) : Token(Tool, Sequence) {
        public override string[] Components()  => throw new NotImplementedException();
    }

    public record Route(double StartX, double StartY, double StartZ, double EndX, double EndY, double EndZ, string Tool, int Sequence) : Token(Tool, Sequence) {
        public override string[] Components() => throw new NotImplementedException();
    }

}
