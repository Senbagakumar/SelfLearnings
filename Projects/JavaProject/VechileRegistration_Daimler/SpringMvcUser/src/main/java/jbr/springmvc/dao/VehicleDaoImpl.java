package jbr.springmvc.dao;

import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.util.Date;
import java.util.List;

import javax.sql.DataSource;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.core.PreparedStatementSetter;

import jbr.springmvc.dao.rowmapper.DriversListMappers;
import jbr.springmvc.dao.rowmapper.DriversMappers;
import jbr.springmvc.dao.rowmapper.SlotsMappers;
import jbr.springmvc.dao.rowmapper.VehicleInfoMappers;
import jbr.springmvc.dao.rowmapper.VehicleMappers;
import jbr.springmvc.dao.rowmapper.YardsMappers;
import jbr.springmvc.model.DriverVO;
import jbr.springmvc.model.SlotsVO;
import jbr.springmvc.model.VehicleInfo;
import jbr.springmvc.model.VehicleVO;
import jbr.springmvc.model.YardVO;
import jbr.springmvc.utility.Utility;

public class VehicleDaoImpl implements VehicleDao {

  @Autowired
  DataSource datasource;

  @Autowired
  JdbcTemplate jdbcTemplate;

  public List<VehicleVO> getVehicles() {
    String sql = "SELECT vehicle_No,supplier,supplier_Name,RCBOOK_Valid_Date,insurance_Valid_Date,FC_Valid_Date,license_Valid_Date,pollution_Certificate_Valid_Date,pass_Type,status_update,truck_No,vehicle_Type,ReportingTime,pass_Validity_Date FROM vehicle";
    List<VehicleVO> vehicles = jdbcTemplate.query(sql, new VehicleMappers());
    return vehicles;
  }
  
  public List<VehicleInfo> getVehiclesInfo(){
    String sql = "select a.name, b.supplier_Name,b.vehicle_No,b.BoomInDateTime from driver a , vehicle b where a.vehicle_no = b.vehicle_no";
    List<VehicleInfo> vehicleInfo = jdbcTemplate.query(sql, new VehicleInfoMappers());
    return vehicleInfo;
  }
  
  public VehicleVO getVehicle(String vehicle) {
    String sql = "SELECT vehicle_No,vehicle_type,supplier,supplier_Name,RCBOOK_Valid_Date,insurance_Valid_Date,FC_Valid_Date,license_Valid_Date,pollution_Certificate_Valid_Date,pass_Type,status_update,truck_No,vehicle_Type,ReportingTime,pass_Validity_Date FROM vehicle where vehicle_No='"+vehicle+"'";
    List<VehicleVO> vehicles = jdbcTemplate.query(sql, new VehicleMappers());
    if(vehicles.size() > 0) {
      return vehicles.get(0);
    }
    return new VehicleVO();
  }

  public List<YardVO> getYardInfo() {
    String sql = "SELECT driver.ID,entry_No,yard_details.vehicle_No,supplier,reporting_Time,in_Time,out_Time,current_Place,parking_Allotment,contact_No FROM yard_details INNER JOIN vehicle ON yard_details.vehicle_No=vehicle.vehicle_No INNER JOIN driver ON yard_details.vehicle_No=driver.vehicle_No";
    List<YardVO> yards = jdbcTemplate.query(sql, new YardsMappers());
    return yards;
  }

  public DriverVO getDriver(String driver_Pass) {
    String sql = "SELECT ID,name,contact_No,license_No,license_Validity,driver.pass_Type,pass_Expiry_Date,driver.vehicle_No,supplier FROM driver,vehicle where ID='"+driver_Pass+"'";
    List<DriverVO> drivers = jdbcTemplate.query(sql, new DriversMappers());
    if(drivers.size() > 0) {
      return drivers.get(0);
    }
    return new DriverVO();
  }

  public List<SlotsVO> getSlots() {
    String sql = "SELECT slot, vehical_No, bay from slots";
    List<SlotsVO> slots = jdbcTemplate.query(sql, new SlotsMappers());
    return slots;
  }

