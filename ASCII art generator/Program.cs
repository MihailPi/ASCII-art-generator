using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Linq;

namespace ASCII_art_generator
{
    class Program
    {
        //  Компенсатор ширины символа в консоли
        private const double WIDTH_OFFSET = 2.5;
        //  Ширина арта
        private const int MAX_WIDTH = 120;

        [STAThread]
        static void Main(string[] args)
        {
            //  Открываем диалог для выбора файла с фильтром
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "Images | *.bmp; *.png; *.jpg; *.JPEG"
            };

            Console.WriteLine("Для старта нажми Enter!");
            
            while (true)
            {
                Console.ReadLine();
                //  Если не выбрал картинку
                if (fileDialog.ShowDialog() != DialogResult.OK)
                    continue;

                //  Очищаем для отображения нового арта
                Console.Clear();
                
                var bitmap=new Bitmap(fileDialog.FileName);
                //  Рескейлим
                bitmap = ResizeBitmap(bitmap);
                //  Переводим в оттенки серого
                bitmap.ConvertToGrayscale();
                //  Переобразовываем картинку в ascii
                var converter = new ImageToASCIIConverter(bitmap);
                var rowsImage = converter.ConvertForConsole();

                foreach (var row in rowsImage)
                {
                    Console.WriteLine(row);
                }
                //  Курсор в начало
                Console.SetCursorPosition(0, 0);

                //  Путь для сохранения рядом с картинкой
                string pathImageToTxt = Path.ChangeExtension(fileDialog.FileName, null)+"_ASCII.txt";
                Console.WriteLine("Арт сохранен по пути - " + pathImageToTxt);
                //  Сохраняем в txt
                var rowsImageForFile = converter.ConvertForFile();
                File.WriteAllLines(pathImageToTxt, rowsImageForFile.Select(rows => new string(rows)));
            }
        }

        static Bitmap ResizeBitmap(Bitmap imageForResize)
        {
            //  Пропорционально меняем высоту
            var newHeight = imageForResize.Height / WIDTH_OFFSET * MAX_WIDTH / imageForResize.Width;
            //  Если размер картинки меньше максимального то оставляем как есть
            if (imageForResize.Width > MAX_WIDTH || imageForResize.Height > newHeight)
                imageForResize = new Bitmap(imageForResize, new Size(MAX_WIDTH, (int)newHeight));

            return imageForResize;
        }

    }

}
