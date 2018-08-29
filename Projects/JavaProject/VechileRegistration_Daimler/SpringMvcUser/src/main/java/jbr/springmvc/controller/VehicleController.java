package jbr.springmvc.controller;

import java.text.ParseException;
import java.util.ArrayList;
import java.util.Date;
import java.util.Iterator;
import java.util.List;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.servlet.ModelAndView;

import com.fasterxml.jackson.core.JsonProcessingException;

import jbr.springmvc.model.DriverVO;
import jbr.springmvc.model.SlotsVO;
import jbr.springmvc.model.VehicleInfo;
import jbr.springmvc.model.VehicleVO;
import jbr.springmvc.model.YardVO;
import jbr.springmvc.service.VehicleService;
import jbr.springmvc.utility.Utility;

@Controller
public class VehicleController {

  @Autowired
  VehicleService vehicalService;

  @RequestMapping(value = "/yardIn", method = RequestMethod.GET)
  public ModelAndView showYardIn(HttpServletRequest request, HttpServletResponse response) {
    ModelAndView mav = new ModelAndView("yardIn");
    StringBuffer table = new StringBuffer();
    List<YardVO> yards = vehicalService.getYardInfo();
    
    for (YardVO yard : yards) {
      table.append("<tr>");
      table.append("<td>").append(yard.getEntry_No()).append("</td>");
      table.append("<td>").append(yard.getVehicle().getVehicle_No()).append("</td>");
      table.append("<td>").append(yard.getVehicle().getSupplier()).append("</td>");
      table.append("<td>").append(yard.getReporting_Time()).append("</td>");
      table.append("<td>").append(yard.getIn_Time()).append("</td>");
      table.append("<td>").append(yard.getHalt_Time()).append("</td>");
      table.append("<td>").append(yard.getCurrent_Place()).append("</td>");
      table.append("<td>").append(yard.getParking_Allotment()).append("</td>");
      table.append("<td>").append(yard.getDriver().getContact_No()).append("</td>");
      table.append("</tr>");
    }
    
    mav.addObject("yards",table.toString());
    mav.addObject("yardsLst",yards);
    return mav;
  }
  
  private Boolean isVehicleSlotted(String vehicle) {
    List<YardVO> yards = vehicalService.getYardInfo();
    
    for (YardVO yardVO : yards) {
      System.out.println(yardVO.getVehicle().getVehicle_No());
      if(yardVO.getVehicle().getVehicle_No().equals(vehicle)) {
        if(yardVO.getParking_Allotment()!=null) {
        return true;
        }
      }
    }
    return false;
  }

  @RequestMapping(value = "/vehicleList", method = RequestMethod.GET)
  public ModelAndView showVehicleList(HttpServletRequest request, HttpServletResponse response) {
    ModelAndView mav = new ModelAndView("vehicleList");
    StringBuffer table = new StringBuffer();
    List<VehicleVO> vehicles = vehicalService.getVehicles();
    
    for (VehicleVO vehicle : vehicles) {
      table.append("<tr>");
      table.append("<td>").append(vehicle.getYard().getEntry_No()).append("</td>");
      table.append("<td>").append(vehicle.getVehicle_No()).append("</td>");
      table.append("<td>").append(vehicle.getDriver().getId()).append("</td>");
      table.append("<td>").append(vehicle.getSupplier_Name()).append("</td>");
      table.append("<td>").append(vehicle.getSupplier()).append("</td>");
      table.append("<td>").append(vehicle.getDriver().getName()).append("</td>");
      table.append("<td>").append(vehicle.getDriver().getContact_No()).append("</td>");
      table.append("<td>").append(vehicle.getYard().getIn_Time()).append("</td>");
      table.append("<td>").append(vehicle.getRemarks()).append("</td>");
      table.append("<td>").append(0).append("</td>");
      table.append("<td>").append(vehicle.getYard().getCurrent_Place()).append("</td>");
      table.append("<td><button class=\"btn btn-warning btn-icon\"><i style=\"margin-right:0px;\" class=\"icofont icofont-pencil\"></i></button>&nbsp;<button class=\"btn btn-info btn-icon\"><i style=\"margin-right:0px;\"  class=\"icofont icofont-eye-alt\"></i></button> &nbsp;<button class=\"btn btn-danger btn-icon\"><i style=\"margin-right:0px;\"  class=\"icofont icofont-delete-alt\"></i></button>&nbsp;<button class=\"btn btn-primary btn-icon\"><i style=\"margin-right:0px;\"  class=\"icofont icofont-print\"></i></button></td>");
      table.append("</tr>");
    }
    
    mav.addObject("yards",table.toString());
    mav.addObject("vehicles",vehicles);
    return mav;
  }
  
