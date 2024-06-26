﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Vector4 = System.Numerics.Vector4;
using Matrix4x4 = System.Numerics.Matrix4x4;

namespace JA.Geometry
{
    /// <summary>Represents a vector with three  single-precision floating-point values.</summary>
    public struct Vector3 : 
        IEquatable<Vector3>, 
        ICollection<double>,
        System.Collections.ICollection,
        IFormattable
    {
        /// <summary>The X component of the vector.</summary>
        readonly double _x;
        /// <summary>The Y component of the vector.</summary>
        readonly double _y;
        /// <summary>The Z component of the vector.</summary>
        readonly double _z;

        #region Factory
        /// <summary>Creates a new <see cref="T:System.Numerics.Vector3" /> object whose three elements have the same value.</summary>
        /// <param name="value">The value to assign to all three elements.</param>
        public Vector3(double value)
        {
            this = new Vector3(value, value, value);
        }

        /// <summary>Creates a   new <see cref="T:System.Numerics.Vector3" /> object from the specified <see cref="T:System.Numerics.Vector2" /> object and the specified value.</summary>
        /// <param name="value">The vector with two elements.</param>
        /// <param name="z">The additional value to assign to the <see cref="F:System.Numerics.Vector3.Z" /> field.</param>
        public Vector3(Vector2 value, double z)
        {
            this = new Vector3(value.X, value.Y, z);
        }

        /// <summary>Creates a vector whose elements have the specified values.</summary>
        /// <param name="x">The value to assign to the <see cref="F:System.Numerics.Vector3.X" /> field.</param>
        /// <param name="y">The value to assign to the <see cref="F:System.Numerics.Vector3.Y" /> field.</param>
        /// <param name="z">The value to assign to the <see cref="F:System.Numerics.Vector3.Z" /> field.</param>
        public Vector3(double x, double y, double z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        /// <summary>Gets a vector whose 3 elements are equal to zero.</summary>
        /// <returns>A vector whose three elements are equal to zero (that is, it returns the vector <c>(0,0,0)</c>.</returns>
        public static Vector3 Zero { get; } = new Vector3(0, 0, 0);

        /// <summary>Gets a vector whose 3 elements are equal to one.</summary>
        /// <returns>A vector whose three elements are equal to one (that is, it returns the vector <c>(1,1,1)</c>.</returns>
        public static Vector3 One { get; } = new Vector3(1, 1, 1);

        /// <summary>Gets the vector (1,0,0).</summary>
        /// <returns>The vector <c>(1,0,0)</c>.</returns>
        public static Vector3 UnitX { get; } = new Vector3(1, 0, 0);

        /// <summary>Gets the vector (0,1,0).</summary>
        /// <returns>The vector <c>(0,1,0)</c>.</returns>
        public static Vector3 UnitY { get; } = new Vector3(0, 1, 0);

        /// <summary>Gets the vector (0,0,1).</summary>
        /// <returns>The vector <c>(0,0,1)</c>.</returns>
        public static Vector3 UnitZ { get; } = new Vector3(0, 0, 1);

        public static implicit operator Vector3(System.Numerics.Vector3 vector)
            => new Vector3(vector.X, vector.Y, vector.Z);
        public static implicit operator System.Numerics.Vector3(Vector3 vector)
            => new System.Numerics.Vector3((float)vector.X, (float)vector.Y, (float)vector.Z);

        static readonly Random rng = new Random();

        public static Vector3 Random(double minValue = 0, double maxValue = 1)
            => new Vector3(
                minValue + (maxValue-minValue) * rng.NextDouble(),
                minValue + (maxValue-minValue) * rng.NextDouble(),
                minValue + (maxValue-minValue) * rng.NextDouble());

        #endregion

        #region Properties
        /// <summary>The X component of the vector.</summary>		
        public double X => _x;
        /// <summary>The Y component of the vector.</summary>
        public double Y => _y;
        /// <summary>The Z component of the vector.</summary>
        public double Z => _z;

        public bool IsZero => _x ==0 && _y == 0 && _z == 0;

        #endregion

        #region Algebra
        /// <summary>Returns the length of this vector object.</summary>
        /// <returns>The vector's length.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Length()
        {
            double num = Dot(this, this);
            return Math.Sqrt(num);
        }

        /// <summary>Returns the length of the vector squared.</summary>
        /// <returns>The vector's length squared.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double LengthSquared()
        {
            return Dot(this, this);
        }

        /// <summary>Computes the Euclidean distance between the two given points.</summary>
        /// <param name="value1">The first point.</param>
        /// <param name="value2">The second point.</param>
        /// <returns>The distance.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(Vector3 value1, Vector3 value2)
        {
            Vector3 vector = value1 - value2;
            double num = Dot(vector, vector);
            return Math.Sqrt(num);
        }

        /// <summary>Returns the Euclidean distance squared between two specified points.</summary>
        /// <param name="value1">The first point.</param>
        /// <param name="value2">The second point.</param>
        /// <returns>The distance squared.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DistanceSquared(Vector3 value1, Vector3 value2)
        {
            Vector3 vector = value1 - value2;
            return Dot(vector, vector);
        }

        /// <summary>Returns a vector with the same direction as the specified vector, but with a length of one.</summary>
        /// <param name="value">The vector to normalize.</param>
        /// <returns>The normalized vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Normalize(Vector3 value)
        {
            double value2 = value.Length();
            return value / value2;
        }

        /// <summary>Computes the cross product of two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The cross product.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Cross(Vector3 vector1, Vector3 vector2)
        {
            return new Vector3(
                vector1.Y * vector2.Z - vector1.Z * vector2.Y,
                vector1.Z * vector2.X - vector1.X * vector2.Z,
                vector1.X * vector2.Y - vector1.Y * vector2.X);
        }

        /// <summary>Returns the reflection of a vector off a surface that has the specified normal.</summary>
        /// <param name="vector">The source vector.</param>
        /// <param name="normal">The normal of the surface being reflected off.</param>
        /// <returns>The reflected vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Reflect(Vector3 vector, Vector3 normal)
        {
            double right = Dot(vector, normal);
            Vector3 right2 = normal * right * 2f;
            return vector - right2;
        }

        /// <summary>Restricts a vector between a minimum and a maximum value.</summary>
        /// <param name="value1">The vector to restrict.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The restricted vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Clamp(Vector3 value1, Vector3 min, Vector3 max)
        {
            double x = value1.X;
            x = ((x > max.X) ? max.X : x);
            x = ((x < min.X) ? min.X : x);
            double y = value1.Y;
            y = ((y > max.Y) ? max.Y : y);
            y = ((y < min.Y) ? min.Y : y);
            double z = value1.Z;
            z = ((z > max.Z) ? max.Z : z);
            z = ((z < min.Z) ? min.Z : z);
            return new Vector3(x, y, z);
        }

        /// <summary>Performs a linear interpolation between two vectors based on the given weighting.</summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="amount">A value between 0 and 1 that indicates the weight of <paramref name="value2" />.</param>
        /// <returns>The interpolated vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Lerp(Vector3 value1, Vector3 value2, double amount)
        {
            Vector3 left = value1 * (1 - amount);
            Vector3 right = value2 * amount;
            return left + right;
        }

        /// <summary>Transforms a vector by the specified Quaternion rotation value.</summary>
        /// <param name="value">The vector to rotate.</param>
        /// <param name="rotation">The rotation to apply.</param>
        /// <returns>The transformed vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Transform(Vector3 value, Quaternion rotation)
        {
            double num = rotation.X + rotation.X;
            double num2 = rotation.Y + rotation.Y;
            double num3 = rotation.Z + rotation.Z;
            double num4 = rotation.W * num;
            double num5 = rotation.W * num2;
            double num6 = rotation.W * num3;
            double num7 = rotation.X * num;
            double num8 = rotation.X * num2;
            double num9 = rotation.X * num3;
            double num10 = rotation.Y * num2;
            double num11 = rotation.Y * num3;
            double num12 = rotation.Z * num3;
            return new Vector3(
                value.X * (1 - num10 - num12) + value.Y * (num8 - num6) + value.Z * (num9 + num5),
                value.X * (num8 + num6) + value.Y * (1 - num7 - num12) + value.Z * (num11 - num4),
                value.X * (num9 - num5) + value.Y * (num11 + num4) + value.Z * (1 - num7 - num10));
        }

        /// <summary>Adds two vectors together.</summary>
        /// <param name="left">The first vector to add.</param>
        /// <param name="right">The second vector to add.</param>
        /// <returns>The summed vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Add(Vector3 left, Vector3 right)
        {
            return left + right;
        }

        /// <summary>Subtracts the second vector from the first.</summary>
        /// <param name="left">The first vector.</param>
        /// <param name="right">The second vector.</param>
        /// <returns>The difference vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Subtract(Vector3 left, Vector3 right)
        {
            return left - right;
        }

        /// <summary>Returns a new vector whose values are the product of each pair of elements in two specified vectors.</summary>
        /// <param name="left">The first vector.</param>
        /// <param name="right">The second vector.</param>
        /// <returns>The element-wise product vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Multiply(Vector3 left, Vector3 right)
        {
            return left * right;
        }

        /// <summary>Multiplies a vector by a specified scalar.</summary>
        /// <param name="left">The vector to multiply.</param>
        /// <param name="right">The scalar value.</param>
        /// <returns>The scaled vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Multiply(Vector3 left, double right)
        {
            return left * right;
        }

        /// <summary>Multiplies a scalar value by a specified vector.</summary>
        /// <param name="left">The scaled value.</param>
        /// <param name="right">The vector.</param>
        /// <returns>The scaled vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Multiply(double left, Vector3 right)
        {
            return left * right;
        }

        /// <summary>Divides the first vector by the second.</summary>
        /// <param name="left">The first vector.</param>
        /// <param name="right">The second vector.</param>
        /// <returns>The vector resulting from the division.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Divide(Vector3 left, Vector3 right)
        {
            return left / right;
        }

        /// <summary>Divides the specified vector by a specified scalar value.</summary>
        /// <param name="left">The vector.</param>
        /// <param name="divisor">The scalar value.</param>
        /// <returns>The vector that results from the division.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Divide(Vector3 left, double divisor)
        {
            return left / divisor;
        }

        /// <summary>Negates a specified vector.</summary>
        /// <param name="value">The vector to negate.</param>
        /// <returns>The negated vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Negate(Vector3 value)
        {
            return -value;
        }
        /// <summary>Returns the dot product of two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The dot product.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Dot(Vector3 vector1, Vector3 vector2)
        {
            return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;
        }

        /// <summary>Returns a vector whose elements are the minimum of each of the pairs of elements in two specified vectors.</summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The minimized vector.</returns>
        public static Vector3 Min(Vector3 value1, Vector3 value2)
        {
            return new Vector3((value1.X < value2.X) ? value1.X : value2.X, (value1.Y < value2.Y) ? value1.Y : value2.Y, (value1.Z < value2.Z) ? value1.Z : value2.Z);
        }

        /// <summary>Returns a vector whose elements are the maximum of each of the pairs of elements in two specified vectors.</summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The maximized vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Max(Vector3 value1, Vector3 value2)
        {
            return new Vector3((value1.X > value2.X) ? value1.X : value2.X, (value1.Y > value2.Y) ? value1.Y : value2.Y, (value1.Z > value2.Z) ? value1.Z : value2.Z);
        }

        /// <summary>Returns a vector whose elements are the absolute values of each of the specified vector's elements.</summary>
        /// <param name="value">A vector.</param>
        /// <returns>The absolute value vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Abs(Vector3 value)
        {
            return new Vector3(Math.Abs(value.X), Math.Abs(value.Y), Math.Abs(value.Z));
        }

        /// <summary>Returns a vector whose elements are the square root of each of a specified vector's elements.</summary>
        /// <param name="value">A vector.</param>
        /// <returns>The square root vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 SquareRoot(Vector3 value)
        {
            return new Vector3(Math.Sqrt(value.X), Math.Sqrt(value.Y), Math.Sqrt(value.Z));
        }
        #endregion

        #region Operators
        /// <summary>Adds two vectors together.</summary>
        /// <param name="left">The first vector to add.</param>
        /// <param name="right">The second vector to add.</param>
        /// <returns>The summed vector.</returns>
        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        /// <summary>Subtracts the second vector from the first.</summary>
        /// <param name="left">The first vector.</param>
        /// <param name="right">The second vector.</param>
        /// <returns>The vector that results from subtracting <paramref name="right" /> from <paramref name="left" />.</returns>
        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        /// <summary>Returns a new vector whose values are the product of each pair of elements in two specified vectors.</summary>
        /// <param name="left">The first vector.</param>
        /// <param name="right">The second vector.</param>
        /// <returns>The element-wise product vector.</returns>
        public static Vector3 operator *(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
        }

        /// <summary>Multiples the specified vector by the specified scalar value.</summary>
        /// <param name="left">The vector.</param>
        /// <param name="right">The scalar value.</param>
        /// <returns>The scaled vector.</returns>
        public static Vector3 operator *(Vector3 left, double right)
        {
            return left * new Vector3(right);
        }

        /// <summary>Multiples the scalar value by the specified vector.</summary>
        /// <param name="left">The vector.</param>
        /// <param name="right">The scalar value.</param>
        /// <returns>The scaled vector.</returns>
        public static Vector3 operator *(double left, Vector3 right)
        {
            return new Vector3(left) * right;
        }

        /// <summary>Divides the first vector by the second.</summary>
        /// <param name="left">The first vector.</param>
        /// <param name="right">The second vector.</param>
        /// <returns>The vector that results from dividing <paramref name="left" /> by <paramref name="right" />.</returns>
        public static Vector3 operator /(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X / right.X, left.Y / right.Y, left.Z / right.Z);
        }

        /// <summary>Divides the specified vector by a specified scalar value.</summary>
        /// <param name="value1">The vector.</param>
        /// <param name="value2">The scalar value.</param>
        /// <returns>The result of the division.</returns>
        public static Vector3 operator /(Vector3 value1, double value2)
        {
            double num = 1 / value2;
            return new Vector3(value1.X * num, value1.Y * num, value1.Z * num);
        }

        /// <summary>Negates the specified vector.</summary>
        /// <param name="value">The vector to negate.</param>
        /// <returns>The negated vector.</returns>
        public static Vector3 operator -(Vector3 value)
        {
            return Zero - value;
        }
        #endregion

        #region Equality
        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            var hashCode = -307843816;
            hashCode=hashCode*-1521134295+X.GetHashCode();
            hashCode=hashCode*-1521134295+Y.GetHashCode();
            hashCode=hashCode*-1521134295+Z.GetHashCode();
            return hashCode;
        }

