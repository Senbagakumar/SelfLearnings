package jbr.springmvc.service;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import jbr.springmvc.dao.VehicleDao;
import jbr.springmvc.model.DriverVO;
import jbr.springmvc.model.SlotsVO;
import jbr.springmvc.model.VehicleInfo;
import jbr.springmvc.model.VehicleVO;
import jbr.springmvc.model.YardVO;

@Service
public class VehicleServiceImpl implements VehicleService {

  @Autowired
  public VehicleDao vehicleDao;

  public List<VehicleVO> getVehicles() {
    return vehicleDao.getVehicles();
  }

  public List<VehicleInfo> getVehiclesInfo(){
    return vehicleDao.getVehiclesInfo();
  }
  
  public List<YardVO> getYardInfo() {
    return vehicleDao.getYardInfo();
  }

  public VehicleVO getVehicle(String vehicle_No) {
    return vehicleDao.getVehicle(vehicle_No);
  }

  public DriverVO getDriver(String driver_Pass) {
    return vehicleDao.getDriver(driver_Pass);
  }

  public List<SlotsVO> getSlots() {
    return vehicleDao.getSlots();
  }

  public void saveVehicle(VehicleVO vehicle) {
    vehicleDao.saveVehicle(vehicle);
    vehicleDao.saveSlotDetail(vehicle);
    
  }


  public void updateServiceEntry(VehicleVO vehicle, String current_Place, String slot) {
    vehicleDao.updateServiceEntry(vehicle,current_Place,slot);
    vehicleDao.updateSlots(vehicle, current_Place, slot);
    
  }

  public List<DriverVO> getDrivers() {
    return vehicleDao.getDrivers();    
  }

  public void saveDriver(DriverVO driver, String type) {
    if(driver.getId()==null) {
      driver.setId(generateSeqNO("DRV"));
    }
    vehicleDao.saveDrivers(driver,type);
    
  }

  public synchronized String generateSeqNO(String forAttribute) {
    int nextNo = vehicleDao.generateSeqNO(forAttribute)+1;
    vehicleDao.updateSeqNO(forAttribute, nextNo);
    return "DRV"+nextNo;
  }

  public void mapDriverToVehicle(DriverVO driver) {
    vehicleDao.mapDriverToVehicle(driver);
    
  }

  public void updateGateEntry(VehicleVO vehicle) {
    vehicleDao.updateGateEntry(vehicle);
    
  }

  

}
