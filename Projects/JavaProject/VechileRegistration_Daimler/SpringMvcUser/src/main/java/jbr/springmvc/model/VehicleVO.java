package jbr.springmvc.model;

import java.util.Date;

public class VehicleVO {

  private String entry_No = null;
  private String vehicle_No = null;
  private String trimedVehicle = null;
  private String supplier = null;
  private String supplier_Name = null;
  private Date RCBOOK_Valid_Date = null;
  private Date insurance_Valid_Date = null;
  private Date FC_Valid_Date = null;
  private Date license_Valid_Date = null;
  private Date pollution_Certificate_Valid_Date = null;
  private String pass_Type = null;
  private Date pass_Validity_Date = null;
  private YardVO yard = null;
  private DriverVO driver = null;
  private String remarks = null;
  private String criticalPart = null;
  private String vehicle_Type = null;
  private String status_update = null;
  private String truck_No = null;
  private String ReportingTime=null;

  
  
  public String getTruck_No() {
    return truck_No;
  }

  public void setTruck_No(String truck_No) {
    this.truck_No = truck_No;
  }

  public String getReportingTime() {
    return ReportingTime;
  }
  
  public void setReportingTime(String reportingTime) {
    this.ReportingTime=reportingTime;
  }
  
  public String getStatus_update() {
    return status_update;
  }

  public void setStatus_update(String status_update) {
    this.status_update = status_update;
  }

  public String getTrimedVehicle() {
    return getVehicle_No().replace(" ", "");
  }

  public void setTrimedVehicle(String trimedVehicle) {
    this.trimedVehicle = trimedVehicle;
  }

  public Date getPass_Validity_Date() {
    return pass_Validity_Date;
  }

  public void setPass_Validity_Date(Date pass_Validity_Date) {
    this.pass_Validity_Date = pass_Validity_Date;
  }

  public String getVehicle_Type() {
    return vehicle_Type;
  }

  public void setVehicle_Type(String vehicle_Type) {
    this.vehicle_Type = vehicle_Type;
  }

  public String getEntry_No() {
    return entry_No;
  }

  public void setEntry_No(String entry_No) {
    this.entry_No = entry_No;
  }

  public String getVehicle_No() {
  //  if (vehicle_No==null) {
   //   vehicle_No="";
   // }
    return vehicle_No;
  }

  public void setVehicle_No(String vehicle_No) {
    this.vehicle_No = vehicle_No;
  }

  public String getSupplier() {
    return supplier;
  }

  public void setSupplier(String supplier) {
    this.supplier = supplier;
  }

  public String getSupplier_Name() {
    return supplier_Name;
  }

  public void setSupplier_Name(String supplier_Name) {
    this.supplier_Name = supplier_Name;
  }

  public Date getRCBOOK_Valid_Date() {
    return RCBOOK_Valid_Date;
  }

  public void setRCBOOK_Valid_Date(Date rCBOOK_Valid_Date) {
    RCBOOK_Valid_Date = rCBOOK_Valid_Date;
  }

  public Date getInsurance_Valid_Date() {
    return insurance_Valid_Date;
  }

  public void setInsurance_Valid_Date(Date insurance_Valid_Date) {
    this.insurance_Valid_Date = insurance_Valid_Date;
  }

  public Date getFC_Valid_Date() {
    return FC_Valid_Date;
  }

  public void setFC_Valid_Date(Date fC_Valid_Date) {
    FC_Valid_Date = fC_Valid_Date;
  }

  public Date getLicense_Valid_Date() {
    return license_Valid_Date;
  }

  public void setLicense_Valid_Date(Date license_Valid_Date) {
    this.license_Valid_Date = license_Valid_Date;
  }

  public Date getPollution_Certificate_Valid_Date() {
    return pollution_Certificate_Valid_Date;
  }

  public void setPollution_Certificate_Valid_Date(Date pollution_Certificate_Valid_Date) {
    this.pollution_Certificate_Valid_Date = pollution_Certificate_Valid_Date;
  }

  public String getPass_Type() {
    return pass_Type;
  }

  public void setPass_Type(String pass_Type) {
    this.pass_Type = pass_Type;
  }

  public YardVO getYard() {
    if (yard == null) {
      yard = new YardVO();
    }
    return yard;
  }

  public void setYard(YardVO yard) {
    this.yard = yard;
  }

  public DriverVO getDriver() {
    if (driver == null) {
      driver = new DriverVO();
    }
    return driver;
  }

  public void setDriver(DriverVO driver) {
    this.driver = driver;
  }

  public String getRemarks() {
    return remarks;
  }

  public void setRemarks(String remarks) {
    this.remarks = remarks;
  }

  public String getCriticalPart() {
    return criticalPart;
  }

  public void setCriticalPart(String criticalPart) {
    this.criticalPart = criticalPart;
  }

  
}
