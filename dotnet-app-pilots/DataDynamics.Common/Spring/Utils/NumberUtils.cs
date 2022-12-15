using System;
using System.ComponentModel;

namespace DataDynamics.Common.Spring.Utils;

/// <summary>
///     Various utility methods relating to numbers.
/// </summary>
/// <remarks>
///     <p>
///         Mainly for internal use within the framework.
///     </p>
/// </remarks>
/// <author>Aleksandar Seovic</author>
public sealed class NumberUtils
{
    #region Constructor (s) / Destructor

    // CLOVER:OFF

    /// <summary>
    ///     Creates a new instance of the <see cref="Spring.Util.NumberUtils" /> class.
    /// </summary>
    /// <remarks>
    ///     <p>
    ///         This is a utility class, and as such exposes no public constructors.
    ///     </p>
    /// </remarks>
    private NumberUtils()
    {
    }

    // CLOVER:ON

    #endregion

    /// <summary>
    ///     Determines whether the supplied <paramref name="number" /> is an integer.
    /// </summary>
    /// <param name="number">The object to check.</param>
    /// <returns>
    ///     <see lang="true" /> if the supplied <paramref name="number" /> is an integer.
    /// </returns>
    public static bool IsInteger(object number)
    {
        return number is int || number is short || number is long || number is uint
               || number is ushort || number is ulong || number is byte || number is sbyte;
    }

    /// <summary>
    ///     Determines whether the supplied <paramref name="number" /> is a decimal number.
    /// </summary>
    /// <param name="number">The object to check.</param>
    /// <returns>
    ///     <see lang="true" /> if the supplied <paramref name="number" /> is a decimal number.
    /// </returns>
    public static bool IsDecimal(object number)
    {
        return number is float || number is double || number is decimal;
    }

    /// <summary>
    ///     Determines whether the supplied <paramref name="number" /> is of numeric type.
    /// </summary>
    /// <param name="number">The object to check.</param>
    /// <returns>
    ///     <c>true</c> if the specified object is of numeric type; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsNumber(object number)
    {
        return IsInteger(number) || IsDecimal(number);
    }

    /// <summary>
    ///     Determines whether the supplied <paramref name="number" /> can be converted to an integer.
    /// </summary>
    /// <param name="number">The object to check.</param>
    /// <returns>
    ///     <see lang="true" /> if the supplied <paramref name="number" /> can be converted to an integer.
    /// </returns>
    public static bool CanConvertToInteger(object number)
    {
        var converter = TypeDescriptor.GetConverter(number);
        return converter.CanConvertTo(typeof(int))
               || converter.CanConvertTo(typeof(short))
               || converter.CanConvertTo(typeof(long))
               || converter.CanConvertTo(typeof(ushort))
               || converter.CanConvertTo(typeof(ulong))
               || converter.CanConvertTo(typeof(byte))
               || converter.CanConvertTo(typeof(sbyte));
    }

    /// <summary>
    ///     Determines whether the supplied <paramref name="number" /> can be converted to an integer.
    /// </summary>
    /// <param name="number">The object to check.</param>
    /// <returns>
    ///     <see lang="true" /> if the supplied <paramref name="number" /> can be converted to an integer.
    /// </returns>
    public static bool CanConvertToDecimal(object number)
    {
        var converter = TypeDescriptor.GetConverter(number);
        return converter.CanConvertTo(typeof(float))
               || converter.CanConvertTo(typeof(double))
               || converter.CanConvertTo(typeof(decimal));
    }

    /// <summary>
    ///     Determines whether the supplied <paramref name="number" /> can be converted to a number.
    /// </summary>
    /// <param name="number">The object to check.</param>
    /// <returns>
    ///     <c>true</c> if the specified object is decimal number; otherwise, <c>false</c>.
    /// </returns>
    public static bool CanConvertToNumber(object number)
    {
        return CanConvertToInteger(number) || CanConvertToDecimal(number);
    }

