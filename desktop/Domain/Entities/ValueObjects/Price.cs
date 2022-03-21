namespace Domain.Entities.ValueObjects;

public class Price {

    private decimal _price;

    public Price(decimal price) {
        _price = Math.Round(price, 2);
    }

    public decimal Value => _price;

    public static Price operator +(Price val) => val;
    public static Price operator -(Price val) => new(-val.Value);

    public static Price operator +(Price left, Price right) {
        return new(left.Value + right.Value);
    }

    public static Price operator -(Price left, Price right) {
        return new(left.Value - right.Value);
    }
    
    public static Price operator *(Price left, Price right) {
        return new(left.Value * right.Value);
    }

    public static Price operator /(Price numerator, Price denomonater) {
        if (denomonater.Value == 0)
            throw new DivideByZeroException();
        return new(numerator.Value / denomonater.Value);
    }

    public static bool operator ==(Price numerator, Price denomonater) {
        return numerator.Value == denomonater.Value;
    }

    public static bool operator !=(Price numerator, Price denomonater) {
        return !(numerator == denomonater);
    }

    public override bool Equals(object? obj) {
        if (ReferenceEquals(this, obj)) {
            return true;
        }

        if (ReferenceEquals(obj, null)) {
            return false;
        }

        if (obj is not Price || obj is null) return false;

        if (((Price) obj).Value == _price) return true;

        return false;
    }

    public override int GetHashCode() {
        return _price.GetHashCode();
    }
}
