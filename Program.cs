
public readonly struct DualNumber<T>(T r, T d) where T: struct
{
    public readonly T real = r;
    public readonly T dual = d;

    public static DualNumber<T> operator +(DualNumber<T> x) => x;

    public static DualNumber<T> operator -(DualNumber<T> x) => new DualNumber<T>(-1 * (dynamic)x.real, -1 * (dynamic)x.dual);

    public static DualNumber<T> operator +(DualNumber<T> a, DualNumber<T> b)
    {
        return new DualNumber<T>((dynamic)a.real + b.real, (dynamic)a.dual + b.dual);
    }

    public static DualNumber<T> operator -(DualNumber<T> a, DualNumber<T> b)
    {
        return new DualNumber<T>((dynamic)a.real - b.real, (dynamic)a.dual - b.dual);
    }

    public static DualNumber<T> operator *(DualNumber<T> a, DualNumber<T> b)
    {
        return new DualNumber<T>((dynamic)a.real * b.real, ((dynamic)a.real * b.dual) + ((dynamic)a.dual * b.real));
    }

    public static DualNumber<T> operator /(DualNumber<T> a, DualNumber<T> b)
    {
        dynamic denominator = (dynamic) b.real * b.real;
        return new DualNumber<T>((dynamic)a.real * a.real / denominator, ((dynamic)a.dual * b.real) - (dynamic)a.real * b.dual /denominator);
    }

    public static implicit operator DualNumber<T>(T value)
    {
        return new DualNumber<T>(value, (dynamic)0);
    }

    public override string ToString()
    {
        return real.ToString() + " + " + dual.ToString() + "ε";
    }
}

public class Program
{
    public static void Main()
    {
        // Create a function and find the derivate at a certain point
        //
        // static T f<T>(T x) where T: struct
        // {
        //     return (dynamic)4 * x * x * x + 1 / ((dynamic)x * x);
        // }

        // FindDerivativeAt(f, 1.0d);
    }

    public static void FindDerivativeAt<T>(Func<DualNumber<T>, DualNumber<T>> f, T x) where T : struct
    {
        var df = DifferentiateAt(f, x);
        Console.WriteLine("The derivative of the function at " + x.ToString() + " is " + df.ToString());
    }

    private static T DifferentiateAt<T>(Func<DualNumber<T>, DualNumber<T>> f, T x) where T : struct
    {   
        return f(new DualNumber<T>(x, (dynamic)1)).dual;
    }
}