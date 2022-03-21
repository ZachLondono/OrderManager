using System;

namespace OrderManager.Shared;

public class QueryResult<T> : Either<T, Error> {
    public QueryResult(T left) : base(left) {}
    public QueryResult(Error right) : base(right) {}
}

public record Error(string Message, string DetailedMessage);

public class Either<TLeft, TRight> {

    private readonly TLeft _left;
    private readonly TRight _right;
    private readonly bool _isLeft;

    public Either(TLeft left) {
        _left = left;
        _right = default!;
        _isLeft = true;
    }

    public Either(TRight right) {
        _right = right;
        _left = default!;
        _isLeft = false;
    }

    public T Match<T>(Func<TLeft, T> left, Func<TRight, T> right) {
        return _isLeft ? left(_left) : right(_right);
    }

}
