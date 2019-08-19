﻿namespace NWaves.Audio
{
    /// <summary>
    /// Static class providing methods for conversion between PCM bytes and float[] data.
    /// </summary>
    public static class ByteConverter
    {
        /// <summary>
        /// Convert Pcm_8bit to floats
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="floats"></param>
        /// <param name="normalize"></param>
        public static void ToFloats8Bit(byte[] bytes, float[][] floats, bool normalize = true)
        {
            var channelCount = floats.Length;

            if (normalize)
            {
                for (int n = 0; n < channelCount; n++)
                {
                    for (int i = n, j = 0; i < bytes.Length; i += channelCount, j++)
                    {
                        floats[n][j] = (bytes[i] - 128) / 128f;
                    }
                }
            }
            else
            {
                for (int n = 0; n < channelCount; n++)
                {
                    for (int i = n, j = 0; i < bytes.Length; i += channelCount, j++)
                    {
                        floats[n][j] = bytes[i];
                    }
                }
            }
        }

        /// <summary>
        /// Convert floats to Pcm_8bit
        /// </summary>
        /// <param name="floats"></param>
        /// <param name="bytes"></param>
        /// <param name="normalized"></param>
        public static void FromFloats8Bit(float[][] floats, byte[] bytes, bool normalized = true)
        {
            var channelCount = floats.Length;

            if (normalized)
            {
                for (int n = 0; n < channelCount; n++)
                {
                    for (int i = n, j = 0; j < floats[n].Length; i += channelCount, j++)
                    {
                        bytes[i] = (byte)(floats[n][j] * 128 + 128);
                    }
                }
            }
            else
            {
                for (int n = 0; n < channelCount; n++)
                {
                    for (int i = n, j = 0; j < floats[n].Length; i += channelCount, j++)
                    {
                        bytes[i] = (byte)floats[n][j];
                    }
                }
            }
        }

        /// <summary>
        /// Convert Pcm_16bit to floats (little-endian or big-endian)
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="floats"></param>
        /// <param name="normalize"></param>
        /// <param name="bigEndian"></param>
        public static void ToFloats16Bit(byte[] bytes, float[][] floats, bool normalize = true, bool bigEndian = false)
        {
            var channelCount = floats.Length;
            var step = channelCount * 2;

            if (bigEndian)
            {
                if (normalize)
                {
                    for (int n = 0; n < channelCount; n++)
                    {
                        for (int i = 2 * n, j = 0; i < bytes.Length; i += step, j++)
                        {
                            floats[n][j] = (bytes[i] << 8 | bytes[i + 1]) / 32768f;
                        }
                    }
                }
                else
                {
                    for (int n = 0; n < channelCount; n++)
                    {
                        for (int i = 2 * n, j = 0; i < bytes.Length; i += step, j++)
                        {
                            floats[n][j] = bytes[i] << 8 | bytes[i + 1];
                        }
                    }
                }
            }
            else
            {
                if (normalize)
                {
                    for (var n = 0; n < channelCount; n++)
                    {
                        for (int i = 2 * n, j = 0; i < bytes.Length; i += step, j++)
                        {
                            floats[n][j] = (bytes[i] | bytes[i + 1] << 8) / 32768f;
                        }
                    }
                }
                else
                {
                    for (var n = 0; n < channelCount; n++)
                    {
                        for (int i = 2 * n, j = 0; i < bytes.Length; i += step, j++)
                        {
                            floats[n][j] = bytes[i] | bytes[i + 1] << 8;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Convert floats to Pcm_16bit (little-endian or big-endian)
        /// </summary>
        /// <param name="floats"></param>
        /// <param name="bytes"></param>
        /// <param name="normalized"></param>
        /// <param name="bigEndian"></param>
        public static void FromFloats16Bit(float[][] floats, byte[] bytes, bool normalized = true, bool bigEndian = false)
        {
            var channelCount = floats.Length;
            var step = channelCount * 2;

            if (bigEndian)
            {
                if (normalized)
                {
                    for (int n = 0; n < channelCount; n++)
                    {
                        for (int i = 2 * n, j = 0; j < floats[n].Length; i += step, j++)
                        {
                            var s = (int)(floats[n][j] * 32768);

                            bytes[i] = (byte)(s >> 8);
                            bytes[i + 1] = (byte)s;
                        }
                    }
                }
                else
                {
                    for (int n = 0; n < channelCount; n++)
                    {
                        for (int i = 2 * n, j = 0; j < floats[n].Length; i += step, j++)
                        {
                            var s = (int)floats[n][j];

                            bytes[i] = (byte)(s >> 8);
                            bytes[i + 1] = (byte)s;
                        }
                    }
                }
            }
            else
            {
                if (normalized)
                {
                    for (int n = 0; n < channelCount; n++)
                    {
                        for (int i = 2 * n, j = 0; j < floats[n].Length; i += step, j++)
                        {
                            var s = (int)(floats[n][j] * 32768);

                            bytes[i] = (byte)s;
                            bytes[i + 1] = (byte)(s >> 8);
                        }
                    }
                }
                else
                {
                    for (int n = 0; n < channelCount; n++)
                    {
                        for (int i = 2 * n, j = 0; j < floats[n].Length; i += step, j++)
                        {
                            var s = (int)floats[n][j];

                            bytes[i] = (byte)s;
                            bytes[i + 1] = (byte)(s >> 8);
                        }
                    }
                }
            }
        }
    }
}