package jbr.springmvc.utility;

import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.concurrent.TimeUnit;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;

public class Utility {

  public static String convertObjToJson(Object obj) throws JsonProcessingException {
    ObjectMapper mapper = new ObjectMapper();
    String jsonInString = mapper.writeValueAsString(obj);
    return jsonInString;
  }

  public static String compareDates(Date currentDate, Date checkDate) {
    
    long diffInMillies = checkDate.getTime() - currentDate.getTime();
    long diff = TimeUnit.DAYS.convert(diffInMillies, TimeUnit.MILLISECONDS);
    if(diff > 0) {
      return "checkbox-success";
    }
    return "checkbox-danger";
  }
  
  public static String constructJson(String attribute,Object value) {
    StringBuffer sb = new StringBuffer();
    sb.append("\""+attribute+"\":\""+value+"\"");
    return sb.toString();
    
  }
  
  public static String dateToString(Date date) {
    DateFormat df = new SimpleDateFormat("MM/dd/yyyy HH:mm:ss");
    String reportDate = df.format(date);
    return reportDate;
  }
  
  public static Date stringToDate(String date) throws ParseException {
    System.out.println(date);
    return new SimpleDateFormat("yyyy-MM-dd").parse(date);
  }
  
 
  public static void main (String arge[]) throws ParseException {
    Date dt = new Date();
    Calendar c = Calendar.getInstance(); 
    c.setTime(dt); 
    c.add(Calendar.DATE, 40);
    dt = c.getTime();
    System.out.println(compareDates(new Date(), dt));
  }


}