    /// <summary>
    ///     Is the supplied <paramref name="number" /> equal to zero (0)?
    /// </summary>
    /// <param name="number">The number to check.</param>
    /// <returns>
    ///     <see lang="true" /> id the supplied <paramref name="number" /> is equal to zero (0).
    /// </returns>
    public static bool IsZero(object number)
    {
        if (number is int)
            return (int)number == 0;
        if (number is short)
            return (short)number == 0;
        if (number is long)
            return (long)number == 0;
        if (number is ushort)
            return (ushort)number == 0;
        if (number is uint)
            return (uint)number == 0;
        if (number is ulong)
            return Convert.ToDecimal(number) == 0;
        if (number is byte)
            return (byte)number == 0;
        if (number is sbyte)
            return (sbyte)number == 0;
        if (number is float)
            return (float)number == 0f;
        if (number is double)
            return (double)number == 0d;
        if (number is decimal)
            return (decimal)number == 0m;
        return false;
    }

    /// <summary>
    ///     Negates the supplied <paramref name="number" />.
    /// </summary>
    /// <param name="number">The number to negate.</param>
    /// <returns>The supplied <paramref name="number" /> negated.</returns>
    /// <exception cref="System.ArgumentException">
    ///     If the supplied <paramref name="number" /> is not a supported numeric type.
    /// </exception>
    public static object Negate(object number)
    {
        if (number is int)
            return -(int)number;
        if (number is short)
            return -(short)number;
        if (number is long)
            return -(long)number;
        if (number is ushort)
            return -(int)number;
        if (number is uint)
            return -(long)number;
        if (number is ulong)
            return -Convert.ToDecimal(number);
        if (number is byte)
            return -(short)number;
        if (number is sbyte)
            return -(short)number;
        if (number is float)
            return -(float)number;
        if (number is double)
            return -(double)number;
        if (number is decimal)
            return -(decimal)number;
        throw new ArgumentException(string.Format("'{0}' is not one of the supported numeric types.", number));
    }

    /// <summary>
    ///     Returns the bitwise not (~) of the supplied <paramref name="number" />.
    /// </summary>
    /// <param name="number">The number.</param>
    /// <returns>The value of ~<paramref name="number" />.</returns>
    /// <exception cref="System.ArgumentException">
    ///     If the supplied <paramref name="number" /> is not a supported numeric type.
    /// </exception>
    public static object BitwiseNot(object number)
    {
        if (number is bool)
            return !(bool)number;
        if (number is int)
            return ~(int)number;
        if (number is short)
            return ~(short)number;
        if (number is long)
            return ~(long)number;
        if (number is ushort)
            return ~(ushort)number;
        if (number is uint)
            return ~(uint)number;
        if (number is ulong)
            return ~(ulong)number;
        if (number is byte)
            return ~(byte)number;
        if (number is sbyte)
            return ~(sbyte)number;
        throw new ArgumentException(string.Format("'{0}' is not one of the supported integer types.", number));
    }

    /// <summary>
    ///     Bitwise ANDs (&amp;) the specified integral values.
    /// </summary>
    /// <param name="m">The first number.</param>
    /// <param name="n">The second number.</param>
    /// <exception cref="System.ArgumentException">
    ///     If one of the supplied arguments is not a supported integral types.
    /// </exception>
    public static object BitwiseAnd(object m, object n)
    {
        CoerceTypes(ref m, ref n);

        if (n is bool)
            return (bool)m & (bool)n;
        if (n is int)
            return (int)m & (int)n;
        if (n is short)
            return (short)m & (short)n;
        if (n is long)
            return (long)m & (long)n;
        if (n is ushort)
            return (ushort)m & (ushort)n;
        if (n is uint)
            return (uint)m & (uint)n;
        if (n is ulong)
            return (ulong)m & (ulong)n;
        if (n is byte)
            return (byte)m & (byte)n;
        if (n is sbyte)
            return (sbyte)m & (sbyte)n;
        throw new ArgumentException(
            string.Format("'{0}' and/or '{1}' are not one of the supported integral types.", m, n));
    }

    /// <summary>
    ///     Bitwise ORs (|) the specified integral values.
    /// </summary>
    /// <param name="m">The first number.</param>
    /// <param name="n">The second number.</param>
    /// <exception cref="System.ArgumentException">
    ///     If one of the supplied arguments is not a supported integral types.
    /// </exception>
    public static object BitwiseOr(object m, object n)
    {
        CoerceTypes(ref m, ref n);

