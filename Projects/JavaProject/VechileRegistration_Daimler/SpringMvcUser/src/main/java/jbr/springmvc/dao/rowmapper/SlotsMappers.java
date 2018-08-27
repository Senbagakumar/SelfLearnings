package jbr.springmvc.dao.rowmapper;

import java.sql.ResultSet;
import java.sql.SQLException;

import org.springframework.jdbc.core.RowMapper;

import jbr.springmvc.model.DriverVO;
import jbr.springmvc.model.SlotsVO;

public class SlotsMappers implements RowMapper<SlotsVO> {

  public SlotsVO mapRow(ResultSet rs, int arg1) throws SQLException {
    SlotsVO slot = new SlotsVO();

    slot.setBay(rs.getString("bay"));
    slot.setSlot(rs.getString("slot"));
    slot.setVehical_No(rs.getString("vehical_No"));
    
    return slot;
  }
}
