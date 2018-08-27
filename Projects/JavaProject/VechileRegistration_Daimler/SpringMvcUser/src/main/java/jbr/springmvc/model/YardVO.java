package jbr.springmvc.model;

import java.util.Date;

public class YardVO {
  private String entry_No = null;
  private String reporting_Time = null;
  private String in_Time = null;
  private String out_Time = null;
  private String halt_Time = null;
  private String current_Place = null;
  private String parking_Allotment = null;
  private DriverVO driver = null;
  private VehicleVO vehicle = null;
  public String getEntry_No() {
    return entry_No;
  }
  public void setEntry_No(String entry_No) {
    this.entry_No = entry_No;
  }
  public String getReporting_Time() {
    return reporting_Time;
  }
  public void setReporting_Time(String reporting_Time) {
    this.reporting_Time = reporting_Time;
  }
  public String getIn_Time() {
    return in_Time;
  }
  public void setIn_Time(String in_Time) {
    this.in_Time = in_Time;
  }
  public String getOut_Time() {
    return out_Time;
  }
  public void setOut_Time(String out_Time) {
    this.out_Time = out_Time;
  }
  public String getHalt_Time() {
    return halt_Time;
  }
  public void setHalt_Time(String halt_Time) {
    this.halt_Time = halt_Time;
  }
  public String getCurrent_Place() {
    return current_Place;
  }
  public void setCurrent_Place(String current_Place) {
    this.current_Place = current_Place;
  }
  public String getParking_Allotment() {
    return parking_Allotment;
  }
  public void setParking_Allotment(String parking_Allotment) {
    this.parking_Allotment = parking_Allotment;
  }
  public DriverVO getDriver() {
    if(driver == null) {
      driver = new DriverVO();
    }
    return driver;
  }
  public void setDriver(DriverVO driver) {
    this.driver = driver;
  }
  public VehicleVO getVehicle() {
    if (this.vehicle == null) {
      this.vehicle = new VehicleVO();
    }
    return vehicle;
  }
  public void setVehicle(VehicleVO vehicle) {
    this.vehicle = vehicle;
  }
  
  

}