  public void saveVehicle(final VehicleVO vehicle) {
    try {
      String sql = "INSERT INTO vehicle (vehicle_No ,supplier ,supplier_Name ,vehicle_Type ,RCBook_Valid_Date ,insurance_Valid_Date ,FC_Valid_Date ,license_Valid_Date ,pollution_Certificate_Valid_Date ,pass_Type,pass_Validity_Date,ReportingTime) VALUES (?,?,?,?,?,?,?,?,?,?,?,?)";
      jdbcTemplate.update(sql, new PreparedStatementSetter() {
        public void setValues(PreparedStatement stmt) throws SQLException {

          stmt.setString(1, vehicle.getVehicle_No());
          stmt.setString(2, vehicle.getSupplier());
          stmt.setString(3, vehicle.getSupplier_Name());
          stmt.setString(4, vehicle.getVehicle_Type());
          stmt.setDate(5, new java.sql.Date(vehicle.getRCBOOK_Valid_Date().getTime()));
          stmt.setDate(6, new java.sql.Date(vehicle.getInsurance_Valid_Date().getTime()));
          stmt.setDate(7, new java.sql.Date(vehicle.getFC_Valid_Date().getTime()));
          stmt.setDate(8, new java.sql.Date(vehicle.getLicense_Valid_Date().getTime()));
          stmt.setDate(9, new java.sql.Date(vehicle.getPollution_Certificate_Valid_Date().getTime()));
          stmt.setString(10, vehicle.getPass_Type());
          stmt.setDate(11, new java.sql.Date(vehicle.getPass_Validity_Date().getTime()));
          stmt.setString(12, vehicle.getReportingTime());

        }
      });
    } catch (org.springframework.dao.DuplicateKeyException e) {
      String sql = "UPDATE vehicle SET vehicle_No=?,supplier=?,supplier_Name=?,vehicle_Type=?,RCBook_Valid_Date=?,insurance_Valid_Date=?,FC_Valid_Date=?,license_Valid_Date=?,pollution_Certificate_Valid_Date=?,pass_Type=?,pass_Validity_Date=? WHERE vehicle_No=? ";
      
      jdbcTemplate.update(sql, new PreparedStatementSetter() {
        public void setValues(PreparedStatement stmt) throws SQLException {

          stmt.setString(1, vehicle.getVehicle_No());
          stmt.setString(2, vehicle.getSupplier());
          stmt.setString(3, vehicle.getSupplier_Name());
          stmt.setString(4, vehicle.getVehicle_Type());
          stmt.setDate(5, new java.sql.Date(vehicle.getRCBOOK_Valid_Date().getTime()));
          stmt.setDate(6, new java.sql.Date(vehicle.getInsurance_Valid_Date().getTime()));
          stmt.setDate(7, new java.sql.Date(vehicle.getFC_Valid_Date().getTime()));
          stmt.setDate(8, new java.sql.Date(vehicle.getLicense_Valid_Date().getTime()));
          stmt.setDate(9, new java.sql.Date(vehicle.getPollution_Certificate_Valid_Date().getTime()));
          stmt.setString(10, vehicle.getPass_Type());
          stmt.setDate(11, new java.sql.Date(vehicle.getPass_Validity_Date().getTime()));
          stmt.setString(12, vehicle.getVehicle_No());

        }
      });
    }
  }

  public void updateServiceEntry(final VehicleVO vehicle, final String current_Place,final String slot) {
    String sql = null;
    if(current_Place.equalsIgnoreCase("YARD")) {
       sql = "UPDATE yard_details SET current_Place = ?, parking_Allotment= ?, reporting_Time = ? , in_Time = ? WHERE vehicle_No = ?";
    }
    
    if(current_Place.equalsIgnoreCase("SERVICE ROAD")) {
       sql = "UPDATE yard_details SET current_Place = ?, parking_Allotment= ?, reporting_Time = ? , out_Time = ? WHERE vehicle_No = ?";
    }
    
    int updatedRows = jdbcTemplate.update(sql, new PreparedStatementSetter() {
      public void setValues(PreparedStatement stmt) throws SQLException {

        stmt.setString(1, current_Place);
        stmt.setString(2, slot);
        stmt.setDate(3, new java.sql.Date(new Date().getTime()));
        stmt.setDate(4, new java.sql.Date(new Date().getTime()));
        
        stmt.setString(5, vehicle.getVehicle_No());
        
      }
    });
    if(updatedRows < 1) {
      
      sql = "INSERT INTO yard_details (reporting_Time,in_Time,out_Time,current_Place,parking_Allotment,vehicle_No) VALUES (?,?,?,?,?,?)";
      
      String logTime = Utility.dateToString(new Date());
      System.out.println(logTime);
      
      jdbcTemplate.update(sql, new PreparedStatementSetter() {
      public void setValues(PreparedStatement stmt) throws SQLException {

       
        stmt.setString(1, Utility.dateToString(new Date()));
        stmt.setString(2, Utility.dateToString(new Date()));
        stmt.setString(3, Utility.dateToString(new Date()));
        stmt.setString(4, current_Place);
        stmt.setString(5, slot);
        stmt.setString(6, vehicle.getVehicle_No());
        
      }
    });
    }
    
  }

  public void updateSlots(final VehicleVO vehicle, final String current_Place, final String slot) {
    String sql = null;
    System.out.println(slot+current_Place+vehicle.getVehicle_No());
    if(current_Place.equalsIgnoreCase("YARD")) {
      sql = "UPDATE slots SET vehical_No=? where slot=?";
   }
   
   if(current_Place.equalsIgnoreCase("SERVICE ROAD")) {
      sql = "UPDATE slots SET vehical_No=null  where slot=?";
   }
   
    jdbcTemplate.update(sql, new PreparedStatementSetter() {
      public void setValues(PreparedStatement stmt) throws SQLException {
        if(current_Place.equalsIgnoreCase("YARD")) {
          stmt.setString(2, slot);
          stmt.setString(1, vehicle.getVehicle_No()); 
        }
        if(current_Place.equalsIgnoreCase("SERVICE ROAD")) {
          stmt.setString(1, slot);
        }
      }
    });
    
  }