  @RequestMapping(value="getVehicle", method = RequestMethod.GET)
  public @ResponseBody String getVehicle(@RequestParam("vehicle_No") String vehicle_No) throws  ParseException {    
 
      VehicleVO vehicle = vehicalService.getVehicle(vehicle_No);
      DriverVO driver = getDriverForVehicle(vehicle.getVehicle_No());
      StringBuffer json = new StringBuffer("{");
      json.append(Utility.constructJson("FC", Utility.compareDates(new Date(), vehicle.getFC_Valid_Date()))).append(",");
      json.append(Utility.constructJson("FCDate", Utility.dateToString(vehicle.getFC_Valid_Date()))).append(",");
      json.append(Utility.constructJson("RC", Utility.compareDates(new Date(), vehicle.getRCBOOK_Valid_Date()))).append(",");
      json.append(Utility.constructJson("RCDate", Utility.dateToString(vehicle.getRCBOOK_Valid_Date()))).append(",");
      json.append(Utility.constructJson("ins", Utility.compareDates(new Date(), vehicle.getInsurance_Valid_Date()))).append(",");
      json.append(Utility.constructJson("insDate", Utility.dateToString(vehicle.getInsurance_Valid_Date()))).append(",");
      json.append(Utility.constructJson("pollCert", Utility.compareDates(new Date(), vehicle.getPollution_Certificate_Valid_Date()))).append(",");
      json.append(Utility.constructJson("pollCertDate", Utility.dateToString(vehicle.getPollution_Certificate_Valid_Date()))).append(",");
      json.append(Utility.constructJson("vehicle_No", vehicle.getVehicle_No())).append(",");
      json.append(Utility.constructJson("supplier", vehicle.getSupplier())).append(",");
      json.append(Utility.constructJson("truck_No", vehicle.getTruck_No())).append(",");
      json.append(Utility.constructJson("driver_Name", driver.getName())).append(",");
      json.append(Utility.constructJson("licence_validity", driver.getLicense_Validity())).append(",");
      json.append(Utility.constructJson("status_update", vehicle.getStatus_update())).append(",");
      json.append(Utility.constructJson("vehicleslotted", isVehicleSlotted(vehicle_No))).append(",");
      
      json.append(Utility.constructJson("vehicle_type", vehicle.getVehicle_Type())).append(",");
      json.append(Utility.constructJson("ReportingTime", vehicle.getReportingTime())).append(",");
      json.append(Utility.constructJson("supplier_Name", vehicle.getSupplier_Name())).append(",");
      json.append(Utility.constructJson("passType", vehicle.getPass_Type())).append(",");
      json.append(Utility.constructJson("passTypeValidityDate", vehicle.getPass_Validity_Date()));
      
      if(json.toString().contains("danger")) {
        json.append(",").append(Utility.constructJson("alert", "true"));
      }else {
        json.append(",").append(Utility.constructJson("alert", "false"));
      }
      
      json.append("}");
      
      
      return json.toString();
  }
  
  private DriverVO getDriverForVehicle(String vehicle_No) {
    List<DriverVO> driver = vehicalService.getDrivers();
    for (DriverVO driverVO : driver) {
      if(driverVO.getVehicle().getVehicle_No().equals(vehicle_No)) {
        return driverVO;
      }
    }
    return new DriverVO();
  }
  
  @RequestMapping(value="getDriver", method = RequestMethod.GET)
  public @ResponseBody String getDriver(@RequestParam("driver_Pass") String driver_Pass) throws  ParseException {    
 
      DriverVO driver = vehicalService.getDriver(driver_Pass);
      StringBuffer json = new StringBuffer("{");
      json.append(Utility.constructJson("id",driver.getId())).append(",");
      json.append(Utility.constructJson("vehicle_No",driver.getVehicle().getVehicle_No())).append(",");
      json.append(Utility.constructJson("name",driver.getName())).append(",");
      json.append(Utility.constructJson("criticalPart",driver.getVehicle().getCriticalPart())).append(",");
      json.append(Utility.constructJson("supplier",driver.getVehicle().getSupplier())).append(",");
      json.append(Utility.constructJson("licence",driver.getLicense_Validity())).append(",");;
      
      json.append(Utility.constructJson("contactno",driver.getContact_No())).append(",");;
      json.append(Utility.constructJson("licenceno",driver.getLicense_No())).append(",");;
      json.append(Utility.constructJson("passtype",driver.getPass_Type())).append(",");;
      json.append(Utility.constructJson("passExpiryDate",driver.getPass_Expiry_Date()));

      json.append("}");
      
      return json.toString();
  }
  
