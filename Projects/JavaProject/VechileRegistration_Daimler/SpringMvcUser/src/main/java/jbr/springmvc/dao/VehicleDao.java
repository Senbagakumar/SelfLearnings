package jbr.springmvc.dao;

import java.util.List;

import jbr.springmvc.model.DriverVO;
import jbr.springmvc.model.SlotsVO;
import jbr.springmvc.model.VehicleInfo;
import jbr.springmvc.model.VehicleVO;
import jbr.springmvc.model.YardVO;

public interface VehicleDao {

  List<VehicleVO> getVehicles();

  List<VehicleInfo> getVehiclesInfo();
  
  List<YardVO> getYardInfo();
  
  VehicleVO getVehicle(String vehicle_No);

  DriverVO getDriver(String driver_Pass);

  List<SlotsVO> getSlots();
  
  void saveVehicle(VehicleVO vehicle);

  void updateServiceEntry(VehicleVO vehicle, String current_Place, String slot);
  
  void updateSlots(VehicleVO vehicle,String current_Place, String slot);

  List<DriverVO> getDrivers();

  void saveDrivers(DriverVO driver, String type);
  
  Integer generateSeqNO(String forAttribute);
  
  void updateSeqNO(String forAttribute, final Integer currentNO);
  
  void saveSlotDetail(VehicleVO vehicle);

  void mapDriverToVehicle(DriverVO driver);

  void updateGateEntry(VehicleVO vehicle);
}