  public List<DriverVO> getDrivers() {
    String sql = "select ID,name,contact_No ,license_No ,license_Validity ,pass_Type ,pass_Expiry_Date ,vehicle_No FROM driver";
    List<DriverVO> drivers = jdbcTemplate.query(sql, new DriversListMappers());
    
    
    return drivers;
  }
  
  public void saveDrivers(final DriverVO driver, final String type) {
    String sql = null;
    
    if(type.equals("INSERT")) {
      sql = "INSERT INTO driver (ID,name,contact_No ,license_No ,license_Validity ,pass_Type ,pass_Expiry_Date ,vehicle_No) VALUES (?,?,?,?,?,?,?,?)";
    }
    if(type.equals("UPDATE")) {
      sql = "UPDATE driver SET ID = ?,name = ?,contact_No = ? ,license_No = ? ,license_Validity = ? ,pass_Type = ? ,pass_Expiry_Date = ? ,vehicle_No = ? WHERE ID=?";  
    }
    try {
    jdbcTemplate.update(sql, new PreparedStatementSetter() {
      public void setValues(PreparedStatement stmt) throws SQLException {

        stmt.setString(1, driver.getId());
        stmt.setString(2, driver.getName());
        stmt.setString(3, driver.getContact_No());
        stmt.setString(4, driver.getLicense_No());
        stmt.setString(5, driver.getLicense_Validity());
        stmt.setString(6, driver.getPass_Type());
        stmt.setString(7, driver.getPass_Expiry_Date());
        stmt.setString(8, driver.getVehicle().getVehicle_No());
        
        if(type.equals("UPDATE")) {
          stmt.setString(9, driver.getId());
        }
    
      }
    });
    }catch (Exception ex) {
      ex.printStackTrace();
    }
    
  }

  public Integer generateSeqNO(String forAttribute) {
    String sql = "SELECT currentNO from seqNO where attribute = '"+forAttribute+"'";
    long currentNO = (Long) jdbcTemplate.queryForMap(sql).get("currentNO");

    return (int) (currentNO);
  }
  public void updateSeqNO(final String forAttribute, final Integer currentNO) {
    String sql = "UPDATE seqNO SET currentNO=? where attribute=?";
    
    jdbcTemplate.update(sql, new PreparedStatementSetter() {
      public void setValues(PreparedStatement stmt) throws SQLException {
        stmt.setInt(1, currentNO);
        stmt.setString(2, forAttribute);

      }
    });
  
  }
  
  public void saveSlotDetail(final VehicleVO vehicle) {
    String sql = "INSERT INTO yard_details (vehicle_No) VALUES (?)";
    
    jdbcTemplate.update(sql, new PreparedStatementSetter() {
      public void setValues(PreparedStatement stmt) throws SQLException {
        stmt.setString(1, vehicle.getVehicle_No());

      }
    });
  }

  public void mapDriverToVehicle(final DriverVO driver) {
    String sql = "UPDATE driver SET vehicle_No=? WHERE ID=?";
    
    jdbcTemplate.update(sql, new PreparedStatementSetter() {
      public void setValues(PreparedStatement stmt) throws SQLException {
        stmt.setString(1, driver.getVehicle().getVehicle_No());
        stmt.setString(2, driver.getId());

      }
    });
  }

  public void updateGateEntry(final VehicleVO vehicle) {
    String sql = "";
    String sql1 ="update slots SET vehical_No=null where vehical_No=?";
    String status = vehicle.getStatus_update();
    
    if(status.equalsIgnoreCase("BOOMIN"))
      sql = "UPDATE vehicle SET status_update=?, BoomInDateTime=? WHERE vehicle_No=?";
    else if(status.equalsIgnoreCase("DOCRECV"))
      sql = "UPDATE vehicle SET status_update=?, DocRecvDateTime=? WHERE vehicle_No=?";
    else if(status.equalsIgnoreCase("ANNOUNCEMENT"))
      sql = "UPDATE vehicle SET status_update=?, AnnouncementDateTime=? WHERE vehicle_No=?";
    else if(status.equalsIgnoreCase("BOOMOUT"))
      sql = "UPDATE vehicle SET status_update=?, BoomOutDateTime=? WHERE vehicle_No=?";
    else
      sql = "UPDATE vehicle SET status_update=?,DocRecvDateTime=? WHERE vehicle_No=?";
    
    jdbcTemplate.update(sql, new PreparedStatementSetter() {
      public void setValues(PreparedStatement stmt) throws SQLException {
        stmt.setString(3, vehicle.getVehicle_No());
        stmt.setString(2, Utility.dateToString(new Date()));
        stmt.setString(1, vehicle.getStatus_update());
      }
    });
    
    jdbcTemplate.update(sql1, new PreparedStatementSetter() {
      public void setValues(PreparedStatement stmt) throws SQLException {
        stmt.setString(1, vehicle.getVehicle_No());     
      }
    });
    
    
  }

}

