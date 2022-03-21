namespace Domain.Entities.ValueObjects;

public class Quantity {

    private int _quantity;

    public Quantity(int qty) {

        if (qty <= 0) {
            throw new ArgumentOutOfRangeException(nameof(qty));
        }

        _quantity = qty;

    }

    public int Value => _quantity;

    public static Quantity operator +(Quantity val) => val;
    public static Quantity operator -(Quantity val) => new(-val.Value);

    public static Quantity operator +(Quantity left, Quantity right) {
        return new(left.Value + right.Value);
    }

    public static Quantity operator -(Quantity left, Quantity right) {
        return new(left.Value - right.Value);
    }

    public static Quantity operator *(Quantity left, Quantity right) {
        return new(left.Value * right.Value);
    }

    public static Quantity operator /(Quantity numerator, Quantity denomonater) {
        if (denomonater.Value == 0)
            throw new DivideByZeroException();
        return new(numerator.Value / denomonater.Value);
    }

    public static bool operator ==(Quantity numerator, Quantity denomonater) {
        return numerator.Value == denomonater.Value;
    }

    public static bool operator !=(Quantity numerator, Quantity denomonater) {
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

        if (((Price)obj).Value == _quantity) return true;

        return false;
    }

    public override int GetHashCode() {
        return _quantity.GetHashCode();
    }

}
