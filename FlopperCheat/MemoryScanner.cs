using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;

namespace FlopperCheat
{
    internal class MemoryScanner
    {
        private static byte[] buffer = [];
        private const uint bufferSize = 1024;
        private static Type valueType = typeof(int);

        private static List<nint> foundAddresses = [];

        #region WINAPI

        public delegate bool EnumWindowsProc(nint hWnd, nint lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, nint lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowText(nint hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(nint hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(nint hWnd);

        [DllImport("kernel32.dll")]
        public static extern nint OpenProcess(long dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadProcessMemory(nint hProcess, nint lpBaseAddress, byte[] lpBuffer, uint nSize, out nint lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool VirtualQueryEx(nint hProcess, nint lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(nint hProcess, nint lpBaseAddress, byte[] lpBuffer, uint nSize, out nint lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool VirtualProtectEx(
            nint hProcess,         
            nint lpAddress,     
            uint dwSize,              
            uint flNewProtect,       
            out uint lpflOldProtect   
        );

        [StructLayout(LayoutKind.Sequential)]
        struct MEMORY_BASIC_INFORMATION
        {
            public nint BaseAddress;
            public nint AllocationBase;
            public uint AllocationProtect;
            public nint RegionSize;
            public uint State;
            public uint Protect;
            public uint Type;
        }

        [Flags]
        public enum MemoryProtection : uint
        {
            PAGE_READWRITE = 0x04,
            PAGE_EXECUTE_READWRITE = 0x40,
            PAGE_READONLY = 0x02
        }

        [Flags]
        public enum ProcessAccessFlags : long
        {
            PROCESS_VM_OPERATION = 0x0008,
            PROCESS_VM_READ = 0x0010,
            PROCESS_VM_WRITE = 0x0020,
            PROCESS_QUERY_INFORMATION = 0x0400,
            PROCESS_ALL_ACCESS = 0x000F0000L | 0x00100000L | 0xFFF
        }
        #endregion

        public static List<(nint hWnd, string title, uint processId)> GetAllWindows()
        {
            List<(nint hWnd, string title, uint processId)> windows = new();

            EnumWindows((hWnd, lParam) =>
            {
                StringBuilder sb = new StringBuilder(256);
                GetWindowText(hWnd, sb, sb.Capacity);
                string title = sb.ToString();

                if (!string.IsNullOrWhiteSpace(title) && IsWindowVisible(hWnd))
                {
                    GetWindowThreadProcessId(hWnd, out uint processId);
                    windows.Add((hWnd, title, processId));
                }

                return true; 
            }, nint.Zero);

            return windows;
        }

        public static List<nint> MemorySearch(uint processId, long desiredValue)
        {
            ConcurrentBag<nint> result = [];
            int valueSize = Marshal.SizeOf(valueType);

            nint address = nint.Zero;
            nint handle = OpenProcess((int)(ProcessAccessFlags.PROCESS_VM_OPERATION | ProcessAccessFlags.PROCESS_VM_WRITE | ProcessAccessFlags.PROCESS_VM_READ), false, processId);

            MEMORY_BASIC_INFORMATION memInfo = new();
            uint memInfoSize = (uint)Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION));

            while (true)
            {
                if (!VirtualQueryEx(handle, address, out memInfo, memInfoSize))
                {
                    break;
                }

                if (memInfo.State == 0x10000 || memInfo.State == 0x2000)
                {
                    address = (nint)(memInfo.BaseAddress + memInfo.RegionSize.ToInt64());
                    continue;
                }

                if ((memInfo.Protect & (uint)(MemoryProtection.PAGE_READWRITE | MemoryProtection.PAGE_EXECUTE_READWRITE | MemoryProtection.PAGE_READONLY)) != 0)
                {
                    nint regionBaseAddress = memInfo.BaseAddress;
                    long regionSize = memInfo.RegionSize.ToInt64();

                    Parallel.For(0, (int)Math.Ceiling((double)regionSize / bufferSize), chunk =>
                    {
                        long offset = chunk * bufferSize;
                        if (offset >= regionSize)
                            return;

                        nint currentAddress = nint.Add(regionBaseAddress, (int)offset);
                        byte[] buffer = new byte[bufferSize];

                        if (ReadProcessMemory(handle, currentAddress, buffer, bufferSize, out nint bytesRead) && bytesRead > 0)
                        {
                            byte[] desiredBytes = BitConverter.GetBytes(desiredValue);

                            for (int j = 0; j <= bytesRead - valueSize; j++)
                            {
                                bool match = true;
                                for (int k = 0; k < valueSize; k++)
                                {
                                    if (buffer[j + k] != desiredBytes[k])
                                    {
                                        match = false;
                                        break;
                                    }
                                }

                                if (match)
                                {
                                    nint pointer = nint.Add(currentAddress, j);
                                    result.Add(pointer);
                                }
                            }
                        }
                    });
                }

                address = (nint)(memInfo.BaseAddress + memInfo.RegionSize.ToInt64());
            }

            return [.. result.Distinct()];
        }

        public static List<nint> FilterPointers(uint processId, List<nint> pointers, long newValue)
        {
            ConcurrentBag<nint> filteredPointers = [];

            nint handle = OpenProcess((int)(ProcessAccessFlags.PROCESS_VM_OPERATION | ProcessAccessFlags.PROCESS_VM_WRITE | ProcessAccessFlags.PROCESS_VM_READ), false, processId);

            Parallel.ForEach(pointers, pointer =>
            {
                byte[] buffer = new byte[Marshal.SizeOf(valueType)];

                if (ReadProcessMemory(handle, pointer, buffer, (uint)buffer.Length, out nint bytesRead) && bytesRead > 0)
                {
                    long readValue = BufferConvert(buffer, 0);
                    if (readValue == newValue)
                    {
                        filteredPointers.Add(pointer);
                    }
                }
            });

            return filteredPointers.Distinct().ToList();
        }

        private static long BufferConvert(byte[] buffer, int offset) => valueType switch
        {
            var valueType when valueType == typeof(short) => BitConverter.ToInt16(buffer, offset),
            var valueType when valueType == typeof(int) => BitConverter.ToInt32(buffer, offset),
            var valueType when valueType == typeof(long) => BitConverter.ToInt64(buffer, offset),
            _ => BitConverter.ToInt32(buffer, offset),
        };

        public static string WriteAddressValue(uint processId, nint targetPointer, long value)
        {
            byte[] newValueBuffer = BitConverter.GetBytes(value);

            nint handle = OpenProcess((long)(ProcessAccessFlags.PROCESS_VM_READ | ProcessAccessFlags.PROCESS_VM_WRITE | ProcessAccessFlags.PROCESS_QUERY_INFORMATION | ProcessAccessFlags.PROCESS_VM_OPERATION), false, processId);

            if (handle == nint.Zero)
            {
                return ($"Öffnen des Prozesses {processId} fehlgeschlagen. Fehler: {Marshal.GetLastWin32Error()}");
            }

            if (VirtualQueryEx(handle, targetPointer, out MEMORY_BASIC_INFORMATION memInfo, (uint)Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION))))
            {
                if (memInfo.State != 0x1000)
                {
                    return ($"Adresse 0x{targetPointer:X} befindet sich nicht im zugewiesenen Zustand");
                }

                if ((memInfo.Protect & (uint)(MemoryProtection.PAGE_READWRITE | MemoryProtection.PAGE_EXECUTE_READWRITE)) == 0)
                {
                    if (!VirtualProtectEx(handle, targetPointer, (uint)newValueBuffer.Length, (uint)MemoryProtection.PAGE_READWRITE, out uint oldProtect))
                    {
                        return ($"Schutzänderung für Adresse 0x{targetPointer:X} fehlgeschlagen. Fehler: {Marshal.GetLastWin32Error()}");
                    }
                }

                if (WriteProcessMemory(handle, targetPointer, newValueBuffer, (uint)newValueBuffer.Length, out nint bytesWritten) && bytesWritten == newValueBuffer.Length)
                {
                    return ($"Wert {value} erfolgreich in Adresse 0x{targetPointer:X} geschrieben");
                }
                else
                {
                    return ($"Speicher an Adresse 0x{targetPointer:X} konnte nicht beschrieben werden. Fehlercode: {Marshal.GetLastWin32Error()}");
                }
            }
            else
            {
                return ($"VirtualQueryEx für Adresse 0x{targetPointer:X} fehlgeschlagen. Fehler: {Marshal.GetLastWin32Error()}");
            }
        }
    }
}
