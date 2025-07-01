using System;
using System.Collections.Generic; // Diperlukan untuk Dictionary

public class MonthDaysCalculator
{
    // --- Implementasi Enum dalam Table-Driven ---
    // Enum untuk merepresentasikan bulan-bulan.
    // Memberikan kejelasan dan tipe keamanan dibandingkan menggunakan integer biasa.
    public enum Month
    {
        January = 1,  // Kita mulai dari 1 agar sesuai dengan konvensi bulan
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }

    // Tabel data (Dictionary) untuk menyimpan jumlah hari dalam setiap bulan.
    // Menggunakan Enum sebagai kunci (key) dan int sebagai nilai (value).
    private static readonly Dictionary<Month, int> DaysInMonthNonLeapYear = new Dictionary<Month, int>()
    {
        { Month.January, 31 },
        { Month.February, 28 }, // Februari di tahun non-kabisat
        { Month.March, 31 },
        { Month.April, 30 },
        { Month.May, 31 },
        { Month.June, 30 },
        { Month.July, 31 },
        { Month.August, 31 },
        { Month.September, 30 },
        { Month.October, 31 },
        { Month.November, 30 },
        { Month.December, 31 }
    };

    public static void Main(string[] args)
    {
        Console.WriteLine("--- Penghitung Jumlah Hari dalam Bulan (Tahun Non-Kabisat) ---");
        Console.WriteLine("Menggunakan Metode Table-Driven dengan Enum");

        // Contoh penggunaan dengan enum:
        Console.WriteLine($"Jumlah hari di {Month.January}: {GetDaysInMonth(Month.January)} hari");
        Console.WriteLine($"Jumlah hari di {Month.February}: {GetDaysInMonth(Month.February)} hari");
        Console.WriteLine($"Jumlah hari di {Month.September}: {GetDaysInMonth(Month.September)} hari");

        Console.WriteLine("\n-------------------------------------------------");
        Console.WriteLine("Masukkan nama bulan (misal: January, February, dst.) untuk mencari jumlah hari:");
        string? inputMonthName = Console.ReadLine();

        if (Enum.TryParse(inputMonthName, true, out Month parsedMonth))
        {
            try
            {
                int days = GetDaysInMonth(parsedMonth);
                Console.WriteLine($"Jumlah hari di {parsedMonth} adalah: {days} hari.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        else
        {
            Console.WriteLine($"Nama bulan '{inputMonthName}' tidak valid. Harap masukkan nama bulan yang benar.");
        }

        Console.WriteLine("\n-------------------------------------------------");
        Console.WriteLine("Anda juga bisa memasukkan nomor bulan (1-12):");
        if (int.TryParse(Console.ReadLine(), out int monthNumber))
        {
            if (Enum.IsDefined(typeof(Month), monthNumber))
            {
                Month selectedMonth = (Month)monthNumber; // Konversi int ke enum
                try
                {
                    int days = GetDaysInMonth(selectedMonth);
                    Console.WriteLine($"Jumlah hari di {selectedMonth} (bulan ke-{monthNumber}) adalah: {days} hari.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine($"Nomor bulan '{monthNumber}' tidak valid. Harap masukkan antara 1 dan 12.");
            }
        }
        else
        {
            Console.WriteLine("Input tidak valid. Harap masukkan angka.");
        }
    }

    /// <summary>
    /// Mengembalikan jumlah hari dalam bulan tertentu pada tahun non-kabisat menggunakan table-driven method.
    /// Tabel data diimplementasikan menggunakan Dictionary dengan Enum sebagai kunci.
    /// </summary>
    /// <param name="month">Bulan yang ingin dicari jumlah harinya (tipe Enum Month).</param>
    /// <returns>Jumlah hari dalam bulan yang ditentukan.</returns>
    /// <exception cref="ArgumentException">Dilemparkan jika bulan tidak ditemukan dalam tabel.</exception>
    public static int GetDaysInMonth(Month month)
    {
        // Mengakses data dari Dictionary menggunakan Enum sebagai kunci.
        // Ini adalah inti dari metode table-driven dengan Enum.
        if (DaysInMonthNonLeapYear.TryGetValue(month, out int days))
        {
            return days;
        }
        else
        {
            // Ini sebenarnya tidak akan terjadi jika enum didefinisikan dengan benar dan Dictionary lengkap,
            // tetapi baik untuk penanganan kesalahan yang robust.
            throw new ArgumentException($"Bulan {month} tidak ditemukan dalam tabel hari.");
        }
    }
}