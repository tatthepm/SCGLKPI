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
                    var ddlDept = (from d in objBs.dWH_ONTIME_SHIPMENTBs.GetAll()
                                   select new
                                   {
                                       Id = d.DEPARTMENT_ID,
                                       Name = d.DEPARTMENT_Name
                                   }).Take(50000).Distinct().Where(x => !String.IsNullOrEmpty(x.Name)).OrderBy(x => x.Name);
                    foreach (var item in ddlDept)
                    {
                        DropdownlistViewModels model = new DropdownlistViewModels();
                        model.Id = item.Id;
                        model.Name = item.Name;
                        viewModel.Add(model);
                    }
                    return viewModel;
                case "Section":
                    var ddlSec = (from d in objBs.dWH_ONTIME_SHIPMENTBs.GetAll()
                                  //where !String.IsNullOrEmpty(d.SECTION_NAME)
                                  select new
                                  {
                                      Id = d.SECTION_ID,
                                      Name = d.SECTION_NAME
                                  }).Take(50000).Distinct().Where(x => !String.IsNullOrEmpty(x.Name)).OrderBy(x => x.Name);
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
            viewModel.Add(new DropdownlistViewModels() { Id = "FTL CE", Name = "FTL Ceramic" });
            viewModel.Add(new DropdownlistViewModels() { Id = "BULK", Name = "BULK" });
            viewModel.Add(new DropdownlistViewModels() { Id = "BULK RECYCLING", Name = "BULK Recycling" });
            viewModel.Add(new DropdownlistViewModels() { Id = "CONSO EXT", Name = "CONSO External" });
            viewModel.Add(new DropdownlistViewModels() { Id = "CONSO INT", Name = "CONSO Inside" });
            viewModel.Add(new DropdownlistViewModels() { Id = "CONSO NE", Name = "CONSO Next Day" });
            viewModel.Add(new DropdownlistViewModels() { Id = "CONSO WF", Name = "CONSO Ware & Fitting" });
            return viewModel;
        }

        public List<DropdownlistViewModels> GetDropDownListTenderedMonth(string filtername)
        {

            List<DropdownlistViewModels> viewModel = new List<DropdownlistViewModels>();
            switch (filtername)
            {
                case "Matname":
                    var ddlMatName = (from m in objBs.dWH_ONTIME_DNBs.GetAll()
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

                case "ShippingPoint":
                    var ddlShpPnt = (from m in objBs.dWH_ONTIME_DNBs.GetAll()
                                     //where !String.IsNullOrEmpty(m.SHPPOINT)
                                      select new
                                      {
                                          Id = m.SHPPOINT,
                                          Name = m.SHPPOINT
                                      }).Take(50000).Distinct().Where(x => !String.IsNullOrEmpty(x.Name)).OrderBy(x => x.Name);
                    foreach (var item in ddlShpPnt)
                    {
                        DropdownlistViewModels model = new DropdownlistViewModels();
                        model.Id = item.Id;
                        model.Name = item.Name;
                        viewModel.Add(model);
                    }
                    return viewModel;

                case "ShipTo":
                    var ddlShpTo = (from m in objBs.dWH_ONTIME_SHIPMENTBs.GetAll()
                                    //where !String.IsNullOrEmpty(m.LAST_SHPG_LOC_NAME)
                                     select new
                                     {
                                         Id = m.LAST_SHPG_LOC_NAME,
                                         Name = m.LAST_SHPG_LOC_NAME
                                     }).Take(50000).Distinct().Where(x => !String.IsNullOrEmpty(x.Name)).OrderBy(x => x.Name);
                    foreach (var item in ddlShpTo)
                    {
                        DropdownlistViewModels model = new DropdownlistViewModels();
                        model.Id = item.Id;
                        model.Name = item.Name;
                        viewModel.Add(model);
                    }
                    return viewModel;

                case "TruckType":
                    var ddlTruckType = (from m in objBs.dWH_ONTIME_SHIPMENTBs.GetAll()
                                        //where !String.IsNullOrEmpty(m.TRUCK_TYPE)
                                    select new
                                    {
                                        Id = m.TRUCK_TYPE,
                                        Name = m.TRUCK_TYPE
                                    }).Take(50000).Distinct().Where(x => !String.IsNullOrEmpty(x.Name)).OrderBy(x => x.Name);
                    foreach (var item in ddlTruckType)
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