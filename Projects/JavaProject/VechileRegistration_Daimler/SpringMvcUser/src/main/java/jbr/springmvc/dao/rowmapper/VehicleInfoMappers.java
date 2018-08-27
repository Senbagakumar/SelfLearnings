package jbr.springmvc.dao.rowmapper;

import java.sql.ResultSet;
import java.sql.SQLException;

import org.springframework.jdbc.core.RowMapper;

import jbr.springmvc.model.VehicleInfo;
import jbr.springmvc.model.VehicleVO;

public class VehicleInfoMappers implements RowMapper<VehicleInfo> {

  public VehicleInfo mapRow(ResultSet rs, int arg1) throws SQLException {
    VehicleInfo vehicle = new VehicleInfo();

    vehicle.setName(rs.getString("name"));
    vehicle.setTransporter(rs.getString("supplier_Name"));   
    vehicle.setVehicle_No(rs.getString("vehicle_No"));
    vehicle.setBoomInDateTime(rs.getString("BoomInDateTime"));
    return vehicle;
  }
}
