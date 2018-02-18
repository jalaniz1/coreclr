// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/******************************************************************************
 * This file is auto-generated from a template file by the GenerateTests.csx  *
 * script in tests\src\JIT\HardwareIntrinsics\X86\Shared. In order to make    *
 * changes, please update the corresponding template and run according to the *
 * directions listed in the file.                                             *
 ******************************************************************************/

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace JIT.HardwareIntrinsics.X86
{
    public static partial class Program
    {
        private static void BlendVariableDouble()
        {
            var test = new SimpleTernaryOpTest__BlendVariableDouble();

            if (test.IsSupported)
            {
                // Validates basic functionality works, using Unsafe.Read
                test.RunBasicScenario_UnsafeRead();

                // Validates basic functionality works, using Load
                test.RunBasicScenario_Load();

                // Validates basic functionality works, using LoadAligned
                test.RunBasicScenario_LoadAligned();

                // Validates calling via reflection works, using Unsafe.Read
                test.RunReflectionScenario_UnsafeRead();

                // Validates calling via reflection works, using Load
                test.RunReflectionScenario_Load();

                // Validates calling via reflection works, using LoadAligned
                test.RunReflectionScenario_LoadAligned();

                // Validates passing a static member works
                test.RunClsVarScenario();

                // Validates passing a local works, using Unsafe.Read
                test.RunLclVarScenario_UnsafeRead();

                // Validates passing a local works, using Load
                test.RunLclVarScenario_Load();

                // Validates passing a local works, using LoadAligned
                test.RunLclVarScenario_LoadAligned();

                // Validates passing the field of a local works
                test.RunLclFldScenario();

                // Validates passing an instance member works
                test.RunFldScenario();
            }
            else
            {
                // Validates we throw on unsupported hardware
                test.RunUnsupportedScenario();
            }

            if (!test.Succeeded)
            {
                throw new Exception("One or more scenarios did not complete as expected.");
            }
        }
    }

    public sealed unsafe class SimpleTernaryOpTest__BlendVariableDouble
    {
        private const int VectorSize = 32;
        private const int ElementCount = VectorSize / sizeof(Double);

        private static Double[] _data1 = new Double[ElementCount];
        private static Double[] _data2 = new Double[ElementCount];
        private static Double[] _data3 = new Double[ElementCount];

        private static Vector256<Double> _clsVar1;
        private static Vector256<Double> _clsVar2;
        private static Vector256<Double> _clsVar3;

        private Vector256<Double> _fld1;
        private Vector256<Double> _fld2;
        private Vector256<Double> _fld3;

        private SimpleTernaryOpTest__DataTable<Double> _dataTable;

        static SimpleTernaryOpTest__BlendVariableDouble()
        {
            var random = new Random();

            for (var i = 0; i < ElementCount; i++) { _data1[i] = (double)(random.NextDouble()); _data2[i] = (double)(random.NextDouble()); _data3[i] = (double)(((i % 2) == 0) ? -0.0 : 1.0); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector256<Double>, byte>(ref _clsVar1), ref Unsafe.As<Double, byte>(ref _data2[0]), VectorSize);
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector256<Double>, byte>(ref _clsVar2), ref Unsafe.As<Double, byte>(ref _data1[0]), VectorSize);
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector256<Double>, byte>(ref _clsVar3), ref Unsafe.As<Double, byte>(ref _data3[0]), VectorSize);
        }

        public SimpleTernaryOpTest__BlendVariableDouble()
        {
            Succeeded = true;

            var random = new Random();

            for (var i = 0; i < ElementCount; i++) { _data1[i] = (double)(random.NextDouble()); _data2[i] = (double)(random.NextDouble()); _data3[i] = (double)(((i % 2) == 0) ? -0.0 : 1.0); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector256<Double>, byte>(ref _fld1), ref Unsafe.As<Double, byte>(ref _data1[0]), VectorSize);
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector256<Double>, byte>(ref _fld2), ref Unsafe.As<Double, byte>(ref _data2[0]), VectorSize);
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector256<Double>, byte>(ref _fld3), ref Unsafe.As<Double, byte>(ref _data3[0]), VectorSize);

            for (var i = 0; i < ElementCount; i++) { _data1[i] = (double)(random.NextDouble()); _data2[i] = (double)(random.NextDouble()); _data3[i] = (double)(((i % 2) == 0) ? -0.0 : 1.0); }
            _dataTable = new SimpleTernaryOpTest__DataTable<Double>(_data1, _data2, _data3, new Double[ElementCount], VectorSize);
        }

        public bool IsSupported => Avx.IsSupported;

        public bool Succeeded { get; set; }

        public void RunBasicScenario_UnsafeRead()
        {
            var result = Avx.BlendVariable(
                Unsafe.Read<Vector256<Double>>(_dataTable.inArray1Ptr),
                Unsafe.Read<Vector256<Double>>(_dataTable.inArray2Ptr),
                Unsafe.Read<Vector256<Double>>(_dataTable.inArray3Ptr)
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.inArray3Ptr, _dataTable.outArrayPtr);
        }

        public void RunBasicScenario_Load()
        {
            var result = Avx.BlendVariable(
                Avx.LoadVector256((Double*)(_dataTable.inArray1Ptr)),
                Avx.LoadVector256((Double*)(_dataTable.inArray2Ptr)),
                Avx.LoadVector256((Double*)(_dataTable.inArray3Ptr))
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.inArray3Ptr, _dataTable.outArrayPtr);
        }

        public void RunBasicScenario_LoadAligned()
        {
            var result = Avx.BlendVariable(
                Avx.LoadAlignedVector256((Double*)(_dataTable.inArray1Ptr)),
                Avx.LoadAlignedVector256((Double*)(_dataTable.inArray2Ptr)),
                Avx.LoadAlignedVector256((Double*)(_dataTable.inArray3Ptr))
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.inArray3Ptr, _dataTable.outArrayPtr);
        }

        public void RunReflectionScenario_UnsafeRead()
        {
            var result = typeof(Avx).GetMethod(nameof(Avx.BlendVariable), new Type[] { typeof(Vector256<Double>), typeof(Vector256<Double>), typeof(Vector256<Double>) })
                                     .Invoke(null, new object[] {
                                        Unsafe.Read<Vector256<Double>>(_dataTable.inArray1Ptr),
                                        Unsafe.Read<Vector256<Double>>(_dataTable.inArray2Ptr),
                                        Unsafe.Read<Vector256<Double>>(_dataTable.inArray3Ptr)
                                     });

            Unsafe.Write(_dataTable.outArrayPtr, (Vector256<Double>)(result));
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.inArray3Ptr, _dataTable.outArrayPtr);
        }

        public void RunReflectionScenario_Load()
        {
            var result = typeof(Avx).GetMethod(nameof(Avx.BlendVariable), new Type[] { typeof(Vector256<Double>), typeof(Vector256<Double>), typeof(Vector256<Double>) })
                                     .Invoke(null, new object[] {
                                        Avx.LoadVector256((Double*)(_dataTable.inArray1Ptr)),
                                        Avx.LoadVector256((Double*)(_dataTable.inArray2Ptr)),
                                        Avx.LoadVector256((Double*)(_dataTable.inArray3Ptr))
                                     });

            Unsafe.Write(_dataTable.outArrayPtr, (Vector256<Double>)(result));
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.inArray3Ptr, _dataTable.outArrayPtr);
        }

        public void RunReflectionScenario_LoadAligned()
        {
            var result = typeof(Avx).GetMethod(nameof(Avx.BlendVariable), new Type[] { typeof(Vector256<Double>), typeof(Vector256<Double>), typeof(Vector256<Double>) })
                                     .Invoke(null, new object[] {
                                        Avx.LoadAlignedVector256((Double*)(_dataTable.inArray1Ptr)),
                                        Avx.LoadAlignedVector256((Double*)(_dataTable.inArray2Ptr)),
                                        Avx.LoadAlignedVector256((Double*)(_dataTable.inArray3Ptr))
                                     });

            Unsafe.Write(_dataTable.outArrayPtr, (Vector256<Double>)(result));
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.inArray3Ptr, _dataTable.outArrayPtr);
        }

        public void RunClsVarScenario()
        {
            var result = Avx.BlendVariable(
                _clsVar1,
                _clsVar2,
                _clsVar3
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_clsVar1, _clsVar2, _clsVar3, _dataTable.outArrayPtr);
        }

        public void RunLclVarScenario_UnsafeRead()
        {
            var firstOp = Unsafe.Read<Vector256<Double>>(_dataTable.inArray1Ptr);
            var secondOp = Unsafe.Read<Vector256<Double>>(_dataTable.inArray2Ptr);
            var thirdOp = Unsafe.Read<Vector256<Double>>(_dataTable.inArray3Ptr);
            var result = Avx.BlendVariable(firstOp, secondOp, thirdOp);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(firstOp, secondOp, thirdOp, _dataTable.outArrayPtr);
        }

        public void RunLclVarScenario_Load()
        {
            var firstOp = Avx.LoadVector256((Double*)(_dataTable.inArray1Ptr));
            var secondOp = Avx.LoadVector256((Double*)(_dataTable.inArray2Ptr));
            var thirdOp = Avx.LoadVector256((Double*)(_dataTable.inArray3Ptr));
            var result = Avx.BlendVariable(firstOp, secondOp, thirdOp);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(firstOp, secondOp, thirdOp, _dataTable.outArrayPtr);
        }

        public void RunLclVarScenario_LoadAligned()
        {
            var firstOp = Avx.LoadAlignedVector256((Double*)(_dataTable.inArray1Ptr));
            var secondOp = Avx.LoadAlignedVector256((Double*)(_dataTable.inArray2Ptr));
            var thirdOp = Avx.LoadAlignedVector256((Double*)(_dataTable.inArray3Ptr));
            var result = Avx.BlendVariable(firstOp, secondOp, thirdOp);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(firstOp, secondOp, thirdOp, _dataTable.outArrayPtr);
        }

        public void RunLclFldScenario()
        {
            var test = new SimpleTernaryOpTest__BlendVariableDouble();
            var result = Avx.BlendVariable(test._fld1, test._fld2, test._fld3);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(test._fld1, test._fld2, test._fld3, _dataTable.outArrayPtr);
        }

        public void RunFldScenario()
        {
            var result = Avx.BlendVariable(_fld1, _fld2, _fld3);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_fld1, _fld2, _fld3, _dataTable.outArrayPtr);
        }

        public void RunUnsupportedScenario()
        {
            Succeeded = false;

            try
            {
                RunBasicScenario_UnsafeRead();
            }
            catch (PlatformNotSupportedException)
            {
                Succeeded = true;
            }
        }

        private void ValidateResult(Vector256<Double> firstOp, Vector256<Double> secondOp, Vector256<Double> thirdOp, void* result, [CallerMemberName] string method = "")
        {
            Double[] inArray1 = new Double[ElementCount];
            Double[] inArray2 = new Double[ElementCount];
            Double[] inArray3 = new Double[ElementCount];
            Double[] outArray = new Double[ElementCount];

            Unsafe.Write(Unsafe.AsPointer(ref inArray1[0]), firstOp);
            Unsafe.Write(Unsafe.AsPointer(ref inArray2[0]), secondOp);
            Unsafe.Write(Unsafe.AsPointer(ref inArray3[0]), thirdOp);
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Double, byte>(ref outArray[0]), ref Unsafe.AsRef<byte>(result), VectorSize);

            ValidateResult(inArray1, inArray2, inArray3, outArray, method);
        }

        private void ValidateResult(void* firstOp, void* secondOp, void* thirdOp, void* result, [CallerMemberName] string method = "")
        {
            Double[] inArray1 = new Double[ElementCount];
            Double[] inArray2 = new Double[ElementCount];
            Double[] inArray3 = new Double[ElementCount];
            Double[] outArray = new Double[ElementCount];

            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Double, byte>(ref inArray1[0]), ref Unsafe.AsRef<byte>(firstOp), VectorSize);
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Double, byte>(ref inArray2[0]), ref Unsafe.AsRef<byte>(secondOp), VectorSize);
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Double, byte>(ref inArray3[0]), ref Unsafe.AsRef<byte>(thirdOp), VectorSize);
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Double, byte>(ref outArray[0]), ref Unsafe.AsRef<byte>(result), VectorSize);

            ValidateResult(inArray1, inArray2, inArray3, outArray, method);
        }

        private void ValidateResult(Double[] firstOp, Double[] secondOp, Double[] thirdOp, Double[] result, [CallerMemberName] string method = "")
        {
            if (((BitConverter.DoubleToInt64Bits(thirdOp[0]) >> 63) & 1) == 1 ? BitConverter.DoubleToInt64Bits(secondOp[0]) != BitConverter.DoubleToInt64Bits(result[0]) : BitConverter.DoubleToInt64Bits(firstOp[0]) != BitConverter.DoubleToInt64Bits(result[0]))
            {
                Succeeded = false;
            }
            else
            {
                for (var i = 1; i < firstOp.Length; i++)
                {
                    if (((BitConverter.DoubleToInt64Bits(thirdOp[i]) >> 63) & 1) == 1 ? BitConverter.DoubleToInt64Bits(secondOp[i]) != BitConverter.DoubleToInt64Bits(result[i]) : BitConverter.DoubleToInt64Bits(firstOp[i]) != BitConverter.DoubleToInt64Bits(result[i]))
                    {
                        Succeeded = false;
                        break;
                    }
                }
            }

            if (!Succeeded)
            {
                Console.WriteLine($"{nameof(Avx)}.{nameof(Avx.BlendVariable)}<Double>: {method} failed:");
                Console.WriteLine($"    firstOp: ({string.Join(", ", firstOp)})");
                Console.WriteLine($"   secondOp: ({string.Join(", ", secondOp)})");
                Console.WriteLine($"   thirdOp: ({string.Join(", ", thirdOp)})");
                Console.WriteLine($"  result: ({string.Join(", ", result)})");
                Console.WriteLine();
            }
        }
    }
}