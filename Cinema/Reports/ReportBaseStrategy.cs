using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using AutoMapper;
using Cinema.Utils;
using NPOI.SS.UserModel;//библиотека для работы с excel

namespace Cinema.Reports
{
    public abstract class ReportBaseStrategy<T> : IReportBuilder
    {

        protected SqlDataBaseUtil DataBaseUtil { get; set; }
        public Dictionary<string, object> Parameters { get; set; } 

       
        protected ReportBaseStrategy(IMapper mapper)
        {
            DataBaseUtil = new SqlDataBaseUtil(mapper);
            Parameters = new Dictionary<string, object>();
        }
        public string BuildReport()
        {
            var model = GetDataModel();

            CreateReportsDirectoryIfNotExists(); //проверяем существует ли деректория в которую будем сохранять отчёты, если  нет создаём

            var fileName = Path.Combine(Constants.ReportsDirectory, string.Concat(InternalGetDownloadFileName(), DateTime.Now.ToString("_yyyyMMdd-hhmmss"), GetTargetExtension()));

            InternalBuildReport(fileName, model); //берёт файл, заполняет его данными и сохраняет

            return GetFileNameUrl(fileName); // возвращаем ссылку на скачивание файла
        }

        private string GetFileNameUrl(string filePath) //получение линка к файлу
        {
            var approot = HostingEnvironment.ApplicationPhysicalPath.TrimEnd('\\');
            return filePath.Replace(approot, string.Empty).Replace('\\', '/');
        }

        private void InternalBuildReport(string fileName, T model)
        {
            var templatePath = HostingEnvironment.MapPath(Constants.ReportsDirectory); //получение пути к отчётам. путь установлен в web.config
            if (string.IsNullOrEmpty(templatePath))
            {
                throw new ApplicationException($"Unable to map path \'{templatePath}'\".");
            }

            using (var templateFileStream = new FileStream(templatePath,FileMode.Open,FileAccess.Read))
            {
                var workbook = WorkbookFactory.Create(templateFileStream); //создаём еxсel файл

                ProcessWorkBook(workbook, model);// заполняем файл даными

                SaveWorkbook(workbook, fileName);
            }
        }

        private void SaveWorkbook(IWorkbook workbook, string fileName)
        {
            var targetFilePath = HostingEnvironment.MapPath(fileName);
            if (string.IsNullOrEmpty(targetFilePath))
            {
                throw new ApplicationException($"Unable to map path \'{fileName}\'");
            }
            if (File.Exists(targetFilePath))// проверяем существует ли отчёт. если да, удаляем
            {
                File.Delete(targetFilePath);
            }
            using (var outputFileStream = new FileStream(targetFilePath,FileMode.CreateNew))//если в один момент строится несколько отчётов с одинаковым именнем
            {
                workbook.Write(outputFileStream);
                outputFileStream.Close();
            }
        }

        protected abstract void ProcessWorkBook(IWorkbook workbook, T model);

        private object GetTargetExtension()//получаем формат файла
        {
            return Path.GetExtension(TemplateFileName);
        }
        protected string TemplateFileName => InternalGetTemplateFileName();
        protected abstract string InternalGetDownloadFileName(); //имя файла
        protected abstract string InternalGetTemplateFileName(); //имя шаблона

        private static void CreateReportsDirectoryIfNotExists()
        {
            try
            {
                var reportsPath = HostingEnvironment.MapPath(Constants.ReportsDirectory); //получение пути к отчётам. путь установлен в web.config
                if (string.IsNullOrEmpty(reportsPath))
                {
                    throw new ApplicationException($"Unable to map path '{reportsPath}'");
                }

                if (!Directory.Exists(reportsPath))//проверяем наличие папки
                {
                    Directory.CreateDirectory(reportsPath);
                }
            }
            catch (Exception e)
            {

                throw new ApplicationException($"Unable to create Reports directory.", e);
            }
        }

        protected abstract T GetDataModel();
    }

}