  @RequestMapping(value = "/slots", method = { RequestMethod.POST, RequestMethod.GET})
  public ModelAndView showSlots(HttpServletRequest request, HttpServletResponse response) throws JsonProcessingException {
    ModelAndView mav = new ModelAndView("slots");    
    
    List<SlotsVO> slotsLst = vehicalService.getSlots();
    List<VehicleVO> vehicles = vehicalService.getVehicles();
    List<VehicleVO> vehiclesFinal = new ArrayList<VehicleVO>();
    DriverVO driver = new DriverVO();
    driver.setId(request.getParameter("txtDriverno"));
    driver.getVehicle().setVehicle_No(request.getParameter("txtVehicleno"));
    
    vehicalService.mapDriverToVehicle(driver);
    
    for (VehicleVO vehicle : vehicles) {
      for (SlotsVO slot : slotsLst) {
        if (slot.getVehical_No() != null) {
          if (slot.getVehical_No().equals(vehicle.getVehicle_No())) {
            vehiclesFinal.add(vehicle);
          }
        }

      }
    }
    
    String json = Utility.convertObjToJson(slotsLst);
    System.out.println(request.getParameter("txtVehicleno"));
    mav.addObject("vehicle_No",request.getParameter("txtVehicleno"));
    mav.addObject("slots",json);
    mav.addObject("slotLst",slotsLst);
    mav.addObject("vehicles",vehiclesFinal);
    return mav;
  }
  
  @RequestMapping(value = "/vehicleRegistration", method = RequestMethod.GET)
  public ModelAndView vehicleRegistration(HttpServletRequest request, HttpServletResponse response) throws JsonProcessingException {
    ModelAndView mav = showVehicleList(request, response) ;
    mav.setViewName("vehicleRegistration");
    return mav;
  }
  
  @RequestMapping(value = "/driverRegistration", method = RequestMethod.GET)
  public ModelAndView driverRegistration(HttpServletRequest request, HttpServletResponse response) throws JsonProcessingException {
    ModelAndView mav = new ModelAndView("driverRegistration");
    mav.addObject("drivers",vehicalService.getDrivers());
    
    return mav;
  }
  
  @RequestMapping(value = "/updateServiceEntry", method = RequestMethod.POST)
  public ModelAndView updateServiceEntry(HttpServletRequest request, HttpServletResponse response) throws ParseException {
    ModelAndView mav = new ModelAndView("redirect:/yardIn");
    VehicleVO vehicle = new VehicleVO();
    vehicle.setVehicle_No(request.getParameter("txtVehicleno"));
    DriverVO driver = new DriverVO();
    driver.setId(request.getParameter("txtDriverno"));
    driver.getVehicle().setVehicle_No(request.getParameter("txtVehicleno"));
    
    vehicalService.mapDriverToVehicle(driver);
    vehicalService.updateServiceEntry(vehicle,"SERVICE ROAD","");
    
    return mav;
    
  }

  
  @RequestMapping(value="/updateYardEntry", method = RequestMethod.POST)
  public ModelAndView updateYardEntry(HttpServletRequest request, HttpServletResponse response) throws ParseException {    
    
    ModelAndView mav = new ModelAndView("redirect:/yardIn");
    VehicleVO vehicle = new VehicleVO();
    vehicle.setVehicle_No(request.getParameter("hdnVehicle_No"));
    System.out.println(request.getParameter("hdnVehicle_No")+request.getParameter("hdnSlot_No"));
    vehicalService.updateServiceEntry(vehicle,"YARD",request.getParameter("hdnSlot_No"));
    
    return mav;
}
  
