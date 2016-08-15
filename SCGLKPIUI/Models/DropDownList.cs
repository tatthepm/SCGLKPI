using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using BOL;
using BLL;

namespace SCGLKPIUI.Models
{
    public class DropDownList
    {
        private BaseBs objBs;
        public DropDownList()
        {
            objBs = new BaseBs();
        }

        public List<DropdownlistViewModels> GetDropDownList(string filtername)
        {

            List<DropdownlistViewModels> viewModel = new List<DropdownlistViewModels>();
            switch (filtername)
            {

                case "Department":
                    var ddlDept = (from d in objBs.menuTableBs.GetAll()
                                   select new
                                   {
                                       Id = d.DepartmentId,
                                       Name = d.DepartmentName
                                   }).Distinct().OrderBy(x => x.Name);
                    foreach (var item in ddlDept)
                    {
                        DropdownlistViewModels model = new DropdownlistViewModels();
                        model.Id = item.Id;
                        model.Name = item.Name;
                        viewModel.Add(model);
                    }
                    return viewModel;

                case "Section":
                    var ddlSec = (from d in objBs.menuTableBs.GetAll()
                                  select new
                                  {
                                      Id = d.SectionId,
                                      Name = d.SectionName
                                  }).Distinct().OrderBy(x => x.Name);
                    foreach (var item in ddlSec)
                    {
                        DropdownlistViewModels model = new DropdownlistViewModels();
                        model.Id = item.Id;
                        model.Name = item.Name;
                        viewModel.Add(model);
                    }
                    return viewModel;

                default:
                    break;
            }
            return viewModel;
        }

        public List<DropdownlistViewModels> GetDropDownListSegment()
        {
            List<DropdownlistViewModels> viewModel = new List<DropdownlistViewModels>();
            viewModel.Add(new DropdownlistViewModels() { Id="FTL",Name="FTL"});
            viewModel.Add(new DropdownlistViewModels() { Id = "BULK", Name = "BULK" });
            viewModel.Add(new DropdownlistViewModels() { Id = "CONSO", Name = "CONSO" });
            return viewModel;
        }

        public List<DropdownlistViewModels> GetDropDownListAcceptedMonth(string filtername)
        {

            List<DropdownlistViewModels> viewModel = new List<DropdownlistViewModels>();
            switch (filtername)
            {
                case "Matname":
                    var ddlMatName = (from m in objBs.ontimeAcceptMonthBs.GetAll()
                                      where !String.IsNullOrEmpty(m.MatName)
                                      select new
                                      {
                                          Id = m.MatFriGrp,
                                          Name = m.MatName
                                      }).Distinct();
                    foreach (var item in ddlMatName)
                    {
                        DropdownlistViewModels model = new DropdownlistViewModels();
                        model.Id = item.Id;
                        model.Name = item.Name;
                        viewModel.Add(model);
                    }
                    return viewModel;

                case "Year":
                    var ddlYear = (from y in objBs.ontimeAcceptMonthBs.GetAll()
                                   select new
                                   {
                                       Id = y.Year,
                                       Name = y.Year
                                   }).Distinct().OrderBy(x => x.Id);
                    foreach (var item in ddlYear)
                    {
                        DropdownlistViewModels model = new DropdownlistViewModels();
                        model.Id = item.Id;
                        model.Name = item.Name;
                        viewModel.Add(model);
                    }
                    return viewModel;

                case "Month":

                    List<string> months = Enumerable.Range(1, 12).Select(m => DateTimeFormatInfo.InvariantInfo.GetAbbreviatedMonthName(m)).ToList();

                    for (int i = 0; i < months.Count; i++)
                    {
                        DropdownlistViewModels model = new DropdownlistViewModels();
                        model.Id = Convert.ToString(i + 1);
                        model.Name = months[i];
                        viewModel.Add(model);
                    }
                    return viewModel;

                default:
                    break;
            }
            return viewModel;
        }

        public List<DropdownlistViewModels> GetDropDownListMatNameDaily(string filtername)
        {

            List<DropdownlistViewModels> viewModel = new List<DropdownlistViewModels>();
            switch (filtername)
            {

                case "ontime-tendered":
                    return viewModel;

                case "ontime-accepted":
                    var ddlMatName = (from m in objBs.ontimeAcceptBs.GetAll()
                                      where !String.IsNullOrEmpty(m.MatName)
                                      select new
                                      {
                                          Id = m.MatFriGrp,
                                          Name = m.MatName,
                                      }).Distinct();
                    foreach (var item in ddlMatName)
                    {
                        DropdownlistViewModels model = new DropdownlistViewModels();
                        model.Id = item.Id;
                        model.Name = item.Name;
                        viewModel.Add(model);
                    }
                    return viewModel;

                case "ontime-inbound":
                    return viewModel;

                case "ontime-outbound":
                    return viewModel;

                case "ontime-docreturn":
                    return viewModel;
                case "ontime-delivery":
                    var ddlMatNameDelivery = (from m in objBs.ontimeDeliveryBs.GetAll()
                                              where !String.IsNullOrEmpty(m.MatName)
                                              select new
                                              {
                                                  Id = m.MatFriGrp,
                                                  Name = m.MatName,
                                              }).Distinct();
                    foreach (var item in ddlMatNameDelivery)
                    {
                        DropdownlistViewModels model = new DropdownlistViewModels();
                        model.Id = item.Id;
                        model.Name = item.Name;
                        viewModel.Add(model);
                    }
                    return viewModel;

                default:
                    break;
            }
            return viewModel;
        }

