using IntentCADEMR.DAL;
using IntentCADEMR.Models;
using IntentCADEMR.Util;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntentCADEMR.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            UserModel user = UserHelper.GetLoggedInUser();
            if (user != null && user.IsReportAdmin)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public ActionResult ExportToExcel()
        {
            UserModel user = UserHelper.GetLoggedInUser();
            if (user == null || !user.IsReportAdmin)
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                AdminRepository repo = new AdminRepository();
                List<AdminReportModel> list = repo.GetAdminReport();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                //need to use nuget manager to install EPPlus
                ExcelPackage pck = new ExcelPackage();




                AddWorksheet(pck, "IntentCADEMR", list, true);



                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=AdminReport.xlsx");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.BinaryWrite(pck.GetAsByteArray());
                Response.Flush();
                Response.End();
                return View("Reports");
            }
            catch (Exception e)
            {

                return View("Reports");
            }

        }

        private void AddWorksheet(ExcelPackage pck, string wsName, List<AdminReportModel> list, bool isReportAdmin)
        {
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add(wsName);
            ws.Cells["A1"].Value = "UserID";
            ws.Cells["B1"].Value = "FirstName";
            ws.Cells["C1"].Value = "LastName";
            ws.Cells["D1"].Value = "ClinicName";
            ws.Cells["E1"].Value = "MailingAddress";
            ws.Cells["F1"].Value = "City";
            ws.Cells["G1"].Value = "Province";
            ws.Cells["H1"].Value = "PhoneNumber";

            ws.Cells["I1"].Value = "FaxNumber";
            ws.Cells["J1"].Value = "Gender";
            ws.Cells["K1"].Value = "MedicalSpecialty";

            ws.Cells["L1"].Value = "MedicalSpecialtyOther";
            ws.Cells["M1"].Value = "Practice";
            ws.Cells["N1"].Value = "PracticeOther";

            ws.Cells["O1"].Value = "PracticeYears";
            ws.Cells["P1"].Value = "PatientNumbers";

            ws.Cells["Q1"].Value = "ProportionPAD";
            ws.Cells["R1"].Value = "ProportionCD";
            ws.Cells["S1"].Value = "ProportionDM";
            ws.Cells["T1"].Value = "ProportionRD";
            ws.Cells["U1"].Value = "ProportionHF";
            ws.Cells["V1"].Value = "ProportionCurrentSmoker";

            ws.Cells["W1"].Value = "FamiliarCOMPASSTrial";
            ws.Cells["X1"].Value = "SomeCAD_PAD";
            ws.Cells["Y1"].Value = "SomeCAD_2RiskFactors";
            ws.Cells["Z1"].Value = "MostCAD_PAD";
            ws.Cells["AA1"].Value = "MostCAD_2RiskFactors";
            ws.Cells["AB1"].Value = "NoImpact";

            ws.Cells["AC1"].Value = "ASA";
            ws.Cells["AD1"].Value = "Statin";
            ws.Cells["AE1"].Value = "ACEI";
            ws.Cells["AF1"].Value = "OAA";
            ws.Cells["AG1"].Value = "Rivaroxaban";


         
            ws.Cells["AH1"].Value = "LackEvidenceBasedGuidelines";
            ws.Cells["AI1"].Value = "LackApplicabilityGuidelines";
            ws.Cells["AJ1"].Value = "LackTime";
            ws.Cells["AK1"].Value = "OrganizationalInstitutional";
            ws.Cells["AL1"].Value = "ReimbursementFinancial";
            ws.Cells["AM1"].Value = "PatientAdherence";
            ws.Cells["AN1"].Value = "TreatmentAdverseEvents";
            ws.Cells["AO1"].Value = "NoneAbove";
            ws.Cells["AP1"].Value = "EducationalPracticalSolutions";
            ws.Cells["AQ1"].Value = "SubmittedDate";




            int rowStart = 2;
            foreach (var item in list)
            {
                ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("white")));
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.UserID;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.FirstName;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.LastName; //3
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.ClinicName; //3
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.MailingAddress;
                // ws.Cells[string.Format("D{0}", rowStart)].Value = item.Visit1CompletionDate.HasValue ? item.Visit1CompletionDate.Value.ToString("yyyy-MM-dd") : ""; //4
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.City; //5
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.Province;
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.PhoneNumber;
                // ws.Cells[string.Format("G{0}", rowStart)].Value = item.Visit2CompletionDate.HasValue ? item.Visit2CompletionDate.Value.ToString("yyyy-MM-dd") : ""; //7
                ws.Cells[string.Format("I{0}", rowStart)].Value = item.FaxNumber; //8
                ws.Cells[string.Format("J{0}", rowStart)].Value = item.Gender;
                ws.Cells[string.Format("K{0}", rowStart)].Value = item.MedicalSpecialty;
                //  ws.Cells[string.Format("J{0}", rowStart)].Value = item.Visit3CompletionDate.HasValue ? item.Visit3CompletionDate.Value.ToString("yyyy-MM-dd") : ""; //10
                ws.Cells[string.Format("L{0}", rowStart)].Value = item.MedicalSpecialtyOther; //11
                ws.Cells[string.Format("M{0}", rowStart)].Value = item.Practice;
                ws.Cells[string.Format("N{0}", rowStart)].Value = item.PracticeOther;
                //ws.Cells[string.Format("M{0}", rowStart)].Value = item.Visit4CompletionDate.HasValue ? item.Visit4CompletionDate.Value.ToString("yyyy-MM-dd") : ""; //13
                ws.Cells[string.Format("0{0}", rowStart)].Value = item.PracticeYears;
                ws.Cells[string.Format("P{0}", rowStart)].Value = item.PatientNumbers;
                ws.Cells[string.Format("Q{0}", rowStart)].Value = item.ProportionPAD;
                ws.Cells[string.Format("R{0}", rowStart)].Value = item.ProportionCD;
                ws.Cells[string.Format("S{0}", rowStart)].Value = item.ProportionDM;
                ws.Cells[string.Format("T{0}", rowStart)].Value = item.ProportionRD;
                ws.Cells[string.Format("U{0}", rowStart)].Value = item.ProportionHF;

                ws.Cells[string.Format("V{0}", rowStart)].Value = item.ProportionCurrentSmoker;
                ws.Cells[string.Format("W{0}", rowStart)].Value = item.FamiliarCOMPASSTrial;
                ws.Cells[string.Format("X{0}", rowStart)].Value = item.SomeCAD_PAD;
                ws.Cells[string.Format("Y{0}", rowStart)].Value = item.SomeCAD_2RiskFactors;
                ws.Cells[string.Format("Z{0}", rowStart)].Value = item.MostCAD_PAD;
                ws.Cells[string.Format("AA{0}", rowStart)].Value = item.MostCAD_2RiskFactors;
                ws.Cells[string.Format("AB{0}", rowStart)].Value = item.NoImpact;
                ws.Cells[string.Format("AC{0}", rowStart)].Value = item.ASA;
                ws.Cells[string.Format("AD{0}", rowStart)].Value = item.Statin;
                ws.Cells[string.Format("AE{0}", rowStart)].Value = item.ACEI;
                ws.Cells[string.Format("AF{0}", rowStart)].Value = item.OAA;
                ws.Cells[string.Format("AG{0}", rowStart)].Value = item.Rivaroxaban;




                ws.Cells[string.Format("AH{0}", rowStart)].Value = item.LackEvidenceBasedGuidelines;
                ws.Cells[string.Format("AI{0}", rowStart)].Value = item.LackApplicabilityGuidelines;
                ws.Cells[string.Format("AJ{0}", rowStart)].Value = item.LackTime;
                ws.Cells[string.Format("AK{0}", rowStart)].Value = item.OrganizationalInstitutional;
                ws.Cells[string.Format("AL{0}", rowStart)].Value = item.ReimbursementFinancial;
                ws.Cells[string.Format("AM{0}", rowStart)].Value = item.PatientAdherence;
                ws.Cells[string.Format("AN{0}", rowStart)].Value = item.TreatmentAdverseEvents;
                ws.Cells[string.Format("AO{0}", rowStart)].Value = item.NoneAbove;
                ws.Cells[string.Format("AP{0}", rowStart)].Value = item.EducationalPracticalSolutions;
                ws.Cells[string.Format("AQ{0}", rowStart)].Value = item.SubmittedDate.HasValue ? item.SubmittedDate.Value.ToString("yyyy-MM-dd") : null;




                rowStart = rowStart + 1;
            }


            ws.Cells["A:AZ"].AutoFitColumns();
            ws.View.FreezePanes(2, 1); //freeze first row
        }

    }
}