<!DOCTYPE html>
<%@ taglib prefix="form" uri="http://www.springframework.org/tags/form" %>
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
     <!-- Data Table Css -->
    <link rel="stylesheet" type="text/css" href="resources/bower_components/datatables.net-bs4/css/dataTables.bootstrap4.min.css">
    <link rel="stylesheet" type="text/css" href="resources/assets/pages/data-table/css/buttons.dataTables.min.css">
    <link rel="stylesheet" type="text/css" href="resources/bower_components/datatables.net-responsive-bs4/css/responsive.bootstrap4.min.css">
    <link rel="stylesheet" type="text/css" href="resources/assets/pages/data-table/extensions/responsive/css/responsive.dataTables.css">
    
    <!-- Date-time picker css -->
    <link rel="stylesheet" type="text/css" href="resources/assets/pages/advance-elements/css/bootstrap-datetimepicker.css">
    <!-- scrollbar.css -->
    <link rel="stylesheet" type="text/css" href="resources/assets/css/jquery.mCustomScrollbar.css">
    <!-- radial chart.css -->
    <link rel="stylesheet" href="resources/assets/pages/chart/radial/css/radial.css" type="text/css" media="all">
    <!-- Style.css -->
    <link rel="stylesheet" type="text/css" href="resources/assets/css/style.css">
    <script src="https://code.jquery.com/jquery-1.9.1.min.js"></script>
    <script type="text/javascript">
    	$(document).ready(function () {
    		$(".alert").alert('close');
    		$('#txtVehicleno').blur(function() {
    			var vehicleNo=$('#txtVehicleno').val();
    			if(vehicleNo == '')
    				return;
    			getVehicleAjax(vehicleNo);
    		});
    		
    		$('#txtDriverno').blur(function() {
    			var driverNo=$('#txtDriverno').val();
    			if(driverNo == '')
    				return;
    			getDriverAjax($('#txtDriverno').val());
    		});
    	});
		
		function getDriverAjax(driver_Pass) {
	        
			$.ajax({
				url : "/SpringMvcUser/getDriver",
				type : 'GET',
				dataType : 'text',
				data: { 
				    driver_Pass:driver_Pass
				},
				contentType : 'application/json',
				mimeType : 'application/json',
				success : function(data) {
					var obj = JSON.parse(data);
					$("label[for='name']").text(": "+obj.name);
					$("label[for='vehicle_No']").text(": "+obj.vehicle_No);
					$("label[for='supplier']").text(": "+obj.supplier);
					$("label[for='licence']").text(": "+obj.licence);
					$("label[for='criticalPart']").text(": "+obj.criticalPart);
					if(obj.name == "null"){
						
					}else{
						$('#subBtnDiv').css('display', 'block');
						$('#subBtnDiv').css('text-align', 'center');
					}
					
				},
				error : function(XMLHttpRequest, textStatus, errorThrown) {
	                alert("Invalid Pass: Contact Admin "+errorThrown);
	            }
			});
		}
		
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
					$('#FCDiv').removeClass("checkbox-success").addClass(obj.FC);
					$("label[for='FCDate']").text("FC: "+obj.FCDate);
					
					$('#RCDiv').removeClass("checkbox-success").addClass(obj.RC);
					$("label[for='RCDate']").text("RC: "+obj.RCDate);
					
					$('#insDiv').removeClass("checkbox-success").addClass(obj.ins);
					$("label[for='insDate']").text("Insurance: "+obj.insDate);
					
					$('#pollCertDiv').removeClass("checkbox-success").addClass(obj.pollCert);
					$("label[for='pollCertDate']").text("Pollution Certificate: "+obj.pollCertDate);
					$('#divCertificates').css('display', 'block');
					if(obj.vehicleslotted == "true"){
						$('#vehicleslottedDiv').removeClass("checkbox-success").addClass("checkbox-danger");
						$("label[for='vehicleslotted']").text("Already Allocated");
						obj.alert = "true";
					}else{
						$('#vehicleslottedDiv').removeClass("checkbox-danger").addClass("checkbox-success");
						$("label[for='vehicleslotted']").text("Ready to Allocate");
					}
					//alert(obj.vehicleslotted);
					
					if(obj.alert == "true"){
						$("#txtDriverno").attr("disabled", "disabled"); 
						$('#btnYard').attr("disabled", "disabled"); 
						$('#btnServiceEntry').attr("disabled", "disabled"); 
						$(".alert").alert();
					}else{
						$("#txtDriverno").removeAttr("disabled")
					}
				},
				error : function(XMLHttpRequest, textStatus, errorThrown) {
					$('#divCertificates').css('display', 'none');
	                alert("Invalid Vehicle"+errorThrown);
	            }
			});
		}
	</script>