        if (n is bool) return (bool)m | (bool)n;

        if (n is int) return (int)m | (int)n;

        if (n is short) return (short)m | (short)n;

        if (n is long) return (long)m | (long)n;

        if (n is ushort) return (ushort)m | (ushort)n;

        if (n is uint) return (uint)m | (uint)n;

        if (n is ulong) return (ulong)m | (ulong)n;

        if (n is byte) return (byte)m | (byte)n;

        if (n is sbyte)
        {
            if (SystemUtils.MonoRuntime)
            {
                var x = (sbyte)n;
                var y = (sbyte)m;
                var result = x | y;
                return sbyte.Parse(result.ToString());
            }

            return (sbyte)((sbyte)m | (sbyte)n);
        }

        throw new ArgumentException(string.Format("'{0}' and/or '{1}' are not one of the supported integral types.",
            m, n));
    }

    /// <summary>
    ///     Bitwise XORs (^) the specified integral values.
    /// </summary>
    /// <param name="m">The first number.</param>
    /// <param name="n">The second number.</param>
    /// <exception cref="System.ArgumentException">
    ///     If one of the supplied arguments is not a supported integral types.
    /// </exception>
    public static object BitwiseXor(object m, object n)
    {
        CoerceTypes(ref m, ref n);

        if (n is bool)
            return (bool)m ^ (bool)n;
        if (n is int)
            return (int)m ^ (int)n;
        if (n is short)
            return (short)m ^ (short)n;
        if (n is long)
            return (long)m ^ (long)n;
        if (n is ushort)
            return (ushort)m ^ (ushort)n;
        if (n is uint)
            return (uint)m ^ (uint)n;
        if (n is ulong)
            return (ulong)m ^ (ulong)n;
        if (n is byte)
            return (byte)m ^ (byte)n;
        if (n is sbyte)
            return (sbyte)m ^ (sbyte)n;
        throw new ArgumentException(
            string.Format("'{0}' and/or '{1}' are not one of the supported integral types.", m, n));
    }

    /// <summary>
    ///     Adds the specified numbers.
    /// </summary>
    /// <param name="m">The first number.</param>
    /// <param name="n">The second number.</param>
    public static object Add(object m, object n)
    {
        CoerceTypes(ref m, ref n);

        if (n is int)
            return (int)m + (int)n;
        if (n is short)
            return (short)m + (short)n;
        if (n is long)
            return (long)m + (long)n;
        if (n is ushort)
            return (ushort)m + (ushort)n;
        if (n is uint)
            return (uint)m + (uint)n;
        if (n is ulong)
            return (ulong)m + (ulong)n;
        if (n is byte)
            return (byte)m + (byte)n;
        if (n is sbyte)
            return (sbyte)m + (sbyte)n;
        if (n is float)
            return (float)m + (float)n;
        if (n is double)
            return (double)m + (double)n;
        if (n is decimal)
            return (decimal)m + (decimal)n;
        throw new ArgumentException(
            string.Format("'{0}' and/or '{1}' are not one of the supported numeric types.", m, n));
    }

    /// <summary>
    ///     Subtracts the specified numbers.
    /// </summary>
    /// <param name="m">The first number.</param>
    /// <param name="n">The second number.</param>
    public static object Subtract(object m, object n)
    {
        CoerceTypes(ref m, ref n);

        if (n is int)
            return (int)m - (int)n;
        if (n is short)
            return (short)m - (short)n;
        if (n is long)
            return (long)m - (long)n;
        if (n is ushort)
            return (ushort)m - (ushort)n;
        if (n is uint)
            return (uint)m - (uint)n;
        if (n is ulong)
            return (ulong)m - (ulong)n;
        if (n is byte)
            return (byte)m - (byte)n;
        if (n is sbyte)
            return (sbyte)m - (sbyte)n;
        if (n is float)
            return (float)m - (float)n;
        if (n is double)
            return (double)m - (double)n;
        if (n is decimal)
            return (decimal)m - (decimal)n;
        throw new ArgumentException(
            string.Format("'{0}' and/or '{1}' are not one of the supported numeric types.", m, n));
    }