        public List<DropdownlistViewModels> GetDropDownListTenderedMonth(string filtername)
        {

            List<DropdownlistViewModels> viewModel = new List<DropdownlistViewModels>();
            switch (filtername)
            {
                case "Matname":
                    var ddlMatName = (from m in objBs.ontimeTenderMonthBs.GetAll()
                                      where !String.IsNullOrEmpty(m.MatName)
                                      select new
                                      {
                                          Id = m.MatFriGrp,
                                          Name = m.MatName
                                      }).Distinct();
                    foreach (var item in ddlMatName)
                    {
                        DropdownlistViewModels model = new DropdownlistViewModels();
                        model.Id = item.Id;
                        model.Name = item.Name;
                        viewModel.Add(model);
                    }
                    return viewModel;

                case "Year":
                    var ddlYear = (from y in objBs.ontimeTenderMonthBs.GetAll()
                                   select new
                                   {
                                       Id = y.Year,
                                       Name = y.Year
                                   }).Distinct().OrderBy(x => x.Id);
                    foreach (var item in ddlYear)
                    {
                        DropdownlistViewModels model = new DropdownlistViewModels();
                        model.Id = item.Id;
                        model.Name = item.Name;
                        viewModel.Add(model);
                    }
                    return viewModel;

                case "Month":

                    List<string> months = Enumerable.Range(1, 12).Select(m => DateTimeFormatInfo.InvariantInfo.GetAbbreviatedMonthName(m)).ToList();

                    for (int i = 0; i < months.Count; i++)
                    {
                        DropdownlistViewModels model = new DropdownlistViewModels();
                        model.Id = Convert.ToString(i + 1);
                        model.Name = months[i];
                        viewModel.Add(model);
                    }
                    return viewModel;

                default:
                    break;
            }
            return viewModel;
        }

        public List<DropdownlistViewModels> GetDropDownListInboundMonth(string filtername)
        {

            List<DropdownlistViewModels> viewModel = new List<DropdownlistViewModels>();
            switch (filtername)
            {
                case "Matname":
                    var ddlMatName = (from m in objBs.ontimeInboundMonthBs.GetAll()
                                      where !String.IsNullOrEmpty(m.MatName)
                                      select new
                                      {
                                          Id = m.MatFriGrp,
                                          Name = m.MatName
                                      }).Distinct();
                    foreach (var item in ddlMatName)
                    {
                        DropdownlistViewModels model = new DropdownlistViewModels();
                        model.Id = item.Id;
                        model.Name = item.Name;
                        viewModel.Add(model);
                    }
                    return viewModel;

                case "Year":
                    var ddlYear = (from y in objBs.ontimeInboundMonthBs.GetAll()
                                   select new
                                   {
                                       Id = y.Year,
                                       Name = y.Year
                                   }).Distinct().OrderBy(x => x.Id);
                    foreach (var item in ddlYear)
                    {
                        DropdownlistViewModels model = new DropdownlistViewModels();
                        model.Id = item.Id;
                        model.Name = item.Name;
                        viewModel.Add(model);
                    }
                    return viewModel;

                case "Month":

                    List<string> months = Enumerable.Range(1, 12).Select(m => DateTimeFormatInfo.InvariantInfo.GetAbbreviatedMonthName(m)).ToList();

                    for (int i = 0; i < months.Count; i++)
                    {
                        DropdownlistViewModels model = new DropdownlistViewModels();
                        model.Id = Convert.ToString(i + 1);
                        model.Name = months[i];
                        viewModel.Add(model);
                    }
                    return viewModel;

                default:
                    break;
            }
            return viewModel;
        }

