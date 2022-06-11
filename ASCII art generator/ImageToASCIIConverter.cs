using System.Drawing;

namespace ASCII_art_generator
{
    public class ImageToASCIIConverter
    {
        //  Символы для перевода в ascii
        private readonly char[] _asciiTable = { '.', ',', ':', '+', '*', '?', '#', '%', '$', '@' };
        private readonly char[] _asciiTableInverted = { '@', '$', '%', '#', '?', '*', '+', ':', ',', '.' };
        //  Ссылка на картинку
        private readonly Bitmap _bitmap;
        public ImageToASCIIConverter(Bitmap bitmapForConvert)
        {
            _bitmap = bitmapForConvert;
        }

        //  Шоб в консоли и в файле выглядело нормально 
        public char[][] ConvertForConsole()
        {
            return ConverToAscii(_asciiTable);
        }
        public char[][] ConvertForFile()
        {
            return ConverToAscii(_asciiTableInverted);
        }

        private char[][] ConverToAscii(char[] asciiTable)
        {
            var convertionArr = new char[_bitmap.Height][];
  
            for (int y=0; y < _bitmap.Height; y++)
            {   
                convertionArr[y] = new char[_bitmap.Width];

                for (int x=0; x < _bitmap.Width; x++)
                {   //  Переводим градацию серого в символ из таблицы (R G B - одинаковые)
                    int indexMap = (int)MapDiapazon(_bitmap.GetPixel(x, y).R, 0, 255, 0, asciiTable.Length-1);
                    convertionArr[y][x] = asciiTable[indexMap];
                }
            }

            return convertionArr;   
        }

        //  Пропорциональный перевода из одного диапазона в другой
        private float MapDiapazon(float valueToMap, float startOld, float finOld, float startNew, float finNew)
        {
            return ((valueToMap - startOld) / (finOld - startOld)) * (finNew - startNew) + startNew;
        }
    }
}
