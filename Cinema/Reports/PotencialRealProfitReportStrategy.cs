using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AutoMapper;
using NPOI.SS.UserModel;

namespace Cinema.Reports
{
    public class PotencialRealProfitReportStrategy : ReportBaseStrategy<PotencialRealProfitReportModel>
    {
        public PotencialRealProfitReportStrategy(IMapper mapper):base(mapper)
        {

        }
        protected override PotencialRealProfitReportModel GetDataModel()
        {
            var parameters = new[]
            {
                new SqlParameter("@DateFrom",(DateTime)Parameters["DateFrom"]),
                new SqlParameter("@DateTo",(DateTime)Parameters["DateTo"])
            };

            var reportRows = DataBaseUtil.Execute<PotentialRealProfitReportRow>("PotentialRealProfit", parameters);
            return new PotencialRealProfitReportModel
            {
                Rows = reportRows
            };
        }

        protected override string InternalGetDownloadFileName()
        {
            return "PotencialRealProfitReport";
        }

        protected override string InternalGetTemplateFileName()
        {
            return "PotencialRealProfitReport.xlsx";
        }

        protected override void ProcessWorkBook(IWorkbook workbook, PotencialRealProfitReportModel model)
        {
            var sheet = workbook.GetSheetAt(0);
            var rowIndex = 1;
            foreach (var row in model.Rows)
            {
                var documentRow = sheet.CreateRow(rowIndex);
                documentRow.CreateCell(SummaryColumns.MovieName).SetCellValue(row.Name);
                documentRow.CreateCell(SummaryColumns.GuaranteedProfit).SetCellValue(row.GuaranteedProfit);
                documentRow.CreateCell(SummaryColumns.PotencialProfit).SetCellValue(row.PotencialProfit);
            }
            sheet.AutoSizeColumn(SummaryColumns.MovieName);
            sheet.AutoSizeColumn(SummaryColumns.GuaranteedProfit);
            sheet.AutoSizeColumn(SummaryColumns.PotencialProfit);
        }
        private static class SummaryColumns//класс описывает структуру отчёта в excel
        {
            public const int MovieName = 0;
            public const int GuaranteedProfit = 1;
            public const int PotencialProfit = 2;
        }
    }
}