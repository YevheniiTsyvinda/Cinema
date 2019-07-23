using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AutoMapper;
using NPOI.SS.UserModel;

namespace Cinema.Reports
{
    public class UnprofitableMoviesReportStrategy : ReportBaseStrategy<UnprofitableMoviesReportModel>
    {
        public UnprofitableMoviesReportStrategy(IMapper mapper):base(mapper)
        {

        }
        protected override UnprofitableMoviesReportModel GetDataModel()
        {
            var parametr = new[]
            {
                 new SqlParameter("@DateFrom",(DateTime)Parameters["DateFrom"]),
                new SqlParameter("@DateTo",(DateTime)Parameters["DateTo"]),
                 new SqlParameter("@Threshold",(float)Parameters["Threshold"])
           };

            var reportRows = DataBaseUtil.Execute<UnprofitableMoviesReportRow>("UnprofitableMovies", parametr);

            return new UnprofitableMoviesReportModel
            {
                Rows = reportRows
            };
        }

        protected override string InternalGetDownloadFileName()
        {
            return "UnprofitableMoviesReport";
        }

        protected override string InternalGetTemplateFileName()
        {
            return "UnprofitableMoviesReport.xlsx";
        }

        protected override void ProcessWorkBook(IWorkbook workbook, UnprofitableMoviesReportModel model)
        {
            var sheet = workbook.GetSheetAt(0);
            var rowIndex = 1;
            foreach (var row in model.Rows)
            {
                var documentRow = sheet.CreateRow(rowIndex);
                documentRow.CreateCell(SummaryColumns.MovieName).SetCellValue(row.MovieName);
                documentRow.CreateCell(SummaryColumns.Profit).SetCellValue(row.Profit);
                rowIndex++;
            }
            sheet.AutoSizeColumn(SummaryColumns.MovieName);
            sheet.AutoSizeColumn(SummaryColumns.Profit);
           
        }

        private static class SummaryColumns
        {
            public const int MovieName = 0;
            public const int Profit = 1;

        }
    }
}