        /// <summary>Returns a value that indicates whether this instance and a specified object are equal.</summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>
        ///   <see langword="true" /> if the current instance and <paramref name="obj" /> are equal; otherwise, <see langword="false" />. If <paramref name="obj" /> is <see langword="null" />, the method returns <see langword="false" />.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            if (obj is Vector3 vector)
            {
                return Equals(vector);
            }
            return false;
        }

        /// <summary>Returns a value that indicates whether this instance and another vector are equal.</summary>
        /// <param name="other">The other vector.</param>
        /// <returns>
        ///   <see langword="true" /> if the two vectors are equal; otherwise, <see langword="false" />.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector3 vector)
            => _x==vector._x && _y == vector._y && _z==vector._z;

        /// <summary>Returns a value that indicates whether each pair of elements in two specified vectors is equal.</summary>
        /// <param name="left">The first vector to compare.</param>
        /// <param name="right">The second vector to compare.</param>
        /// <returns>
        ///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <see langword="false" />.</returns>
        public static bool operator ==(Vector3 left, Vector3 right)
            => left.Equals(right);

        /// <summary>Returns a value that indicates whether two specified vectors are not equal.</summary>
        /// <param name="left">The first vector to compare.</param>
        /// <param name="right">The second vector to compare.</param>
        /// <returns>
        ///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, <see langword="false" />.</returns>
        public static bool operator !=(Vector3 left, Vector3 right)
            => !(left==right);

        #endregion

        #region Formatting
        /// <summary>Returns the string representation of the current instance using default formatting.</summary>
        /// <returns>The string representation of the current instance.</returns>
        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }

        /// <summary>Returns the string representation of the current instance using the specified format string to format individual elements.</summary>
        /// <param name="format">A standard or custom numeric format string that defines the format of individual elements.</param>
        /// <returns>The string representation of the current instance.</returns>
        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        /// <summary>Returns the string representation of the current instance using the specified format string to format individual elements and the specified format provider to define culture-specific formatting.</summary>
        /// <param name="format">A standard or custom numeric format string that defines the format of individual elements.</param>
        /// <param name="formatProvider">A format provider that supplies culture-specific formatting information.</param>
        /// <returns>The string representation of the current instance.</returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string numberGroupSeparator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
            stringBuilder.Append('<');
            stringBuilder.Append(X.ToString(format, formatProvider));
            stringBuilder.Append(numberGroupSeparator);
            stringBuilder.Append(' ');
            stringBuilder.Append(Y.ToString(format, formatProvider));
            stringBuilder.Append(numberGroupSeparator);
            stringBuilder.Append(' ');
            stringBuilder.Append(Z.ToString(format, formatProvider));
            stringBuilder.Append('>');
            return stringBuilder.ToString();
        }
        #endregion

        #region ICollection

        public bool IsReadOnly => true;
        public int Count => 3;
        public unsafe ReadOnlySpan<double> AsSpan()
        {
            fixed (double* ptr = &_x)
            {
                return new ReadOnlySpan<double>(ptr, 3);
            }
        }

        public double[] ToArray()
        {
            return AsSpan().ToArray();
        }
        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return _x;
                    case 1: return _y;
                    case 2: return _z;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
        }
        public int IndexOf(double item) => throw new NotImplementedException();
        public bool Contains(double item) => IndexOf(item)>=0;
        public void CopyTo(Array array, int index)
            => Array.Copy(ToArray(), 0, array, index, Count);
        /// <summary>Copies the elements of the vector to a specified array.</summary>
        /// <param name="array">The destination array.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="array" /> is <see langword="null" />.</exception>
        /// <exception cref="T:System.ArgumentException">The number of elements in the current instance is greater than in the array.</exception>
        /// <exception cref="T:System.RankException">
        ///   <paramref name="array" /> is multidimensional.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(double[] array)
        {
            CopyTo(array, 0);
        }

        /// <summary>Copies the elements of the vector to a specified array starting at a specified index position.</summary>
        /// <param name="array">The destination array.</param>
        /// <param name="index">The index at which to copy the first element of the vector.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="array" /> is <see langword="null" />.</exception>
        /// <exception cref="T:System.ArgumentException">The number of elements in the current instance is greater than in the array.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///   <paramref name="index" /> is less than zero.  
        /// -or-  
        /// <paramref name="index" /> is greater than or equal to the array length.</exception>
        /// <exception cref="T:System.RankException">
        ///   <paramref name="array" /> is multidimensional.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(double[] array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            if (index >= 0 && index < array.Length)
            {
                if (array.Length - index < 3)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                array[index] = _x;
                array[index + 1] = _y;
                array[index + 2] = _z;
                return;
            }
            throw new ArgumentOutOfRangeException(nameof(index));
        }
        public IEnumerator<double> GetEnumerator()
        {
            yield return _x;
            yield return _y;
            yield return _z;
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        bool System.Collections.ICollection.IsSynchronized => false;
        object System.Collections.ICollection.SyncRoot => new NotSupportedException();
        void ICollection<double>.Add(double item) => throw new NotSupportedException();
        void ICollection<double>.Clear() => throw new NotSupportedException();
        bool ICollection<double>.Remove(double item) => throw new NotSupportedException();
        #endregion
    }


}
