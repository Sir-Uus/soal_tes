using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    // Kamus yang berisi pasangan huruf dan angka
    static Dictionary<char, int> kamus = new Dictionary<char, int>
    {
        { 'A', 0 }, { 'B', 1 }, { 'C', 1 }, { 'D', 1 }, { 'E', 2 }, { 'F', 3 },
        { 'G', 3 }, { 'H', 3 }, { 'I', 4 }, { 'J', 5 }, { 'K', 5 }, { 'L', 5 },
        { 'M', 5 }, { 'N', 5 }, { 'O', 6 }, { 'P', 7 }, { 'Q', 7 }, { 'R', 7 },
        { 'S', 7 }, { 'T', 7 }, { 'U', 8 }, { 'V', 9 }, { 'W', 9 }, { 'X', 9 },
        { 'Y', 9 }, { 'Z', 9 }, { 'a', 9 }, { 'b', 8 }, { 'c', 8 }, { 'd', 8 },
        { 'e', 7 }, { 'f', 6 }, { 'g', 6 }, { 'h', 6 }, { 'i', 5 }, { 'j', 4 },
        { 'k', 4 }, { 'l', 4 }, { 'm', 4 }, { 'n', 4 }, { 'o', 3 }, { 'p', 2 },
        { 'q', 2 }, { 'r', 2 }, { 's', 2 }, { 't', 2 }, { 'u', 2 }, { 'v', 0 },
        { 'w', 0 }, { 'x', 0 }, { 'y', 0 }, { 'z', 0 }, { ' ', 0 }
    };

    // Fungsi untuk mengubah angka menjadi huruf
    static string AngkaKeHuruf(List<int> numbers)
    {
        var reverseKamus = kamus
            .GroupBy(kvp => kvp.Value)
            .ToDictionary(g => g.Key, g => g.First().Key);

        return string.Join(" ", numbers.Select(num => reverseKamus.ContainsKey(num) ? reverseKamus[num].ToString() : ""));
    }

    // Fungsi untuk mengurai angka
    static List<int> UraiKeAngka(int target)
    {
        var numbers = new List<int>();
        int currentSum = 0;
        int nextNumber = 0;

        while (currentSum < target)
        {
            if (currentSum + nextNumber > target)
            {
                nextNumber = 0;
            }
            numbers.Add(nextNumber);
            currentSum += nextNumber;
            nextNumber++;
        }
        return numbers;
    }

    // Fungsi untuk mengonversi kalimat menjadi angka
    static string Konversi(string sentence)
    {
        return string.Concat(sentence.Select(c => kamus.ContainsKey(c) ? kamus[c].ToString() : ""));
    }

    // Fungsi untuk operasi bergantian
    static int OperasiBergantian(string sentence)
    {
        var values = sentence.Select(c => kamus.ContainsKey(c) ? kamus[c] : 0).ToList();
        int result = values.FirstOrDefault();

        for (int i = 1; i < values.Count; i++)
        {
            if (i % 2 == 0)
                result -= values[i];
            else
                result += values[i];
        }

        return result;
    }

    // Fungsi untuk operasi transformasi angka
    static List<int> OperasiTransformasiAngka(List<int> values)
    {
        var newValues = new List<int>(values);
        if (newValues.Count >= 2)
        {
            newValues[newValues.Count - 2] += 1;
            newValues[newValues.Count - 1] += 1;
        }
        return newValues;
    }

    // Fungsi untuk cek dan ubah angka Genap
    static List<int> CekDanUbahGenap(List<string> huruf)
    {
        var values = huruf.Select(c => kamus.ContainsKey(c[0]) ? kamus[c[0]] : -1).ToList();
        return values.Select(value => (value != -1 && value % 2 == 0) ? value + 1 : value).ToList();
    }

    // Program utama
    static void Main()
    {
        Console.Write("Masukkan kalimat: ");
        string input = Console.ReadLine();

        // Proses setiap langkah sesuai urutan
        string hasilKonversi = Konversi(input);
        int hasilOperasi = OperasiBergantian(input);
        int plushasilOperasi = Math.Abs(hasilOperasi);
        var angkaIndividu = UraiKeAngka(plushasilOperasi);
        string hasilHuruf = AngkaKeHuruf(angkaIndividu);
        var hasilTransformasiAngka = OperasiTransformasiAngka(UraiKeAngka(plushasilOperasi));
        string hasilLog4 = AngkaKeHuruf(hasilTransformasiAngka);
        var hasilLog5 = CekDanUbahGenap(hasilLog4.Split(' ').ToList());

        // Menampilkan hasil
        Console.WriteLine($"1. Hasil no 1: {hasilKonversi}"); //✔️
        Console.WriteLine($"2. Hasil no 2: {hasilOperasi}");
        Console.WriteLine($"3. Hasil no 3: {hasilHuruf}");
        Console.WriteLine($"4. Hasil no 4: {hasilLog4}");
        Console.WriteLine($"5. Hasil no 5: {string.Join(" ", hasilLog5)}");
    }
}
