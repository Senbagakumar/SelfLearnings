package jbr.springmvc.model;

public class DriverVO {
  private String id = null;
  private String name = null;
  private String contact_No = null;
  private String license_No = null;
  private String license_Validity = null;
  private String pass_Type = null;
  private String pass_Expiry_Date = null;
  private VehicleVO vehicle = null;
  public String getId() {
    return id;
  }
  public void setId(String id) {
    this.id = id;
  }
  public String getName() {
    if(name==null)
    {
      name="";
    }
    return name;
  }
  public void setName(String name) {
    this.name = name;
  }
  public String getContact_No() {
    return contact_No;
  }
  public void setContact_No(String contact_No) {
    this.contact_No = contact_No;
  }
  public String getLicense_No() {
    return license_No;
  }
  public void setLicense_No(String license_No) {
    this.license_No = license_No;
  }
  public String getLicense_Validity() {
    return license_Validity;
  }
  public void setLicense_Validity(String license_Validity) {
    this.license_Validity = license_Validity;
  }
  public String getPass_Type() {
    return pass_Type;
  }
  public void setPass_Type(String pass_Type) {
    this.pass_Type = pass_Type;
  }
  public String getPass_Expiry_Date() {
    return pass_Expiry_Date;
  }
  public void setPass_Expiry_Date(String pass_Expiry_Date) {
    this.pass_Expiry_Date = pass_Expiry_Date;
  }
  public VehicleVO getVehicle() {
    if(vehicle == null) {
      vehicle = new VehicleVO();
    }
    return vehicle;
  }
  public void setVehicle(VehicleVO vehicle) {
    this.vehicle = vehicle;
  }
  
  
}
