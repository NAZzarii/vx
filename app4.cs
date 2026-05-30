using System;

class Square
{
    public double A { get; set; }

    public Square() : this(1) { }
    public Square(double a) => A = a;

    public override string ToString() => $"Квадрат зі стороною {A}";

    public static Square operator ++(Square s) { s.A++; return s; }
    public static Square operator --(Square s) { s.A = Math.Max(0, s.A - 1); return s; }

    public static Square operator +(Square s, double n) => new Square(s.A + n);
    public static Square operator -(Square s, double n) => new Square(Math.Max(0, s.A - n));
    public static Square operator *(Square s, double n) => new Square(s.A * n);
    public static Square operator /(Square s, double n) => new Square(n != 0 ? s.A / n : 0);

    public static bool operator ==(Square s1, Square s2) => s1.A == s2.A;
    public static bool operator !=(Square s1, Square s2) => s1.A != s2.A;
    public static bool operator >(Square s1, Square s2) => s1.A > s2.A;
    public static bool operator <(Square s1, Square s2) => s1.A < s2.A;
    public static bool operator >=(Square s1, Square s2) => s1.A >= s2.A;
    public static bool operator <=(Square s1, Square s2) => s1.A <= s2.A;

    public override bool Equals(object obj) => obj is Square s && s.A == A;
    public override int GetHashCode() => A.GetHashCode();

    public static bool operator true(Square s) => s.A != 0;
    public static bool operator false(Square s) => s.A == 0;

    public static implicit operator Rectangle(Square s) => new Rectangle(s.A, s.A);
    public static implicit operator int(Square s) => (int)s.A;
}

class Rectangle
{
    public double A { get; set; }
    public double B { get; set; }

    public Rectangle() : this(1, 1) { }
    public Rectangle(double a, double b) { A = a; B = b; }

    public override string ToString() => $"Прямокутник {A}x{B}";

    public static Rectangle operator ++(Rectangle r) { r.A++; r.B++; return r; }
    public static Rectangle operator --(Rectangle r) { r.A = Math.Max(0, r.A - 1); r.B = Math.Max(0, r.B - 1); return r; }

    public static Rectangle operator +(Rectangle r, double n) => new Rectangle(r.A + n, r.B + n);
    public static Rectangle operator -(Rectangle r, double n) => new Rectangle(Math.Max(0, r.A - n), Math.Max(0, r.B - n));
    public static Rectangle operator *(Rectangle r, double n) => new Rectangle(r.A * n, r.B * n);
    public static Rectangle operator /(Rectangle r, double n) => new Rectangle(n != 0 ? r.A / n : 0, n != 0 ? r.B / n : 0);

    public static bool operator ==(Rectangle r1, Rectangle r2) => r1.A == r2.A && r1.B == r2.B;
    public static bool operator !=(Rectangle r1, Rectangle r2) => !(r1 == r2);
    public static bool operator >(Rectangle r1, Rectangle r2) => (r1.A * r1.B) > (r2.A * r2.B);
    public static bool operator <(Rectangle r1, Rectangle r2) => (r1.A * r1.B) < (r2.A * r2.B);
    public static bool operator >=(Rectangle r1, Rectangle r2) => (r1.A * r1.B) >= (r2.A * r2.B);
    public static bool operator <=(Rectangle r1, Rectangle r2) => (r1.A * r1.B) <= (r2.A * r2.B);

    public override bool Equals(object obj) => obj is Rectangle r && r.A == A && r.B == B;
    public override int GetHashCode() => HashCode.Combine(A, B);

    public static bool operator true(Rectangle r) => r.A != 0 && r.B != 0;
    public static bool operator false(Rectangle r) => r.A == 0 || r.B == 0;

    public static explicit operator Square(Rectangle r) => new Square((r.A + r.B) / 2);
    public static explicit operator int(Rectangle r) => (int)(r.A * r.B);
}
