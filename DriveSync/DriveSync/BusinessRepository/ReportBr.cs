using Azure;
using Dapper;
using DriveSync.BusinessRepository.IBusinessRepository;
using DriveSync.DatabaseLayer.Dapper.Fines;
using DriveSync.DatabaseLayer.DBContext;
using DriveSync.DTOS.Request;
using DriveSync.DTOS.Response;
using DriveSync.Handlers.Constants;
using DriveSync.Services.IServices;
using Microsoft.Data.SqlClient;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace DriveSync.BusinessRepository
{
    public class ReportBr(DriveSyncDbContext mDriveSyncDbContext, ISqlService mSqlService):IReportBr
    {
        public async Task<List<FineReportResponse>> GetFineReportData(FineReportRequest request)
        {
            string intervaloffset = CommonConstants.INTERVAL_OFFSET.Replace("$offset", $"{request.Offset}");
            List<FineReportResponse> response;
            using (SqlConnection sqlConnection = mSqlService.GetSqlConnection())
            {
                await sqlConnection.OpenAsync();
                response = (await
                    sqlConnection.QueryAsync<FineReportResponse>(FineResource.FineReport,
                    new
                    {
                        status = request.Status,
                        fromDate = request.FromDate,
                        toDate = request.ToDate,
                        offset = intervaloffset,
                        searchText = request.searchText
                    })).ToList();
                await sqlConnection.CloseAsync();
            }
           
            return response;
        }


        public async Task<MemoryStream> GetFineReport(List<FineReportResponse> data)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var excelStream = new MemoryStream();
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Fine Report");
                int startRow = 1;

                worksheet.Cells[startRow, 1].Value = "Report - Fine Report";
                worksheet.Cells[startRow, 1, startRow, 10].Merge = true;
                worksheet.Cells[startRow, 1, startRow, 10].Style.Font.Size = 14;
                worksheet.Cells[startRow, 1, startRow, 10].Style.Font.Bold = true;
                worksheet.Cells[startRow, 1, startRow, 10].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells[startRow, 1, startRow, 10].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DarkBlue);
                worksheet.Cells[startRow, 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells[startRow, 1, startRow, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                startRow++;

                worksheet.Cells[startRow, 1].Value = "S.No";
                worksheet.Cells[startRow, 2].Value = "StaffId";
                worksheet.Cells[startRow, 3].Value = "DriverName";
                worksheet.Cells[startRow, 4].Value = "VehicleNumber";
                worksheet.Cells[startRow, 5].Value = "FineNumber";
                worksheet.Cells[startRow, 6].Value = "Amount";
                worksheet.Cells[startRow, 7].Value = "ViolationType";
                worksheet.Cells[startRow, 8].Value = "Reason";
                worksheet.Cells[startRow, 9].Value = "IssuedDate";
                worksheet.Cells[startRow, 10].Value = "Status";

                var headerRange = worksheet.Cells[startRow, 1, startRow, 10];
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Font.Size = 11;
                headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                headerRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                headerRange.Style.Border.BorderAround(ExcelBorderStyle.Thick);

                int row = startRow + 1;
                int serialNo = 1;

                foreach (var item in data)
                {
                    worksheet.Cells[row, 1].Value = serialNo;
                    worksheet.Cells[row, 2].Value = item.StaffIdNum;
                    worksheet.Cells[row, 3].Value = item.Name;
                    worksheet.Cells[row, 4].Value = item.PlateNumber;
                    worksheet.Cells[row, 5].Value = item.FineNumber;
                    worksheet.Cells[row, 6].Value = item.Amount;
                    worksheet.Cells[row, 7].Value = item.ViolationType;
                    worksheet.Cells[row, 8].Value = item.Reason;
                    worksheet.Cells[row, 9].Value = item.IssuedDate.ToString("dd: MM : yyyy \n HH : mm") ?? "-";
                    worksheet.Cells[row, 10].Value = item.Paid == 1 ? "Paid" : "Not Paid";
                    worksheet.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells.Style.ShrinkToFit = true;
                    worksheet.Cells.Style.Font.Size = 10;

                    serialNo++;
                    row++;
                }

                var fullDataRange = worksheet.Cells[worksheet.Dimension.Address];

                fullDataRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                fullDataRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                fullDataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                fullDataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                fullDataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                fullDataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                worksheet.Cells.AutoFitColumns();
                worksheet.Column(2).Width += 10;
                package.SaveAs(excelStream);
            }
            excelStream.Position = 0;
            return excelStream;
        }

        public async Task<byte[]> GetFineReportPdf(List<FineReportResponse> data)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(1, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11).FontFamily(Fonts.Verdana));

                    // Header Section
                    page.Header().Row(row =>
                    {
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Text("Fine Report").FontSize(24).Bold().FontColor(Colors.Blue.Medium);
                            col.Item().Text($"Generated on: {DateTime.Now:dd/MM/yyyy}").FontSize(10).Italic();
                        });
                    });

                    page.Content().PaddingTop(10).Table(table =>
                    {
                        // 10 Columns total
                        table.ColumnsDefinition(cols =>
                        {
                            cols.ConstantColumn(25); // S.No
                            cols.RelativeColumn(2);  // StaffId
                            cols.RelativeColumn(3);  // DriverName
                            cols.RelativeColumn(2);  // VehicleNumber
                            cols.RelativeColumn(2);  // FineNumber
                            cols.RelativeColumn(1.5f); // Amount
                            cols.RelativeColumn(2);  // ViolationType
                            //cols.RelativeColumn(2);  // Reason
                            cols.RelativeColumn(2);  // IssuedDate
                            cols.RelativeColumn(1.5f); // Status
                        });

                        // Header Styling
                        table.Header(h =>
                        {
                            // Define helper for header cell styles
                            IContainer HeaderStyle(IContainer container) =>
                                container.DefaultTextStyle(x => x.SemiBold().FontColor(Colors.White))
                                         .PaddingVertical(5)
                                         .PaddingHorizontal(2)
                                         .Background(Colors.Blue.Medium)
                                         .AlignCenter()
                                         .AlignMiddle();
                                         //.Border(0.5f)
                                         //.BorderColor(Colors.Black);

                            h.Cell().Element(HeaderStyle).Text("S.NO");
                            h.Cell().Element(HeaderStyle).Text("Staff_Id");
                            h.Cell().Element(HeaderStyle).Text("Driver");
                            h.Cell().Element(HeaderStyle).Text("Vehicle");
                            h.Cell().Element(HeaderStyle).Text("Fine\nNumber");
                            h.Cell().Element(HeaderStyle).Text("Amount");
                            h.Cell().Element(HeaderStyle).Text("Violation\nType");
                            //h.Cell().Element(HeaderStyle).Text("Reason");
                            h.Cell().Element(HeaderStyle).Text("FineIssued\nDate");
                            h.Cell().Element(HeaderStyle).Text("Status");
                        });

                        // Data Rows
                        int i = 1;
                        foreach (var item in data)
                        {
                            // Alternating background for rows
                            var bgColor = i % 2 == 0 ? Colors.Grey.Lighten4 : Colors.White;

                            IContainer CellStyle(IContainer container) =>
                                container.Padding(5)
                                         .Background(bgColor)
                                         .BorderBottom(0.5f)
                                         .BorderColor(Colors.Grey.Lighten2)
                                         .AlignMiddle();

                            table.Cell().Element(CellStyle).AlignCenter().Text(i++.ToString()).FontSize(10);
                            table.Cell().Element(CellStyle).Text(item.StaffIdNum).FontSize(10);
                            table.Cell().Element(CellStyle).Text(item.Name).FontSize(10);
                            table.Cell().Element(CellStyle).AlignCenter().Text(item.PlateNumber).FontSize(10);
                            table.Cell().Element(CellStyle).AlignCenter().Text(item.FineNumber).FontSize(10);
                            table.Cell().Element(CellStyle).AlignRight().Text($"{item.Amount:N2}").FontSize(10);
                            table.Cell().Element(CellStyle).Text(item.ViolationType).FontSize(10);
                            //table.Cell().Element(CellStyle).Text(item.Reason);

                            // Fixed Date Format (MM is months, mm is minutes)
                            table.Cell().Element(CellStyle).AlignCenter().Text(item.IssuedDate.ToString("dd/MM/yyyy")).FontSize(10);

                            // Status styling (Green for Paid, Red for Not Paid)
                            var statusColor = item.Paid == 1 ? Colors.Green.Medium : Colors.Red.Medium;
                            table.Cell().Element(CellStyle).AlignCenter().Text(item.Paid == 1 ? "Paid" : "Unpaid").FontColor(statusColor).Bold().FontSize(10);
                        }
                    });

                    // Footer Section
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                    });
                });
            }).GeneratePdf();
        }
    }
}