    /// <summary>
    ///     Multiplies the specified numbers.
    /// </summary>
    /// <param name="m">The first number.</param>
    /// <param name="n">The second number.</param>
    public static object Multiply(object m, object n)
    {
        CoerceTypes(ref m, ref n);

        if (n is int)
            return (int)m * (int)n;
        if (n is short)
            return (short)m * (short)n;
        if (n is long)
            return (long)m * (long)n;
        if (n is ushort)
            return (ushort)m * (ushort)n;
        if (n is uint)
            return (uint)m * (uint)n;
        if (n is ulong)
            return (ulong)m * (ulong)n;
        if (n is byte)
            return (byte)m * (byte)n;
        if (n is sbyte)
            return (sbyte)m * (sbyte)n;
        if (n is float)
            return (float)m * (float)n;
        if (n is double)
            return (double)m * (double)n;
        if (n is decimal)
            return (decimal)m * (decimal)n;
        throw new ArgumentException(
            string.Format("'{0}' and/or '{1}' are not one of the supported numeric types.", m, n));
    }

    /// <summary>
    ///     Divides the specified numbers.
    /// </summary>
    /// <param name="m">The first number.</param>
    /// <param name="n">The second number.</param>
    public static object Divide(object m, object n)
    {
        CoerceTypes(ref m, ref n);

        if (n is int)
            return (int)m / (int)n;
        if (n is short)
            return (short)m / (short)n;
        if (n is long)
            return (long)m / (long)n;
        if (n is ushort)
            return (ushort)m / (ushort)n;
        if (n is uint)
            return (uint)m / (uint)n;
        if (n is ulong)
            return (ulong)m / (ulong)n;
        if (n is byte)
            return (byte)m / (byte)n;
        if (n is sbyte)
            return (sbyte)m / (sbyte)n;
        if (n is float)
            return (float)m / (float)n;
        if (n is double)
            return (double)m / (double)n;
        if (n is decimal)
            return (decimal)m / (decimal)n;
        throw new ArgumentException(
            string.Format("'{0}' and/or '{1}' are not one of the supported numeric types.", m, n));
    }

    /// <summary>
    ///     Calculates remainder for the specified numbers.
    /// </summary>
    /// <param name="m">The first number (dividend).</param>
    /// <param name="n">The second number (divisor).</param>
    public static object Modulus(object m, object n)
    {
        CoerceTypes(ref m, ref n);

        if (n is int)
            return (int)m % (int)n;
        if (n is short)
            return (short)m % (short)n;
        if (n is long)
            return (long)m % (long)n;
        if (n is ushort)
            return (ushort)m % (ushort)n;
        if (n is uint)
            return (uint)m % (uint)n;
        if (n is ulong)
            return (ulong)m % (ulong)n;
        if (n is byte)
            return (byte)m % (byte)n;
        if (n is sbyte)
            return (sbyte)m % (sbyte)n;
        if (n is float)
            return (float)m % (float)n;
        if (n is double)
            return (double)m % (double)n;
        if (n is decimal)
            return (decimal)m % (decimal)n;
        throw new ArgumentException(
            string.Format("'{0}' and/or '{1}' are not one of the supported numeric types.", m, n));
    }

    /// <summary>
    ///     Raises first number to the power of the second one.
    /// </summary>
    /// <param name="m">The first number.</param>
    /// <param name="n">The second number.</param>
    public static object Power(object m, object n)
    {
        return Math.Pow(Convert.ToDouble(m), Convert.ToDouble(n));
    }

    /// <summary>
    ///     Coerces the types so they can be compared.
    /// </summary>
    /// <param name="m">The right.</param>
    /// <param name="n">The left.</param>
    public static void CoerceTypes(ref object m, ref object n)
    {
        var leftTypeCode = Convert.GetTypeCode(m);
        var rightTypeCode = Convert.GetTypeCode(n);

        if (leftTypeCode > rightTypeCode)
            n = Convert.ChangeType(n, leftTypeCode);
        else
            m = Convert.ChangeType(m, rightTypeCode);
    }
}