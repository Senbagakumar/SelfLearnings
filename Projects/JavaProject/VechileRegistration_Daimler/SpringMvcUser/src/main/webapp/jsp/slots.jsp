<%@ taglib uri = "http://java.sun.com/jsp/jstl/core" prefix = "c" %>
<%@ taglib uri="http://java.sun.com/jsp/jstl/functions" prefix="fn" %> 
<!DOCTYPE html>
<html lang="en">

<head>
    <title>DAIMLER </title>
    
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <meta name="author" content="codedthemes" />
    <!-- Favicon icon -->
    <link rel="icon" href="resources/assets/images/favicon.ico" type="image/x-icon">
    <!-- Google font
	<link href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600" rel="stylesheet">-->
    <!-- Required Fremwork -->
    <link rel="stylesheet" type="text/css" href="resources/bower_components/bootstrap/css/bootstrap.min.css">
    <!-- themify icon -->
    <link rel="stylesheet" type="text/css" href="resources/assets/icon/themify-icons/themify-icons.css">
    <!-- Font Awesome -->
    <link rel="stylesheet" type="text/css" href="resources/assets/icon/font-awesome/css/font-awesome.min.css">
     <!-- ico font -->
    <link rel="stylesheet" type="text/css" href="resources/assets/icon/icofont/css/icofont.css">
    <!-- Date-time picker css -->
    <link rel="stylesheet" type="text/css" href="resources/assets/pages/advance-elements/css/bootstrap-datetimepicker.css">
    <!-- Style.css -->
    <link rel="stylesheet" type="text/css" href="resources/assets/css/style.css">
    <!-- scrollbar.css -->
    <link rel="stylesheet" type="text/css" href="resources/assets/css/jquery.mCustomScrollbar.css">
    <script src="https://code.jquery.com/jquery-1.9.1.min.js"></script>
    <script>
    $( document ).ready(function() {
    	data = ${slots}
    	$.each(data, function(index) {
            if(data[index].vehical_No != null && data[index].vehical_No != ""){
            	$("label[for='"+data[index].slot+"']").text('');
            	$("label[for='"+data[index].slot+"']").wrapInner('<a data-toggle="modal" data-target="#default-Modal'+data[index].vehical_No.replace(/\s/g, '')+'" href="#">'+data[index].slot+" : "+data[index].vehical_No+'<a/>')
            	$("label[for='"+data[index].slot+"']").removeClass("label-inverse-warning").addClass("label-danger");
            	
            }
        });
    	
		function getVehicleAjax(vehicle_No) {
	        
			$.ajax({
				url : "/SpringMvcUser/getVehicle",
				type : 'GET',
				dataType : 'text',
				data: { 
				    vehicle_No:vehicle_No
				},
				contentType : 'application/json',
				mimeType : 'application/json',
				success : function(data) {
					
					var obj = JSON.parse(data);
					alert(data);
				},
				error : function(XMLHttpRequest, textStatus, errorThrown) {
	                alert("error "+errorThrown);
	            }
			});
		}
    	
	    	<c:forEach var="slot" items="${slotLst}">
	    		<c:if test = "${empty slot.vehical_No}">
			    	$("label[for='${slot.slot}']").hover(
			   			  function() {
			   				if($("#hdnVehicle_No").val() != ""){
				  				$( this ).wrapInner('<i class="fa fa-tasks"></i>  ')
				  				$( this ).text("Assign Vehicle to slot ${slot.slot}");
				   			    $( this ).addClass( "label-success" );
				   			 	$( this ).css('cursor', 'pointer');
			   				}
			   			  }, function() {
			   				if($("#hdnVehicle_No").val() != ""){
				   				$( this ).text(" ${slot.slot}");
				   				$( this ).wrapInner('<i class="fa fa-tasks"></i> ')
				   			    $( this ).removeClass( "label-success" );
			   				}
			   			  });
				    	$("label[for='${slot.slot}']").click(function () {
			    			//alert("clicked ${slot.slot} ${vehicle_No}");
			    			//alert("clicked ${vehicle_No}" + $("#hdnSlot_No").val());
			    			if($("#hdnVehicle_No").val() != ""){
			    				$("#hdnSlot_No").val("${slot.slot}");
			    				$("#updateYardEntryFrm").submit();
			    			}
			    			//saveYardAjax("${vehicle_No}","YARD",$("#hdnSlot_No"));
			    		});
		    	</c:if>
		    	
			</c:forEach>
			
    });
    
    </script>
    
    
