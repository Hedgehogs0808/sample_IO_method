using System;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace NPOI_Excel
{
    class CreateFile
    {
        static void Main()
        {
            try
            {
                string fileName = @"C:\Users\hiro1\Desktop\reps\sample_IO_method\data\sample.xlsx"; //ファイル名を決めておく

                var book = WorkbookFactory.Create(fileName); //ブックを作成
                var Isheet = book.GetSheet("input"); //シートの作成
                var Osheet = book.GetSheet("output"); //シートの作成

                int i = 0;
                int j = 0;
                for (i = 0; i < 3; i++)
                {
                    for (j = 0; j < 3; j++)
                    {
                        WriteCell(Osheet, i+1, j+1, getCellValue(Isheet, i, j));
                    }
                }

                var pict = book.GetAllPictures();

                //ブックを保存
                string fileName2 = @"C:\Users\hiro1\Desktop\reps\sample_IO_method\data\sample2.xlsx"; //ファイル名を決めておく
                using (var fs = new FileStream(fileName2, FileMode.OpenOrCreate))
                {
                    //新規ブック作成　エラー無く動作OK
                    //既存ブック読み込み　エラーにて保存できない。
                    book.Write(fs);
                }

                double[] dataX = new double[] { 1, 2, 3, 4, 5 };
                double[] dataY = new double[] { 1, 4, 9, 16, 25 };
                double[] dataY2 = new double[] { 2, 5, 66, 76, 68 };
                var plt = new ScottPlot.Plot(400, 300);
                plt.AddScatter(dataX, dataY);
                plt.AddScatter(dataX, dataY2);
                plt.SaveFig(@"C:\Users\hiro1\Desktop\reps\sample_IO_method\data\quickstart.png");

            }
            //ファイル作成時に例外が発生した場合の処理
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        //指定したセルの値を取得する
        public static string getCellValue(ISheet sheet, int idxColumn, int idxRow)
        {
            var row = sheet.GetRow(idxRow) ?? sheet.CreateRow(idxRow); //指定した行を取得できない時はエラーとならないよう新規作成している
            var cell = row.GetCell(idxColumn) ?? row.CreateCell(idxColumn); //一行上の処理の列版
            string value;

            switch (cell.CellType)
            {
                case CellType.String:
                    value = cell.StringCellValue;
                    break;
                case CellType.Numeric:
                    value = cell.NumericCellValue.ToString();
                    break;
                case CellType.Boolean:
                    value = cell.BooleanCellValue.ToString();
                    break;
                default:
                    value = "Value無し";
                    break;
            }
            Console.WriteLine("value: " + value);
            return value;
        }

        //セル設定(文字列用)
        public static void WriteCell(ISheet sheet, int columnIndex, int rowIndex, string value)
        {
            var row = sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
            var cell = row.GetCell(columnIndex) ?? row.CreateCell(columnIndex);

            cell.SetCellValue(value);
        }

        //セル設定(数値用)
        public static void WriteCell(ISheet sheet, int columnIndex, int rowIndex, double value)
        {
            var row = sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
            var cell = row.GetCell(columnIndex) ?? row.CreateCell(columnIndex);

            cell.SetCellValue(value);
        }

        //セル設定(日付用)
        public static void WriteCell(ISheet sheet, int columnIndex, int rowIndex, DateTime value)
        {
            var row = sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
            var cell = row.GetCell(columnIndex) ?? row.CreateCell(columnIndex);

            cell.SetCellValue(value);
        }

        //書式変更
        public static void WriteStyle(ISheet sheet, int columnIndex, int rowIndex, ICellStyle style)
        {
            var row = sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
            var cell = row.GetCell(columnIndex) ?? row.CreateCell(columnIndex);

            cell.CellStyle = style;
        }

    }
}