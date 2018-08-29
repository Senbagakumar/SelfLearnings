package jbr.springmvc.dao.rowmapper;

import java.sql.ResultSet;
import java.sql.SQLException;

import org.springframework.jdbc.core.RowMapper;

import jbr.springmvc.model.VehicleVO;

public class VehicleMappers implements RowMapper<VehicleVO> {

  public VehicleVO mapRow(ResultSet rs, int arg1) throws SQLException {
    VehicleVO vehicle = new VehicleVO();

    vehicle.setVehicle_No(rs.getString("vehicle_No"));
    vehicle.setSupplier(rs.getString("supplier"));
    vehicle.setSupplier_Name(rs.getString("supplier_Name"));
    vehicle.setRCBOOK_Valid_Date(rs.getDate("RCBOOK_Valid_Date"));
    vehicle.setInsurance_Valid_Date(rs.getDate("insurance_Valid_Date"));
    vehicle.setFC_Valid_Date(rs.getDate("FC_Valid_Date"));
    vehicle.setLicense_Valid_Date(rs.getDate("license_Valid_Date"));
    vehicle.setPollution_Certificate_Valid_Date(rs.getDate("pollution_Certificate_Valid_Date"));
    vehicle.setPass_Type(rs.getString("pass_Type"));
    vehicle.setStatus_update(rs.getString("status_update"));
    vehicle.setTruck_No(rs.getString("truck_No"));
    vehicle.setVehicle_Type(rs.getString("vehicle_type")); 
    vehicle.setReportingTime(rs.getString("ReportingTime"));
    vehicle.setPass_Validity_Date(rs.getDate("pass_Validity_Date"));
    return vehicle;
  }
}
