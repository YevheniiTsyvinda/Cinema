using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Cinema.Models.Reports;
using Cinema.Reports;

namespace Cinema.Factories
{
    public static class ReportsFactory
    {
        public static string GetReportFormView(ReportType type)
        {
            switch (type)
            {
                case ReportType.PotentialRealProfit:
                    return "~/Views/Tickets/Reports/PotentialRealProfitForm.cshtml";
                    
                case ReportType.UnprofitableMovies:
                    return "~/Views/Tickets/Reports/UnprofitableMoviesForm.cshtml";
                    
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static BaseReportForm GetReportFormModel(ControllerContext context, ReportType type,IMapper mapper)
        {
            switch (type)
            {
                case ReportType.PotentialRealProfit:
                    return mapper.Map<PotentialRealProfitReportForm>(context);
                case ReportType.UnprofitableMovies:
                    return mapper.Map<UnprofitableMoviesReportForm>(context);
                   
                default:
                    throw new ArgumentOutOfRangeException(nameof(type),type, null);
            }
        }

        public static IReportBuilder GetReportStrategy(BaseReportForm form,IMapper mapper)
        {
            switch (form.ReportType)
            {
                case ReportType.PotentialRealProfit:
                    {
                        var formModel = (PotentialRealProfitReportForm)form;
                        var strategy = new PotentialRealProfitReportStrategy(mapper)
                        {
                            Parameters = formModel.GetParameters()
                        };
                        return strategy;  
                    }
                    
                case ReportType.UnprofitableMovies:
                    {
                        var formModel = (UnprofitableMoviesReportForm)form;
                        var strategy = new UnprofitableMoviesReportStrategy(mapper)
                        {
                            Parameters = formModel.GetParameters()
                        };
                        return strategy;
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}