</head>

<body>
    <!-- Pre-loader start -->
    <div class="alert alert-warning alert-dismissible fade close" role="alert">
	  <strong>Holy guacamole!</strong> You should check in on some of those fields below.
	  <button type="button" class="close" data-dismiss="alert" aria-label="Close">
	    <span aria-hidden="true">&times;</span>
	  </button>
	</div>
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
										
                                        <li class="active">
                                            <a href="javascript:void(0)">
                                                <span class="pcoded-micon"><i class="ti-layout"></i></span>
                                                <span class="pcoded-mtext">Yard In</span>
                                                <span class="pcoded-mcaret"></span>
                                            </a>
                                        </li>
                                        <li class=" ">
                                            <a href="slots.html">
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
                            <div class="main-body" >
                                <div class="page-wrapper"  style="padding:0px !important;">
                                    <!-- Page-body start -->
                                    <div class="page-body">
                                                                                
                                        <div class="card borderless-card">
                                            <div class="card-block default-breadcrumb">
                                                <div class="breadcrumb-header">
                                                    <h5><i class="fa fa-truck"></i> YARD IN</h5>
                                                </div>
                                                <div class="m-t-10">
                                                    <ul class="breadcrumb-title">
                                                        <li class="breadcrumb-item">
                                                            <a href="index.html">
                                                                <i class="icofont icofont-home"></i>
                                                            </a>
                                                        </li>
                                                       
                                                        <li class="breadcrumb-item"><a href="#!">Yard In</a>
                                                        </li>
                                                       </ul>
                                                </div>
                                            </div>
                                        </div>
                                        
                                        <div class="card">
                                            <div class="panel panel-default">
                                                <div class="panel-heading bg-default txt-white">
                                                   <i class="icofont icofont-plus-circle"></i>  YARD IN
                                                </div>
                                                <div class="panel-body">
                                                   <form id="second" action="#" method="POST" novalidate>
                                                    <div class="card-block">
                                                       <div class="form-group row">
                                                          <div class="col-lg-12" >
                                                            <div class="row row-mar-botm">
                                                                <div class="col-sm-3">&nbsp;</div>
                                                                <label class="col-sm-2 col-form-label">Scan Vehicle Pass</label>
                                                                <div class="col-sm-3">
                                                                    <div class="input-group">
                                                                        <input type="text" class="form-control" id="txtVehicleno" name="txtVehicleno" placeholder="Enter Vehicle Pass">
                                                                        <span class="input-group-addon" id="basic-addon5"><i class="fa fa-qrcode"></i></span>
                                                                    </div>
                                                                    
                                                                    <span class="messages popover-valid"></span>
                                                                </div>
                                                                
                                                                 <div class="col-sm-1">
                                                                 <a href="#" style="padding:5px 5px 5px 10px;" class="btn btn-danger"><b><i class="fa fa-copyright"></i></b></a>                                                                   
                                                                </div>
                                                                
                                                                <div class="col-sm-3">&nbsp;</div>
                                                                 
                                                             </div>
                                                              
                                                             
                                                             <div class="row row-mar-botm">
                                                                <div class="col-sm-5">&nbsp;</div>
                                                                <label class="col-sm-2 col-form-label text-center"><b>Valid Date</b><hr></label>
                                                                <div class="col-sm-5">&nbsp;</div>                                                                 
                                                             </div>
                                                             
                                                             <div class="row row-mar-botm">
                                                               
                                                                <div id="divCertificates" class="col-sm-12 text-center" style="display: none;">
                                                                   <div id="FCDiv" class="checkbox-color checkbox-success">
                                                                    <input id="FC" type="checkbox" disabled checked="">
                                                                    <label for="FCDate">
                                                                    </label>
                                                                    </div>
                                                                    
                                                                    <div id="RCDiv" class="checkbox-color checkbox-success">
                                                                    <input id="RC" type="checkbox" disabled checked="">
                                                                    <label for="RCDate">
                                                                    </label>
                                                                    </div>
                                                                    
                                                                    <div id="insDiv" class="checkbox-color checkbox-success">
                                                                    <input id="ins" type="checkbox" disabled checked="">
                                                                    <label for="insDate">
                                                                    </label>
                                                                    </div>
                                                                    
                                                                     <div id="pollCertDiv" class="checkbox-color checkbox-success">
                                                                    <input id="pollCert" type="checkbox" disabled checked="">
                                                                    <label for="pollCertDate">
                                                                    </label>
                                                                    </div>
                                                                    
                                                                     <div id="vehicleslottedDiv" class="checkbox-color checkbox-success">
                                                                    <input id="vehicleslotted" type="checkbox" disabled checked="">
                                                                    <label for="vehicleslotted">
                                                                    </label>
                                                                    </div>
                                                                </div>

                                                              </div>
                                                             <hr>
                                                              <div class="row row-mar-botm">
                                                                <div class="col-sm-3">&nbsp;</div>
                                                                <label class="col-sm-2 col-form-label">Scan Driver Pass</label>
                                                                <div class="col-sm-3">
                                                                    <div class="input-group">
                                                                        <input type="text" class="form-control" id="txtDriverno" disabled name="txtDriverno" placeholder="Scan Driver Pass">
                                                                        <span class="input-group-addon" id="basic-addon5"><i class="fa fa-qrcode"></i></span>
                                                                    </div>
                                                                    
                                                                    <span class="messages popover-valid"></span>
                                                                </div>
                                                                <div class="col-sm-3">&nbsp;</div>
                                                               </div>
                                                               <div class="row row-mar-botm">
                                                               <label class="col-sm-2 col-form-label">&nbsp;</label>
                                                               <label class="col-sm-2 col-form-label">Vehicle No </label>
                                                               <label class="col-sm-2 col-form-label" for="vehicle_No"></label>
                                                               <label class="col-sm-2 col-form-label">IBL / Supplier</label>
                                                               <label class="col-sm-2 col-form-label" for="supplier"></label>
                                                               <label class="col-sm-2 col-form-label">&nbsp;</label>
                                                               </div>
                                                               <div class="row row-mar-botm">
                                                               <label class="col-sm-2 col-form-label">&nbsp;</label>
                                                               <label class="col-sm-2 col-form-label">Driver Name </label>
                                                               <label class="col-sm-2 col-form-label" for="name"></label>
                                                               <label class="col-sm-2 col-form-label">Licence Validity</label>
                                                               <label class="col-sm-2 col-form-label" for="licence"></label>
                                                               <label class="col-sm-2 col-form-label">&nbsp;</label>
                                                               </div>
                                                               <div class="row row-mar-botm">
                                                               <label class="col-sm-2 col-form-label">&nbsp;</label>
                                                               <label class="col-sm-2 col-form-label">Critical Parts </label>
                                                               <label class="col-sm-2 col-form-label" for="criticalPart"></label>
                                                               
                                                               <label class="col-sm-2 col-form-label">&nbsp;</label>
                                                               </div>
                                                               
                                                             </div>
                                                            </div>
                                                          </div>
                                                   <div class="panel-footer">
                                                            <div class="row">
                                                                <!-- <label class="col-sm-2"></label> -->
                                                                <div id="subBtnDiv" class="col-sm-12" style="text-align: center; display:none">
                                                                 <button type="submit" id="btnYard" onclick='this.form.action="slots";' class="btn btn-default btn-primary">Yard Entry</button>&nbsp;
                                                                 <button type="submit" id="btnServiceEntry" onclick='this.form.action="updateServiceEntry";' class="btn btn-default btn-primary">Service Road Entry</button>&nbsp; 
                                                                </div>
                                                                <div class="col-sm-12" style="text-align: center;">
                                                                </br>
                                                                <button type="button" id="btnClear" onclick='location.reload(true);' class="btn btn-default btn-primary">Clear</button>&nbsp;
                                                                </div>
                                                            </div>
                                                   </div>
                                                   </form>
                                                </div>
                                                
                                            </div>
                                        </div>
                                                            
                                      
                                    <div class="card">
                                            <div class="panel panel-default">
                                                <div class="panel-heading bg-default txt-white">
                                                   <i class="icofont icofont-list"></i> VEHICLE LIST 
                                                </div>
                                                <div class="panel-body">
                                                   <div class="card">
                                                     <div class="card-block">
                                                <div class="">
                                                    <div class="dt-responsive table-responsive ">
                                                        <table id="new-cons" class="table  table-striped table-bordered nowrap">
                                                            <thead>
                                                                <tr>
                                                                    <th>Entry No</th>
                                                                    <th>Vehicle No</th>
                                                                    <th>Supplier/TVS</th>
                                                                    <th>Reporting Time</th>                            
                                                                    <th >In Time</th>
                                                                     <th style="text-align:center">Halt Time in yard <br> (HH:mm)</th>
                                                                    <th style="text-align:center">Current <br> Palce</th>
                                                                    <th style="text-align:center">Parking <br> Allotment</th>
                                                                    <th  data-hide="all">Phone No</th>
                                                                    <!--<th  class="no-sort" >Options</th>-->
                                                                    
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                            ${yards}
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                                   </div>
                                        <!-- Config. table end -->

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
    </div>
    <!-- Required Jquery -->
    
    <script  src="resources/bower_components/jquery/js/jquery.min.js"></script>
    <script  src="resources/bower_components/jquery-ui/js/jquery-ui.min.js"></script>
    <script  src="resources/bower_components/popper.js/js/popper.min.js"></script>
    <script  src="resources/bower_components/bootstrap/js/bootstrap.min.js"></script>
    <!-- jquery slimscroll js -->
    <script  src="resources/bower_components/jquery-slimscroll/js/jquery.slimscroll.js"></script>
    <!-- modernizr js -->
    <script  src="resources/bower_components/modernizr/js/modernizr.js"></script>
    <script  src="resources/bower_components/modernizr/js/css-scrollbars.js"></script>
   
    <!-- Bootstrap date-time-picker js -->
    <script  src="resources/assets/pages/advance-elements/moment-with-locales.min.js"></script>
    <script  src="resources/bower_components/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <script  src="resources/assets/pages/advance-elements/bootstrap-datetimepicker.min.js"></script>
    <!-- data-table js -->
    <script src="resources/bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script src="resources/bower_components/datatables.net-buttons/js/dataTables.buttons.min.js"></script>
    <script src="resources/assets/pages/data-table/js/jszip.min.js"></script>
    <script src="resources/assets/pages/data-table/js/pdfmake.min.js"></script>
    <script src="resources/assets/pages/data-table/js/vfs_fonts.js"></script>
    <script src="resources/assets/pages/data-table/extensions/responsive/js/dataTables.responsive.min.js"></script>
    <script src="resources/bower_components/datatables.net-buttons/js/buttons.print.min.js"></script>
    <script src="resources/bower_components/datatables.net-buttons/js/buttons.html5.min.js"></script>
    <script src="resources/bower_components/datatables.net-bs4/js/dataTables.bootstrap4.min.js"></script>
    <script src="resources/bower_components/datatables.net-responsive/js/dataTables.responsive.min.js"></script>
    <script src="resources/bower_components/datatables.net-responsive-bs4/js/responsive.bootstrap4.min.js"></script>
    <!-- Custom js -->
    
    <script src="resources/assets/pages/data-table/extensions/responsive/js/responsive-custom.js"></script>
     <!-- menu js -->
    <script src="resources/assets/js/pcoded.min.js"></script>
    <script src="resources/assets/js/navbar-image/vertical-layout.min.js "></script>
    
    <script  src="resources/assets/js/script.js"></script>
</body>

</html>
