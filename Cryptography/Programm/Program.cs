﻿using System;
using System.IO;
using System.Text;
using Cryptography.Algorithms.Crypt;
using Cryptography.Algorithms.Signature;
using Cryptography.Algorithms.RandomNumbers;
using Cryptography.Math;

namespace Cryptography.Programm
{
    class Program
    {
        static void Output16(byte[] text)
        {   
            for(int i=0; i < text.Length; i++)
            {
                Console.Write(Convert.ToString(text[i],16) + " ");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Выберите действие:\n 1. Шифрование;\n 2. Электронная подпись;\n 3. ГПСЧ; ");
            int act = Convert.ToInt32(Console.ReadLine());

            if (act == 1)
            {
                Console.WriteLine("Выберите алгоритм:\n 1. Фейстель; 2. Магма; 3. Эль-Гамаль; 4. На основе Эллиптических кривых; ");
                int alg = Convert.ToInt32(Console.ReadLine());
                string currentCipher;
                switch (alg)
                {
                    case 1:
                        currentCipher = "FEISTELMODIFIED";
                        break;
                    case 2:
                        currentCipher = "MAGMA";
                        break;
                    case 3:
                        currentCipher = "ELGAMAL";
                        break;
                    case 4:
                        currentCipher = "ELLIPTICCURVE";
                        break;
                    default:
                        return;
                }

                Crypter crypter = new Crypter();

                byte[] Text = Encoding.Default.GetBytes("Всем привет Всем привет Всем привет Всем привет Всем привет Всем привет Всем привет Всем привет");
                // byte[] Text = { (byte)5 };

                Console.WriteLine("Исходный текст:\n" + Encoding.Default.GetString(Text));
                // Output16(Text);
                byte[] CryptedText = crypter.Encrypt(Text, currentCipher);

                Console.WriteLine("\nЗашифрованный текст:\n" + Encoding.Default.GetString(CryptedText));
                // Output16(CryptedText);

                byte[] DecryptedText = crypter.Decrypt(CryptedText, currentCipher, Text);
                Console.WriteLine("\nРасшифрованный текст:\n" + Encoding.Default.GetString(DecryptedText));
                // Output16(DecryptedText);

            }
            if (act == 2)
            {
                Console.WriteLine("Выберите алгоритм:\n 1. Эль-Гамаль; ");
                int alg = Convert.ToInt32(Console.ReadLine());
                string currentSign;
                switch (alg)
                {
                    case 1:
                        currentSign = "ELGAMAL";
                        break;
                    //case 2:
                    //    currentSign = "ELLIPTICCURVE";
                    //    break;
                    default:
                        return;
                }


                int r, s;
                byte[] Message = Encoding.Default.GetBytes("Всем привет Всем привет Всем привет Всем привет Всем привет Всем привет Всем привет Всем привет ");

                Signer signer = new Signer();

                (r, s) = signer.CreateSignature(Message, "ELGAMAL");

                Console.WriteLine("\nИсходный текст:\n" + Encoding.Default.GetString(Message));
                Console.WriteLine("Подпись: {0}, {1}", r, s);    

                Console.WriteLine();
                bool result = signer.CheckSignature(Message, r, s, "ELGAMAL");

                if (result)
                {
                    Console.WriteLine("Подпись подтверждена.");
                }
                else
                {
                    Console.WriteLine("Подпись не подтверждена, вас обманывают");
                }
            }
            if (act == 3)
            {
                Console.WriteLine("Генератор Блюм-Блюм-Шуба: ");

                Randomizer randomizer = new Randomizer();

                int n = 256;
                int n1 = 50000;
                int[] Nums = new int[n];

                for (int i = 0; i < n; i++)
                {
                    Nums[i] = 0;

                }

                for (int i = 0; i < n1; i++)
                {
                    int r = randomizer.GetRandom("BBS");
                    Nums[r]++;
                }

                for (int i = 0; i < n; i++)
                {
                    Console.WriteLine("{0}: {1}", i, Nums[i]);
                }
            }

        }
    }
}