</head>

<body>
	<form id="updateYardEntryFrm" name="updateYardEntryFrm"
		action="updateYardEntry" method="POST" novalidate>
		<input type="hidden" id="hdnVehicle_No" name="hdnVehicle_No" value="${vehicle_No}" /> 
		<input type="hidden" id="hdnSlot_No"  name="hdnSlot_No"/>
		<input type="hidden" id="hdnDRV_No"  name="hdnDRV_No"/>
	</form>
	<!-- Pre-loader start -->
    <div class="theme-loader">
        <div class="loader-track">
            <div class="loader-bar"></div>
        </div>
    </div>
    <!-- Pre-loader end -->
    <div id="pcoded" class="pcoded">
        <div class="pcoded-overlay-box"></div>
        <div class="pcoded-container navbar-wrapper">
            <nav class="navbar header-navbar pcoded-header">
                <div class="navbar-wrapper">
                    <div class="navbar-logo">
                        <a class="mobile-menu" id="mobile-collapse" href="#!">
                            <i class="ti-menu"></i>
                        </a>
                        <div class="mobile-search">
                            <div class="header-search">
                                <div class="main-search morphsearch-search">
                                    <div class="input-group">
                                        <span class="input-group-addon search-close"><i class="ti-close"></i></span>
                                        <input type="text" class="form-control" placeholder="Enter Keyword">
                                        <span class="input-group-addon search-btn"><i class="ti-search"></i></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <a href="index.html">
                            <img class="img-fluid" src="resources/assets/images/logo.png" alt="Theme-Logo" />
                        </a>
                        <a class="mobile-options">
                            <i class="ti-more"></i>
                        </a>
                    </div>

                    <div class="navbar-container container-fluid">
                        <ul class="nav-left">
                            <li>
                                <div class="sidebar_toggle"><a href="javascript:void(0)"><i class="ti-menu"></i></a></div>
                            </li>
                            <li class="header-search">
                                <div class="main-search morphsearch-search">
                                    <div class="input-group">
                                        <span class="input-group-addon search-close"><i class="ti-close"></i></span>
                                        <input type="text" class="form-control">
                                        <span class="input-group-addon search-btn"><i class="ti-search"></i></span>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <a href="#!" onClick="javascript:toggleFullScreen()">
                                    <i class="ti-fullscreen"></i>
                                </a>
                            </li>
                        </ul>
                        <ul class="nav-right">
                            <li class="header-notification">
                                <a href="#!">
                                    <i class="ti-bell"></i>
                                    <span class="badge bg-c-pink"></span>
                                </a>
                                <ul class="show-notification">
                                    <li>
                                        <h6>Notifications</h6>
                                        <label class="label label-danger">New</label>
                                    </li>
                                    <li>
                                        <div class="media">
                                            <img class="d-flex align-self-center img-radius" src="resources/assets/images/avatar-2.jpg" alt="Generic placeholder image">
                                            <div class="media-body">
                                                <h5 class="notification-user">Truck Name</h5>
                                                <p class="notification-msg">Lorem ipsum dolor sit amet, consectetuer elit.</p>
                                                <span class="notification-time">30 minutes ago</span>
                                            </div>
                                        </div>
                                    </li>
                                    
                                </ul>
                            </li>
                           
                            <li class="user-profile header-notification">
                                <a href="#!">
                                    <img src="resources/assets/images/avatar-4.jpg" class="img-radius" alt="User-Profile-Image">
                                    <span>John Doe</span>
                                    <i class="ti-angle-down"></i>
                                </a>
                                <ul class="show-notification profile-notification">
                                    <li>
                                        <a href="#!">
                                            <i class="ti-settings"></i> Settings
                                        </a>
                                    </li>
                                    <li>
                                        <a href="#">
                                            <i class="ti-user"></i> Profile
                                        </a>
                                    </li>
                                    <li>
                                        <a href="#">
                                            <i class="ti-email"></i> My Messages
                                        </a>
                                    </li>
                                    <li>
                                        <a href="#">
                                            <i class="ti-lock"></i> Lock Screen
                                        </a>
                                    </li>
                                    <li>
                                        <a href="#">
                                        <i class="ti-layout-sidebar-left"></i> Logout
                                    </a>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
            
            <!-- Sidebar inner chat end-->
            <div class="pcoded-main-container">
                <div class="pcoded-wrapper">
                 <nav class="pcoded-navbar">
                        <div class="sidebar_toggle"><a href="#"><i class="icon-close icons"></i></a></div>
                        <div class="pcoded-inner-navbar main-menu">

                            <ul class="pcoded-item pcoded-left-item">
                                <li class=" ">
                                    <a href="index.html">
                                        <span class="pcoded-micon"><i class="ti-home"></i><b>D</b></span>
                                        <span class="pcoded-mtext">Dashboard</span>
                                        <span class="pcoded-mcaret"></span>
                                    </a>
                                    
                                </li>
                                
                                <li class="pcoded-hasmenu active">
                                    <a href="javascript:void(0)">
                                        <span class="pcoded-micon"><i class="fa fa-check-square"></i><b>P</b></span>
                                        <span class="pcoded-mtext">Yard</span>                                        
                                        <span class="pcoded-mcaret"></span>
                                    </a>
                                    <ul class="pcoded-submenu">
										
                                        <li class="">
                                            <a href="yardIn.html">
                                                <span class="pcoded-micon"><i class="ti-layout"></i></span>
                                                <span class="pcoded-mtext">Yard In</span>
                                                <span class="pcoded-mcaret"></span>
                                            </a>
                                        </li>
                                        <li class="active ">
                                            <a href="javascript:void(0)">
                                                <span class="pcoded-micon"><i class="ti-layout"></i></span>
                                                <span class="pcoded-mtext">Yard Slots</span>
                                                <span class="pcoded-mcaret"></span>
                                            </a>
                                        </li>
                                        <li class="">
                                            <a href="vehicleList.html">
                                                <span class="pcoded-micon"><i class="ti-angle-right"></i></span>
                                                <span class="pcoded-mtext">Truck List</span>
                                                <span class="pcoded-mcaret"></span>
                                            </a>
                                        </li>
                                        
                                    </ul>
                                </li>
                                
                                <li class="pcoded-hasmenu">
                                    <a href="javascript:void(0)">
                                        <span class="pcoded-micon"><i class="icofont icofont-long-drive"></i><b>W</b></span>
                                        <span class="pcoded-mtext">VRO</span>                                        
                                        <span class="pcoded-mcaret"></span>
                                    </a>
                                    <ul class="pcoded-submenu">
                                        <li class="">
                                            <a href="vehicleRegistration.html">
                                                <span class="pcoded-micon"><i class="ti-angle-right"></i></span>
                                                <span class="pcoded-mtext">Vehicle Registration</span>
                                                <span class="pcoded-mcaret"></span>
                                            </a>
                                        </li>
                                        <li class=" ">
                                            <a href="driverRegistration.html">
                                                <span class="pcoded-micon"><i class="ti-angle-right"></i></span>
                                                <span class="pcoded-mtext">Driver Registration</span>
                                                <span class="pcoded-mcaret"></span>
                                            </a>
                                        </li>
                                       
                                    </ul>
                                </li>
                                <!-- <i class="fa fa-truck"></i> -->
                                
                                <li class="pcoded-hasmenu">
                                    <a href="javascript:void(0)">
                                        <span class="pcoded-micon"><i class="fa fa-truck"></i><b>W</b></span>
                                        <span class="pcoded-mtext">Loading</span>
                                        <span class="pcoded-mcaret"></span>
                                    </a>
                                    <ul class="pcoded-submenu">
                                        <li class=" ">
                                            <a href="unloadingDockEntry.html">
                                                <span class="pcoded-micon"><i class="ti-angle-right"></i></span>
                                                <span class="pcoded-mtext">Unloading Dock Entry</span>
                                                <span class="pcoded-mcaret"></span>
                                            </a>
                                        </li>
                                        <li class=" ">
                                            <a href="emptyDockEntry.html">
                                                <span class="pcoded-micon"><i class="ti-angle-right"></i></span>
                                                <span class="pcoded-mtext">Empty Dock Entry</span>
                                                <span class="pcoded-mcaret"></span>
                                            </a>
                                        </li>
                                     </ul>
                                 </li>     
                                        
                                <li class="pcoded-hasmenu">
                                    <a href="javascript:void(0)">
                                        <span class="pcoded-micon"><i class="icofont icofont-upload"></i><b>W</b></span>
                                        <span class="pcoded-mtext">IBL</span>                                        
                                        <span class="pcoded-mcaret"></span>
                                    </a>
                                    <ul class="pcoded-submenu">
                                        <li class="">
                                            <a href="uploadCriticalPart.html">
                                                <span class="pcoded-micon"><i class="ti-angle-right"></i></span>
                                                <span class="pcoded-mtext">Upload Critical Part</span>
                                                <span class="pcoded-mcaret"></span>
                                            </a>
                                        </li>
                                        <li class=" ">
                                            <a href="approveCriticalPart.html">
                                                <span class="pcoded-micon"><i class="ti-angle-right"></i></span>
                                                <span class="pcoded-mtext">Approve Critical Part</span>
                                                <span class="pcoded-mcaret"></span>
                                            </a>
                                        </li>
                                        
                                    </ul>
                                </li>
                                
                                <li class="">
                                    <a href="gateEntry.html">
                                        <span class="pcoded-micon"><i class="icofont icofont-ui-unlock"></i><b>N</b></span>
                                        <span class="pcoded-mtext">Gate Entry</span>
                                        <span class="pcoded-mcaret"></span>
                                    </a>
                                </li>
                                <li class="">
                                    <a href="vehicleTracking.html">
                                        <span class="pcoded-micon"><i class="fa fa-map-marker"></i><b>N</b></span>
                                        <span class="pcoded-mtext">Truck Tracker</span>
                                        <span class="pcoded-mcaret"></span>
                                    </a>
                                </li>
                                
                            </ul>                            
                        </div>
                    </nav>
                    <div class="pcoded-content">
                        <div class="pcoded-inner-content">
                            <!-- Main-body start -->
                            <div class="main-body">
                                <div class="page-wrapper" style="padding:0px !important;">
                                    <!-- Page-body start -->
                                    <div class="page-body">
                                                                                
                                        <div class="card borderless-card">
                                            <div class="card-block default-breadcrumb">
                                                <div class="breadcrumb-header">
                                                    <h5><i class="fa fa-truck"></i> Slot</h5>
                                                </div>
                                                <div class="m-t-10">
                                                    <ul class="breadcrumb-title">
                                                        <li class="breadcrumb-item">
                                                            <a href="index.html">
                                                                <i class="icofont icofont-home"></i>
                                                            </a>
                                                        </li>
                                                        <li class="breadcrumb-item"><a href="#!">Yard</a>
                                                        </li>
                                                        <li class="breadcrumb-item"><a href="#!">Slot</a>
                                                        </li>
                                                       </ul>
                                                </div>
                                            </div>
                                        </div>
                                        
                                        <div class="card">
                                            <div class="panel panel-default">
                                                <div class="panel-heading bg-default txt-white">
                                                   <i class="icofont icofont-plus-circle"></i> SLOT 
                                                </div>
                                                <div class="panel-body" >
                                                              
                                                           
                                                   <form id="second" action="updateYardEntry" method="POST" novalidate>
                                                   <input type="hidden" id="hdnVehicle_No" value="${vehicle_No}"/>
                                                   <input type="hidden" id="hdnSlot_No"/>
                                                    <div class="card-block">
                                                      
                                                      <div class="row ">
                                                    
                                                    <div class="col-md-12 col-lg-12" >
                                                         <div class="row form-group" ><div class="col-lg-2">&nbsp;</div>
                                                         &nbsp;&nbsp;&nbsp;&nbsp;<label class="label label-inverse-primary"><i class="fa fa-truck"></i> SUPPLIER TRUCKS YET TO ALLOT </label>&nbsp;&nbsp;                                                           
                                                            <label class="label label-inverse-warning"><i class="fa fa-truck"></i> IBL TRUCKS YET TO ALLOT</label>&nbsp;&nbsp;
                                                             <label class="label label-success"><i class="fa fa-truck"></i> SELECTED</label>
                                                             &nbsp;&nbsp;
                                                             <label class="label label-danger"><i class="fa fa-truck"></i> ALLOTTED</label> </div><div class="row form-group">&nbsp;</div>
                                                             
                                                             
                                                             
                                                             
                                                   <div class="row form-group">
                                                    <div class="col-lg-1">&nbsp;</div>
                                                    <div class="col-lg-4">
                                                        <div class="panel panel-default">
                                                          <div class="panel-heading bg-inverse text-center txt-white">
                                                             BAY : 01
                                                          </div>
                                                          <div class="panel-body">
                                                          <div style="padding: 15px 5px;" >
                                                       <div class="form-group row">
                                                          <div class="col-sm-12 ">
                                                            <div class="form-group row" >                                                             
                                                             <div class="col-sm-2  ">
                                                             <div style="height:50px" class="col-sm-12"  id="1" >&nbsp;</div>
                                                             <label for="1" class="label col-sm-12 text-center label-inverse-primary " style=" padding-top:85px;height:180px;"><i class="fa fa-tasks"></i><br> 1 </label>
                                                             </div>
                                                             <div class="col-sm-6  " >
                                                              <div class="form-group row" >
                                                               <div class="col-sm-12 ">
                                                               <label for="14A" class="label col-sm-12 text-center label-inverse-primary"><i class="fa fa-tasks"></i> 14A </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="14" class="label col-sm-12 text-center label-inverse-primary"><i class="fa fa-tasks"></i> 14 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="13" class="label col-sm-12 text-center label-inverse-primary"><i class="fa fa-tasks"></i> 13 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="12" class="label col-sm-12 text-center label-inverse-primary"><i class="fa fa-tasks"></i> 12 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for ="11" class="label col-sm-12 text-center label-inverse-primary"><i class="fa fa-tasks"></i> 11 </label>
                                                               </div>
                                                              </div>
                                                             </div>
                                                             
                                                            </div>
                                                            <div class="form-group row" >                                                             
                                                             <div class="col-sm-2  " >
                                                             
                                                             <label for="2" class="label col-sm-12 text-center label-inverse-primary " style=" padding-top:85px;height:180px;"><i class="fa fa-tasks"></i><br> 2 </label>
                                                             </div>
                                                             <div class="col-sm-6  " >
                                                              <div class="form-group row" >
                                                               <div class="col-sm-12 ">
                                                               <label for="14B" class="label col-sm-12 text-center label-inverse-primary"><i class="fa fa-tasks"></i> 14B </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="10" class="label col-sm-12 text-center label-inverse-primary"><i class="fa fa-tasks"></i> 10 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="9" class="label col-sm-12 text-center label-inverse-primary"><i class="fa fa-tasks"></i> 9 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="8" class="label col-sm-12 text-center label-inverse-primary"><i class="fa fa-tasks"></i> 8 </label>
                                                               </div>
                                                              </div>
                                                              
                                                             </div>
                                                          
                                                            </div>
                                                            <div class="form-group row" >                                                             
                                                             <div class="col-sm-2 ">
                                                              <label for="3" class="label col-sm-12 text-center label-inverse-primary "><i class="fa fa-tasks"></i><br> 3 </label>
                                                             </div>
                                                             <div class="col-sm-6  " >
                                                             <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="7" class="label col-sm-12 text-center label-inverse-primary"><i class="fa fa-tasks"></i> 7 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" >
                                                               <div class="col-sm-12 ">
                                                               <label for="6" class="label col-sm-12 text-center label-inverse-primary"><i class="fa fa-tasks"></i> 6 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="5" class="label col-sm-12 text-center label-inverse-primary"><i class="fa fa-tasks"></i> 5 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="4" class="label col-sm-12 text-center label-inverse-primary"><i class="fa fa-tasks"></i> 4 </label>
                                                               </div>
                                                              </div>
                                                              
                                                             </div>
                                                            </div>
                                                          </div>
                                                          
                                                        </div>
                                                        
                                                        
                                                           </div>

                                                          </div>
                                                       </div>
                                                 </div>
                                                    <div class="col-lg-2">
                                                        <div class="panel panel-default">
                                                          <div class="panel-heading bg-inverse text-center txt-white">
                                                             Bay : 2
                                                          </div>
                                                          <div class="panel-body">
                                                           <div style="padding: 15px 5px;" >
                                                            <div class="form-group row" >                                                             
                                                             <div class="col-sm-12  " >
                                                             <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="15" class="label col-sm-12 text-center label-inverse-primary"><i class="fa fa-tasks"></i> 15 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" >
                                                               <div class="col-sm-12 ">
                                                               <label for="16" class="label col-sm-12 text-center label-inverse-primary"><i class="fa fa-tasks"></i> 16 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="17" class="label col-sm-12 text-center label-inverse-primary"><i class="fa fa-tasks"></i> 17 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="18" class="label col-sm-12 text-center label-inverse-primary"><i class="fa fa-tasks"></i> 18 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="19" class="label col-sm-12 text-center label-inverse-primary"><i class="fa fa-tasks"></i> 19 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="20" class="label col-sm-12 text-center label-inverse-primary"><i class="fa fa-tasks"></i> 20 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="21" class="label col-sm-12 text-center label-inverse-primary"><i class="fa fa-tasks"></i> 21 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="22" class="label col-sm-12 text-center label-inverse-primary"><i class="fa fa-tasks"></i> 22 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="23" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 23 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="24" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 24 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="25" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 25 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="26" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 26 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="27" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 27 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="28" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 28 </label>
                                                               </div>
                                                              </div>
                                                              
                                                             </div>
                                                             
                                                             
                                                             
                                                            </div>
                                                            
                                                                 
                                                             
                                                           </div>
                                                          </div>
                                                       </div>
                                                    </div>
                                                    
                                                    <div class="col-lg-2">
                                                        <div class="panel panel-default">
                                                          <div class="panel-heading bg-inverse text-center txt-white">
                                                             Bay : 3
                                                          </div>
                                                          <div class="panel-body">
                                                             <div style="padding: 15px 5px;" >
                                                                 <div class="form-group row" >                                                             
                                                             <div class="col-sm-12  " >
                                                             <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="42" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 42 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" >
                                                               <div class="col-sm-12 ">
                                                               <label for="41" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 41 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="40" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 40 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="39" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 39 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="38" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 38 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="37" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 37 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="36" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 36 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="35" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 35 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="34" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 34 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="33" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 33 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="32" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 32 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="31" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 31 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="30" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 30 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="29" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 29 </label>
                                                               </div>
                                                              </div>
                                                              
                                                             </div>
                                                             
                                                             
                                                             
                                                            </div>
                                                           </div>
                                                          </div>
                                                       </div>
                                                    </div>
                                                    
                                                    <div class="col-lg-2">
                                                        <div class="panel panel-default">
                                                          <div class="panel-heading bg-inverse text-center txt-white">
                                                             Bay : 4
                                                          </div>
                                                          <div class="panel-body">
                                                             <div style="padding: 15px 5px;" >
                                                                 <div class="form-group row" >                                                             
                                                             <div class="col-sm-12  " >
                                                             <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="43A" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 43A </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" >
                                                               <div class="col-sm-12 ">
                                                               <label for="43" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 43 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="44" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 44 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="45" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 45 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="46" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 46 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="47" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 47 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="48" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 48 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="49" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 49 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="50" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 50 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="51" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 51 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="52" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 52 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="53" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 53 </label>
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="54" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 54 </label>
                                                               </div>
                                                              </div>
                                                              
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="55" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 55 </label>
                                                             <c:forEach var="vehicle" items="${vehicles}">
                                                                <div class="modal fade" id="default-Modal${vehicle.trimedVehicle}" tabindex="-1" role="dialog">
                                                                    <div class="modal-dialog" role="document">
                                                                        <div class="modal-content">
                                                                            <div class="modal-header">
                                                                                <h4 class="modal-title">Vehicle Details</h4>
                                                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                            						<span aria-hidden="true">&times;</span>
                                                        						</button>
                                                                            </div>
                                                                            <div class="modal-body">
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <div class="row row-mar-botm">
                                                              
                                                               <label class="col-sm-4 col-form-label">Vehicle No </label>
                                                               <label class="col-sm-6 col-form-label"> : &nbsp;<b>${vehicle.vehicle_No}</b></label>
                                                               </div>
                                                               <div class="row row-mar-botm">
                                                               <label class="col-sm-4 col-form-label">IBL / Supplier</label>
                                                               <label class="col-sm-6 col-form-label"> : &nbsp;<b>${vehicle.supplier}</b></label>
                                                               
                                                               </div>
                                                              
                                                               <div class="row row-mar-botm">
                                                               
                                                               <label class="col-sm-4 col-form-label">Supplier Name </label>
                                                               <label class="col-sm-6 col-form-label"> : &nbsp;<b>${vehicle.supplier_Name}</b></label>
                                                               </div>
                                                               <div class="row row-mar-botm">
                                                               <label class="col-sm-4 col-form-label">Licence Validity</label>
                                                               <label class="col-sm-6 col-form-label"> : &nbsp;<b>${vehicle.license_Valid_Date}</b></label>
                                                               
                                                               </div>
                                                               <div class="row row-mar-botm">
                                                               
                                                               <label class="col-sm-4 col-form-label">Critical Parts </label>
                                                               <label class="col-sm-6 col-form-label"> : &nbsp;<b>Yes</b></label>
                                                               
                                                               </div>
                                                               </div>
                                                              </div>
                                                                            </div>
                                                                            <div class="modal-footer">
                                                                                <button type="button" class="btn btn-default waves-effect " data-dismiss="modal">Close</button>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                               </c:forEach>
                                            
                                                               </div>
                                                              </div>
                                                              <div class="form-group row" > 
                                                               <div class="col-sm-12 ">
                                                               <label for="56" class="label col-sm-12 text-center label-inverse-warning"><i class="fa fa-tasks"></i> 56 </label>
                                                               </div>
                                                              </div>
                                                              
                                                             </div> 
                                                             
                                                           </div>
                                                          </div>
                                                       </div>
                                                    </div>
                                                </div>
                                                
                                                    </div>
                                                    
                                                    
                                                    
                                                </div>
                                                
                                                <!--<div class="col-md-3 col-lg-3">
                                                         <div class="card-block">
                                                         <h4 style="margin-left:30px"> TRUCK</h4>
                                                        <ul class="feed-blog">
                                                        <div class="row" >&nbsp;</div>
                                                            <li class="">
                                                                <div class="feed-user-img">
                                                                    <img src="resources/assets/images/avatar-4.jpg" class="img-radius " alt="User-Profile-Image">
                                                                </div>
                                                               <p>5 Min ago<br>TN O9 2279 98589</p>
                                                            </li>
                                                            <li class="">
                                                                <div class="feed-user-img">
                                                                    <img src="resources/assets/images/avatar-4.jpg" class="img-radius " alt="User-Profile-Image">
                                                                </div>
                                                               <p>5 Min ago<br>TN O9 0079 98559</p>
                                                            </li>
                                                            <li class="">
                                                                <div class="feed-user-img">
                                                                    <img src="resources/assets/images/avatar-4.jpg" class="img-radius " alt="User-Profile-Image">
                                                                </div>
                                                               <p>11.00 AM<br>TN O9 7879 98559</p>
                                                            </li>
                                                            <li class="">
                                                                <div class="feed-user-img">
                                                                    <img src="resources/assets/images/avatar-4.jpg" class="img-radius " alt="User-Profile-Image">
                                                                </div>
                                                               <p>1.00 PM<br>TN O9 0079 98558</p>
                                                            </li>
                                                            <li class="">
                                                                <div class="feed-user-img">
                                                                    <img src="resources/assets/images/avatar-4.jpg" class="img-radius " alt="User-Profile-Image">
                                                                </div>
                                                               <p>05.25 PM<br>TN O9 0079 98560</p>
                                                            </li>
                                                            <li class="">
                                                                <div class="feed-user-img">
                                                                    <img src="resources/assets/images/avatar-4.jpg" class="img-radius " alt="User-Profile-Image">
                                                                </div>
                                                               <p>9.00 PM<br>TN O9 0079 98559</p>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    
                                                    </div>-->
                                                
                                                     
                                                    </div>
                                                       </div>
                                                       </form>
                                                   </div>
                                                   
                                                </div>
                                                
                                            </div>
                                        </div>
                                                            
                                        
                                    </div>
                                    <!-- Page-body end -->
                                </div>
                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
   
    <script  src="resources/bower_components/jquery/js/jquery.min.js"></script>
    <script  src="resources/bower_components/jquery-ui/js/jquery-ui.min.js"></script>
    <script  src="resources/bower_components/popper.js/js/popper.min.js"></script>
    <script  src="resources/bower_components/bootstrap/js/bootstrap.min.js"></script>
    <!-- jquery slimscroll js -->
    <script  src="resources/bower_components/jquery-slimscroll/js/jquery.slimscroll.js"></script>
    <!-- modernizr js -->
    <script  src="resources/bower_components/modernizr/js/modernizr.js"></script>
    <script  src="resources/bower_components/modernizr/js/css-scrollbars.js"></script>
  
    
     <!-- menu js -->
    <script src="resources/assets/js/pcoded.min.js"></script>
    <script src="resources/assets/js/navbar-image/vertical-layout.min.js "></script>
    
    <script src="resources/assets/js/jquery.mCustomScrollbar.concat.min.js"></script>
    
    <script>
	 $(document).ready(function() {
            $('[data-toggle="tooltip"]').tooltip();
        });

        $(document).ready(function() {
            $('[data-toggle="popover"]').popover({
                html: true,
                content: function() {
                    return $('#primary-popover-content').html();
                }
            });
        });
	</script>
    <script  src="resources/assets/js/script.js"></script>
</body>

</html>
