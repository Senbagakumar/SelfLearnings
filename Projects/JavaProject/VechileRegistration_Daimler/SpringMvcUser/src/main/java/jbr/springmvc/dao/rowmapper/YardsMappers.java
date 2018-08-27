package jbr.springmvc.dao.rowmapper;

import java.sql.ResultSet;
import java.sql.SQLException;

import org.springframework.jdbc.core.RowMapper;

import jbr.springmvc.model.YardVO;

public class YardsMappers implements RowMapper<YardVO> {

  public YardVO mapRow(ResultSet rs, int arg1) throws SQLException {
    YardVO yard = new YardVO();
    //SELECT entry_No,yard_details.vehicle_No,supplier,reporting_Time,in_Time,out_Time,current_Place,parking_Allotment,
    //contact_No from yard_details,vehicle,driver where yard_details.vehicle_No=vehicle.vehicle_No";
    
    yard.setEntry_No(rs.getString("entry_No"));
    yard.getVehicle().setVehicle_No(rs.getString("vehicle_No"));
    yard.getVehicle().setSupplier(rs.getString("supplier"));
    yard.setReporting_Time(rs.getString("reporting_Time"));
    yard.setIn_Time(rs.getString("in_Time"));
    yard.setOut_Time(rs.getString("out_Time"));
    yard.setCurrent_Place(rs.getString("current_Place"));
    yard.setParking_Allotment(rs.getString("parking_Allotment"));
    yard.getDriver().setContact_No(rs.getString("contact_No"));
    yard.getDriver().setId(rs.getString("ID"));

    return yard;
  }
}
