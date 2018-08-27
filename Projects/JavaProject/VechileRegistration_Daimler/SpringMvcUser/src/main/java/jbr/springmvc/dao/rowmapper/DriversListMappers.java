package jbr.springmvc.dao.rowmapper;

import java.sql.ResultSet;
import java.sql.SQLException;

import org.springframework.jdbc.core.RowMapper;

import jbr.springmvc.model.DriverVO;

public class DriversListMappers implements RowMapper<DriverVO> {

  public DriverVO mapRow(ResultSet rs, int arg1) throws SQLException {
    DriverVO driver = new DriverVO();

    driver.setId(rs.getString("ID"));
    driver.setName(rs.getString("name"));
    driver.setContact_No(rs.getString("contact_No"));
    driver.setLicense_Validity(rs.getString("license_Validity"));
    driver.setLicense_No(rs.getString("license_No"));
    driver.setPass_Type(rs.getString("pass_Type"));
    driver.setPass_Expiry_Date(rs.getString("pass_Expiry_Date"));
    driver.getVehicle().setVehicle_No(rs.getString("vehicle_No"));
    
    
    return driver;
  }
}
