using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Cinema.Factories;
using Cinema.Models.Reports;

namespace Cinema.Binders
{
    public class BaseReportFormModelBinder : DefaultModelBinder
    {
        private readonly IMapper _mapper;
        public BaseReportFormModelBinder()
        {
            _mapper = DependencyResolver.Current.GetService<IMapper>(); //позволяет достать с DI-контейнера конкретную реализацию
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (!(base.BindModel(controllerContext, bindingContext)is BaseReportForm model))
            {
                return null;
            }
            return ReportsFactory.GetReportFormModel(controllerContext, model.ReportType, _mapper);//
            
        }
    }
}