  @RequestMapping(value = "/saveVehicle", method = RequestMethod.POST)
  public ModelAndView saveVehicle(HttpServletRequest request, HttpServletResponse response) throws ParseException {
    ModelAndView mav = new ModelAndView("redirect:/vehicleRegistration");

    VehicleVO vehicle = new VehicleVO();
    vehicle.setVehicle_No(request.getParameter("txtVehicleno"));
    vehicle.setSupplier(request.getParameter("cmbSupplierIBL1"));
    vehicle.setSupplier_Name(request.getParameter("cmbSupplierName"));
    vehicle.setVehicle_Type(request.getParameter("cmbVehiceleType"));
    vehicle.setRCBOOK_Valid_Date(Utility.stringToDate(request.getParameter("txtRcBookValidDate")));
    vehicle.setInsurance_Valid_Date(Utility.stringToDate(request.getParameter("txtInsuranceValidDate")));
    vehicle.setFC_Valid_Date(Utility.stringToDate(request.getParameter("txtFCValidDate")));
    vehicle.setLicense_Valid_Date(Utility.stringToDate(request.getParameter("txtLicenceValidDate")));
    vehicle.setPollution_Certificate_Valid_Date(Utility.stringToDate(request.getParameter("txtPollutionCertificateValidDate")));
    vehicle.setPass_Type(request.getParameter("cmbPassType"));
    vehicle.setReportingTime(Utility.dateToString(new Date()));
    if(vehicle.getPass_Type().equals("ONE")) {
      vehicle.setPass_Validity_Date(new Date());
    }else if(vehicle.getPass_Type().equals("MULTI")) {
      vehicle.setPass_Validity_Date(Utility.stringToDate(request.getParameter("txtNTimePassValitity")));
    }
    
    vehicalService.saveVehicle(vehicle);
  
    return mav;
  }
  
  @RequestMapping(value = "/saveDriver", method = RequestMethod.POST)
  public ModelAndView saveDriver(HttpServletRequest request, HttpServletResponse response) throws ParseException {
    ModelAndView mav = new ModelAndView("redirect:/driverRegistration");
    System.out.println("*******saveDriver"+request.getParameter("hdnAction"));
    DriverVO driver = new DriverVO();
    
    //driver.setId(id);TODO autogenerated
    driver.setName(request.getParameter("txtDriverName"));
    driver.setContact_No(request.getParameter("txtContactno"));
    driver.setLicense_No(request.getParameter("txtLicenceNo"));
    driver.setLicense_Validity(request.getParameter("txtLicenceValitity"));
    driver.setPass_Type(request.getParameter("cmbPassType"));
    if(driver.getPass_Type().equals("ONE")) {
      driver.setPass_Expiry_Date(Utility.dateToString(new Date()));
    }
      
    if(driver.getPass_Type().equals("MULTI")) {
      driver.setPass_Expiry_Date(request.getParameter("txtNTimePassValitity"));
    }
    
    vehicalService.saveDriver(driver,request.getParameter("hdnAction"));
    
    return mav;
    
  }

  @RequestMapping(value = "/gateEntry", method = RequestMethod.GET)
  public ModelAndView gateEntry(HttpServletRequest request, HttpServletResponse response) {
    ModelAndView mav = new ModelAndView("gateEntry");
    
    StringBuffer table = new StringBuffer();
    List<VehicleInfo> vInfo = vehicalService.getVehiclesInfo();
    
    for (VehicleInfo info : vInfo) {
      String boomDateTime = info.getBoomInDateTime();
      table.append("<tr>");
      table.append("<td>").append("NA").append("</td>");
      table.append("<td>").append("NA").append("</td>");
      table.append("<td>").append(info.getVehicle_No()).append("</td>");     
      table.append("<td>").append("NA").append("</td>");
      if(boomDateTime!=null && !boomDateTime.isEmpty())
        table.append("<td>").append(boomDateTime).append("</td>");
      else
        table.append("<td>").append("NA").append("</td>");
      
      table.append("<td>").append(info.getTransporter()).append("</td>");
      table.append("<td>").append("NA").append("</td>");  
      table.append("<td>").append(info.getName()).append("</td>");
    }
    
    mav.addObject("vInfo",table.toString());
    mav.addObject("vInfoLst",vInfo);
    return mav;

  }
  
  @RequestMapping(value = "/saveGateEntry", method = RequestMethod.POST)
  public ModelAndView saveGateEntry(HttpServletRequest request, HttpServletResponse response) {
    ModelAndView mav = new ModelAndView("redirect:/gateEntry");
    VehicleVO vehicle = new VehicleVO();
    vehicle.setStatus_update(request.getParameter("hdnaction"));
    vehicle.setVehicle_No(request.getParameter("txtVehicleno"));
    System.out.println(request.getParameter("hdnaction")+"***"+request.getParameter("txtVehicleno"));
    vehicalService.updateGateEntry(vehicle);
    return mav;
  }
}