        public List<DropdownlistViewModels> GetDropDownListOutboundMonth(string filtername)
        {

            List<DropdownlistViewModels> viewModel = new List<DropdownlistViewModels>();
            switch (filtername)
            {
                case "Matname":
                    var ddlMatName = (from m in objBs.ontimeOutboundMonthBs.GetAll()
                                      where !String.IsNullOrEmpty(m.MatName)
                                      select new
                                      {
                                          Id = m.MatFriGrp,
                                          Name = m.MatName
                                      }).Distinct();
                    foreach (var item in ddlMatName)
                    {
                        DropdownlistViewModels model = new DropdownlistViewModels();
                        model.Id = item.Id;
                        model.Name = item.Name;
                        viewModel.Add(model);
                    }
                    return viewModel;

                case "Year":
                    var ddlYear = (from y in objBs.ontimeOutboundMonthBs.GetAll()
                                   select new
                                   {
                                       Id = y.Year,
                                       Name = y.Year
                                   }).Distinct().OrderBy(x => x.Id);
                    foreach (var item in ddlYear)
                    {
                        DropdownlistViewModels model = new DropdownlistViewModels();
                        model.Id = item.Id;
                        model.Name = item.Name;
                        viewModel.Add(model);
                    }
                    return viewModel;

                case "Month":

                    List<string> months = Enumerable.Range(1, 12).Select(m => DateTimeFormatInfo.InvariantInfo.GetAbbreviatedMonthName(m)).ToList();

                    for (int i = 0; i < months.Count; i++)
                    {
                        DropdownlistViewModels model = new DropdownlistViewModels();
                        model.Id = Convert.ToString(i + 1);
                        model.Name = months[i];
                        viewModel.Add(model);
                    }
                    return viewModel;

                default:
                    break;
            }
            return viewModel;
        }

        public List<DropdownlistViewModels> GetDropDownListDocReturnMonth(string filtername)
        {

            List<DropdownlistViewModels> viewModel = new List<DropdownlistViewModels>();
            switch (filtername)
            {
                case "Matname":
                    var ddlMatName = (from m in objBs.ontimeDocReturnMonthBs.GetAll()
                                      where !String.IsNullOrEmpty(m.MatName)
                                      select new
                                      {
                                          Id = m.MatFriGrp,
                                          Name = m.MatName
                                      }).Distinct();
                    foreach (var item in ddlMatName)
                    {
                        DropdownlistViewModels model = new DropdownlistViewModels();
                        model.Id = item.Id;
                        model.Name = item.Name;
                        viewModel.Add(model);
                    }
                    return viewModel;

                case "Year":
                    var ddlYear = (from y in objBs.ontimeDocReturnMonthBs.GetAll()
                                   select new
                                   {
                                       Id = y.Year,
                                       Name = y.Year
                                   }).Distinct().OrderBy(x => x.Id);
                    foreach (var item in ddlYear)
                    {
                        DropdownlistViewModels model = new DropdownlistViewModels();
                        model.Id = item.Id;
                        model.Name = item.Name;
                        viewModel.Add(model);
                    }
                    return viewModel;

                case "Month":

                    List<string> months = Enumerable.Range(1, 12).Select(m => DateTimeFormatInfo.InvariantInfo.GetAbbreviatedMonthName(m)).ToList();

                    for (int i = 0; i < months.Count; i++)
                    {
                        DropdownlistViewModels model = new DropdownlistViewModels();
                        model.Id = Convert.ToString(i + 1);
                        model.Name = months[i];
                        viewModel.Add(model);
                    }
                    return viewModel;

                default:
                    break;
            }
            return viewModel;
        }

        public List<DropdownlistViewModels> GetDropDownListDeliveryMonth(string filtername)
        {

            List<DropdownlistViewModels> viewModel = new List<DropdownlistViewModels>();
            switch (filtername)
            {
                case "Matname":
                    var ddlMatName = (from m in objBs.dOM_SAP_MATFRIGRPBs.GetAll()
                                      where !String.IsNullOrEmpty(m.MATNAME)
                                      select new
                                      {
                                          Id = m.MATFRIGRP,
                                          Name = m.MATNAME
                                      }).Distinct();
                    foreach (var item in ddlMatName)
                    {
                        DropdownlistViewModels model = new DropdownlistViewModels();
                        model.Id = item.Id;
                        model.Name = item.Name;
                        viewModel.Add(model);
                    }
                    return viewModel;

                case "Year":
                    var ddlYear = (from y in objBs.ontimeDeliveryMonthBs.GetAll()
                                   select new
                                   {
                                       Id = y.Year,
                                       Name = y.Year
                                   }).Distinct().OrderBy(x => x.Id);
                    foreach (var item in ddlYear)
                    {
                        DropdownlistViewModels model = new DropdownlistViewModels();
                        model.Id = item.Id;
                        model.Name = item.Name;
                        viewModel.Add(model);
                    }
                    return viewModel;

                case "Month":

                    List<string> months = Enumerable.Range(1, 12).Select(m => DateTimeFormatInfo.InvariantInfo.GetAbbreviatedMonthName(m)).ToList();

                    for (int i = 0; i < months.Count; i++)
                    {
                        DropdownlistViewModels model = new DropdownlistViewModels();
                        model.Id = Convert.ToString(i + 1);
                        model.Name = months[i];
                        viewModel.Add(model);
                    }
                    return viewModel;

                default:
                    break;
            }
            return viewModel;
        }
    }
}