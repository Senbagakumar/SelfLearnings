package jbr.springmvc.service;

import java.util.List;

import org.springframework.stereotype.Service;

import jbr.springmvc.model.DriverVO;
import jbr.springmvc.model.SlotsVO;
import jbr.springmvc.model.VehicleInfo;
import jbr.springmvc.model.VehicleVO;
import jbr.springmvc.model.YardVO;

@Service
public interface VehicleService {

  List<VehicleVO> getVehicles();
  
  List<VehicleInfo> getVehiclesInfo();

  List<YardVO> getYardInfo();

  VehicleVO getVehicle(String vehicelId);

  DriverVO getDriver(String driver_Pass);

  List<SlotsVO> getSlots();
  
  void saveVehicle(VehicleVO vehicle);

  void updateServiceEntry(VehicleVO vehicle,String current_Place, String slot);

  List<DriverVO> getDrivers();

  void saveDriver(DriverVO driver, String type);
  
  String generateSeqNO(String forAttribute);

  void mapDriverToVehicle(DriverVO driver);
  
  void updateGateEntry(VehicleVO vehicle);
  
}
