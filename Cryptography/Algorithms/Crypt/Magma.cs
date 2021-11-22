using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptography.Algorithms.Crypt
{
    public static class Magma
    {
        static void Output16(byte[] text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(Convert.ToString(text[i], 16) + " ");
            }
            // Console.WriteLine();
        }
        public static byte[][] GenerateKeys()
        {
            // Тестовый ключ из ГОСТ 34.12
            byte[] key =
                { 0xff, 0xee, 0xdd, 0xcc, 0xbb, 0xaa, 0x99, 0x88, 0x77, 0x66, 0x55, 0x44, 0x33, 0x22, 0x11, 0x00,
                  0xf0, 0xf1, 0xf2, 0xf3, 0xf4, 0xf5, 0xf6, 0xf7, 0xf8, 0xf9, 0xfa, 0xfb, 0xfc, 0xfd, 0xfe, 0xff};

            byte[][] iterKeys = new byte[32][];
            for (int i = 0; i < 32; i++)
            {
                iterKeys[i] = new byte[4];
            }

            // Формирование раундовых ключей
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    iterKeys[i][j] = key[4 * i + j];
                    iterKeys[i + 8][j] = key[4 * i + j];
                    iterKeys[i + 16][j] = key[4 * i + j];
                    iterKeys[i + 24][j] = key[32 - 4 * (i + 1) + j];
                }
            }

            return iterKeys;
        }

        // Преобразование в число из блока
        public static UInt32 BytesToInt(byte[] bytes)
        {
            UInt32 result = 0, mult=1;
            for (int i = bytes.Length - 1; i >= 0; i--, mult *= 256)
            {
                result += mult * bytes[i];
            }
            return result;
        }

        // Преобразование из блока в число
        public static byte[] IntToBytes(UInt32 num)
        {
            byte[] bytes = new byte[4];

            var hihi = BitConverter.GetBytes(num);
            for(int i=0; i < hihi.Length; i++)
            {
                bytes[i] = hihi[3 - i];
            }

            return bytes;
        }

        // Сумма двух чисел по модулю 2^32
        public static byte[] SummMod32(byte[] a, byte[] b)
        {
            byte[] res;
            UInt32 A = BytesToInt(a);
            UInt32 B = BytesToInt(b);
            A += B;
            res = IntToBytes(A);
            return res;
        }

        // Преобразование T (нелинейное биективное преобразование)
        public static byte[] Magma_T(byte[] block)
        {
            // Таблица из ГОСТ 34.12
            int[,] Pi =
            {
            { 1,7,14,13,0,5,8,3,4,15,10,6,9,12,11,2},
            { 8,14,2,5,6,9,1,12,15,4,11,0,13,10,3,7},
            { 5,13,15,6,9,2,12,10,11,7,8,1,4,3,14,0},
            { 7,15,5,10,8,1,6,13,0,9,3,14,11,4,2,12},
            { 12,8,2,1,13,4,15,6,7,0,10,5,3,14,9,11},
            { 11,3,5,8,2,15,10,13,14,1,7,4,12,9,6,0},
            { 6,8,2,3,9,10,5,12,1,14,4,7,11,13,0,15},
            { 12,4,6,2,10,5,11,9,14,8,13,7,0,3,15,1}
            };

            int leftPart, rightPart;
            for (int i = 0; i < 4; i++)
            {
                leftPart = (block[i] & 0xf0) / 0x10;
                rightPart = (block[i] & 0xf);

                leftPart = Pi[i * 2, leftPart];
                rightPart = Pi[i * 2 + 1, rightPart];

                block[i] = (byte)((leftPart * 0x10) + rightPart);
            }

            return block;
        }

        // Преобразование g 
        public static byte[] Magma_g(byte[] rightBlock, byte[] key)
        {
            UInt32 intResultRight;
            rightBlock = SummMod32(rightBlock, key);
            rightBlock = Magma_T(rightBlock);
            intResultRight = BytesToInt(rightBlock);

            // Циклический сдвиг на 11 бит влево
            intResultRight = (intResultRight << 11) + ((intResultRight & 0xffe00000) / 0x200000);

            rightBlock = IntToBytes(intResultRight);
            return rightBlock;
        }
        
        // Функция зашифрования / расшифрования (encrypt = true - зашифрование, false - расшифрование)
        public static byte[] Encrypt(byte[] text, bool encrypt)
        {
            var iterKeys = GenerateKeys();
            int last = 8 - text.Length % 8;

            byte[] cryptedText = new byte[text.Length + last];

            byte[] leftBlock = new byte[4];
            byte[] rightBlock = new byte[4];
            byte[] rightBlockStart = new byte[4];

            UInt32 intResultRight, intResultLeft;

            int keyIndex;
            int i = 0;

            // Обработка текста по блокам
            while (i < text.Length)
            {
                // Формирование левого и правого блоков

                for (int j = 0, k = i; j < 4; j++, k++)
                {
                    leftBlock[j] = (k < text.Length) ? text[k] : (byte)0x00;
                    rightBlock[j] = (k + 4 < text.Length) ? text[k + 4] : (byte)0x00;
                }

                for (int round = 0; round < 32; round++)
                {
                    // Обработка правого блока

                    // При расшифровании ключи применяются в обратном порядке
                    keyIndex = encrypt ? round : 31 - round;

                    rightBlockStart = rightBlock;

                    rightBlock = Magma_g(rightBlock, iterKeys[keyIndex]);
                    intResultRight = BytesToInt(rightBlock);

                    // Обработка левого блока
                    intResultLeft = BytesToInt(leftBlock) ^ intResultRight;
                    leftBlock = IntToBytes(intResultLeft);             

                    // Перемещение блоков
                    if (round != 31)
                    {
                        rightBlock = leftBlock;
                        leftBlock = rightBlockStart;
                    }
                    else
                    {
                        rightBlock = rightBlockStart;
                    }
                }
                for (int j = 0, k = i; j < 4; j++, k++)
                {
                    cryptedText[k] = leftBlock[j];
                    cryptedText[k + 4] = rightBlock[j];
                }

                i += 8;
            }
            return cryptedText;
        }
    }
}
