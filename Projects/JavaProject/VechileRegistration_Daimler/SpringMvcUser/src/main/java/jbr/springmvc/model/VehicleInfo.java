package jbr.springmvc.model;

import java.util.Date;

public class VehicleInfo 
{
  private String Name  = null;
  private String Transporter  = null;
  private String BoomIn = null;
  private String VehicleNo = null;
  private String BoomInDateTime = null;
  
  public String getBoomInDateTime() {
    return BoomInDateTime;
  }
  
  public void setBoomInDateTime(String boomInDateTime) {
    this.BoomInDateTime = boomInDateTime;
  }
  
  public String getVehicle_No() {
    return VehicleNo;
  }

  public void setVehicle_No(String vehicleNo) {
    this.VehicleNo = vehicleNo;
  }

  public String getTransporter() {
    return Transporter;
  }

  public void setTransporter(String transporter) {
    this.Transporter = transporter;
  }
  
  public String getName() {
    return Name;
  }

  public void setName(String name) {
    this.Name = name;
  }
  
  public String getBoomIn() {
    return BoomIn;
  }

  public void setBoomIn(String boomIn) {
    this.BoomIn = boomIn;
  }
  
  
}
