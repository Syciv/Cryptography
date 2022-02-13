using Cryptography.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptography.Algorithms.Crypt
{
    public static class AES
    {
        static int Nk = 8;
        static int Nb = 4;
        static int Nr = 14;

        static byte[,] sBox =
            {
                { 0x63, 0x7c, 0x77, 0x7b, 0xf2, 0x6b, 0x6f, 0xc5, 0x30, 0x01, 0x67, 0x2b, 0xfe, 0xd7, 0xab, 0x76 },
                { 0xca, 0x82, 0xc9, 0x7d, 0xfa, 0x59, 0x47, 0xf0, 0xad, 0xd4, 0xa2, 0xaf, 0x9c, 0xa4, 0x72, 0xc0 },
                { 0xb7, 0xfd, 0x93, 0x26, 0x36, 0x3f, 0xf7, 0xcc, 0x34, 0xa5, 0xe5, 0xf1, 0x71, 0xd8, 0x31, 0x15 },
                { 0x04, 0xc7, 0x23, 0xc3, 0x18, 0x96, 0x05, 0x9a, 0x07, 0x12, 0x80, 0xe2, 0xeb, 0x27, 0xb2, 0x75 },
                { 0x09, 0x83, 0x2c, 0x1a, 0x1b, 0x6e, 0x5a, 0xa0, 0x52, 0x3b, 0xd6, 0xb3, 0x29, 0xe3, 0x2f, 0x84 },
                { 0x53, 0xd1, 0x00, 0xed, 0x20, 0xfc, 0xb1, 0x5b, 0x6a, 0xcb, 0xbe, 0x39, 0x4a, 0x4c, 0x58, 0xcf },
                { 0xd0, 0xef, 0xaa, 0xfb, 0x43, 0x4d, 0x33, 0x85, 0x45, 0xf9, 0x02, 0x7f, 0x50, 0x3c, 0x9f, 0xa8 },
                { 0x51, 0xa3, 0x40, 0x8f, 0x92, 0x9d, 0x38, 0xf5, 0xbc, 0xb6, 0xda, 0x21, 0x10, 0xff, 0xf3, 0xd2 },
                { 0xcd, 0x0c, 0x13, 0xec, 0x5f, 0x97, 0x44, 0x17, 0xc4, 0xa7, 0x7e, 0x3d, 0x64, 0x5d, 0x19, 0x73 },
                { 0x60, 0x81, 0x4f, 0xdc, 0x22, 0x2a, 0x90, 0x88, 0x46, 0xee, 0xb8, 0x14, 0xde, 0x5e, 0x0b, 0xdb },
                { 0xe0, 0x32, 0x3a, 0x0a, 0x49, 0x06, 0x24, 0x5c, 0xc2, 0xd3, 0xac, 0x62, 0x91, 0x95, 0xe4, 0x79 },
                { 0xe7, 0xc8, 0x37, 0x6d, 0x8d, 0xd5, 0x4e, 0xa9, 0x6c, 0x56, 0xf4, 0xea, 0x65, 0x7a, 0xae, 0x08 },
                { 0xba, 0x78, 0x25, 0x2e, 0x1c, 0xa6, 0xb4, 0xc6, 0xe8, 0xdd, 0x74, 0x1f, 0x4b, 0xbd, 0x8b, 0x8a },
                { 0x70, 0x3e, 0xb5, 0x66, 0x48, 0x03, 0xf6, 0x0e, 0x61, 0x35, 0x57, 0xb9, 0x86, 0xc1, 0x1d, 0x9e },
                { 0xe1, 0xf8, 0x98, 0x11, 0x69, 0xd9, 0x8e, 0x94, 0x9b, 0x1e, 0x87, 0xe9, 0xce, 0x55, 0x28, 0xdf },
                { 0x8c, 0xa1, 0x89, 0x0d, 0xbf, 0xe6, 0x42, 0x68, 0x41, 0x99, 0x2d, 0x0f, 0xb0, 0x54, 0xbb, 0x16 }
            };

        static byte[,] invSBox =
        {
            { 0x52, 0x09, 0x6a, 0xd5, 0x30, 0x36, 0xa5, 0x38, 0xbf, 0x40, 0xa3, 0x9e, 0x81, 0xf3, 0xd7, 0xfb },
            { 0x7c, 0xe3, 0x39, 0x82, 0x9b, 0x2f, 0xff, 0x87, 0x34, 0x8e, 0x43, 0x44, 0xc4, 0xde, 0xe9, 0xcb },
            { 0x54, 0x7b, 0x94, 0x32, 0xa6, 0xc2, 0x23, 0x3d, 0xee, 0x4c, 0x95, 0x0b, 0x42, 0xfa, 0xc3, 0x4e },
            { 0x08, 0x2e, 0xa1, 0x66, 0x28, 0xd9, 0x24, 0xb2, 0x76, 0x5b, 0xa2, 0x49, 0x6d, 0x8b, 0xd1, 0x25 },
            { 0x72, 0xf8, 0xf6, 0x64, 0x86, 0x68, 0x98, 0x16, 0xd4, 0xa4, 0x5c, 0xcc, 0x5d, 0x65, 0xb6, 0x92 },
            { 0x6c, 0x70, 0x48, 0x50, 0xfd, 0xed, 0xb9, 0xda, 0x5e, 0x15, 0x46, 0x57, 0xa7, 0x8d, 0x9d, 0x84 },
            { 0x90, 0xd8, 0xab, 0x00, 0x8c, 0xbc, 0xd3, 0x0a, 0xf7, 0xe4, 0x58, 0x05, 0xb8, 0xb3, 0x45, 0x06 },
            { 0xd0, 0x2c, 0x1e, 0x8f, 0xca, 0x3f, 0x0f, 0x02, 0xc1, 0xaf, 0xbd, 0x03, 0x01, 0x13, 0x8a, 0x6b },
            { 0x3a, 0x91, 0x11, 0x41, 0x4f, 0x67, 0xdc, 0xea, 0x97, 0xf2, 0xcf, 0xce, 0xf0, 0xb4, 0xe6, 0x73 },
            { 0x96, 0xac, 0x74, 0x22, 0xe7, 0xad, 0x35, 0x85, 0xe2, 0xf9, 0x37, 0xe8, 0x1c, 0x75, 0xdf, 0x6e },
            { 0x47, 0xf1, 0x1a, 0x71, 0x1d, 0x29, 0xc5, 0x89, 0x6f, 0xb7, 0x62, 0x0e, 0xaa, 0x18, 0xbe, 0x1b },
            { 0xfc, 0x56, 0x3e, 0x4b, 0xc6, 0xd2, 0x79, 0x20, 0x9a, 0xdb, 0xc0, 0xfe, 0x78, 0xcd, 0x5a, 0xf4 },
            { 0x1f, 0xdd, 0xa8, 0x33, 0x88, 0x07, 0xc7, 0x31, 0xb1, 0x12, 0x10, 0x59, 0x27, 0x80, 0xec, 0x5f },
            { 0x60, 0x51, 0x7f, 0xa9, 0x19, 0xb5, 0x4a, 0x0d, 0x2d, 0xe5, 0x7a, 0x9f, 0x93, 0xc9, 0x9c, 0xef },
            { 0xa0, 0xe0, 0x3b, 0x4d, 0xae, 0x2a, 0xf5, 0xb0, 0xc8, 0xeb, 0xbb, 0x3c, 0x83, 0x53, 0x99, 0x61 },
            { 0x17, 0x2b, 0x04, 0x7e, 0xba, 0x77, 0xd6, 0x26, 0xe1, 0x69, 0x14, 0x63, 0x55, 0x21, 0x0c, 0x7d }
        };

        public static byte[,] ShiftRows(byte[,] state, bool enc)
        {
            byte[,] result = state;
            for (int i = 1; i < 4; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (enc)
                    {
                        byte tmp = result[i, 0];
                        for (int k = 0; k < 3; k++)
                        {
                            result[i, k] = result[i, k + 1];
                        }
                        result[i, 3] = tmp;
                    }
                    else
                    {
                        byte tmp = result[i, 3];
                        for (int k = 3; k >= 1; k--)
                        {
                            result[i, k] = result[i, k - 1];
                        }
                        result[i, 0] = tmp;
                    }
                }
            }
            return result;
        }

        public static byte[,] SubBytes(byte[,] state, bool enc)
        {
            byte[,] result = new byte[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    byte row = (byte)(state[i, j] / 0x10);
                    byte col = (byte)(state[i, j] % 0x10);

                    if (enc)
                    {
                        result[i, j] = sBox[row, col];
                    }
                    else
                    {
                        result[i, j] = invSBox[row, col];
                    }
                }
            }
            return result;
        }

        public static byte[,] MixColumns(byte[,] state, bool enc)
        {
            byte[,] result = new byte[4, 4];
            byte s0, s1, s2, s3;
            for (int i = 0; i < 4; i++)
            {
                if (enc)
                {
                    s0 = (byte)(GF.MultBy02(state[0, i]) ^ GF.MultBy03(state[1, i]) ^ state[2, i] ^ state[3, i]);
                    s1 = (byte)(state[0, i] ^ GF.MultBy02(state[1, i]) ^ GF.MultBy03(state[2, i]) ^ state[3, i]);
                    s2 = (byte)(state[0, i] ^ state[1, i] ^ GF.MultBy02(state[2, i]) ^ GF.MultBy03(state[3, i]));
                    s3 = (byte)(GF.MultBy03(state[0, i]) ^ state[1, i] ^ state[2, i] ^ GF.MultBy02(state[3, i]));
                }
                else
                {
                    s0 = (byte)(GF.MultBy0e(state[0,i]) ^ GF.MultBy0b(state[1,i]) ^ GF.MultBy0d(state[2,i]) ^ GF.MultBy09(state[3,i]));
                    s1 = (byte)(GF.MultBy09(state[0,i]) ^ GF.MultBy0e(state[1,i]) ^ GF.MultBy0b(state[2,i]) ^ GF.MultBy0d(state[3,i]));
                    s2 = (byte)(GF.MultBy0d(state[0,i]) ^ GF.MultBy09(state[1,i]) ^ GF.MultBy0e(state[2,i]) ^ GF.MultBy0b(state[3,i]));
                    s3 = (byte)(GF.MultBy0b(state[0,i]) ^ GF.MultBy0d(state[1,i]) ^ GF.MultBy09(state[2,i]) ^ GF.MultBy0e(state[3,i]));
                }

                result[0, i] = s0;
                result[1, i] = s1;
                result[2, i] = s2;
                result[3, i] = s3;
            }
            return result;
        }

        public static byte[,] GenerateKey()
        {
            byte[,] result =
            {
                { 0x01, 0x02, 0x03, 0x04 },
                { 0x01, 0x02, 0x03, 0x04 },
                { 0x01, 0x02, 0x03, 0x04 },
                { 0x01, 0x02, 0x03, 0x04 }
            };
            return result;
        }

        public static byte[,] KeyExpansion(byte[,] key)
        {
            byte[,] rCon =
            {
                { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1B, 0x36},
	            { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00},
	            { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00},
	            { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00}
            };
            byte[,] baseKey = key;
            byte[,] keyShedule = new byte[4, 4 * (Nr + 1)];

            for(int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    keyShedule[i, j] = baseKey[i, j];
                }
            }

            for(int col = Nk; col < Nb * (Nr + 1); col++)
            {
                if(col % Nk == 0)
                {
                    byte[] sPrev = new byte[4];
                    for(int row = 1; row < 4; row++)
                    {
                        sPrev[row-1] = keyShedule[row, col - 1];
                    }
                    sPrev[3] = keyShedule[0, col - 1];

                    for (int i = 0; i < 4; i++)
                    {
                        byte sRow = (byte)(sPrev[i] / 0x10);
                        byte sCol = (byte)(sPrev[i] % 0x10);

                        sPrev[i] = sBox[sRow, sCol];
                    }

                    for(int row = 0; row < 4; row++)
                    {   
                        byte s = (byte)(keyShedule[row, col - 4] ^ sPrev[row] ^ rCon[row, col / Nk - 1]);
                        keyShedule[row, col] = s;
                    }
                }
                else
                {
                    for (int row = 0; row < 4; row++)
                    {
                        byte s = (byte)(keyShedule[row, col - 4] ^ keyShedule[row, col - 1]);
                        keyShedule[row, col] = s;
                    }
                }                
            }

            return keyShedule;
        }

        public static byte[,] AddRoundKey(byte[,] state, byte[,] keyShedule, int round)
        {
            byte s0, s1, s2, s3;
            byte[,] result = new byte[4,4];
            for (int col = 0; col < 4; col++)
            {
                s0 = (byte)(state[0,col] ^ keyShedule[0, Nb * round + col]);
                s1 = (byte)(state[1,col] ^ keyShedule[1, Nb * round + col]);
                s2 = (byte)(state[2,col] ^ keyShedule[2, Nb * round + col]);
                s3 = (byte)(state[3,col] ^ keyShedule[3, Nb * round + col]);

                result[0,col] = s0;
                result[1,col] = s1;
                result[2,col] = s2;
                result[3,col] = s3;
            }
            return result;
        }

        public static byte[] Encrypt(byte[] text)
        {
            int last = 16 - text.Length % 16;

            byte[] cryptedText = new byte[text.Length + last];
            byte[] aText = new byte[cryptedText.Length];

            for(int j=0; j < text.Length; j++)
            {
                aText[j] = text[j];
            }
            for(int j = text.Length; j < aText.Length; j++)
            {
                aText[j] = 0x00;
            }

            byte[,] key = KeyExpansion(GenerateKey());
            int i = 0;

            byte[,] state = new byte[4, 4];
           

            while (i < aText.Length)
            {
                // Формируем блок
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        state[j,k] = aText[i + j + 4*k];
                    }
                }

                for (int round = 1; round < Nr; round++)
                {
                    state = SubBytes(state, true);
                    state = ShiftRows(state, true);
                    state = MixColumns(state, true);
                    state = AddRoundKey(state, key, round);
                }

                state = SubBytes(state, true);
                state = ShiftRows(state, true);
                state = AddRoundKey(state, key, Nr);

                // ????????????????
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        cryptedText[i + j + 4*k] = state[j,k];
                    }
                }
                i += 16;
            }
            return cryptedText;
        }

        public static byte[] Decrypt(byte[] text)
        {
            int last = 16 - text.Length % 16;

            byte[] cryptedText = new byte[text.Length + last];
            byte[] aText = new byte[cryptedText.Length];

            for (int j = 0; j < text.Length; j++)
            {
                aText[j] = text[j];
            }
            for (int j = text.Length; j < aText.Length; j++)
            {
                aText[j] = 0x00;
            }

            byte[,] key = KeyExpansion(GenerateKey());
            int i = 0;
            Console.WriteLine(text.Length);
            byte[,] state = new byte[4, 4];

            while (i < aText.Length)
            {
                // Формируем блок
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        state[j,k] = aText[i + j + 4*k];
                    }
                }

                for (int round = Nr; round > 1; round--)
                {
                    state = ShiftRows(state, false);
                    state = SubBytes(state, false);
                    state = AddRoundKey(state, key, round);
                    state = MixColumns(state, false);        
                }

                state = ShiftRows(state, false);
                state = SubBytes(state, false);     
                state = AddRoundKey(state, key, 1);

                // ????????????????
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        cryptedText[i + j + 4*k] = state[j,k];
                    }
                }
                i += 16;
            }
            return cryptedText;
        }
    }
}
