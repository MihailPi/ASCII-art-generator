using System.Drawing;

namespace ASCII_art_generator
{
    public static class Extension
    {
        public static void ConvertToGrayscale(this Bitmap bitmapForConvert)
        {
            //  Проходим по картинке
            for (int y = 0; y < bitmapForConvert.Height; y++)
            {
                for (int x = 0; x < bitmapForConvert.Width; x++)
                {   //  Изменяем значение каждого пикселя
                    var pixelColor = bitmapForConvert.GetPixel(x, y);
                    var newColor = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    bitmapForConvert.SetPixel(x, y, 
                        Color.FromArgb(pixelColor.A, newColor, newColor, newColor));
                }
            }
        }

    }
}
